using CommandLine;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ReboundAI.Console
{
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
                       // Show input values
                       System.Console.WriteLine($"Input Path: {options.InputFilePath}");
                       System.Console.WriteLine($"Output Path: {options.OutputFilePath}");
                       System.Console.WriteLine($"Key Phrase Output Path: {options.KeyPhraseFilePath}");
                       System.Console.WriteLine($"Analysis Token: {options.AnalysisToken}");
                       string numberToProcess = options.Take.HasValue ? options.Take.Value.ToString() : "All";
                       System.Console.WriteLine($"Number of rows to process: {numberToProcess}");

                       // Do work
                       Analyzer runner = new Analyzer();
                       runner.Run(options);
                   });
        }
    }
}
