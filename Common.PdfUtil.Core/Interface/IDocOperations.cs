using Common.PdfUtil.Core.Model;

namespace Common.PdfUtil.Core.Interface
{
    public interface IDocOperations
    {
        public delegate void ProcessUpdate(ProcessEventArgs eventArgs);

        public event ProcessUpdate ProcessUpdateEvent;

        bool ProcessDocument<TInput>(TInput operation);
    }
}
