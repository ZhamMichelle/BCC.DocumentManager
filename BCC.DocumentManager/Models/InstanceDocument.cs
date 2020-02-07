using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCC.DocumentManager.Models
{
    public class InstanceDocument
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public File File { get; set; }
        public int ProcessDocumentId { get; set; }
        public ProcessDocument ProcessDocument { get; set; }
    }
}
