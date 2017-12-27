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
    public class ProjectTeamsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProjectTeams
        public IQueryable<ProjectTeam> GetProjectTeams()
        {
            return db.ProjectTeams;
        }

        // GET: api/ProjectTeams/5
        [ResponseType(typeof(ProjectTeam))]
        public async Task<IHttpActionResult> GetProjectTeam(int id)
        {
            ProjectTeam projectTeam = await db.ProjectTeams.FindAsync(id);
            if (projectTeam == null)
            {
                return NotFound();
            }

            return Ok(projectTeam);
        }

        // PUT: api/ProjectTeams/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProjectTeam(int id, ProjectTeam projectTeam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectTeam.ID)
            {
                return BadRequest();
            }

            db.Entry(projectTeam).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectTeamExists(id))
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

        // POST: api/ProjectTeams
        [ResponseType(typeof(ProjectTeam))]
        public async Task<IHttpActionResult> PostProjectTeam(ProjectTeam projectTeam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProjectTeams.Add(projectTeam);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = projectTeam.ID }, projectTeam);
        }

        // DELETE: api/ProjectTeams/5
        [ResponseType(typeof(ProjectTeam))]
        public async Task<IHttpActionResult> DeleteProjectTeam(int id)
        {
            ProjectTeam projectTeam = await db.ProjectTeams.FindAsync(id);
            if (projectTeam == null)
            {
                return NotFound();
            }

            db.ProjectTeams.Remove(projectTeam);
            await db.SaveChangesAsync();

            return Ok(projectTeam);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectTeamExists(int id)
        {
            return db.ProjectTeams.Count(e => e.ID == id) > 0;
        }
    }
}