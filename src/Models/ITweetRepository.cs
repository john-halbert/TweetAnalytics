using System;
using System.Collections.Generic;
using System.Linq;

namespace TweetAnalytics.Models {
    public interface ITweetRepository {

        /// <summary>
        /// Gets all tweets from underlying datastore
        /// </summary>
        /// <returns>A list of tweets</returns>
        List<Tweet> GetTweets ();

        /// <summary>
        /// Add a tweet to the underlying data store
        /// </summary>
        /// <param name="tweet">The <see cref="Tweet" /> to use</param>
        void AddTweet (Tweet tweet);

        /// <summary>
        /// Gets all domains from underlying datastore, sorted by frequency used
        /// </summary>
        /// <returns>An dictionary ordered by frequency of usage</returns>
        Dictionary<string, int> GetDomainUsage ();

        /// <summary>
        /// Adds a Url to the underlying data store
        /// </summary>
        /// <param name="domains">A dictionary of URLs and frequency used inside a single tweet</param>
        void AddDomainUsage (Dictionary<string, int> domains);

        /// <summary>
        /// Gets all hashtags from underlying datastore, sorted by frequency used
        /// </summary>
        /// <returns>An dictionary ordered by frequency of usage</returns>
        Dictionary<string, int> GetHashtagUsage ();

        /// <summary>
        /// Add a hashtag to the underlying data store
        /// </summary>
        /// <param name="hashtags">A dictionary of hashtags and frequency used inside a single tweet</param>
        void AddHashtagUsage (Dictionary<string, int> hashtag);
    }
}