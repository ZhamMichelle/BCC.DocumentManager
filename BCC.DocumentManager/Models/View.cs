namespace BCC.DocumentManager.Models
{
    public class View
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProcessId { get; set; }
        public Process Process { get; set; }
    }
}
