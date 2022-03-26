using System.Collections.Generic;
using System.Security;

namespace Common.PdfUtil.Core.Model
{
    public enum PdfOperation { Split, Merge, Protect, Unlock };
    public class ProtectUnlockOperation : BaseOperation
    {
        public SecureString PdfPass {get;set;}
    }
    public class BaseOperation
    {
        public List<string> PdfFilePath { get; set; }
        public string FolderPath { get; set; }
        public string OutputFolder { get; set; }
        public PdfOperation Operation { get; set; }
        public bool OpenFile { get; set; }
    }
}