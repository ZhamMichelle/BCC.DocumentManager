using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCC.DocumentManager.Models;

namespace BCC.DocumentManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private PostgresContext db = new PostgresContext();

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocuments()
        {
            return await db.Documents.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> GetDocument(int id)
        {
            Document document = await db.Documents.FirstOrDefaultAsync(x => x.Id == id);
            if (document == null)
                return NotFound();
            return new ObjectResult(document);
        }

        [HttpPost]
        public async Task<ActionResult<Document>> PostDoc(Document document)
        {
            if (document == null)
            {
                return BadRequest();
            }
            db.Documents.Add(document);
            await db.SaveChangesAsync();
            return Ok(document);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Document>> DeleteDoc(int id)
        {
            Document document = db.Documents.FirstOrDefault(x => x.Id == id);
            if (document == null)
            {
                return NotFound();
            }
            db.Documents.Remove(document);
            await db.SaveChangesAsync();
            return Ok(document);
        }


    }
}