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
    public class CustomApplicationDetailsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CustomApplicationDetails
        public IQueryable<CustomApplicationDetails> GetCustomApplicationDetails()
        {
            return db.CustomApplicationDetails;
        }

        // GET: api/CustomApplicationDetails/5
        [ResponseType(typeof(CustomApplicationDetails))]
        public async Task<IHttpActionResult> GetCustomApplicationDetails(int id)
        {
            CustomApplicationDetails customApplicationDetails = await db.CustomApplicationDetails.FindAsync(id);
            if (customApplicationDetails == null)
            {
                return NotFound();
            }

            return Ok(customApplicationDetails);
        }

        // PUT: api/CustomApplicationDetails/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCustomApplicationDetails(int id, CustomApplicationDetails customApplicationDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customApplicationDetails.ID)
            {
                return BadRequest();
            }

            db.Entry(customApplicationDetails).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomApplicationDetailsExists(id))
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

        // POST: api/CustomApplicationDetails
        [ResponseType(typeof(CustomApplicationDetails))]
        public async Task<IHttpActionResult> PostCustomApplicationDetails(CustomApplicationDetails customApplicationDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CustomApplicationDetails.Add(customApplicationDetails);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = customApplicationDetails.ID }, customApplicationDetails);
        }

        // DELETE: api/CustomApplicationDetails/5
        [ResponseType(typeof(CustomApplicationDetails))]
        public async Task<IHttpActionResult> DeleteCustomApplicationDetails(int id)
        {
            CustomApplicationDetails customApplicationDetails = await db.CustomApplicationDetails.FindAsync(id);
            if (customApplicationDetails == null)
            {
                return NotFound();
            }

            db.CustomApplicationDetails.Remove(customApplicationDetails);
            await db.SaveChangesAsync();

            return Ok(customApplicationDetails);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomApplicationDetailsExists(int id)
        {
            return db.CustomApplicationDetails.Count(e => e.ID == id) > 0;
        }
    }
}