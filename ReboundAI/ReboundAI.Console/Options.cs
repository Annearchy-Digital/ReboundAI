using CommandLine;

namespace ReboundAI.Console
{
    /// <summary>
    /// Class describing the command-line parameters for the console app
    /// Powered by CommandLine NuGet package: https://github.com/commandlineparser/commandline
    /// </summary>
    public class Options
    {
        [Option('i', "inputFile", Required = true, HelpText = "Set the input file (CSV)")]
        public string InputFilePath { get; set; }

        [Option('o', "outputFile", Default = "Output.csv", HelpText = "Set the output file (CSV), defaults to 'Output.csv'")]
        public string OutputFilePath { get; set; }

        [Option('k', "keyPhraseFile", Default = "KeyPhrases.csv", HelpText = "Set the key phrase file (CSV) which accumulates each phrase indvidually (with repeats), defaults to 'KeyPhrases.csv'")]
        public string KeyPhraseFilePath { get; set; }

        [Option('a', "analysisToken", Default = "_analysis", HelpText = "The string a column name must end with in order to be analyzed")]
        public string AnalysisToken { get; set; }

        [Option('t', "take", Default = null, HelpText = "The number of records to process, default all of them")]
        public int? Take { get; set; }
    }
}
