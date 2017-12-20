namespace ZenGrantService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ZenGrantService.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ZenGrantService.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ZenGrantService.Models.ApplicationDbContext context)
        {
            context.Organizations.AddOrUpdate(x => x.ID,
         new Organization() { OrgName = "Test Sponsorship Limited", OrgAddress = "2, kkk, street", OrgState = "Lagos State", OrgCountry = "Nigeria", OrgPhone = "+23408874223", OrgEmail = "me@me.com", OrgWebsite = "www.me.com", CreatedDate = DateTime.Now, isDeleted = false }
         );
        }
    }
}
