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
    public class ProjectMeetingsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProjectMeetings
        public IQueryable<ProjectMeeting> GetProjectMeetings()
        {
            return db.ProjectMeetings;
        }

        // GET: api/ProjectMeetings/5
        [ResponseType(typeof(ProjectMeeting))]
        public async Task<IHttpActionResult> GetProjectMeeting(int id)
        {
            ProjectMeeting projectMeeting = await db.ProjectMeetings.FindAsync(id);
            if (projectMeeting == null)
            {
                return NotFound();
            }

            return Ok(projectMeeting);
        }

        // PUT: api/ProjectMeetings/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProjectMeeting(int id, ProjectMeeting projectMeeting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectMeeting.ID)
            {
                return BadRequest();
            }

            db.Entry(projectMeeting).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectMeetingExists(id))
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

        // POST: api/ProjectMeetings
        [ResponseType(typeof(ProjectMeeting))]
        public async Task<IHttpActionResult> PostProjectMeeting(ProjectMeeting projectMeeting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProjectMeetings.Add(projectMeeting);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = projectMeeting.ID }, projectMeeting);
        }

        // DELETE: api/ProjectMeetings/5
        [ResponseType(typeof(ProjectMeeting))]
        public async Task<IHttpActionResult> DeleteProjectMeeting(int id)
        {
            ProjectMeeting projectMeeting = await db.ProjectMeetings.FindAsync(id);
            if (projectMeeting == null)
            {
                return NotFound();
            }

            db.ProjectMeetings.Remove(projectMeeting);
            await db.SaveChangesAsync();

            return Ok(projectMeeting);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectMeetingExists(int id)
        {
            return db.ProjectMeetings.Count(e => e.ID == id) > 0;
        }
    }
}