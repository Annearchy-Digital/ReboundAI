using CommandLine;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ReboundAI.Console
{
    /// <summary>
    /// Class describing the command-line parameters for the console app
    /// Powered by CommandLine NuGet package: https://github.com/commandlineparser/commandline
    /// </summary>
    public class Options
    {
        [Option('i', "inputFile", Required = true, HelpText = "Set the input file (CSV)")]
        public string InputeFilePath { get; set; }

        [Option('o', "outputFile", Default = "Output.csv", HelpText = "Set the output file (CSV), defaults to 'Output.csv'")]
        public string OutputFilePath { get; set; }

        [Option('a', "analysisToken", Default = "_analysis", HelpText = "The string a column name must end with in order to be analyzed")]
        public string AnalysisToken { get; set; }
    }

    /// <summary>
    /// Entry point for the console application
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed(options =>
                   {
                       Run(options);
                   });
        }

        private static void Run(Options options)
        {
            // Check file
            System.Console.WriteLine($"FilePath: {options.InputeFilePath}");

            // Read file
            // Right now, it assumes there is a header record and anything that has a "_analyze" at the end will be analyzed
            using (var reader = new StreamReader(options.InputeFilePath))
            using (var writer = new StreamWriter(options.OutputFilePath))
            using (var input = new CsvReader(reader, CultureInfo.InvariantCulture))
            using (var output = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                IEnumerable<dynamic> rows = input.GetRecords<dynamic>();

                // Headers
                dynamic headerRow = rows.First();
                foreach (dynamic headerCol in headerRow)
                {
                    string header = headerCol.Key;
                    string value = headerCol.Value;
                    output.WriteField(header);
                    if (IsAnalysisColumn(options, header))
                    {
                        output.WriteField($"{header}_positiveSentiment");
                        output.WriteField($"{header}_neutralSentiment");
                        output.WriteField($"{header}_negativeeSentiment");
                        output.WriteField($"{header}_keywords");
                    }
                }
                output.NextRecord();

                // Data
                IEnumerable<dynamic> dataRows = rows.Skip(1);
                foreach (dynamic row in dataRows)
                {
                    List<string> outputRow = new List<string>();
                    foreach (dynamic col in row)
                    {
                        string header = col.Key;
                        string value = col.Value;
                        output.WriteField(value);
                        if (IsAnalysisColumn(options, header))
                        {
                            output.WriteField($"Data for {value}_positiveSentiment");
                            output.WriteField($"Data for {value}_neutralSentiment");
                            output.WriteField($"Data for {value}_negativeeSentiment");
                            output.WriteField($"Data for {value}_keywords");
                        }
                    }
                    output.NextRecord();
                }
                System.Console.WriteLine($"Analysis Done");
                output.Flush();
                writer.Flush();
            }
        }

        private static bool IsAnalysisColumn(Options options, string header)
        {
            return header.EndsWith(options.AnalysisToken);
        }
    }
}
