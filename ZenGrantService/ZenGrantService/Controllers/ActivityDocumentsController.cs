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
    public class ActivityDocumentsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ActivityDocuments
        public IQueryable<ActivityDocument> GetActivityDocuments()
        {
            return db.ActivityDocuments;
        }

        // GET: api/ActivityDocuments/5
        [ResponseType(typeof(ActivityDocument))]
        public async Task<IHttpActionResult> GetActivityDocument(int id)
        {
            ActivityDocument activityDocument = await db.ActivityDocuments.FindAsync(id);
            if (activityDocument == null)
            {
                return NotFound();
            }

            return Ok(activityDocument);
        }

        // PUT: api/ActivityDocuments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutActivityDocument(int id, ActivityDocument activityDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activityDocument.ID)
            {
                return BadRequest();
            }

            db.Entry(activityDocument).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityDocumentExists(id))
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

        // POST: api/ActivityDocuments
        [ResponseType(typeof(ActivityDocument))]
        public async Task<IHttpActionResult> PostActivityDocument(ActivityDocument activityDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ActivityDocuments.Add(activityDocument);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = activityDocument.ID }, activityDocument);
        }

        // DELETE: api/ActivityDocuments/5
        [ResponseType(typeof(ActivityDocument))]
        public async Task<IHttpActionResult> DeleteActivityDocument(int id)
        {
            ActivityDocument activityDocument = await db.ActivityDocuments.FindAsync(id);
            if (activityDocument == null)
            {
                return NotFound();
            }

            db.ActivityDocuments.Remove(activityDocument);
            await db.SaveChangesAsync();

            return Ok(activityDocument);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActivityDocumentExists(int id)
        {
            return db.ActivityDocuments.Count(e => e.ID == id) > 0;
        }
    }
}