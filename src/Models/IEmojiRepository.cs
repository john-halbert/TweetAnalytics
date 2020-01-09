using System.Collections.Generic;
using System.Text;

namespace TweetAnalytics.Models
{
    public interface IEmojiRepository
    {

        /// <summary>
        /// Adds an emoji to the underlying data store
        /// </summary>
        /// <param name="emojis">A dictionary of emojis and frequency used inside a single tweet</param>
        void AddEmojiUsage(Dictionary<Rune, int> emojis);
        /// <summary>
        /// Get all emojis from underlying datastore
        /// </summary>
        /// <returns>An dictionary ordered by frequency of usage</returns>
        Dictionary<Rune, int> GetEmojiUsage();
    }
}