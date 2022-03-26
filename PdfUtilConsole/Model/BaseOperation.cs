using CommandLine;
using Common.PdfUtil.Core.Model;
using System.Collections.Generic;

namespace PdfUtil.Model
{
    public class BaseOperation
    {
        [Option('f', "filepath", Required = false, HelpText = "Please provide the valid filepath.", Group = "FileGroup")]
        public IEnumerable<string> PdfFilePath { get; set; }

        [Option("directory", Required = false, HelpText = "Please provide the valid directory path with files.", Group = "FileGroup")]
        public string FolderPath { get; set; }

        [Option('d', "output", Required = false, HelpText = "Destination path where output will be placed.")]
        public string OutputFolder { get; set; }

        [Option('o', "output", Required = false, HelpText = "Destination path where output will be placed.")]
        public PdfOperation Operation { get; set; }

        [Option('l', "launch", Required = true, HelpText = "open file once processed.")]
        public bool OpenFile { get; set; }

        [Option('p', "password", Required = false, HelpText = "provide the password phrase to unlock.")]
        public string PdfPassword { get; set; }
    }
}
