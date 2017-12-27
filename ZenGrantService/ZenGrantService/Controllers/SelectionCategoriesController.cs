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
    public class SelectionCategoriesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SelectionCategories
        public IQueryable<SelectionCategory> GetSelectionCategory()
        {
            return db.SelectionCategory;
        }

        // GET: api/SelectionCategories/5
        [ResponseType(typeof(SelectionCategory))]
        public async Task<IHttpActionResult> GetSelectionCategory(int id)
        {
            SelectionCategory selectionCategory = await db.SelectionCategory.FindAsync(id);
            if (selectionCategory == null)
            {
                return NotFound();
            }

            return Ok(selectionCategory);
        }

        // PUT: api/SelectionCategories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSelectionCategory(int id, SelectionCategory selectionCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != selectionCategory.ID)
            {
                return BadRequest();
            }

            db.Entry(selectionCategory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SelectionCategoryExists(id))
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

        // POST: api/SelectionCategories
        [ResponseType(typeof(SelectionCategory))]
        public async Task<IHttpActionResult> PostSelectionCategory(SelectionCategory selectionCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SelectionCategory.Add(selectionCategory);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = selectionCategory.ID }, selectionCategory);
        }

        // DELETE: api/SelectionCategories/5
        [ResponseType(typeof(SelectionCategory))]
        public async Task<IHttpActionResult> DeleteSelectionCategory(int id)
        {
            SelectionCategory selectionCategory = await db.SelectionCategory.FindAsync(id);
            if (selectionCategory == null)
            {
                return NotFound();
            }

            db.SelectionCategory.Remove(selectionCategory);
            await db.SaveChangesAsync();

            return Ok(selectionCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SelectionCategoryExists(int id)
        {
            return db.SelectionCategory.Count(e => e.ID == id) > 0;
        }
    }
}