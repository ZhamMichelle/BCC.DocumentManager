using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCC.DocumentManager.Models
{
    public class ProcessDocument
    {
        public int Id { get; set; }
        public int ProcessId{ get; set; }
        public Process Process { get; set; }
        public int DocumentId { get; set; }
        public Document Document { get; set; }
        public string Required { get; set; }
    }
}
