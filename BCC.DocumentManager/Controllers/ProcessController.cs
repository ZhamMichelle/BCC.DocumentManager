using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCC.DocumentManager.Models;

namespace BCC.DocumentManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        PostgresContext db = new PostgresContext();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Process>>> GetProcesses()
        {
            return await db.Processes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Process>> GetProcess(int id)
        {
            Process process = await db.Processes.FirstOrDefaultAsync(x => x.Id == id);
            if (process == null)
                return NotFound();
            return new ObjectResult(process);
        }
       
        [HttpGet("getprocdoc")]
        public async Task<ActionResult<IEnumerable<ProcessDocument>>> GetProcessDocuments()
        {
            return await db.ProcessDocuments.ToListAsync();
        }

        [HttpGet("getprocdoc/{id}")]
        public async Task<ActionResult<ProcessDocument>> GetProcessDocument(int id)
        {
            ProcessDocument processdocument = await db.ProcessDocuments.FirstOrDefaultAsync(x => x.Id == id);
            if (processdocument == null)
                return NotFound();
            return new ObjectResult(processdocument);
        }


        [HttpPost("postprocdoc")]
        public async Task<ActionResult<ProcessDocument>> PostDoc(ProcessDocument procdoc)
        {
            if (procdoc == null)
            {
                return BadRequest();
            }
            db.ProcessDocuments.Add(procdoc);
            await db.SaveChangesAsync();
            return Ok(procdoc);
        }

        [HttpDelete("delprocdoc/{id}")]
        public async Task<ActionResult<ProcessDocument>> DeleteProcDoc(int id)
        {
            ProcessDocument procdoc = db.ProcessDocuments.FirstOrDefault(x => x.Id == id);
            if (procdoc == null)
            {
                return NotFound();
            }
            db.ProcessDocuments.Remove(procdoc);
            await db.SaveChangesAsync();


            Process process = db.Processes.FirstOrDefault(x => x.Id == procdoc.ProcessId);
            if (process == null)
            {
                return NotFound();
            }
            db.Processes.Remove(process);
            await db.SaveChangesAsync();


            Document document = db.Documents.FirstOrDefault(x => x.Id == procdoc.DocumentId);
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