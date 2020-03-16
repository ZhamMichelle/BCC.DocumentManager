using Bcc.DocumentManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;

namespace Bcc.DocumentManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private readonly PostgresContext _context;
        private readonly ILogger<ProcessController> _logger;

        public ProcessController(PostgresContext context, ILogger<ProcessController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet("{page}/{size}")]
        public async Task<ActionResult<PagedResult<Process>>> Get(int page, int size, string text)
        {
            var query = _context.Processes.AsQueryable();
            if(!string.IsNullOrEmpty(text))
            {
                query = query.Where(c => c.Name.ToLower().Contains(text.ToLower()));
            }
            return await query.GetPagedAsync(page, size);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Process>> Get(string id)
        {
            Process process = await _context.Processes
            .Include(i => i.Documents)
            .ThenInclude(ti => ti.Document)
            .Include(m => m.Views)
            .ThenInclude(mi => mi.Documents)
            .FirstOrDefaultAsync(x => x.Id == id);
            if (process == null)
            {
                return NotFound();
            }

            process.Views.ForEach(view => { view.Documents = view.Documents.OrderBy(v => v.Order).ToList(); });

            return process;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Process process)
        {
            if (process == null)
            {
                return BadRequest();
            }
            _context.Processes.Add(process);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id) {
            var process = await _context.Processes.FirstOrDefaultAsync(m => m.Id == id);
            if(process == null) {
                return NotFound();
            }
            else {
                _context.Remove(process);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpPost("postprocdoc")]
        public async Task<ActionResult> PostDoc(ProcessDocument procdoc)
        {
            if (procdoc == null)
            {
                return BadRequest();
            }
            _context.ProcessDocuments.Add(procdoc);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("postprocundoc")]
        public async Task<ActionResult> PostUnDoc(ProcessDocument procdoc) {
            var document = await _context.ProcessDocuments.FirstOrDefaultAsync(m => m.DocumentId == procdoc.DocumentId && m.ProcessId == procdoc.ProcessId);
            if(document == null) {
                return NotFound();
            }
            else{
                _context.ProcessDocuments.Remove(document);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpPost("required")]
        public async Task<ActionResult> setDocRequire(ProcessDocument procdoc) {
            var document = await _context.ProcessDocuments.FirstOrDefaultAsync(m => m.DocumentId == procdoc.DocumentId && m.ProcessId == procdoc.ProcessId);
            if(document == null) {
                return NotFound();
            }
            else {
                document.IsRequired = procdoc.IsRequired;
                _context.ProcessDocuments.Update(document);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpPost("capturePhoto")]
        public async Task<ActionResult> setCapturePhoto(ProcessDocument procdoc) {
            var document = await _context.ProcessDocuments.FirstOrDefaultAsync(m => m.DocumentId == procdoc.DocumentId && m.ProcessId == procdoc.ProcessId);
            if(document == null) {
                return NotFound();
            }
            else {
                document.IsCapturePhoto = procdoc.IsCapturePhoto;
                _context.ProcessDocuments.Update(document);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }
    }
}