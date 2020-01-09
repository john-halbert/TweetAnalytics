using System.Collections.Generic;
using System.Linq;
using System.Text;
using TweetAnalytics.Extensions;

namespace TweetAnalytics.Models {
    public class EmojiRepository : IEmojiRepository {
        private Dictionary<Rune, int> _emojiUsage = new Dictionary<Rune, int> ();

        public void AddEmojiUsage (Dictionary<Rune, int> emojis) {
            _emojiUsage = _emojiUsage.AddUsage(emojis);
        }

        public Dictionary<Rune, int> GetEmojiUsage () {
            return _emojiUsage.OrderByDescending (x => x.Value).ToDictionary (x => x.Key, x => x.Value);
        }
    }
}