using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCC.DocumentManager.Models;
using Microsoft.Extensions.Logging;

namespace BCC.DocumentManager.Controllers
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
        public async Task<ActionResult<View>> PostView(View view)
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
            View view = await _context.Views.FirstOrDefaultAsync(x => x.Id == id);
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
        public async Task<ActionResult<ViewDocument>> PostViewDoc(ViewDocument viewdocument)
        {
            if (viewdocument == null)
            {
                return BadRequest();
            }
            _context.ViewDocuments.Add(viewdocument);
            await _context.SaveChangesAsync();
            return Ok(viewdocument);
        }

        [HttpGet("getviewdoc")]
        public async Task<ActionResult<IEnumerable<ViewDocument>>> GetViewDocs()
        {
            return await _context.ViewDocuments.ToListAsync();
        }
    }
}