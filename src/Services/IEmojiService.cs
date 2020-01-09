using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using TweetAnalytics.Models;

namespace TweetAnalytics.Services {
    public interface IEmojiService
    {
        /// <summary>
        /// Extracts emojis from a tweet's text field
        /// </summary>
        /// <param name="tweet">Tweet to be parsed</param>
        /// <returns>A list containing each emoji in a tweet with the amount of times it appears</returns>
        Dictionary<Rune, int> GetEmojisFromTweet(Tweet tweet);

        /// <summary>
        /// Retrieves a list of top emojis
        /// </summary>
        /// <param name="count">Number of entries in the list</param>
        /// <returns>List of top emojis</returns>
        List<string> GetTopEmojis (int count);
    }
}