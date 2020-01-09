using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace TweetAnalytics
{
    public class Program {
        public async static Task Main (string[] args) {
            var host = CreateHostBuilderAsync (args);
            await host.Build ().RunAsync ();
        }

        public static IHostBuilder CreateHostBuilderAsync (string[] args) {
            var builder = new ConfigurationBuilder ()
                .SetBasePath (Directory.GetCurrentDirectory ())
                .AddJsonFile ($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional : true, reloadOnChange : true)
                .AddJsonFile ($"appsettings.json", optional : true, reloadOnChange : true);
            var config = builder.Build ();
            return Host.CreateDefaultBuilder (args)
                .ConfigureWebHostDefaults (webBuilder => {
                    webBuilder.UseConfiguration (config);
                    webBuilder.UseStartup<Startup> ();
                });
        }
    }
}