using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        
        [HttpGet("{page}/{size}")]
        public async Task<ActionResult<PagedResult<Document>>> GetPaged(int page, int size)
        {
            return await db.Documents.GetPagedAsync(page, size);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> Get(int id)
        {
            Document document = await db.Documents.FirstOrDefaultAsync(x => x.Id == id);
            if (document == null)
                return NotFound();
            return Ok(document);
        }

        [HttpPost]
        public async Task<ActionResult<Document>> Post(Document document)
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
        public async Task<ActionResult<Document>> Delete(int id)
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