using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TweetAnalytics.Models;

namespace TweetAnalytics.Services
{
    public class TweetService : ITweetService {
        private readonly ITweetRepository _tweetRepository;

        public TweetService (
            ITweetRepository tweetRepository
        ) {
            _tweetRepository = tweetRepository;
        }

        public Dictionary<string, int> GetHashtagsFromTweet (Tweet tweet) {
            var collection = new Dictionary<string, int> ();
            if (!string.IsNullOrEmpty (tweet.Text)) {
                var matches = Regex.Matches (tweet.Text, "\\#[a-zA-Z0-9]+").ToList ();

                foreach (var item in matches) {
                    if (collection.ContainsKey (item.Value)) {
                        collection[item.Value] += 1;
                    } else {
                        collection.Add (item.Value, 1);
                    }
                }
            }
            return collection;
        }

        public int GetTweetCount () {
            return _tweetRepository.GetTweets ().Count;
        }

        public Dictionary<string, int> GetUrlsFromTweet (Tweet tweet, List<string> additionalDomains = null) {
            var rankedUrls = new Dictionary<string, int> ();
            var urls = tweet.Entities.Urls;

            var extendedEntities = tweet.ExtendedEntities?.Media;
            if (extendedEntities != null && additionalDomains != null) {
                rankedUrls = CheckExtendedEntities(tweet, ref rankedUrls, additionalDomains);
            }

            if (urls.Count > 0) {
                foreach (var url in urls) {
                    if (rankedUrls.ContainsKey (url.ExpandedUrl.Host)) {
                        rankedUrls[url.ExpandedUrl.Host]++;
                    } else {
                        rankedUrls.Add (url.ExpandedUrl.Host, 1);
                    }
                }
            }
            return rankedUrls.OrderBy (x => x.Value).ToDictionary (x => x.Key, x => x.Value);
        }

        /// <summary>
        /// Search extended_entities provided by Twitter
        /// </summary>
        /// <param name="tweet">A <see cref="Tweet" /></param>
        /// <param name="rankedUrls">A reference to a list of media urls</param>
        /// <param name="additionalDomains">A whitelist of acceptable domains for media urls</param>
        /// <returns></returns>
        private Dictionary<string, int> CheckExtendedEntities(Tweet tweet, ref Dictionary<string, int> rankedUrls, List<string> additionalDomains = null) {
            foreach (var entity in tweet.ExtendedEntities.Media) {
                if (additionalDomains != null && !additionalDomains.Contains (entity.MediaUrl.Host)) {
                    // this was an image provider exclusive search
                    break;
                }
                if (rankedUrls.ContainsKey (entity.MediaUrl.Host)) {
                    rankedUrls[entity.MediaUrl.Host]++;
                } else {
                    rankedUrls.Add (entity.MediaUrl.Host, 1);
                }
            }
            return rankedUrls;
        }

        public List<string> GetTopDomains (int count = 10) {
            return _tweetRepository.GetDomainUsage ().Distinct ().Take (count).Select (x => x.Key).ToList ();
        }

        public List<string> GetTopHashtags (int count = 10) {
            return _tweetRepository.GetHashtagUsage ().Distinct ().Take (count).Select (x => x.Key).ToList ();
        }

        public double GetPercentageContainingHashtags () {
            return GetPercentage (_tweetRepository.GetTweets ().Count, _tweetRepository.GetTweets ().Where (x => x.ContainsEmoji).Count ());
        }

        public double GetPercentageContainingUrls (bool photoUrls) {
            if (photoUrls) {
                return GetPercentage (_tweetRepository.GetTweets ().Count, _tweetRepository.GetTweets ().Where (x => x.ContainsPhotoUrls).Count ());
            }
            return GetPercentage (_tweetRepository.GetTweets ().Count, _tweetRepository.GetTweets ().Where (x => x.ContainsUrls).Count ());
        }

        public double GetPercentageContainingEmojis () {
            return GetPercentage (_tweetRepository.GetTweets ().Count, _tweetRepository.GetTweets ().Where (x => x.ContainsEmoji).Count ());
        }

        private double GetPercentage (int total, int partial) {
            double result = Math.Round (((double)partial / (double)total) * 100, 3);
            return Double.IsInfinity(result) ? 0 : result;
        }
    }
}