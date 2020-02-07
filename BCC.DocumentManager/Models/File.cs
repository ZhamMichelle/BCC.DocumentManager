namespace BCC.DocumentManager.Models
{
    public class File
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int BlobId { get; set; }
        public Blob Blob { get; set; }
    }
}
