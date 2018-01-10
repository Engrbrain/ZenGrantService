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
    public class ProjectActivitiesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProjectActivities
        public IQueryable<ProjectActivity> GetProjectActivity()
        {
            return db.ProjectActivity;
        }

        [Route("GetProActSelectList")]
        public List<ProjectActivitySelectModel> GetProActSelectList()
        {
            List<ProjectActivitySelectModel> proAct = new List<ProjectActivitySelectModel>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var _proAct = (from P in db.ProjectActivity
                               select new ProjectActivitySelectModel
                               {
                                    ID = P.ID,
                                   ActivityTitle = P.ActivityTitle
                                });
                proAct = _proAct.ToList();

            }
            return proAct;
        }

        // GET: api/ProjectActivities/5
        [ResponseType(typeof(ProjectActivity))]
        public async Task<IHttpActionResult> GetProjectActivity(int id)
        {
            ProjectActivity projectActivity = await db.ProjectActivity.FindAsync(id);
            if (projectActivity == null)
            {
                return NotFound();
            }

            return Ok(projectActivity);
        }

        // PUT: api/ProjectActivities/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProjectActivity(int id, ProjectActivity projectActivity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectActivity.ID)
            {
                return BadRequest();
            }

            db.Entry(projectActivity).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectActivityExists(id))
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

        // POST: api/ProjectActivities
        [ResponseType(typeof(ProjectActivity))]
        public async Task<IHttpActionResult> PostProjectActivity(ProjectActivity projectActivity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProjectActivity.Add(projectActivity);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = projectActivity.ID }, projectActivity);
        }

        // DELETE: api/ProjectActivities/5
        [ResponseType(typeof(ProjectActivity))]
        public async Task<IHttpActionResult> DeleteProjectActivity(int id)
        {
            ProjectActivity projectActivity = await db.ProjectActivity.FindAsync(id);
            if (projectActivity == null)
            {
                return NotFound();
            }

            db.ProjectActivity.Remove(projectActivity);
            await db.SaveChangesAsync();

            return Ok(projectActivity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectActivityExists(int id)
        {
            return db.ProjectActivity.Count(e => e.ID == id) > 0;
        }
    }
}