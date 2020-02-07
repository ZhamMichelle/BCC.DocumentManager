﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCC.DocumentManager.Models
{
    public class ViewDocument
    {
        public int Id { get; set; }
        public int ViewId { get; set; }
        public View View { get; set; }
        public int DocumentId { get; set; }
        public Document Document { get; set; }
    }
}