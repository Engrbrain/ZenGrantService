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
    public class ActivityRisksController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ActivityRisks
        public IQueryable<ActivityRisk> GetActivityRisks()
        {
            return db.ActivityRisks.Include(a => a.Organization).Include(a => a.ProjectActivity);
            
        }

        // GET: api/ActivityRisks/5
        [ResponseType(typeof(ActivityRisk))]
        public async Task<IHttpActionResult> GetActivityRisk(int id)
        {
            ActivityRisk activityRisk = await db.ActivityRisks.FindAsync(id);
            if (activityRisk == null)
            {
                return NotFound();
            }

            return Ok(activityRisk);
        }

        // PUT: api/ActivityRisks/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutActivityRisk(int id, ActivityRisk activityRisk)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activityRisk.ID)
            {
                return BadRequest();
            }

            db.Entry(activityRisk).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityRiskExists(id))
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

        // POST: api/ActivityRisks
        [ResponseType(typeof(ActivityRisk))]
        public async Task<IHttpActionResult> PostActivityRisk(ActivityRisk activityRisk)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ActivityRisks.Add(activityRisk);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = activityRisk.ID }, activityRisk);
        }

        // DELETE: api/ActivityRisks/5
        [ResponseType(typeof(ActivityRisk))]
        public async Task<IHttpActionResult> DeleteActivityRisk(int id)
        {
            ActivityRisk activityRisk = await db.ActivityRisks.FindAsync(id);
            if (activityRisk == null)
            {
                return NotFound();
            }

            db.ActivityRisks.Remove(activityRisk);
            await db.SaveChangesAsync();

            return Ok(activityRisk);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActivityRiskExists(int id)
        {
            return db.ActivityRisks.Count(e => e.ID == id) > 0;
        }
    }
}