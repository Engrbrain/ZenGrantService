using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ZenGrantService.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public enumManager.State State { get; set; }
        public enumManager.country Nationality { get; set; }
        public enumManager.Gender Gender { get; set; }
        public string UserSummary { get; set; }
        public string JobDesignation { get; set; }
        public byte[] UserImage { get; set; }
        public enumManager.Scope scope { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime TimeStamp { get; set; }



        public ApplicationUser()
        {
            TimeStamp = DateTime.Now;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Configuration.LazyLoadingEnabled = false;
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<ZenGrantService.Models.Organization> Organizations { get; set; }
        public DbSet<ZenGrantService.Models.ActivityDocument> ActivityDocuments { get; set; }
        public DbSet<ZenGrantService.Models.ActivityRisk> ActivityRisks { get; set; }
        public DbSet<ZenGrantService.Models.ApplicationDocument> ApplicationDocument { get; set; }
        public DbSet<ZenGrantService.Models.Assessor> Assessors { get; set; }
        public DbSet<ZenGrantService.Models.BudgetTemplate> BudgetTemplate { get; set; }
        public DbSet<ZenGrantService.Models.CustomApplicationDetails> CustomApplicationDetails { get; set; }
        public DbSet<ZenGrantService.Models.FocusArea> FocusAreas { get; set; }
        public DbSet<ZenGrantService.Models.MeetingAttendance> MeetingAttendances { get; set; }
        public DbSet<ZenGrantService.Models.ProgApplication> ProgApplication { get; set; }
        public DbSet<ZenGrantService.Models.Programme> Programmes { get; set; }
        public DbSet<ZenGrantService.Models.Project> Project { get; set; }
        public DbSet<ZenGrantService.Models.ProjectActivity> ProjectActivity { get; set; }
        public DbSet<ZenGrantService.Models.ProjectActivityComment> ProjectActivityComments { get; set; }
        public DbSet<ZenGrantService.Models.ProjectBudget> ProjectBudget { get; set; }
        public DbSet<ZenGrantService.Models.ProjectComment> ProjectComments { get; set; }
        public DbSet<ZenGrantService.Models.ProjectDocument> ProjectDocument { get; set; }
        public DbSet<ZenGrantService.Models.ProjectMeeting> ProjectMeetings { get; set; }
        public DbSet<ZenGrantService.Models.ProjectRisk> ProjectRisks { get; set; }
        public DbSet<ZenGrantService.Models.ProjectTeam> ProjectTeams { get; set; }
        public DbSet<ZenGrantService.Models.ProjectTemplate> ProjectTemplates { get; set; }
        public DbSet<ZenGrantService.Models.ProjectTransactionHeader> ProjectTransactionHeader { get; set; }
        public DbSet<ZenGrantService.Models.ProjectTransactionLine> ProjectTransactionLine { get; set; }
        public DbSet<ZenGrantService.Models.ProposalTemplate> ProposalTemplates { get; set; }
        public DbSet<ZenGrantService.Models.Renewal> Renewals { get; set; }
        public DbSet<ZenGrantService.Models.SelectionAnswer> SelectionAnswer { get; set; }
        public DbSet<ZenGrantService.Models.SelectionCategory> SelectionCategory { get; set; }
        public DbSet<ZenGrantService.Models.SelectionQuestion> SelectionQuestions { get; set; }
        public DbSet<ZenGrantService.Models.Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));

         modelBuilder.Entity<ProjectActivity>().
              HasOptional(e => e.Dependency).
              WithMany().
              HasForeignKey(m => m.DependencyID);

         modelBuilder.Entity<ProjectActivity>()
                .HasRequired(d => d.Organization)
                .WithMany(w => w.ProjectActivity)
                .HasForeignKey(d => d.OrganizationID)
                .WillCascadeOnDelete(false);

         modelBuilder.Entity<Project>()
                .HasRequired(d => d.Organization)
                .WithMany(w => w.Project)
                .HasForeignKey(d => d.OrganizationID)
                .WillCascadeOnDelete(false);

         modelBuilder.Entity<ActivityDocument>()
                .HasRequired(d => d.Organization)
                .WithMany(w => w.ActivityDocument)
                .HasForeignKey(d => d.OrganizationID)
                .WillCascadeOnDelete(false);

         modelBuilder.Entity<ActivityRisk>()
                .HasRequired(d => d.Organization)
                .WithMany(w => w.ActivityRisk)
                .HasForeignKey(d => d.OrganizationID)
                .WillCascadeOnDelete(false);

         modelBuilder.Entity<ApplicationDocument>()
                .HasRequired(d => d.Organization) 
                .WithMany(w => w.ApplicationDocument) 
                .HasForeignKey(d => d.OrganizationID) 
                .WillCascadeOnDelete(false);

         modelBuilder.Entity<CustomApplicationDetails>()
               .HasRequired(d => d.Organization)
               .WithMany(w => w.CustomApplicationDetails)
               .HasForeignKey(d => d.OrganizationID)
               .WillCascadeOnDelete(false);

         modelBuilder.Entity<MeetingAttendance>()
              .HasRequired(d => d.Organization)
              .WithMany(w => w.MeetingAttendance)
              .HasForeignKey(d => d.OrganizationID)
              .WillCascadeOnDelete(false);

         modelBuilder.Entity<MeetingAttendance>()
             .HasRequired(d => d.Project)
             .WithMany(w => w.MeetingAttendance)
             .HasForeignKey(d => d.ProjectID)
             .WillCascadeOnDelete(false);


         modelBuilder.Entity<ProgApplication>()
           .HasRequired(d => d.Organization)
           .WithMany(w => w.ProgApplication)
           .HasForeignKey(d => d.OrganizationID)
           .WillCascadeOnDelete(false);

        modelBuilder.Entity<Project>()
        .HasRequired(d => d.Organization)
        .WithMany(w => w.Project)
        .HasForeignKey(d => d.OrganizationID)
        .WillCascadeOnDelete(false);

       modelBuilder.Entity<ProjectActivity>()
          .HasRequired(d => d.Organization)
          .WithMany(w => w.ProjectActivity)
          .HasForeignKey(d => d.OrganizationID)
          .WillCascadeOnDelete(false);

       modelBuilder.Entity<ProjectActivityComment>()
         .HasRequired(d => d.Organization)
         .WithMany(w => w.ProjectActivityComment)
         .HasForeignKey(d => d.OrganizationID)
         .WillCascadeOnDelete(false);

       modelBuilder.Entity<ProjectBudget>()
         .HasRequired(d => d.Organization)
         .WithMany(w => w.ProjectBudget)
         .HasForeignKey(d => d.OrganizationID)
         .WillCascadeOnDelete(false);

      modelBuilder.Entity<ProjectComment>()
        .HasRequired(d => d.Organization)
        .WithMany(w => w.ProjectComment)
        .HasForeignKey(d => d.OrganizationID)
        .WillCascadeOnDelete(false);

      modelBuilder.Entity<ProjectDocument>()
        .HasRequired(d => d.Organization)
        .WithMany(w => w.ProjectDocument)
        .HasForeignKey(d => d.OrganizationID)
        .WillCascadeOnDelete(false);

      modelBuilder.Entity<ProjectMeeting>()
        .HasRequired(d => d.Organization)
        .WithMany(w => w.ProjectMeeting)
        .HasForeignKey(d => d.OrganizationID)
        .WillCascadeOnDelete(false);

      modelBuilder.Entity<ProjectRisk>()
       .HasRequired(d => d.Organization)
       .WithMany(w => w.ProjectRisk)
       .HasForeignKey(d => d.OrganizationID)
       .WillCascadeOnDelete(false);

      modelBuilder.Entity<ProjectRisk>()
       .HasRequired(d => d.Organization)
       .WithMany(w => w.ProjectRisk)
       .HasForeignKey(d => d.OrganizationID)
       .WillCascadeOnDelete(false);

      modelBuilder.Entity<ProjectTeam>()
      .HasRequired(d => d.Organization)
      .WithMany(w => w.ProjectTeam)
      .HasForeignKey(d => d.OrganizationID)
      .WillCascadeOnDelete(false);

    modelBuilder.Entity<ProjectTransactionHeader>()
     .HasRequired(d => d.Organization)
     .WithMany(w => w.ProjectTransactionHeader)
     .HasForeignKey(d => d.OrganizationID)
     .WillCascadeOnDelete(false);

   modelBuilder.Entity<ProjectTransactionHeader>()
     .HasRequired(d => d.Project)
     .WithMany(w => w.ProjectTransactionHeader)
     .HasForeignKey(d => d.ProjectID)
     .WillCascadeOnDelete(false);

     modelBuilder.Entity<ProjectTransactionLine>()
     .HasRequired(d => d.Organization)
     .WithMany(w => w.ProjectTransactionLine)
     .HasForeignKey(d => d.OrganizationID)
     .WillCascadeOnDelete(false);

     modelBuilder.Entity<SelectionAnswer>()
     .HasRequired(d => d.Organization)
     .WithMany(w => w.SelectionAnswer)
     .HasForeignKey(d => d.OrganizationID)
     .WillCascadeOnDelete(false);

     modelBuilder.Entity<SelectionQuestion>()
     .HasRequired(d => d.Organization)
     .WithMany(w => w.SelectionQuestion)
     .HasForeignKey(d => d.OrganizationID)
     .WillCascadeOnDelete(false);

    modelBuilder.Entity<SelectionCategory>()
     .HasRequired(d => d.Organization)
     .WithMany(w => w.SelectionCategory)
     .HasForeignKey(d => d.OrganizationID)
     .WillCascadeOnDelete(false);

   modelBuilder.Entity<ProgApplication>()
     .HasRequired(d => d.Programme)
     .WithMany(w => w.ProgApplication)
     .HasForeignKey(d => d.ProgrammeID)
     .WillCascadeOnDelete(false);

  modelBuilder.Entity<CustomApplicationDetails>()
     .HasRequired(d => d.ProposalTemplate)
     .WithMany(w => w.CustomApplicationDetails)
     .HasForeignKey(d => d.ProposalTemplateID)
     .WillCascadeOnDelete(false);

  modelBuilder.Entity<ProposalTemplate>()
     .HasRequired(d => d.Organization)
     .WithMany(w => w.ProposalTemplate)
     .HasForeignKey(d => d.OrganizationID)
     .WillCascadeOnDelete(false);
  
 modelBuilder.Entity<Subscription>()
     .HasRequired(d => d.Organization)
     .WithMany(w => w.Subscription)
     .HasForeignKey(d => d.OrganizationID)
     .WillCascadeOnDelete(false);


















            base.OnModelCreating(modelBuilder);
        }
    }
}