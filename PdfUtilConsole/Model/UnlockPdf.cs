using CommandLine;
using Common.PdfUtil.Core.Util;
using System.Security;

namespace PdfUtil.Model
{
    public class UnlockPdf : BaseOperation
    {
        private string _pdfPassword;

        [Option('p', "password", Required = true, HelpText = "provide the password phrase to unlock.")]
        public new string PdfPassword
        {
            get { return _pdfPassword; }
            set
            {
                _pdfPassword = value;
                PdfPass = null;
                if (!string.IsNullOrEmpty(value))
                {
                    PdfPass = _pdfPassword.ConvertToSecureString();
                }
            }
        }
        public SecureString PdfPass { get; set; }
    }
}
