namespace ZenGrantService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivityDocumentUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActivityDocuments", "LocalFilePath", c => c.String());
            AddColumn("dbo.ActivityDocuments", "FileName", c => c.String());
            AddColumn("dbo.ActivityDocuments", "FileLength", c => c.Long(nullable: false));
            DropColumn("dbo.ActivityDocuments", "DocumentFile");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ActivityDocuments", "DocumentFile", c => c.Binary());
            DropColumn("dbo.ActivityDocuments", "FileLength");
            DropColumn("dbo.ActivityDocuments", "FileName");
            DropColumn("dbo.ActivityDocuments", "LocalFilePath");
        }
    }
}
