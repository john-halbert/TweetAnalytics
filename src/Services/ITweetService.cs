using System;
using System.Collections.Generic;
using TweetAnalytics.Models;

namespace TweetAnalytics.Services {
    public interface ITweetService {
        /// <summary>
        /// Extracts hashtags from a tweet's text field
        /// </summary>
        /// <param name="tweet">Tweet to be parsed</param>
        /// <returns>A list containing each hashtag in a tweet with the amount of times it appears</returns>
        Dictionary<string, int> GetHashtagsFromTweet (Tweet tweet);
        /// <summary>
        /// Extracts <see cref="Uri" />s from a tweet's text field
        /// </summary>
        /// <param name="tweet">Tweet to be parsed</param>
        /// <param name="additionalDomains">Whitelist certain domains only</param>
        /// <returns>A list containing each link in a tweet with the amount of times it appears</returns>
        Dictionary<string, int> GetUrlsFromTweet (Tweet tweet, List<string> additionalDomains = null);
        /// <summary>
        /// Get a count of all tweets currently in the repository
        /// </summary>
        /// <returns>A count of all tweets</returns>
        int GetTweetCount ();
        /// <summary>
        /// Retrieves a list of top domains
        /// </summary>
        /// <param name="count">Number of entries in the list</param>
        /// <returns>List of top domains</returns>
        List<string> GetTopDomains (int count);
        /// <summary>
        /// Retrieves a list of top hashtags
        /// </summary>
        /// <param name="count">Number of entries in the list</param>
        /// <returns>List of top hashtags</returns>
        List<string> GetTopHashtags (int count);
        /// <summary>
        /// Retrieves a percentage tweets containing hashtags
        /// </summary>
        /// <returns>Percentage of tweets containing hashtags</returns>
        double GetPercentageContainingHashtags ();
        /// <summary>
        /// Retrieves a percentage tweets containing urls
        /// </summary>
        /// <returns>Percentage of tweets containing urls</returns>
        double GetPercentageContainingUrls (bool photoUrls);
        /// <summary>
        /// Retieves percentage of tweets containing emojis
        /// </summary>
        /// <returns>Percentage of tweets containing emojis</returns>
        double GetPercentageContainingEmojis ();
    }
}