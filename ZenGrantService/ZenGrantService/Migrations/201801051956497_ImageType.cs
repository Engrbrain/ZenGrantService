namespace ZenGrantService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organizations", "ImageType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organizations", "ImageType");
        }
    }
}
