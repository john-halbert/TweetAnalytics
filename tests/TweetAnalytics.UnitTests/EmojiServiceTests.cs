using System.Text;
using Moq;
using TweetAnalytics.Models;
using Xunit;

namespace TweetAnalytics.UnitTests
{
    public class EmojiServiceTests {
        [Fact]
        public void GetTopEmojis_WhenTweetHasEmojis_ReturnsRankedEmojiOccurrenceDictionary () {
            //arrange
            var emojiRepository = Mock.Of<IEmojiRepository> ();
            var emojiService = new EmojiService (emojiRepository);
            var tweet = new Tweet {
                Text = "this is a tweet 💯😍"
            };

            //act
            var sut = emojiService.GetEmojisFromTweet (tweet);

            //assert
            Rune.DecodeFromUtf16 ("💯", out Rune result, out int charsConsumed);
            Assert.True (sut.ContainsKey (result));
            Assert.True (sut[result] == 1);

            Rune.DecodeFromUtf16 ("😍", out Rune result2, out int charsConsumed2);
            Assert.True (sut.ContainsKey (result2));
            Assert.True (sut[result2] == 1);
        }

        [Fact]
        public void GetTopEmojis_WhenTweetHasNoEmojis_ReturnsEmptyList () {
            //arrange
            var emojiRepository = Mock.Of<IEmojiRepository> ();
            var emojiService = new EmojiService (emojiRepository);
            var tweet = new Tweet {
                Text = "this is a tweet"
            };

            //act
            var sut = emojiService.GetEmojisFromTweet (tweet);

            //assert
            Assert.True (sut.Count == 0);
        }
    }
}