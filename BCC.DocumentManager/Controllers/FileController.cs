using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCC.DocumentManager.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace BCC.DocumentManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly PostgresContext _context;
        private readonly ILogger<FileController> _logger;

        public FileController(PostgresContext context, ILogger<FileController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("{page}/{size}")]
        public async Task<ActionResult<PagedResult<File>>> GetFiles(int page, int size)
        {
            var result = await _context.Files.GetPagedAsync(page, size);
            return Ok(result);
        }
        

        [HttpGet("{id}")]
        public async Task<ActionResult<File>> GetFile(int id)
        {
            File file = await _context.Files.FirstOrDefaultAsync(x => x.Id == id);
            if (file == null)
                return NotFound();
            return new ObjectResult(file);
        }

        [HttpGet("clientiin/{iin}")]
        public async Task<ActionResult<List<File>>> GetFilesByClientIin(string iin)
        {
            return await _context.Files.Where(w => w.ClientIin == iin).ToListAsync();
        }

        [HttpGet("view/{viewId}/iin/{iin}")]
        public async Task<ActionResult<List<File>>> GetFilesByViewAndIin(int viewId, string iin)
        {
            _context.Files.Include(i => i.Document).ThenInclude(ti => ti.Views);
            return await _context.Files.Where(w => w.ClientIin == iin).Where(w => w.Document.Views.Any(view => view.ViewId == viewId)).ToListAsync();
        }
    }
}