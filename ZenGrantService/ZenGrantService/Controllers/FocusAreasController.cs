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
    public class FocusAreasController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/FocusAreas
        public List<FocusArea> GetFocusAreas()
        {
            var focusAreas = db.FocusAreas.Include(f => f.Organization);
            return focusAreas.ToList();
        }

        // GET: api/FocusAreas/5
        [ResponseType(typeof(FocusArea))]
        public async Task<IHttpActionResult> GetFocusArea(int id)
        {
            FocusArea focusArea = await db.FocusAreas.FindAsync(id);
            if (focusArea == null)
            {
                return NotFound();
            }

            return Ok(focusArea);
        }

        // PUT: api/FocusAreas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFocusArea(int id, FocusArea focusArea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != focusArea.ID)
            {
                return BadRequest();
            }

            db.Entry(focusArea).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FocusAreaExists(id))
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

        // POST: api/FocusAreas
        [ResponseType(typeof(FocusArea))]
        public async Task<IHttpActionResult> PostFocusArea(FocusArea focusArea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FocusAreas.Add(focusArea);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = focusArea.ID }, focusArea);
        }

        // DELETE: api/FocusAreas/5
        [ResponseType(typeof(FocusArea))]
        public async Task<IHttpActionResult> DeleteFocusArea(int id)
        {
            FocusArea focusArea = await db.FocusAreas.FindAsync(id);
            if (focusArea == null)
            {
                return NotFound();
            }

            db.FocusAreas.Remove(focusArea);
            await db.SaveChangesAsync();

            return Ok(focusArea);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FocusAreaExists(int id)
        {
            return db.FocusAreas.Count(e => e.ID == id) > 0;
        }
    }
}