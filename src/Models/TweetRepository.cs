using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using TweetAnalytics.Extensions;

namespace TweetAnalytics.Models
{
    public class TweetRepository : ITweetRepository {
        private List<Tweet> _tweets = new List<Tweet>();
        private Dictionary<string, int> _domainUsage = new Dictionary<string, int>();
        private Dictionary<string, int> _hashtagUsage = new Dictionary<string, int>();

        public void AddDomainUsage(Dictionary<string, int> domains) {
            _domainUsage = _domainUsage.AddUsage(domains);
        }

        public void AddHashtagUsage(Dictionary<string, int> hashtags)
        {
            _hashtagUsage = _hashtagUsage.AddUsage(hashtags);
        }

        public void AddTweet(Tweet tweet)
        {
            _tweets.Add(tweet);
        }

        public Dictionary<string, int> GetDomainUsage()
        {
            return _domainUsage.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        public Dictionary<string, int> GetHashtagUsage()
        {
            return _hashtagUsage.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        public List<Tweet> GetTweets()
        {
            return _tweets;
        }
    }
}