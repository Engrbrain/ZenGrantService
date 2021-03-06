﻿using System;
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
    public class ProgApplicationsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProgApplications
        public IQueryable<ProgApplication> GetProgApplication()
        {
            return db.ProgApplication.Include(p => p.Assessor).Include(p => p.Organization).Include(p => p.Programme);
           
        }

        // GET: api/ProgApplications/5
        [ResponseType(typeof(ProgApplication))]
        public async Task<IHttpActionResult> GetProgApplication(int id)
        {
            ProgApplication progApplication = await db.ProgApplication.FindAsync(id);
            if (progApplication == null)
            {
                return NotFound();
            }

            return Ok(progApplication);
        }

        [Route("GetProgApplicationSelectList")]
        public List<ProgApplicationSelectModel> GetProgApplicationSelectList()
        {
            List<ProgApplicationSelectModel> progApplist = new List<ProgApplicationSelectModel>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var _progApplist = (from P in db.ProgApplication
                                select new ProgApplicationSelectModel
                                {
                                    ID = P.ID,
                                    ApplicantName = P.ApplicantName,
                                    ApplicantReference = P.ApplicantReference
                                });
                progApplist = _progApplist.ToList();
            }
            return progApplist;
        }
        // PUT: api/ProgApplications/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProgApplication(int id, ProgApplication progApplication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != progApplication.ID)
            {
                return BadRequest();
            }

            db.Entry(progApplication).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgApplicationExists(id))
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

        // POST: api/ProgApplications
        [ResponseType(typeof(ProgApplication))]
        public async Task<IHttpActionResult> PostProgApplication(ProgApplication progApplication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProgApplication.Add(progApplication);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = progApplication.ID }, progApplication);
        }

        // DELETE: api/ProgApplications/5
        [ResponseType(typeof(ProgApplication))]
        public async Task<IHttpActionResult> DeleteProgApplication(int id)
        {
            ProgApplication progApplication = await db.ProgApplication.FindAsync(id);
            if (progApplication == null)
            {
                return NotFound();
            }

            db.ProgApplication.Remove(progApplication);
            await db.SaveChangesAsync();

            return Ok(progApplication);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProgApplicationExists(int id)
        {
            return db.ProgApplication.Count(e => e.ID == id) > 0;
        }
    }
}