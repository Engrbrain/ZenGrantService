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
    public class ProposalTemplatesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProposalTemplates
        public IQueryable<ProposalTemplate> GetProposalTemplates()
        {
            return db.ProposalTemplates;
        }

        // GET: api/ProposalTemplates/5
        [ResponseType(typeof(ProposalTemplate))]
        public async Task<IHttpActionResult> GetProposalTemplate(int id)
        {
            ProposalTemplate proposalTemplate = await db.ProposalTemplates.FindAsync(id);
            if (proposalTemplate == null)
            {
                return NotFound();
            }

            return Ok(proposalTemplate);
        }

        [Route("GetProposalTemplateSelectList")]
        public List<ProposalTemplateSelectModel> GetProposalTemplateSelectList()
        {
            List<ProposalTemplateSelectModel> propTemplist = new List<ProposalTemplateSelectModel>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var _propTemplist = (from O in db.ProposalTemplates
                                select new ProposalTemplateSelectModel
                                {
                                    ID = O.ID,
                                    FieldLabel = O.FieldLabel
                                });
                propTemplist = _propTemplist.ToList();
            }
            return propTemplist;
        }

        // PUT: api/ProposalTemplates/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProposalTemplate(int id, ProposalTemplate proposalTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != proposalTemplate.ID)
            {
                return BadRequest();
            }

            db.Entry(proposalTemplate).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProposalTemplateExists(id))
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

        // POST: api/ProposalTemplates
        [ResponseType(typeof(ProposalTemplate))]
        public async Task<IHttpActionResult> PostProposalTemplate(ProposalTemplate proposalTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProposalTemplates.Add(proposalTemplate);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = proposalTemplate.ID }, proposalTemplate);
        }

        // DELETE: api/ProposalTemplates/5
        [ResponseType(typeof(ProposalTemplate))]
        public async Task<IHttpActionResult> DeleteProposalTemplate(int id)
        {
            ProposalTemplate proposalTemplate = await db.ProposalTemplates.FindAsync(id);
            if (proposalTemplate == null)
            {
                return NotFound();
            }

            db.ProposalTemplates.Remove(proposalTemplate);
            await db.SaveChangesAsync();

            return Ok(proposalTemplate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProposalTemplateExists(int id)
        {
            return db.ProposalTemplates.Count(e => e.ID == id) > 0;
        }
    }
}