namespace Common.PdfUtil.Core.Model
{
    public enum DocStatus { Error, Warning, Success, Info }
    public class ProcessEventArgs
    {
        public int currentFile { get; set; }
        public int TotalFiles { get; set; }
        public string FileName { get; set; }
        public string Message { get; set; }
        public DocStatus Status { get; set; }
    }
}
