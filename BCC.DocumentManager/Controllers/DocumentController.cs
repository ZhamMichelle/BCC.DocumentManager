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
    public class DocumentController : ControllerBase
    {
        private readonly PostgresContext _context;
        private readonly ILogger<DocumentController> _logger;

        public DocumentController(PostgresContext context, ILogger<DocumentController> logger) 
        {
            _context = context;
            _logger = logger;
        }

        
        [HttpGet("{page}/{size}")]
        public async Task<ActionResult<PagedResult<Document>>> GetPaged(int page, int size)
        {
            return await _context.Documents.GetPagedAsync(page, size);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> Get(int id)
        {
            Document document = await _context.Documents.FirstOrDefaultAsync(x => x.Id == id);
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
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
            return Ok(document);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Document>> Delete(int id)
        {
            Document document = _context.Documents.FirstOrDefault(x => x.Id == id);
            if (document == null)
            {
                return NotFound();
            }
            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
            return Ok(document);
        }

        [HttpGet("process/{id}")]
        public async Task<ActionResult<List<Document>>> GetDocumentsByProcess(string id)
        {
            _context.ProcessDocuments.Include( i => i.Document);
            return await _context.ProcessDocuments.Where(w => w.ProcessId == id).Select(sel => sel.Document).ToListAsync();
        }

        [HttpGet("view/{id}")]
        public async Task<ActionResult<List<Document>>> GetDocumentsByView(int id)
        {
            _context.ViewDocuments.Include(i => i.Document);
            return await _context.ViewDocuments.Where(w => w.ViewId == id).Select(sel => sel.Document).ToListAsync();
        }
    }
}