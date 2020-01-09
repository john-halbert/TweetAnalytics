using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TweetAnalytics.Models;

namespace TweetAnalytics
{
    public class TweetLoggerSubscriber {
        private string Id;
        public List<Tweet> Tweets = new List<Tweet>();
        public TweetLoggerSubscriber(string id, TweetProducer pub)
        {
            Id = id;
            pub.TweetReceivedEvent += HandleTweetReceivedEvent;
        }
        
        void HandleTweetReceivedEvent(object sender, TweetEventArgs eventArgs) {
            using StreamWriter sw = new StreamWriter(@"tweets.json", true);
            var json = JsonConvert.SerializeObject(eventArgs.NonTypedTweet);
            sw.Write(json + ",");
        }
    }
}