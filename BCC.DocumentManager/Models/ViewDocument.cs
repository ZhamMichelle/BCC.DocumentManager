namespace Bcc.DocumentManager.Models
{
    public class ViewDocument
    {
        public int ViewId { get; set; }
        public View View { get; set; }
        public int DocumentId { get; set; }
        public Document Document { get; set; }
    }
}
