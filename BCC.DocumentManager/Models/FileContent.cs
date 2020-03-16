namespace Bcc.DocumentManager.Models
{
    public class FileContent
    {
        public string Id { get; set; }
        public byte[] Content { get; set; }        
        public File File { get; set; }
     }
}
