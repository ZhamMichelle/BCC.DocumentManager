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
    public class InstDocController : ControllerBase
    {
        PostgresContext db = new PostgresContext();

     
        [HttpPost]
        public async Task<ActionResult<InstanceDocument>> PostInstDoc(InstanceDocument instdoc)
        {
            if (instdoc == null)
            {
                return BadRequest();
            }

            db.InstanceDocuments.Add(instdoc);
            await db.SaveChangesAsync();
            return Ok(instdoc);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstanceDocument>>> GetInstDocs()
        {
            return await db.InstanceDocuments.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InstanceDocument>> GetInstDoc(int id)
        {
            InstanceDocument instdoc = await db.InstanceDocuments.FirstOrDefaultAsync(x => x.Id == id);
            if (instdoc == null)
                return NotFound();
            return new ObjectResult(instdoc);
        }

        [HttpPut]
        public async Task<ActionResult<InstanceDocument>> PutInstDoc(InstanceDocument instdoc)
        {
            if (instdoc == null)
            {
                return BadRequest();
            }
            if (!db.InstanceDocuments.Any(x => x.Id == instdoc.Id))
            {
                return NotFound();
            }
            db.Update(instdoc);
            await db.SaveChangesAsync();
            return Ok(instdoc);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<InstanceDocument>> DeleteInstDoc(int id)
        {
            InstanceDocument instdoc = db.InstanceDocuments.FirstOrDefault(x => x.Id == id);
            if (instdoc == null)
            {
                return NotFound();
            }
            db.InstanceDocuments.Remove(instdoc);
            await db.SaveChangesAsync();


            File file = db.Files.FirstOrDefault(x => x.Id == instdoc.FileId);
            if (file == null)
            {
                return NotFound();
            }
            db.Files.Remove(file);
            await db.SaveChangesAsync();


            ProcessDocument procdoc = db.ProcessDocuments.FirstOrDefault(x => x.Id == instdoc.ProcessDocumentId);
            if (procdoc == null)
            {
                return NotFound();
            }
            db.ProcessDocuments.Remove(procdoc);
            await db.SaveChangesAsync();


            Process process = db.Processes.FirstOrDefault(x => x.Id == procdoc.ProcessId);
            if (process == null)
            {
                return NotFound();
            }
            db.Processes.Remove(process);
            await db.SaveChangesAsync();


            Document document = db.Documents.FirstOrDefault(x => x.Id == procdoc.DocumentId);
            if (document == null)
            {
                return NotFound();
            }
            db.Documents.Remove(document);
            await db.SaveChangesAsync();

            return Ok(instdoc);
        }
    }
}