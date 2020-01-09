using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TweetAnalytics.Exceptions;
using TweetAnalytics.Models;
using TweetAnalytics.Services;

namespace TweetAnalytics
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton(services => {
                var configurationKeys = new string[] {
                    "twitter:consumerKey",
                    "twitter:consumerKeySecret",
                    "twitter:accessToken",
                    "twitter:accessTokenSecret"
                };

                foreach (var configurationKey in configurationKeys) {
                    if (string.IsNullOrEmpty(Configuration[configurationKey])) {
                        throw new ConfigurationException("Configuration value {configurationKey}");
                    }
                }

                var publisher = new TweetProducer(
                    Configuration["twitter:consumerKey"],
                    Configuration["twitter:consumerKeySecret"],
                    Configuration["twitter:accessToken"],
                    Configuration["twitter:accessTokenSecret"]
                );
                var tweetRepository = services.GetService<ITweetRepository>();
                var emojiRepository = services.GetService<IEmojiRepository>();
                var emojiService = services.GetService<IEmojiService>();
                var tweetService = services.GetService<ITweetService>();
                var configuration = services.GetService<IConfiguration>();

                TweetProcessorSubscriber tweetProcessorSubscriber = new TweetProcessorSubscriber(
                    tweetRepository,
                    emojiRepository,
                    emojiService,
                    tweetService,
                    configuration
                );
                publisher.TweetReceivedEvent += tweetProcessorSubscriber.HandleTweetReceivedEvent;
                
                return publisher;
            });
            services.AddSingleton<IHostedService>(p => p.GetService<TweetProducer>());
            services.AddSingleton<ITweetRepository, TweetRepository>();
            services.AddSingleton<IEmojiRepository, EmojiRepository>();

            services.AddSingleton<IEmojiService>(services => {
                var emojiRepository = services.GetService<IEmojiRepository>();
                var emojiService = new EmojiService(emojiRepository);
                return emojiService;
            });
            services.AddSingleton<ITweetService>(services => {
                var tweetRepository = services.GetService<ITweetRepository>();
                var tweetService = new TweetService(tweetRepository);
                return tweetService;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
