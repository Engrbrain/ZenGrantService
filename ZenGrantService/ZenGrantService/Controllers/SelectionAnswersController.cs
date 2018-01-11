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
    public class SelectionAnswersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SelectionAnswers
        public IQueryable<SelectionAnswer> GetSelectionAnswer()
        {

            return db.SelectionAnswer.Include(s => s.Assessor).Include(s => s.Organization).Include(s => s.ProgApplication).Include(s => s.SelectionQuestion);
        }

        // GET: api/SelectionAnswers/5
        [ResponseType(typeof(SelectionAnswer))]
        public async Task<IHttpActionResult> GetSelectionAnswer(int id)
        {
            SelectionAnswer selectionAnswer = await db.SelectionAnswer.FindAsync(id);
            if (selectionAnswer == null)
            {
                return NotFound();
            }

            return Ok(selectionAnswer);
        }

        // PUT: api/SelectionAnswers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSelectionAnswer(int id, SelectionAnswer selectionAnswer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != selectionAnswer.ID)
            {
                return BadRequest();
            }

            db.Entry(selectionAnswer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SelectionAnswerExists(id))
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

        // POST: api/SelectionAnswers
        [ResponseType(typeof(SelectionAnswer))]
        public async Task<IHttpActionResult> PostSelectionAnswer(SelectionAnswer selectionAnswer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SelectionAnswer.Add(selectionAnswer);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = selectionAnswer.ID }, selectionAnswer);
        }

        // DELETE: api/SelectionAnswers/5
        [ResponseType(typeof(SelectionAnswer))]
        public async Task<IHttpActionResult> DeleteSelectionAnswer(int id)
        {
            SelectionAnswer selectionAnswer = await db.SelectionAnswer.FindAsync(id);
            if (selectionAnswer == null)
            {
                return NotFound();
            }

            db.SelectionAnswer.Remove(selectionAnswer);
            await db.SaveChangesAsync();

            return Ok(selectionAnswer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SelectionAnswerExists(int id)
        {
            return db.SelectionAnswer.Count(e => e.ID == id) > 0;
        }
    }
}