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
    public class ProjectCommentsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProjectComments
        public IQueryable<ProjectComment> GetProjectComments()
        {
           return db.ProjectComments.Include(p => p.Organization).Include(p => p.Project);
            
        }

        // GET: api/ProjectComments/5
        [ResponseType(typeof(ProjectComment))]
        public async Task<IHttpActionResult> GetProjectComment(int id)
        {
            ProjectComment projectComment = await db.ProjectComments.FindAsync(id);
            if (projectComment == null)
            {
                return NotFound();
            }

            return Ok(projectComment);
        }

        // PUT: api/ProjectComments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProjectComment(int id, ProjectComment projectComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectComment.ID)
            {
                return BadRequest();
            }

            db.Entry(projectComment).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectCommentExists(id))
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

        // POST: api/ProjectComments
        [ResponseType(typeof(ProjectComment))]
        public async Task<IHttpActionResult> PostProjectComment(ProjectComment projectComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProjectComments.Add(projectComment);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = projectComment.ID }, projectComment);
        }

        // DELETE: api/ProjectComments/5
        [ResponseType(typeof(ProjectComment))]
        public async Task<IHttpActionResult> DeleteProjectComment(int id)
        {
            ProjectComment projectComment = await db.ProjectComments.FindAsync(id);
            if (projectComment == null)
            {
                return NotFound();
            }

            db.ProjectComments.Remove(projectComment);
            await db.SaveChangesAsync();

            return Ok(projectComment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectCommentExists(int id)
        {
            return db.ProjectComments.Count(e => e.ID == id) > 0;
        }
    }
}