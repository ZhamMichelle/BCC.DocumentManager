using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bcc.DocumentManager.Models
{
    public class Document
    {
        public Document() 
        {
            Files = new List<File>();
            Processes = new List<ProcessDocument>();
            Views = new List<ViewDocument>();
            Types = new List<DocumentType>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<DocumentType> Types { get;set; }
        public List<File> Files { get; set; }
        public List<ProcessDocument> Processes { get; set; }
        public List<ViewDocument> Views { get; set; }
    }
}
