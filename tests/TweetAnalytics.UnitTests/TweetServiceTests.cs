using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TweetAnalytics.Models;
using TweetAnalytics.Services;
using Xunit;

namespace TweetAnalytics.UnitTests
{
    public class TweetServiceTests
    {
        #region GetUrls 
        [Fact()]
        public void GetUrlsFromTweet_WhenTweetHasNoUrls_ReturnsEmptyList()
        {
            // arrange
            var tweetService = new TweetService(new Mock<ITweetRepository>().Object);
            var tweet = new Tweet {
                Entities = new Entities {
                    Urls = new List<TwitterUrl> {
                    }
                }
            };

            // act
            var sut = tweetService.GetUrlsFromTweet(tweet);

            // assert
            Assert.True(sut.Count == 0);
        }
        [Fact]
        public void GetUrls_WhenTweetHasMultipleValidUrls_ReturnsListWithFullUrls() {
            // arrange
            var tweetService = new TweetService(new Mock<ITweetRepository>().Object);
            var ddgUrl = new Uri ("https://duckduckgo.com/?q=asdf");
            var googleUrl = new Uri ("https://google.com/search?q=hello");
            var bingUrl = new Uri ("http://bing.com/helloworld"); 
            var tweet = new Tweet {
                Entities = new Entities {
                    Urls = new List<TwitterUrl> {
                        new TwitterUrl {
                            ExpandedUrl = ddgUrl
                        },
                        new TwitterUrl {
                            ExpandedUrl = googleUrl
                        },
                        new TwitterUrl {
                            ExpandedUrl = bingUrl
                        },
                    }
                }
            };

            // act
            var sut = tweetService.GetUrlsFromTweet(tweet);

            // assert
            Assert.True(sut.ContainsKey("duckduckgo.com"));
            Assert.True(sut["duckduckgo.com"] == 1);
            Assert.True(sut.ContainsKey("google.com"));
            Assert.True(sut["google.com"] == 1);
            Assert.True(sut.ContainsKey("bing.com"));
            Assert.True(sut["bing.com"] == 1);
        }

        [Fact()]
        public void GetUrls_WhenTweetHasOneValidUrl_ReturnsListWithFullUrl() {
            // arrange
            var tweetService = new TweetService(new Mock<ITweetRepository>().Object);
            var ddgUrl = new Uri ("https://duckduckgo.com/?q=asdf");
            var tweet = new Tweet {
                Entities = new Entities {
                    Urls = new List<TwitterUrl> {
                        new TwitterUrl {
                            ExpandedUrl = ddgUrl
                        },
                    }
                }
            };
            var domains = new List<string> { "duckduckgo.com" };

            // act
            var sut = tweetService.GetUrlsFromTweet(tweet, domains);

            // assert
            Assert.True(sut.ContainsKey("duckduckgo.com"));
            Assert.True(sut["duckduckgo.com"] == 1);
        }
        #endregion
        #region GetPhotoUrls
        [Fact]
        public void GetUrlsFromTweet_WhenTweetHasWhitelistButNoPhotoUrls_ReturnsEmptyList()
        {
            // arrange
            var tweetService = new TweetService(new Mock<ITweetRepository>().Object);
            var tweet = new Tweet {
                Entities = new Entities {
                    Urls = new List<TwitterUrl> {
                        new TwitterUrl {
                            ExpandedUrl = new Uri ("https://duckduckgo.com/?q=asdf")
                        }
                    }
                }
            };
            var domains = new List<string> { "pbs.twimg.com" };

            // act
            var sut = tweetService.GetUrlsFromTweet(tweet, domains);

            // assert
            Assert.True(sut.Count == 0);
        }
        [Fact()]
        public void GetPhotoUrls_WhenTweetHasImageUrl_ReturnsListWithImageUrl()
        {
            // arrange
            var tweetService = new TweetService(new Mock<ITweetRepository>().Object);
            var tweet = new Tweet {
                Entities = new Entities {
                    Urls = new List<TwitterUrl> {
                        new TwitterUrl {
                            ExpandedUrl = new Uri ("https://pbs.twimg.com/?q=asdf")
                        }
                    }
                }
            };
            var domains = new List<string> { "pbs.twimg.com" };

            // act

            var sut = tweetService.GetUrlsFromTweet(tweet, domains);

            // assert
            Assert.True(sut.Count > 0);
            Assert.True(sut.ContainsKey("pbs.twimg.com"));
            Assert.True(sut["pbs.twimg.com"] == 1);
        }
        #endregion
    }
}
