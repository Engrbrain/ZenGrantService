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
    public class ProjectRisksController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProjectRisks
        public IQueryable<ProjectRisk> GetProjectRisks()
        {
            return db.ProjectRisks.Include(p => p.Organization).Include(p => p.Project).Include(u => u.User);
           
        }

        // GET: api/ProjectRisks/5
        [ResponseType(typeof(ProjectRisk))]
        public async Task<IHttpActionResult> GetProjectRisk(int id)
        {
            ProjectRisk projectRisk = await db.ProjectRisks.FindAsync(id);
            if (projectRisk == null)
            {
                return NotFound();
            }

            return Ok(projectRisk);
        }

        // PUT: api/ProjectRisks/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProjectRisk(int id, ProjectRisk projectRisk)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectRisk.ID)
            {
                return BadRequest();
            }

            db.Entry(projectRisk).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectRiskExists(id))
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

        // POST: api/ProjectRisks
        [ResponseType(typeof(ProjectRisk))]
        public async Task<IHttpActionResult> PostProjectRisk(ProjectRisk projectRisk)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProjectRisks.Add(projectRisk);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = projectRisk.ID }, projectRisk);
        }

        // DELETE: api/ProjectRisks/5
        [ResponseType(typeof(ProjectRisk))]
        public async Task<IHttpActionResult> DeleteProjectRisk(int id)
        {
            ProjectRisk projectRisk = await db.ProjectRisks.FindAsync(id);
            if (projectRisk == null)
            {
                return NotFound();
            }

            db.ProjectRisks.Remove(projectRisk);
            await db.SaveChangesAsync();

            return Ok(projectRisk);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectRiskExists(int id)
        {
            return db.ProjectRisks.Count(e => e.ID == id) > 0;
        }
    }
}