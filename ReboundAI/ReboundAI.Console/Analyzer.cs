using CommandLine;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ReboundAI.Console
{
    /// <summary>
    /// Core logic for running through riles
    /// </summary>
    public class Analyzer
    {
        public void Run(Options options)
        {
            // Read file
            // Right now, it assumes there is a header record and anything that has a "_analyze" at the end will be analyzed
            using (var reader = new StreamReader(options.InputFilePath))
            using (var input = new CsvReader(reader, CultureInfo.InvariantCulture))
            using (var writer = new StreamWriter(options.OutputFilePath))
            using (var output = new CsvWriter(writer, CultureInfo.InvariantCulture))
            using (var keyPhraseWriter = new StreamWriter(options.KeyPhraseFilePath))
            using (var keyPhraseOutput = new CsvWriter(keyPhraseWriter, CultureInfo.InvariantCulture))
            {
                // TODO: Implement streaming such that you don't have to hold both files in memory at the same time
                IEnumerable<dynamic> rows = input.GetRecords<dynamic>();

                ReadAndWriteHeaders(options, output, rows);

                ReadAndWriteRows(options, output, rows, keyPhraseOutput);
                keyPhraseOutput.Flush();
                output.Flush();
                writer.Flush();
            }
        }

        /// <summary>
        /// Extracts the headers, optionally expands them if they're text analytics columns, then writes them to the output file
        /// </summary>
        /// <param name="options"></param>
        /// <param name="output"></param>
        /// <param name="rows"></param>
        virtual protected void ReadAndWriteHeaders(Options options, CsvWriter output, IEnumerable<dynamic> rows)
        {
            // Headers
            dynamic headerRow = rows.First();
            foreach (dynamic headerCol in headerRow)
            {
                string header = headerCol.Key;
                string value = headerCol.Value;
                output.WriteField(header);
                if (IsAnalysisColumn(options, header))
                {
                    output.WriteField($"{header}_sentiment");
                    // TODO: Sentiment Scores
                    output.WriteField($"{header}_keywords");
                }
            }
            output.NextRecord();
        }

        /// <summary>
        /// Extracts the data, runs it through text analytics, then writes it back into the output
        /// </summary>
        /// <param name="options"></param>
        /// <param name="output"></param>
        /// <param name="rows"></param>
        virtual protected void ReadAndWriteRows(Options options, CsvWriter output, IEnumerable<dynamic> rows, CsvWriter keyPhraseOutput)
        {
            // Data
            var rowsToProcess = rows.Skip(1);
            if (options.Take.HasValue)
                rowsToProcess = rowsToProcess.Take(options.Take.Value);

            int index = 1;
            IEnumerable<dynamic> dataRows = rowsToProcess;
            TextAnalytics analytics = new TextAnalytics();
            foreach (dynamic row in dataRows)
            {
                System.Console.WriteLine($"Processing row {index++}");
                List<string> outputRow = new List<string>();
                foreach (dynamic col in row)
                {
                    string header = col.Key;
                    string value = col.Value;
                    output.WriteField(value);
                    if (IsAnalysisColumn(options, header))
                    {
                        TextAnalyticsResult result = analytics.Run(value);
                        output.WriteField(result.Sentiment.ToString());
                        // TODO: Sentiment Scores
                        output.WriteField(result.KeyPhrasesSummary);
                        foreach (string phrase in result.KeyPhrases)
                        {
                            keyPhraseOutput.WriteField(phrase);
                            keyPhraseOutput.NextRecord();
                        }
                    }
                }
                output.NextRecord();
            }
            System.Console.WriteLine($"Analysis Done on {index - 1} rows");
        }

        private bool IsAnalysisColumn(Options options, string header)
        {
            return header.EndsWith(options.AnalysisToken);
        }
    }
}
