namespace Bcc.DocumentManager.Models
{
    public class ProcessDocument
    {
        public string ProcessId{ get; set; }
        public Process Process { get; set; }
        public int DocumentId { get; set; }
        public Document Document { get; set; }
        public bool IsRequired { get; set; }
        public bool IsCapturePhoto { get; set; }
    }
}
