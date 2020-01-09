using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TweetAnalytics.Services;

namespace TweetAnalytics.Controllers
{
    [ApiController]
    [Route("tweet-analytics")]
    public class TweetAnalyticsController : ControllerBase
    {
        private readonly ITweetService _tweetService;
        private readonly IEmojiService _emojiService;

        public TweetAnalyticsController(
            ITweetService tweetService,
            IEmojiService emojiService
        ) { 
            _tweetService = tweetService;
            _emojiService = emojiService;
        }

        [HttpGet("stats.json")]
        public IActionResult Get()
        {
            var totalTweets = _tweetService.GetTweetCount();
            var startTime = Process.GetCurrentProcess().StartTime;
            var topEmojis = _emojiService.GetTopEmojis(10);
            var topHashtags = _tweetService.GetTopHashtags(10);
            var topDomains = _tweetService.GetTopDomains(10);
            var nowTime = DateTime.Now;
            var elapsedTime = nowTime - startTime;
            Dictionary<string, int> averageTweets = new Dictionary<string, int>
            {
                {"second", totalTweets / (int)elapsedTime.TotalSeconds },
                {"minute", totalTweets / (int)elapsedTime.TotalSeconds *  60 },
                {"hour", totalTweets / (int)elapsedTime.TotalSeconds * 3600 }
            };
            double percentContainingEmoji = _tweetService.GetPercentageContainingEmojis(); 
            double percentContainingUrls = _tweetService.GetPercentageContainingUrls(false);
            double percentContainingPhotoUrls = _tweetService.GetPercentageContainingUrls(true);

            var response = new {
                totalTweets,
                runtime = elapsedTime.ToString(),
                averages = new {
                    perHour = averageTweets["hour"],
                    perMinute = averageTweets["minute"],
                    perSecond = averageTweets["second"]
                },
                percentTweetsContaining = new {
                    emojis = percentContainingEmoji,
                    url = percentContainingUrls,
                    photoUrls = percentContainingPhotoUrls
                },
                top = new {
                    domains = string.Join(" ", topDomains),
                    emojis = string.Join(" ", topEmojis),
                    hashtags = string.Join(" ", topHashtags)
                }
            };
            return Ok(JsonConvert.SerializeObject(response));
        }
    }
}
