using System.Collections.Generic;
using System.Linq;
using System.Text;
using TweetAnalytics.Models;
using TweetAnalytics.Services;

namespace TweetAnalytics
{
    public class EmojiService : IEmojiService {
        private readonly IEmojiRepository _emojiRepository;
        public EmojiService (IEmojiRepository emojiRepository) {
            _emojiRepository = emojiRepository;
        }

        public Dictionary<Rune, int> GetEmojisFromTweet (Tweet tweet) {
            var foundEmojis = new Dictionary<Rune, int>();
            foreach (var rune in tweet.Text.EnumerateRunes()) {
                if (rune.IsAscii || !Rune.IsSymbol(rune)) continue;
                if (foundEmojis.ContainsKey(rune)) {
                    foundEmojis[rune] += 1;
                } else {
                    foundEmojis.Add(rune, 1);
                }
            }
            return foundEmojis;
        }

        public List<string> GetTopEmojis(int count = 10) {
            return _emojiRepository.GetEmojiUsage().Take(count).Select(x => x.Key.ToString()).ToList();
        }
    }
}