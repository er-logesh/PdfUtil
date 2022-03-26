using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using System;
using System.IO;
using System.Security;

namespace Common.PdfUtil.Core.Util
{
    public class UnlockPdf
    {
        private SecureString pwd;
        public UnlockPdf(SecureString pass)
        {
            pwd = pass;
        }
        public void UnlockOperation(string filePath, string outputFolder, out string newPath)
        {
            var rename = string.IsNullOrEmpty(outputFolder);
            outputFolder = string.IsNullOrEmpty(outputFolder) ? Directory.GetParent(filePath).FullName : outputFolder;
            string filename = Path.GetFileNameWithoutExtension(filePath);
            newPath = Path.Combine(outputFolder, rename ? filename + "_Removed.pdf" : filename + ".pdf");
            File.Copy(filePath, newPath, true);
            PdfDocument document = PdfReader.Open(newPath, PdfDocumentOpenMode.Modify, PasswordProvider);
            document.Save(newPath);
        }
        private void PasswordProvider(PdfPasswordProviderArgs args)
        {
            args.Password = pwd.ConvertToString();
        }
    }
}
