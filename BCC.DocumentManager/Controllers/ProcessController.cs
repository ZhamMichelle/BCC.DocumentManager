using Bcc.DocumentManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

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


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Process>>> GetProcesses()
        {
            return await _context.Processes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Process>> GetProcess(string id)
        {
            Process process = await _context.Processes.FirstOrDefaultAsync(x => x.Id == id);
            if (process == null)
                return NotFound();
            return new ObjectResult(process);
        }

        [HttpPost("postprocdoc")]
        public async Task<ActionResult<ProcessDocument>> PostDoc(ProcessDocument procdoc)
        {
            if (procdoc == null)
            {
                return BadRequest();
            }
            _context.ProcessDocuments.Add(procdoc);
            await _context.SaveChangesAsync();
            return Ok(procdoc);
        }
    }
}