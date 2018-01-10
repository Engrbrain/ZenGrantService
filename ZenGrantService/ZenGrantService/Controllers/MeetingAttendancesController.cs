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
    public class MeetingAttendancesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MeetingAttendances
        public List<MeetingAttendance> GetMeetingAttendances()
        {
            var meetingAttendances = db.MeetingAttendances.Include(m => m.Organization).Include(m => m.Project).Include(m => m.ProjectMeeting);
            return meetingAttendances.ToList();
        }

        // GET: api/MeetingAttendances/5
        [ResponseType(typeof(MeetingAttendance))]
        public async Task<IHttpActionResult> GetMeetingAttendance(int id)
        {
            MeetingAttendance meetingAttendance = await db.MeetingAttendances.FindAsync(id);
            if (meetingAttendance == null)
            {
                return NotFound();
            }

            return Ok(meetingAttendance);
        }

        // PUT: api/MeetingAttendances/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMeetingAttendance(int id, MeetingAttendance meetingAttendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != meetingAttendance.ID)
            {
                return BadRequest();
            }

            db.Entry(meetingAttendance).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeetingAttendanceExists(id))
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

        // POST: api/MeetingAttendances
        [ResponseType(typeof(MeetingAttendance))]
        public async Task<IHttpActionResult> PostMeetingAttendance(MeetingAttendance meetingAttendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MeetingAttendances.Add(meetingAttendance);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = meetingAttendance.ID }, meetingAttendance);
        }

        // DELETE: api/MeetingAttendances/5
        [ResponseType(typeof(MeetingAttendance))]
        public async Task<IHttpActionResult> DeleteMeetingAttendance(int id)
        {
            MeetingAttendance meetingAttendance = await db.MeetingAttendances.FindAsync(id);
            if (meetingAttendance == null)
            {
                return NotFound();
            }

            db.MeetingAttendances.Remove(meetingAttendance);
            await db.SaveChangesAsync();

            return Ok(meetingAttendance);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MeetingAttendanceExists(int id)
        {
            return db.MeetingAttendances.Count(e => e.ID == id) > 0;
        }
    }
}