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
    public class AssessorsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Assessors
        public List<Assessor> GetAssessors()
        {
            var assessors = db.Assessors.Include(a => a.Organization).Include(a => a.User);
            return assessors.ToList();
        }

        // GET: api/Assessors/5
        [ResponseType(typeof(Assessor))]
        public async Task<IHttpActionResult> GetAssessor(int id)
        {
            Assessor assessor = await db.Assessors.FindAsync(id);
            if (assessor == null)
            {
                return NotFound();
            }

            return Ok(assessor);
        }
        [Route("GetAssessorSelectList")]
        public List<AssessorSelectModel> GetAssessorSelectList()
        {
            List<AssessorSelectModel> assessorlist = new List<AssessorSelectModel>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var _assessorlist = (from A in db.Assessors
                                    select new AssessorSelectModel
                                    {
                                        ID = A.ID,
                                        AssessorName = A.AssessorName
                                    });
                assessorlist = _assessorlist.ToList();
            }
            return assessorlist;
        }

        // PUT: api/Assessors/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAssessor(int id, Assessor assessor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != assessor.ID)
            {
                return BadRequest();
            }

            db.Entry(assessor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssessorExists(id))
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

        // POST: api/Assessors
        [ResponseType(typeof(Assessor))]
        public async Task<IHttpActionResult> PostAssessor(Assessor assessor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Assessors.Add(assessor);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = assessor.ID }, assessor);
        }

        // DELETE: api/Assessors/5
        [ResponseType(typeof(Assessor))]
        public async Task<IHttpActionResult> DeleteAssessor(int id)
        {
            Assessor assessor = await db.Assessors.FindAsync(id);
            if (assessor == null)
            {
                return NotFound();
            }

            db.Assessors.Remove(assessor);
            await db.SaveChangesAsync();

            return Ok(assessor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssessorExists(int id)
        {
            return db.Assessors.Count(e => e.ID == id) > 0;
        }
    }
}