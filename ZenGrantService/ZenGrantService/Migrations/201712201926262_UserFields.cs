namespace ZenGrantService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "firstname", c => c.String());
            AddColumn("dbo.AspNetUsers", "lastname", c => c.String());
            AddColumn("dbo.AspNetUsers", "scope", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "TimeStamp", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "TimeStamp");
            DropColumn("dbo.AspNetUsers", "CreatedDate");
            DropColumn("dbo.AspNetUsers", "scope");
            DropColumn("dbo.AspNetUsers", "lastname");
            DropColumn("dbo.AspNetUsers", "firstname");
        }
    }
}
