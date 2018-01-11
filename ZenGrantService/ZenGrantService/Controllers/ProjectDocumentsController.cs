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
    public class ProjectDocumentsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProjectDocuments
        public IQueryable<ProjectDocument> GetProjectDocuments()
        {
          return db.ProjectDocument.Include(p => p.Organization).Include(p => p.Project).Include(u => u.User);
          
        }

        // GET: api/ProjectDocuments/5
        [ResponseType(typeof(ProjectDocument))]
        public async Task<IHttpActionResult> GetProjectDocument(int id)
        {
            ProjectDocument projectDocument = await db.ProjectDocument.FindAsync(id);
            if (projectDocument == null)
            {
                return NotFound();
            }

            return Ok(projectDocument);
        }

        // PUT: api/ProjectDocuments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProjectDocument(int id, ProjectDocument projectDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectDocument.ID)
            {
                return BadRequest();
            }

            db.Entry(projectDocument).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectDocumentExists(id))
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

        // POST: api/ProjectDocuments
        [ResponseType(typeof(ProjectDocument))]
        public async Task<IHttpActionResult> PostProjectDocument(ProjectDocument projectDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProjectDocument.Add(projectDocument);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = projectDocument.ID }, projectDocument);
        }

        // DELETE: api/ProjectDocuments/5
        [ResponseType(typeof(ProjectDocument))]
        public async Task<IHttpActionResult> DeleteProjectDocument(int id)
        {
            ProjectDocument projectDocument = await db.ProjectDocument.FindAsync(id);
            if (projectDocument == null)
            {
                return NotFound();
            }

            db.ProjectDocument.Remove(projectDocument);
            await db.SaveChangesAsync();

            return Ok(projectDocument);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectDocumentExists(int id)
        {
            return db.ProjectDocument.Count(e => e.ID == id) > 0;
        }
    }
}