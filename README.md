# ReboundAI
Measuring economic mood at scale using Azure Cognitive Services. To learn more about the project, visit [RebountTriCities.Com](http://reboundtricities.com/).

## What Rebound?
Since the start of the Washington state COVID-19 lockdowns in March, our local business community leaders having be polling local business owners as a means of keeping the pulse during this crisis. This tool is intended to accelerate the process of analyzing data returned by survey tools (like SurveyMonkey, Google Forms, etc).

As we move toward re-opening following the 4-phase process in Washington state, ReboundAI will be the machine learning enhanced brain for rapid analysis of polling data. Hopefully, this would provide a leading indicator of the "Rebound" in small business by rapidly tracking the mood at small businesses rather than taking on the effort of measuring trailing economic indicators.

## What does it do?
In its current form, ReboundAI uses [Microsoft Azure Cognitive Services](https://azure.microsoft.com/en-us/services/cognitive-services/text-analytics/) to perform sentiment analysis (percent positive, negative, and neutral) and keyword extraction on CSV data. This allows it to take in fairly universal input, providing flexibility, and illustrates the potential usefulness of machine learning in providing deeper understanding.

## What could it do in the future?
If successful and useful, ReboundAI could expand to offer features like:
* Web interface
* Built-in visualization
* Direct integration with polling apps (SurveyMonkey, Google Forms, etc)
* Quantities analysis
* Correlations between sentiment and keyword such that we could categorize topics like "Keywords of concern for most negative businesses"
* Batched processing to speed up analysis time

## Getting Started
To ready the tool itself, follow these steps:
1. Clone the repository
2. Open Solution in Visual Studio 2019
3. [Create your own Text Analytics instance in Microsoft Azure](https://docs.microsoft.com/en-us/azure/cognitive-services/cognitive-services-apis-create-account?tabs=multiservice%2Cwindows)
4. Copy the key and endpoint values from the Azure resource to the AppSettings.json file 
5. Build project (should auto-restore dependencies from NuGet)
5. Prepare your CSV by adding the "Analysis Token" to the end of the header names in relevant columns that need text analysis
6. Run it from the command-line supplying arguments (examine the Options class to learn about command-line args) OR change the project properties to reflect your own configuration and hit "Debug"
7. Patiently wait as each record is processed since it can take 1-3 seconds per text analysis

Before setting up the tool, you will need to get your data into a CSV file. Most survey tools (SurveyMonkey and Google Forms included) allow for CSV export, but I must leave it to you to Google how to do it in your case.

Given that the free tier of Azure's Text Analytics service allows for 5,000 transactions a month, most reasonable needs should be zero cost. Keep in mind that every cell that requires text analysis is a request, so you may burn through your quota quite quickly if you have a lot of free text fields in your survey.
