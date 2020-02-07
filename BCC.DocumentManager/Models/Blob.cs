namespace BCC.DocumentManager.Models
{
    public class Blob
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public File File { get; set; }
        public byte[] Data { get; set; }
    }
}