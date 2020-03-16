namespace Bcc.DocumentManager.Models
{
    public class File
    {
        public int Id { get; set; }
        public string BusinessKey {get;set;}
        public string ClientIin { get; set; }
        public string ColvirId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string FileContentId { get; set; }
        public FileContent FileContent { get; set; }        
        public string DocumentTypeId { get; set; }
        public DocumentType documentType { get; set; }
        public int DocumentId { get; set; }
        public Document Document { get; set; }
        public bool IsRequired { get; set; }
        public bool IsCapturePhoto { get; set; }
    }
}
