namespace ZenGrantService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ZenGrantService.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var manager = new UserManager<ApplicationUser>( new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole { Name = "GrantGiver" });
                roleManager.Create(new IdentityRole { Name = "GrantBeneficiary" });
                roleManager.Create(new IdentityRole { Name = "ProjectTeam" });
                roleManager.Create(new IdentityRole { Name = "Assessor" });
                roleManager.Create(new IdentityRole { Name = "Accountant" });
                roleManager.Create(new IdentityRole { Name = "Auditor" });
                roleManager.Create(new IdentityRole { Name = "ReportView" });
                roleManager.Create(new IdentityRole { Name = "SystemAdmin" });
                roleManager.Create(new IdentityRole { Name = "SuperAdmin" });
                roleManager.Create(new IdentityRole { Name = "User" });

            }
            for (int i = 0; i < 4; i++)
            {
                var user = new ApplicationUser()
                {
                    UserName = string.Format("testuser{0}", i.ToString()),
                    firstname = string.Format("HARRISON{0}", i.ToString()),
                    lastname = string.Format("UHUNGHAMA{0}", i.ToString()),
                    PhoneNumber = string.Format("0814191818{0}", i.ToString()),
                    Address = "IDIMU",
                    City = "ALIMISHO",
                    State = enumManager.State.Lagos,
                    Nationality = enumManager.country.Nigeria,
                    Gender = enumManager.Gender.Male,
                    UserSummary = "A Proven Leader in Enterprise Analytics and Cutting Edge Business Solutions Development with Over 6 years’ experience in Technology consulting. An Expert in Driving Global Industry Standard best practices through the deployment of proven technologies and a working engine room of Real-time Analytics while establishing a solutions environment that enhances day to day transactional computation and a rapid reporting Infrastruc-ture with an end game of achieving business objectives and adding style and ease to business Operations.",
                    JobDesignation = "Grant Manager",
                    scope = enumManager.Scope.GrantGiver,
                    CreatedDate = DateTime.Now
                };
                manager.Create(user, string.Format("demopass{0}", i.ToString()));

                if (i > 2)
                {
                    var adminUser = manager.FindByName(string.Format("testuser{0}", i.ToString()));

                    manager.AddToRoles(adminUser.Id, new string[] { "SuperAdmin", "SystemAdmin" });
                }
                else {
                    var defaultuser = manager.FindByName(string.Format("testuser{0}", i.ToString()));

                    manager.AddToRoles(defaultuser.Id, new string[] { "GrantGiver", "SuperAdmin" });
                }
            }

        }
    }
}
