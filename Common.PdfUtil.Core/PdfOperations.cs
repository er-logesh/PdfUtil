using Common.PdfUtil.Core.Model;
using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Diagnostics;
using Common.PdfUtil.Core.Util;

namespace Common.PdfUtil.Core
{
    public class PdfOperations
    {
        public delegate void PdfEvent(BaseOperation operation, string filename, int processingcount, int totalFile);
        public event PdfEvent PdfOperationsEvent;
        public void UnlockPdf(ProtectUnlockOperation operation)
        {
            var filesToUnlock = GetPdfFiles(operation);
            var unlockPdf = new UnlockPdf(operation.PdfPass);
            string newPath = string.Empty;
            int i = 1;
            foreach (var file in filesToUnlock)
            {
                unlockPdf.UnlockOperation(file, operation.OutputFolder, out newPath);
                PdfOperationsEvent?.Invoke(operation, newPath, i, filesToUnlock.Count());
                i++;
                if (operation.OpenFile)
                {
                    Process process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    process.StartInfo = startInfo;
                    startInfo.FileName = newPath;
                    startInfo.UseShellExecute = true;
                    process.Start();
                }
            }
            i = 1;
        }
        private List<string> GetPdfFiles(BaseOperation operation)
        {
            var pdfFiles = new List<string>();
            if (!string.IsNullOrEmpty(operation.FolderPath))
            {
                if (Directory.Exists(operation.FolderPath))
                {
                    pdfFiles = Directory.GetFiles(operation.FolderPath, "*.pdf").ToList();
                    if (!pdfFiles.Any())
                        throw new Exception("Couldn't find pdf files in the given directory path");
                    return pdfFiles;
                }
                else
                {
                    throw new Exception("Invalid directory path.");
                }
            }
            else if (!string.IsNullOrEmpty(operation.PdfFilePath))
            {
                if (File.Exists(operation.PdfFilePath))
                {
                    if (!Path.GetExtension(operation.PdfFilePath).Equals(".pdf", StringComparison.InvariantCultureIgnoreCase))
                    {
                        throw new Exception("Invalid file extension");
                    }
                    pdfFiles.Add(operation.PdfFilePath);
                    return pdfFiles;
                }
                else
                {
                    throw new Exception("Invalid file path.");
                }
            }
            else
            {
                throw new Exception("Please provide file or directoryPath.");
            }
        }
    }
}