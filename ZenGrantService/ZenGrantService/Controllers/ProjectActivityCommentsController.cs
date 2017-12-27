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
    public class ProjectActivityCommentsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProjectActivityComments
        public IQueryable<ProjectActivityComment> GetProjectActivityComments()
        {
            return db.ProjectActivityComments;
        }

        // GET: api/ProjectActivityComments/5
        [ResponseType(typeof(ProjectActivityComment))]
        public async Task<IHttpActionResult> GetProjectActivityComment(int id)
        {
            ProjectActivityComment projectActivityComment = await db.ProjectActivityComments.FindAsync(id);
            if (projectActivityComment == null)
            {
                return NotFound();
            }

            return Ok(projectActivityComment);
        }

        // PUT: api/ProjectActivityComments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProjectActivityComment(int id, ProjectActivityComment projectActivityComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectActivityComment.ID)
            {
                return BadRequest();
            }

            db.Entry(projectActivityComment).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectActivityCommentExists(id))
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

        // POST: api/ProjectActivityComments
        [ResponseType(typeof(ProjectActivityComment))]
        public async Task<IHttpActionResult> PostProjectActivityComment(ProjectActivityComment projectActivityComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProjectActivityComments.Add(projectActivityComment);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = projectActivityComment.ID }, projectActivityComment);
        }

        // DELETE: api/ProjectActivityComments/5
        [ResponseType(typeof(ProjectActivityComment))]
        public async Task<IHttpActionResult> DeleteProjectActivityComment(int id)
        {
            ProjectActivityComment projectActivityComment = await db.ProjectActivityComments.FindAsync(id);
            if (projectActivityComment == null)
            {
                return NotFound();
            }

            db.ProjectActivityComments.Remove(projectActivityComment);
            await db.SaveChangesAsync();

            return Ok(projectActivityComment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectActivityCommentExists(int id)
        {
            return db.ProjectActivityComments.Count(e => e.ID == id) > 0;
        }
    }
}