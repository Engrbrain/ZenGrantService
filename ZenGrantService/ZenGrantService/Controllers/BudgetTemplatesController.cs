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
    public class BudgetTemplatesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/BudgetTemplates
        public List<BudgetTemplate> GetBudgetTemplate()
        {
            var budgetTemplates = db.BudgetTemplate.Include(b => b.Organization);
            return budgetTemplates.ToList();
        }

        // GET: api/BudgetTemplates/5
        [ResponseType(typeof(BudgetTemplate))]
        public async Task<IHttpActionResult> GetBudgetTemplate(int id)
        {
            BudgetTemplate budgetTemplate = await db.BudgetTemplate.FindAsync(id);
            if (budgetTemplate == null)
            {
                return NotFound();
            }

            return Ok(budgetTemplate);
        }

        // PUT: api/BudgetTemplates/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBudgetTemplate(int id, BudgetTemplate budgetTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != budgetTemplate.ID)
            {
                return BadRequest();
            }

            db.Entry(budgetTemplate).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BudgetTemplateExists(id))
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

        // POST: api/BudgetTemplates
        [ResponseType(typeof(BudgetTemplate))]
        public async Task<IHttpActionResult> PostBudgetTemplate(BudgetTemplate budgetTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BudgetTemplate.Add(budgetTemplate);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = budgetTemplate.ID }, budgetTemplate);
        }

        // DELETE: api/BudgetTemplates/5
        [ResponseType(typeof(BudgetTemplate))]
        public async Task<IHttpActionResult> DeleteBudgetTemplate(int id)
        {
            BudgetTemplate budgetTemplate = await db.BudgetTemplate.FindAsync(id);
            if (budgetTemplate == null)
            {
                return NotFound();
            }

            db.BudgetTemplate.Remove(budgetTemplate);
            await db.SaveChangesAsync();

            return Ok(budgetTemplate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BudgetTemplateExists(int id)
        {
            return db.BudgetTemplate.Count(e => e.ID == id) > 0;
        }
    }
}