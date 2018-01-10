namespace ZenGrantService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class applicationdocumentmodified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationDocuments", "LocalFilePath", c => c.String());
            AddColumn("dbo.ApplicationDocuments", "FileName", c => c.String());
            DropColumn("dbo.ApplicationDocuments", "DocumentFile");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationDocuments", "DocumentFile", c => c.Binary());
            DropColumn("dbo.ApplicationDocuments", "FileName");
            DropColumn("dbo.ApplicationDocuments", "LocalFilePath");
        }
    }
}
