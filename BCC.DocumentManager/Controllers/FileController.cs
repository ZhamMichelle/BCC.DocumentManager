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

        [HttpGet("{page}/{size}")]
        public async Task<ActionResult<PagedResult<File>>> GetFiles(int page, int size)
        {
            var result = await db.Files.GetPagedAsync(page, size);
            return Ok(result);
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