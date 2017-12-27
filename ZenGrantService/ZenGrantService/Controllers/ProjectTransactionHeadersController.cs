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
    public class ProjectTransactionHeadersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProjectTransactionHeaders
        public IQueryable<ProjectTransactionHeader> GetProjectTransactionHeaders()
        {
            return db.ProjectTransactionHeader;
        }

        // GET: api/ProjectTransactionHeaders/5
        [ResponseType(typeof(ProjectTransactionHeader))]
        public async Task<IHttpActionResult> GetProjectTransactionHeader(int id)
        {
            ProjectTransactionHeader projectTransactionHeader = await db.ProjectTransactionHeader.FindAsync(id);
            if (projectTransactionHeader == null)
            {
                return NotFound();
            }

            return Ok(projectTransactionHeader);
        }

        // PUT: api/ProjectTransactionHeaders/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProjectTransactionHeader(int id, ProjectTransactionHeader projectTransactionHeader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectTransactionHeader.ID)
            {
                return BadRequest();
            }

            db.Entry(projectTransactionHeader).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectTransactionHeaderExists(id))
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

        // POST: api/ProjectTransactionHeaders
        [ResponseType(typeof(ProjectTransactionHeader))]
        public async Task<IHttpActionResult> PostProjectTransactionHeader(ProjectTransactionHeader projectTransactionHeader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProjectTransactionHeader.Add(projectTransactionHeader);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = projectTransactionHeader.ID }, projectTransactionHeader);
        }

        // DELETE: api/ProjectTransactionHeaders/5
        [ResponseType(typeof(ProjectTransactionHeader))]
        public async Task<IHttpActionResult> DeleteProjectTransactionHeader(int id)
        {
            ProjectTransactionHeader projectTransactionHeader = await db.ProjectTransactionHeader.FindAsync(id);
            if (projectTransactionHeader == null)
            {
                return NotFound();
            }

            db.ProjectTransactionHeader.Remove(projectTransactionHeader);
            await db.SaveChangesAsync();

            return Ok(projectTransactionHeader);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectTransactionHeaderExists(int id)
        {
            return db.ProjectTransactionHeader.Count(e => e.ID == id) > 0;
        }
    }
}