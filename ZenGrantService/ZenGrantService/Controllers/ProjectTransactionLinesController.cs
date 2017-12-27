using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ZenGrantService.Models;

namespace ZenGrantService.Controllers
{
    public class ProjectTransactionLinesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProjectTransactionLines
        public IQueryable<ProjectTransactionLine> GetProjectTransactionLine()
        {
            return db.ProjectTransactionLine;
        }

        // GET: api/ProjectTransactionLines/5
        [ResponseType(typeof(ProjectTransactionLine))]
        public async Task<IHttpActionResult> GetProjectTransactionLine(int id)
        {
            ProjectTransactionLine projectTransactionLine = await db.ProjectTransactionLine.FindAsync(id);
            if (projectTransactionLine == null)
            {
                return NotFound();
            }

            return Ok(projectTransactionLine);
        }

        // PUT: api/ProjectTransactionLines/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProjectTransactionLine(int id, ProjectTransactionLine projectTransactionLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectTransactionLine.ID)
            {
                return BadRequest();
            }

            db.Entry(projectTransactionLine).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectTransactionLineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ProjectTransactionLines
        [ResponseType(typeof(ProjectTransactionLine))]
        public async Task<IHttpActionResult> PostProjectTransactionLine(ProjectTransactionLine projectTransactionLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProjectTransactionLine.Add(projectTransactionLine);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = projectTransactionLine.ID }, projectTransactionLine);
        }

        // DELETE: api/ProjectTransactionLines/5
        [ResponseType(typeof(ProjectTransactionLine))]
        public async Task<IHttpActionResult> DeleteProjectTransactionLine(int id)
        {
            ProjectTransactionLine projectTransactionLine = await db.ProjectTransactionLine.FindAsync(id);
            if (projectTransactionLine == null)
            {
                return NotFound();
            }

            db.ProjectTransactionLine.Remove(projectTransactionLine);
            await db.SaveChangesAsync();

            return Ok(projectTransactionLine);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectTransactionLineExists(int id)
        {
            return db.ProjectTransactionLine.Count(e => e.ID == id) > 0;
        }
    }
}