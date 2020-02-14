using System.Collections.Generic;

namespace BCC.DocumentManager.Models
{
    public class Process
    {
        public Process() 
        {
            Documents = new List<ProcessDocument>();
            Views = new List<View>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProcessDocument> Documents { get; set; }
        public List<View> Views { get; set; }

    }
}
