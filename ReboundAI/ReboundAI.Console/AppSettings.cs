using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReboundAI.Console
{
    /// <summary>
    /// Reads the AppSettings.json file in the current directory
    /// </summary>
    public static class AppSettings
    {
        public static IConfiguration Configuration { get; private set; }
        private static IConfigurationSection TextAnalyticsSection { get; set; }

        public static string TextAnalyticsKey { get { return TextAnalyticsSection["Key"]; } }
        public static string TextAnalyticsEndpoint { get { return TextAnalyticsSection["Endpoint"]; } }

        static AppSettings()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            TextAnalyticsSection = Configuration.GetSection("TextAnalyticsCredentials");
        }
    }
}
