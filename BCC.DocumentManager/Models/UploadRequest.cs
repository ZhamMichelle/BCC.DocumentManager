using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bcc.DocumentManager.Models
{
    public class UploadRequest
    {
        public int FileId { get;set; }
        public IFormFile File { get; set;}
    }
}