using CommandLine;
using Common.PdfUtil.Core.Model;

namespace PdfUtil.Model
{
    public class CmdOptions : UnlockPdf
    {
        [Option('o', "operation", Required = true, HelpText = "Select One off the Avaliable option: Protect,Unlock")]
        public PdfOperation DocumentOperation { get; set; }
    }
}
