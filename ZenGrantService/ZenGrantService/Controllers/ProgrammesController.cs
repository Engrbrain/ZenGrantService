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
    public class ProgrammesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Programmes
        public IQueryable<Programme> GetProgrammes()
        {
            return db.Programmes.Include(p => p.Organization).Include(a => a.User);
            
        }

        // GET: api/Programmes/5
        [ResponseType(typeof(Programme))]
        public async Task<IHttpActionResult> GetProgramme(int id)
        {
            Programme programme = await db.Programmes.FindAsync(id);
            if (programme == null)
            {
                return NotFound();
            }

            return Ok(programme);
        }

        [Route("GetProgrammeSelectList")]
        public List<ProgrammeSelectModel> GetProgrammeSelectList()
        {
            List<ProgrammeSelectModel> proglist = new List<ProgrammeSelectModel>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var _proglist = (from O in db.Programmes
                                select new ProgrammeSelectModel
                                {
                                    ID = O.ID,
                                    ProgrammeName = O.ProgrammeName
                                });
                proglist = _proglist.ToList();
            }
            return proglist;
        }

        // PUT: api/Programmes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProgramme(int id, Programme programme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != programme.ID)
            {
                return BadRequest();
            }

            db.Entry(programme).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgrammeExists(id))
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

        // POST: api/Programmes
        [ResponseType(typeof(Programme))]
        public async Task<IHttpActionResult> PostProgramme(Programme programme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Programmes.Add(programme);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = programme.ID }, programme);
        }

        // DELETE: api/Programmes/5
        [ResponseType(typeof(Programme))]
        public async Task<IHttpActionResult> DeleteProgramme(int id)
        {
            Programme programme = await db.Programmes.FindAsync(id);
            if (programme == null)
            {
                return NotFound();
            }

            db.Programmes.Remove(programme);
            await db.SaveChangesAsync();

            return Ok(programme);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProgrammeExists(int id)
        {
            return db.Programmes.Count(e => e.ID == id) > 0;
        }
    }
}