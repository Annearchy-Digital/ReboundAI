# ReboundAI
Sentiment analysis and keyword extraction powered by Azure Cognitive Services

## What Rebound?
Since the start of the Washington state COVID-19 lockdowns in March, our local business community leaders having be polling local business owners as a means of keeping the pulse during this crisis. This tool is intended to accelerate the process of analyzing data returned by survey tools (like SurveyMonkey, Google Forms, etc).

As we move toward re-opening following the 4-phase process in Washington state, ReboundAI will be the machine learning enhanced brain for rapid analysis of polling data. Hopefully, this would provide a leading indicator of the "Rebound" in small business by rapidly tracking the mood at small businesses rather than taking on the effort of measuring trailing economic indicators.

## What does it do?
In its current form, ReboundAI uses [Microsoft Azure Cognitive Services](https://azure.microsoft.com/en-us/services/cognitive-services/text-analytics/) to perform sentiment analysis (percent positive, negative, and neutral) and keyword extraction on CSV data. This allows it to take in fairly universal input, providing flexibility.

## What could it do in the future?
If successful and useful, ReboundAI could expand to offer features like:
* Web interface
* Built-in visualization
* Direct integration with polling apps (SurveyMonkey, Google Forms, etc)
* Quantities analysis
* Correlations between sentiment and keyword such that we could categorize topics like "Keywords of concern for most negative businesses"
