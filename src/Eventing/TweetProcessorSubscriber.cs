using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TweetAnalytics.Models;
using TweetAnalytics.Services;

namespace TweetAnalytics
{
    public class TweetProcessorSubscriber
    {
        private readonly ITweetRepository _tweetRepository;
        private readonly IEmojiRepository _emojiRepository;
        private readonly IEmojiService _emojiService;
        private readonly ITweetService _tweetService;
        private readonly IConfiguration _configuration;
        private readonly List<string> _photoDomains;

        public TweetProcessorSubscriber (
            ITweetRepository tweetRepository,
            IEmojiRepository emojiRepository,
            IEmojiService emojiService,
            ITweetService tweetService,
            IConfiguration configuration
        ) {
            _tweetRepository = tweetRepository;
            _emojiRepository = emojiRepository;
            _emojiService = emojiService;
            _tweetService = tweetService;
            _configuration = configuration;
            _photoDomains = _configuration.GetSection("photoDomains").GetChildren().Select(x => x.Value).ToList();
        }
        
        public void HandleTweetReceivedEvent(object sender, TweetEventArgs eventArgs) {
            try {
                if (!Regex.IsMatch(eventArgs.NonTypedTweet, "({\\\"delete)")
                    && !Regex.IsMatch(eventArgs.NonTypedTweet, "({\\\"status_withheld)")
                ) {
                    var tweet = JsonConvert.DeserializeObject<Tweet>(eventArgs.NonTypedTweet);
                    var rankedEmojis = _emojiService.GetEmojisFromTweet(tweet);
                    var rankedUrls = _tweetService.GetUrlsFromTweet(tweet);
                    var rankedHashtags = _tweetService.GetHashtagsFromTweet(tweet);

                    tweet.ContainsUrls = rankedUrls.Count > 0;
                    tweet.ContainsEmoji = rankedEmojis.Count > 0;
                    tweet.ContainsPhotoUrls = _tweetService.GetUrlsFromTweet(tweet, _photoDomains).Count > 0; 

                    _emojiRepository.AddEmojiUsage(rankedEmojis);
                    _tweetRepository.AddDomainUsage(rankedUrls);
                    _tweetRepository.AddHashtagUsage(rankedHashtags);
                    _tweetRepository.AddTweet(tweet);
                    
                }
            } catch( JsonException e){
                using StreamWriter sw = new StreamWriter(@"unprocessable-tweets.json", true);
                var json = JsonConvert.SerializeObject(eventArgs.NonTypedTweet);
                sw.WriteLine(e);
                sw.WriteLine(eventArgs.NonTypedTweet);
            }
        }
    }
}