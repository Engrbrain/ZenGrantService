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
    public class ProjectTemplatesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProjectTemplates
        public IQueryable<ProjectTemplate> GetProjectTemplates()
        {
            return db.ProjectTemplates;
        }

        // GET: api/ProjectTemplates/5
        [ResponseType(typeof(ProjectTemplate))]
        public async Task<IHttpActionResult> GetProjectTemplate(int id)
        {
            ProjectTemplate projectTemplate = await db.ProjectTemplates.FindAsync(id);
            if (projectTemplate == null)
            {
                return NotFound();
            }

            return Ok(projectTemplate);
        }

        // PUT: api/ProjectTemplates/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProjectTemplate(int id, ProjectTemplate projectTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectTemplate.ID)
            {
                return BadRequest();
            }

            db.Entry(projectTemplate).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectTemplateExists(id))
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

        // POST: api/ProjectTemplates
        [ResponseType(typeof(ProjectTemplate))]
        public async Task<IHttpActionResult> PostProjectTemplate(ProjectTemplate projectTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProjectTemplates.Add(projectTemplate);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = projectTemplate.ID }, projectTemplate);
        }

        // DELETE: api/ProjectTemplates/5
        [ResponseType(typeof(ProjectTemplate))]
        public async Task<IHttpActionResult> DeleteProjectTemplate(int id)
        {
            ProjectTemplate projectTemplate = await db.ProjectTemplates.FindAsync(id);
            if (projectTemplate == null)
            {
                return NotFound();
            }

            db.ProjectTemplates.Remove(projectTemplate);
            await db.SaveChangesAsync();

            return Ok(projectTemplate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectTemplateExists(int id)
        {
            return db.ProjectTemplates.Count(e => e.ID == id) > 0;
        }
    }
}