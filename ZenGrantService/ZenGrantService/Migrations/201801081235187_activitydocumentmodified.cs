namespace ZenGrantService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class activitydocumentmodified : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ActivityDocuments", "FileLength");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ActivityDocuments", "FileLength", c => c.Long(nullable: false));
        }
    }
}
