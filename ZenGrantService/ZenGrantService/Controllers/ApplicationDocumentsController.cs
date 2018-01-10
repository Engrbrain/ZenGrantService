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
    public class ApplicationDocumentsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ApplicationDocuments
        public List<ApplicationDocument> GetApplicationDocument()
        {
            var applicationDocuments = db.ApplicationDocument.Include(a => a.Organization).Include(a => a.ProgApplication);
            return applicationDocuments.ToList();
        }

        // GET: api/ApplicationDocuments/5
        [ResponseType(typeof(ApplicationDocument))]
        public async Task<IHttpActionResult> GetApplicationDocument(int id)
        {
            ApplicationDocument applicationDocument = await db.ApplicationDocument.FindAsync(id);
            if (applicationDocument == null)
            {
                return NotFound();
            }

            return Ok(applicationDocument);
        }

        // PUT: api/ApplicationDocuments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutApplicationDocument(int id, ApplicationDocument applicationDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != applicationDocument.ID)
            {
                return BadRequest();
            }

            db.Entry(applicationDocument).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationDocumentExists(id))
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

        // POST: api/ApplicationDocuments
        [ResponseType(typeof(ApplicationDocument))]
        public async Task<IHttpActionResult> PostApplicationDocument(ApplicationDocument applicationDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ApplicationDocument.Add(applicationDocument);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = applicationDocument.ID }, applicationDocument);
        }

        // DELETE: api/ApplicationDocuments/5
        [ResponseType(typeof(ApplicationDocument))]
        public async Task<IHttpActionResult> DeleteApplicationDocument(int id)
        {
            ApplicationDocument applicationDocument = await db.ApplicationDocument.FindAsync(id);
            if (applicationDocument == null)
            {
                return NotFound();
            }

            db.ApplicationDocument.Remove(applicationDocument);
            await db.SaveChangesAsync();

            return Ok(applicationDocument);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplicationDocumentExists(int id)
        {
            return db.ApplicationDocument.Count(e => e.ID == id) > 0;
        }
    }
}