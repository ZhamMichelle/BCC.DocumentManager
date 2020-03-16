using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bcc.DocumentManager.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Bcc.DocumentManager.Controllers
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

        [HttpPost("upload")]
        public async Task<ActionResult> Upload([FromForm] UploadRequest request)
        {
            using var stream = new System.IO.MemoryStream();
            await request.File.CopyToAsync(stream);

            var file = await _context.Files.FindAsync(request.FileId);
            file.Name = request.File.FileName;
            file.Type = request.File.ContentType;

            var fileContent = new FileContent()
            {
                Id = file.FileContentId,
                Content = stream.ToArray(),
                File = file            
            };
            if(string.IsNullOrEmpty(fileContent.Id)){                
                fileContent.Id = System.Guid.NewGuid().ToString();
                _context.FileContents.Add(fileContent);
            }
            else {
                _context.FileContents.Update(fileContent);
            }
            await _context.SaveChangesAsync();
            return Ok(fileContent.Id);
        }

        [HttpGet("{id}/documentType/{documentTypeId}")]
        public async Task<ActionResult> SetDocumentType(int id, string documentTypeId) {
            var file = await _context.Files.FindAsync(id);
            if(file == null) {
                return BadRequest();
            }
            file.DocumentTypeId = documentTypeId;
            _context.Files.Update(file);
            await _context.SaveChangesAsync();            
            return Ok();
        }

        [HttpGet("download/{id}")]
        public async Task<ActionResult> Download(string id)
        {
            var fileContent = await _context.FileContents.Include(fc => fc.File).FirstOrDefaultAsync(fc => fc.Id == id);            
            return File(fileContent.Content, fileContent.File.Type, fileContent.File.Name);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id){
            var fileContent = await _context.FileContents.FindAsync(id);
            if(fileContent == null) {
                return BadRequest();
            }
            _context.FileContents.Remove(fileContent);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("/{id}/procDocRequired/{isRequired}")]
        public async Task<ActionResult> setProcessDocRequired(int id, bool isRequired) {
            var file = await _context.Files.FindAsync(id);        
            if(file == null) {
                return NotFound();
            }
            file.IsRequired = isRequired;
            _context.Files.Update(file);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}