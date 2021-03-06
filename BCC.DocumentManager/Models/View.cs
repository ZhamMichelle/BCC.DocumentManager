﻿using System.Collections.Generic;

namespace Bcc.DocumentManager.Models
{
    public class View
    {
        public View() 
        {
            Documents = new List<ViewDocument>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProcessId { get; set; }
        public Process Process { get; set; }
        public List<ViewDocument> Documents { get; set; }
    }
}
