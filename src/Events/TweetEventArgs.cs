using System;
using TweetAnalytics.Models;

namespace TweetAnalytics
{
    public class TweetEventArgs :  EventArgs {
        public string NonTypedTweet;

        public TweetEventArgs(string tweetJson)
        {
            NonTypedTweet = tweetJson;
        }
    }
}