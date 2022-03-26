using Common.PdfUtil.Core.Interface;
using Common.PdfUtil.Core.Model;
using Common.PdfUtil.Core.Util;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using static Common.PdfUtil.Core.Interface.IDocOperations;

namespace Common.PdfUtil.Core.Implementations
{
    public class UnlockPdf : IDocOperations
    {
        public event ProcessUpdate ProcessUpdateEvent;
        private ProtectUnlockOperation operationModel;
        public bool ProcessDocument<TInput>(TInput operation)
        {
            if (!(operation is ProtectUnlockOperation opModel))
            {
                throw new InvalidOperationException("Invalid operation.");
            }
            operationModel = opModel;
            if (!string.IsNullOrEmpty(opModel.FolderPath))
            {
                if (!Directory.Exists(opModel.FolderPath) || !Directory.GetFiles(opModel.FolderPath).Any())
                    throw new InvalidOperationException("Either folder doesn't exist or folder doesn't have files.");

                opModel.PdfFilePath = Directory.GetFiles(opModel.FolderPath).ToList();
            }
            if (!opModel.PdfFilePath.Any())
                throw new InvalidOperationException("No file found.");
            int i = 1;
            ProcessEventArgs eventArgs = new ProcessEventArgs();
            foreach (var file in opModel.PdfFilePath)
            {
                eventArgs.currentFile = i;
                eventArgs.TotalFiles = opModel.PdfFilePath.Count();
                eventArgs.FileName = Path.GetFileName(file);
                eventArgs.Status = DocStatus.Info;
                eventArgs.Message = $"Processing - {i} of {opModel.PdfFilePath.Count()} files.";
                ProcessFiles(opModel, file, eventArgs);
                i++;

            }
            eventArgs.Message = $"Completed Processing {opModel.PdfFilePath.Count()} files";
            i = 0;
            return true;
        }

        private void ProcessFiles(ProtectUnlockOperation opModel, string file, ProcessEventArgs eventArgs)
        {
            try
            {
                ProcessUpdateEvent?.Invoke(eventArgs);
                DoUnlockPdf(opModel.OutputFolder, file, out var newPath);
                eventArgs.Status = DocStatus.Success;
                eventArgs.Message = $"Successfully Processed file - {Path.GetFileName(file)} - Output can be found {newPath}";

                ProcessUpdateEvent?.Invoke(eventArgs);
                if (opModel.OpenFile)
                {
                    Process process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    process.StartInfo = startInfo;
                    startInfo.FileName = newPath;
                    startInfo.UseShellExecute = true;
                    process.Start();
                }
            }
            catch (Exception ex)
            {
                eventArgs.Status = DocStatus.Error;
                eventArgs.Message = $"Failed in Processing file - {Path.GetFileName(file)}";
                ProcessUpdateEvent?.Invoke(eventArgs);
            }
        }

        private void DoUnlockPdf(string outputFolder, string filePath, out string newPath)
        {
            var rename = string.IsNullOrEmpty(outputFolder);
            outputFolder = string.IsNullOrEmpty(outputFolder) ? Directory.GetParent(filePath).FullName : outputFolder;
            string filename = Path.GetFileNameWithoutExtension(filePath);
            newPath = Path.Combine(outputFolder, rename ? filename + "_Removed.pdf" : filename + ".pdf");
            File.Copy(filePath, newPath, true);
            try
            {
                PdfReader.Open(newPath, operationModel.PdfPass.ConvertToString());
            }
            catch
            {
                ProcessUpdateEvent?.Invoke(new ProcessEventArgs()
                {
                    Message = "Invalid password",
                    Status = DocStatus.Error
                });
            }
            PdfDocument document = PdfReader.Open(newPath, PdfDocumentOpenMode.Modify, PasswordProvider);
            document.Save(newPath);
        }
        private void PasswordProvider(PdfPasswordProviderArgs args)
        {
            args.Password = operationModel.PdfPass.ConvertToString();
        }
    }
}
