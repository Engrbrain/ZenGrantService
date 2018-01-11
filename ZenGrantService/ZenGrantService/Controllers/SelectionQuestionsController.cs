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
    public class SelectionQuestionsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SelectionQuestions
        public IQueryable<SelectionQuestion> GetSelectionQuestions()
        {
            return db.SelectionQuestions.Include(s => s.Organization);
        }

        // GET: api/SelectionQuestions/5
        [ResponseType(typeof(SelectionQuestion))]
        public async Task<IHttpActionResult> GetSelectionQuestion(int id)
        {
            SelectionQuestion selectionQuestion = await db.SelectionQuestions.FindAsync(id);
            if (selectionQuestion == null)
            {
                return NotFound();
            }

            return Ok(selectionQuestion);
        }

        [Route("GetSelectionQuestionSelectList")]
        public List<SelectionQuestionSelectModel> GetSelectionQuestionSelectList()
        {
            List<SelectionQuestionSelectModel> selectionQuestionlist = new List<SelectionQuestionSelectModel>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var _selectionQuestionlist = (from SQ in db.SelectionQuestions
                                select new SelectionQuestionSelectModel
                                {
                                    ID = SQ.ID,
                                    Question = SQ.Question
                                });
                selectionQuestionlist = _selectionQuestionlist.ToList();
            }
            return selectionQuestionlist;
        }

        // PUT: api/SelectionQuestions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSelectionQuestion(int id, SelectionQuestion selectionQuestion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != selectionQuestion.ID)
            {
                return BadRequest();
            }

            db.Entry(selectionQuestion).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SelectionQuestionExists(id))
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

        // POST: api/SelectionQuestions
        [ResponseType(typeof(SelectionQuestion))]
        public async Task<IHttpActionResult> PostSelectionQuestion(SelectionQuestion selectionQuestion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SelectionQuestions.Add(selectionQuestion);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = selectionQuestion.ID }, selectionQuestion);
        }

        // DELETE: api/SelectionQuestions/5
        [ResponseType(typeof(SelectionQuestion))]
        public async Task<IHttpActionResult> DeleteSelectionQuestion(int id)
        {
            SelectionQuestion selectionQuestion = await db.SelectionQuestions.FindAsync(id);
            if (selectionQuestion == null)
            {
                return NotFound();
            }

            db.SelectionQuestions.Remove(selectionQuestion);
            await db.SaveChangesAsync();

            return Ok(selectionQuestion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SelectionQuestionExists(int id)
        {
            return db.SelectionQuestions.Count(e => e.ID == id) > 0;
        }
    }
}