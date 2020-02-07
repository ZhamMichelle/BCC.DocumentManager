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
    public class ViewController : ControllerBase
    {
        PostgresContext db = new PostgresContext();

        [HttpPost]
        public async Task<ActionResult<View>> PostView(View view)
        {
            if (view == null)
            {
                return BadRequest();
            }
            db.Views.Add(view);
            await db.SaveChangesAsync();
            return Ok(view);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<View>>> GetViews()
        {
            return await db.Views.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<View>> GetView(int id)
        {
            View view = await db.Views.FirstOrDefaultAsync(x => x.Id == id);
            if (view == null)
                return NotFound();
            return new ObjectResult(view);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<View>> DeleteView(int id)
        {
            View view = db.Views.FirstOrDefault(x => x.Id == id);
            if (view == null)
            {
                return NotFound();
            }
            db.Views.Remove(view);
            await db.SaveChangesAsync();
            return Ok(view);
        }

        [HttpPost("postviewdoc")]
        public async Task<ActionResult<ViewDocument>> PostViewDoc(ViewDocument viewdocument)
        {
            if (viewdocument == null)
            {
                return BadRequest();
            }
            db.ViewDocuments.Add(viewdocument);
            await db.SaveChangesAsync();
            return Ok(viewdocument);
        }

        [HttpGet("getviewdoc")]
        public async Task<ActionResult<IEnumerable<ViewDocument>>> GetViewDocs()
        {
            return await db.ViewDocuments.ToListAsync();
        }

        [HttpGet("getviewdoc/{id}")]
        public async Task<ActionResult<ViewDocument>> GetViewDoc(int id)
        {
            ViewDocument viewdocument = await db.ViewDocuments.FirstOrDefaultAsync(x => x.Id == id);
            if (viewdocument == null)
                return NotFound();
            return new ObjectResult(viewdocument);
        }

        [HttpPut("putviewdoc")]
        public async Task<ActionResult<ViewDocument>> PutViewDoc(ViewDocument viewdoc)
        {
            if (viewdoc == null)
            {
                return BadRequest();
            }
            if (!db.ViewDocuments.Any(x => x.Id == viewdoc.Id))         //technology LINQ
            {
                return NotFound();
            }
            db.Update(viewdoc);
            await db.SaveChangesAsync();
            return Ok(viewdoc);
        }

        [HttpDelete("delviewdoc/{id}")]
        public async Task<ActionResult<ViewDocument>> DeleteViewDoc(int id)
        {
            ViewDocument viewdoc = db.ViewDocuments.FirstOrDefault(x => x.Id == id);
            if (viewdoc == null)
            {
                return NotFound();
            }
            db.ViewDocuments.Remove(viewdoc);
            await db.SaveChangesAsync();


            View view = db.Views.FirstOrDefault(x => x.Id == viewdoc.ViewId);
            if (view == null)
            {
                return NotFound();
            }
            db.Views.Remove(view);
            await db.SaveChangesAsync();


            Process process = db.Processes.FirstOrDefault(x => x.Id == view.ProcessId);
            if (process == null)
            {
                return NotFound();
            }
            db.Processes.Remove(process);
            await db.SaveChangesAsync();


            Document document = db.Documents.FirstOrDefault(x => x.Id == viewdoc.DocumentId);
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