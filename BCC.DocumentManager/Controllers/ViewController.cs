using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bcc.DocumentManager.Models;
using Microsoft.Extensions.Logging;

namespace Bcc.DocumentManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ViewController : ControllerBase
    {
        private readonly PostgresContext _context;
        private readonly ILogger<ViewController> _logger;

        public ViewController(PostgresContext context, ILogger<ViewController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> PostView(View view)
        {
            if (view == null)
            {
                return BadRequest();
            }
            _context.Views.Add(view);
            await _context.SaveChangesAsync();
            return Ok(view);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<View>>> GetViews()
        {
            return await _context.Views.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<View>> GetView(int id)
        {
            View view = await _context.Views.Include(view => view.Documents)
                                            .ThenInclude(viewDoc => viewDoc.Document)
                                            .ThenInclude(doc => doc.Types)
                                            .FirstOrDefaultAsync(x => x.Id == id);
            if (view == null)
                return NotFound();
            return new ObjectResult(view);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<View>> DeleteView(int id)
        {
            View view = _context.Views.FirstOrDefault(x => x.Id == id);
            if (view == null)
            {
                return NotFound();
            }
            _context.Views.Remove(view);
            await _context.SaveChangesAsync();
            return Ok(view);
        }

        [HttpPost("postviewdoc")]
        public async Task<ActionResult> PostViewDoc(ViewDocument viewdocument)
        {
            if (viewdocument == null)
            {
                return BadRequest();
            }
            _context.ViewDocuments.Add(viewdocument);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("postviewundoc")]
        public async Task<ActionResult> PostViewUnDoc(ViewDocument viewDocument) 
        {
            if(viewDocument == null) {
                return BadRequest();
            }
            _context.ViewDocuments.Remove(viewDocument);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("getviewdoc")]
        public async Task<ActionResult<IEnumerable<ViewDocument>>> GetViewDocs()
        {
            return await _context.ViewDocuments.ToListAsync();
        }

        [HttpPost("GetViewDocs")]
        public async Task<ActionResult> GetViewDocs(string processId, int viewId, [FromBody]File file) 
        {            
            var files = await _context.Files.Include(f => f.Document)
                                            .ThenInclude(doc => doc.Types)
                                            .Where(f => f.BusinessKey == file.BusinessKey).ToListAsync();

            if(!files.Any()) 
            {
                var processDocuments = await _context.ProcessDocuments
                    .Include(p => p.Document)
                    .ThenInclude(d =>d.Types)
                    .Where(procDoc => procDoc.ProcessId == processId).ToListAsync();
                List<File> newFiles = new List<File>();
                processDocuments.ForEach(procDoc => {
                    newFiles.Add(new File() {
                           BusinessKey = file.BusinessKey,
                           ClientIin = file.ClientIin,
                           DocumentId = procDoc.DocumentId,
                           IsRequired = procDoc.IsRequired,
                           IsCapturePhoto = procDoc.IsCapturePhoto,
                           DocumentTypeId = procDoc.Document.Types.FirstOrDefault(t => t.IsDefault == true)?.Id
                       });
                });
                _context.Files.AddRange(newFiles);
                await _context.SaveChangesAsync();

                files = await _context.Files.Include(f => f.Document)
                                            .ThenInclude(doc => doc.Types)
                                            .Where(f => f.BusinessKey == file.BusinessKey).ToListAsync();
            }

            var viewDocs = await _context.ViewDocuments.Where(viewDoc => viewDoc.ViewId == viewId).ToListAsync();

            viewDocs = viewDocs.OrderBy(vd => vd.Order).ToList();
            List<File> result = new List<File>();
            viewDocs.ForEach(vd => result.Add(files.FirstOrDefault(f => f.DocumentId == vd.DocumentId)));

            return Ok(result);
        }

        [HttpPost("readOnly")]
        public async Task<ActionResult> setReadOnly(ViewDocument viewDocument){
            var document = await _context.ViewDocuments.FirstOrDefaultAsync(m => m.DocumentId == viewDocument.DocumentId && m.ViewId == viewDocument.ViewId);
            if(document == null) {
                return NotFound();
            }
            document.IsReadOnly = viewDocument.IsReadOnly;
            _context.ViewDocuments.Update(document);
            await _context.SaveChangesAsync();            
            return Ok();
        }

        [HttpPost("changeOrder")]
        public async Task<ActionResult> changeOrder(ViewDocument[] viewDocuments) {
            var document1 = await _context.ViewDocuments.FirstOrDefaultAsync(m => m.ViewId == viewDocuments[0].ViewId && m.DocumentId == viewDocuments[0].DocumentId);
            var document2 = await _context.ViewDocuments.FirstOrDefaultAsync(m => m.ViewId == viewDocuments[1].ViewId && m.DocumentId == viewDocuments[1].DocumentId);

            var temp = document1.Order;
            document1.Order = document2.Order;
            document2.Order = temp;

            _context.ViewDocuments.Update(document1);
            _context.ViewDocuments.Update(document2);
            await _context.SaveChangesAsync();
            
            return Ok();
        }
    }
}