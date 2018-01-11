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
    public class ProjectBudgetsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProjectBudgets
        public IQueryable<ProjectBudget> GetProjectBudget()
        {
            return  db.ProjectBudget.Include(p => p.Organization).Include(p => p.Project);
        }

        // GET: api/ProjectBudgets/5
        [ResponseType(typeof(ProjectBudget))]
        public async Task<IHttpActionResult> GetProjectBudget(int id)
        {
            ProjectBudget projectBudget = await db.ProjectBudget.FindAsync(id);
            if (projectBudget == null)
            {
                return NotFound();
            }

            return Ok(projectBudget);
        }

        // PUT: api/ProjectBudgets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProjectBudget(int id, ProjectBudget projectBudget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectBudget.ID)
            {
                return BadRequest();
            }

            db.Entry(projectBudget).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectBudgetExists(id))
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

        // POST: api/ProjectBudgets
        [ResponseType(typeof(ProjectBudget))]
        public async Task<IHttpActionResult> PostProjectBudget(ProjectBudget projectBudget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProjectBudget.Add(projectBudget);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = projectBudget.ID }, projectBudget);
        }

        // DELETE: api/ProjectBudgets/5
        [ResponseType(typeof(ProjectBudget))]
        public async Task<IHttpActionResult> DeleteProjectBudget(int id)
        {
            ProjectBudget projectBudget = await db.ProjectBudget.FindAsync(id);
            if (projectBudget == null)
            {
                return NotFound();
            }

            db.ProjectBudget.Remove(projectBudget);
            await db.SaveChangesAsync();

            return Ok(projectBudget);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectBudgetExists(int id)
        {
            return db.ProjectBudget.Count(e => e.ID == id) > 0;
        }
    }
}