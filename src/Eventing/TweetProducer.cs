using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace TweetAnalytics
{
    public class TweetProducer : BackgroundService {
        public event EventHandler<TweetEventArgs> TweetReceivedEvent;
        private string baseUrl = "https://stream.twitter.com/1.1";
        private readonly string _consumerKey;
        private readonly string _consumerKeySecret;
        private readonly string _accessToken;
        private readonly string _accessTokenSecret;
        private readonly HMACSHA1 hmac;
        private readonly HttpClient _client;
        private DateTime epochUtc = new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public TweetProducer (
            string consumerKey,
            string consumerKeySecret,
            string accessToken,
            string accessTokenSecret
        ) {
            _consumerKey = consumerKey;
            _consumerKeySecret = consumerKeySecret;
            _accessToken = accessToken;
            _accessTokenSecret = accessTokenSecret;

            hmac = new HMACSHA1 (new ASCIIEncoding ().GetBytes (string.Concat (consumerKeySecret, "&", accessTokenSecret)));
            _client = new HttpClient ();
        }
        public async Task GetTweetStreamAsync (CancellationToken cancellationToken) {
            var url = $"{baseUrl}/statuses/sample.json";
            var randomNumber = RandomNumberGenerator.GetInt32 (0, Int32.MaxValue);

            // Most authentication taken from https://blog.dantup.com/2016/07/simplest-csharp-code-to-post-a-tweet-using-oauth/

            var timestamp = (int) ((DateTime.UtcNow - epochUtc).TotalSeconds);
            var data = new Dictionary<string, string> { { "oauth_consumer_key", _consumerKey },
                    { "oauth_signature_method", "HMAC-SHA1" },
                    { "oauth_timestamp", timestamp.ToString () },
                    { "oauth_nonce", randomNumber.ToString () },
                    { "oauth_token", _accessToken },
                    { "oauth_version", "1.0" }
                };
            data.Add ("oauth_signature", GenerateSignature (url, data));
            var result = await SendAsync (url, GenerateOAuthHeader (data), cancellationToken);

            var bufferSize = 10240; // weird things happen if this is something lower, like 8192
            byte[] buffer = new byte[bufferSize];
            int read;
            string entireMessage = null;
            while ((read = await result.ReadAsync (buffer, 0, bufferSize)) > 0) {
                var message = Encoding.UTF8.GetString (buffer);
                message = message.Substring (0, read);

                if (message.EndsWith ("\r\n") || message.EndsWith ("\r")) {
                    entireMessage += message;
                } else {
                    // this was a fragment
                    entireMessage += message;
                    continue;
                }

                try {
                    OnTweetReceived (new TweetEventArgs (entireMessage ?? message));
                } catch (Exception e) {
                    using StreamWriter sw = new StreamWriter (@"errors.txt", true);
                    await sw.WriteLineAsync (e.ToString ());
                    await sw.WriteLineAsync (entireMessage);
                } finally {
                    entireMessage = null;
                }
            }
        }

        public async Task<Stream> SendAsync (string url, string oauthHeader, CancellationToken cancellationToken) {
            _client.DefaultRequestHeaders.Add ("Authorization", oauthHeader);
            if (cancellationToken.IsCancellationRequested) return null;
            var req = new HttpRequestMessage (HttpMethod.Get, url);
            req.Headers.Authorization = AuthenticationHeaderValue.Parse (oauthHeader);
            var res = await _client.SendAsync (req, HttpCompletionOption.ResponseHeadersRead);
            return await res.Content.ReadAsStreamAsync ();
        }

        /// <summary>
        /// Generate an OAuth signature from OAuth header values.
        /// </summary>
        string GenerateSignature (string url, Dictionary<string, string> data) {
            var sigString = string.Join (
                "&",
                data
                .Union (data)
                .Select (kvp => string.Format ("{0}={1}", Uri.EscapeDataString (kvp.Key), Uri.EscapeDataString (kvp.Value)))
                .OrderBy (s => s)
            );

            var fullSigData = string.Format (
                "{0}&{1}&{2}",
                "GET",
                Uri.EscapeDataString (url),
                Uri.EscapeDataString (sigString.ToString ())
            );

            return Convert.ToBase64String (hmac.ComputeHash (new ASCIIEncoding ().GetBytes (fullSigData.ToString ())));
        }

        /// <summary>
        /// Generate the raw OAuth HTML header from the values (including signature).
        /// </summary>
        string GenerateOAuthHeader (Dictionary<string, string> data) {
            return "OAuth " + string.Join (
                ", ",
                data
                .Where (kvp => kvp.Key.StartsWith ("oauth_"))
                .Select (kvp => string.Format ("{0}=\"{1}\"", Uri.EscapeDataString (kvp.Key), Uri.EscapeDataString (kvp.Value)))
                .OrderBy (s => s)
            );
        }
        public virtual void OnTweetReceived (TweetEventArgs eventArgs) {
            TweetReceivedEvent?.Invoke (this, eventArgs);
        }

        protected override async Task ExecuteAsync (CancellationToken stoppingToken) {
            await GetTweetStreamAsync (stoppingToken);
        }
    }
}