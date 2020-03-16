namespace Bcc.DocumentManager.Models
{
    public class DocumentType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
        public int DocumentId { get; set; }        
        public Document Document { get; set; }
    }
}