namespace BCC.DocumentManager.Models
{
    public class File
    {
        public int Id { get; set; }
        public string ClientIin { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string ColvirId { get; set; }
        public int DocumentId { get; set; }
        public Document Document { get; set; }
    }
}
