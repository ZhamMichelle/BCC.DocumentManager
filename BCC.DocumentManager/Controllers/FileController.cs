using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCC.DocumentManager.Models;

namespace BCC.DocumentManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        PostgresContext db = new PostgresContext();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<File>>> GetFiles()
        {
            return await db.Files.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<File>> GetFile(int id)
        {
            File file = await db.Files.FirstOrDefaultAsync(x => x.Id == id);
            if (file == null)
                return NotFound();
            return new ObjectResult(file);
        }
    }
}