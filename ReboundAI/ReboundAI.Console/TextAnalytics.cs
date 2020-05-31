using Azure;
using System;
using System.Globalization;
using System.Linq;
using Azure.AI.TextAnalytics;
using System.Collections.Generic;

namespace ReboundAI.Console
{
    /// <summary>
    /// Data class for returning textanalytics info powered by Azure
    /// https://azure.microsoft.com/en-us/services/cognitive-services/text-analytics/
    /// TODO: Make this agnostic to Azure
    /// </summary>
    public class TextAnalyticsResult
    {
        public string Input { get; set; }

        // Sentiment analysis
        public TextSentiment Sentiment { get; set; }
        public double Positive { get; set; }
        public double Negative { get; set; }
        public double Neutral { get; set; }

        // Key phase extraction
        public IEnumerable<string> KeyPhrases { get; set; }
        public string KeyPhrasesSummary
        {
            get
            {
                return string.Join(',', KeyPhrases);
            }
        }
    }

    /// <summary>
    /// Class that wraps the Azure Text Analytics service
    /// TODO: Generalize this process of enriching the data and hide the Azure-specific implementation through thoughtful interfaces and design patterns
    /// </summary>
    public class TextAnalytics
    {
        private static readonly AzureKeyCredential Credentials = new AzureKeyCredential(AppSettings.TextAnalyticsKey);
        private static readonly Uri Endpoint = new Uri(AppSettings.TextAnalyticsEndpoint);

        public TextAnalyticsClient Client { get; } = new TextAnalyticsClient(Endpoint, Credentials);

        virtual public TextAnalyticsResult Run(string input)
        {
            TextAnalyticsResult result = new TextAnalyticsResult { Input = input };
            SentimentAnalysis(result);
            KeyPhraseExtraction(result);
            return result;
        }

        virtual protected void SentimentAnalysis(TextAnalyticsResult result)
        {
            DocumentSentiment documentSentiment = Client.AnalyzeSentiment(result.Input);
            result.Sentiment = documentSentiment.Sentiment;
            result.Positive = documentSentiment.ConfidenceScores.Positive;
            result.Negative = documentSentiment.ConfidenceScores.Negative;
            result.Neutral = documentSentiment.ConfidenceScores.Neutral;
        }

        virtual protected void KeyPhraseExtraction(TextAnalyticsResult result)
        {
            Response<KeyPhraseCollection> response = Client.ExtractKeyPhrases(result.Input);
            result.KeyPhrases = response.Value.ToArray();
        }
    }
}
