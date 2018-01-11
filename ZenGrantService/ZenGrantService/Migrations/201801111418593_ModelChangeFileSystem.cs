namespace ZenGrantService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelChangeFileSystem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MeetingAttendances", "ProjectTeamID", "dbo.Projects");
            DropForeignKey("dbo.CustomApplicationDetails", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.CustomApplicationDetails", "ProgApplicationID", "dbo.ProgApplications");
            DropForeignKey("dbo.CustomApplicationDetails", "ProgrammeID", "dbo.Programmes");
            DropForeignKey("dbo.ProposalTemplates", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.Programmes", "ProposalTemplateID", "dbo.ProposalTemplates");
            DropForeignKey("dbo.ProposalTemplates", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CustomApplicationDetails", "ProposalTemplateID", "dbo.ProposalTemplates");
            DropIndex("dbo.Programmes", new[] { "ProposalTemplateID" });
            DropIndex("dbo.CustomApplicationDetails", new[] { "ProposalTemplateID" });
            DropIndex("dbo.CustomApplicationDetails", new[] { "ProgApplicationID" });
            DropIndex("dbo.CustomApplicationDetails", new[] { "ProgrammeID" });
            DropIndex("dbo.CustomApplicationDetails", new[] { "OrganizationID" });
            DropIndex("dbo.ProposalTemplates", new[] { "OrganizationID" });
            DropIndex("dbo.ProposalTemplates", new[] { "UserId" });
            RenameColumn(table: "dbo.MeetingAttendances", name: "ProjectTeamID", newName: "ProjectTeam_ID");
            RenameIndex(table: "dbo.MeetingAttendances", name: "IX_ProjectTeamID", newName: "IX_ProjectTeam_ID");
            AddColumn("dbo.ProjectComments", "LocalFilePath", c => c.String());
            AddColumn("dbo.ProjectComments", "FileName", c => c.String());
            AddColumn("dbo.ProjectDocuments", "LocalFilePath", c => c.String());
            AddColumn("dbo.ProjectDocuments", "FileName", c => c.String());
            AddColumn("dbo.ProjectActivityComments", "LocalFilePath", c => c.String());
            AddColumn("dbo.ProjectActivityComments", "FileName", c => c.String());
            DropColumn("dbo.Programmes", "ProposalTemplateID");
            DropColumn("dbo.ProjectComments", "TagUser");
            DropColumn("dbo.ProjectComments", "CommentAttachment");
            DropColumn("dbo.ProjectDocuments", "DocumentFile");
            DropColumn("dbo.ProjectActivityComments", "TagUser");
            DropColumn("dbo.ProjectActivityComments", "CommentAttachment");
            DropTable("dbo.CustomApplicationDetails");
            DropTable("dbo.ProposalTemplates");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProposalTemplates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FieldLabel = c.String(),
                        FieldType = c.Int(nullable: false),
                        FieldValue = c.String(),
                        OrganizationID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CustomApplicationDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProposalTemplateID = c.Int(nullable: false),
                        FieldUserInput = c.String(),
                        ProgApplicationID = c.Int(nullable: false),
                        ProgrammeID = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.ProjectActivityComments", "CommentAttachment", c => c.Binary());
            AddColumn("dbo.ProjectActivityComments", "TagUser", c => c.String());
            AddColumn("dbo.ProjectDocuments", "DocumentFile", c => c.Binary());
            AddColumn("dbo.ProjectComments", "CommentAttachment", c => c.Binary());
            AddColumn("dbo.ProjectComments", "TagUser", c => c.String());
            AddColumn("dbo.Programmes", "ProposalTemplateID", c => c.Int(nullable: false));
            DropColumn("dbo.ProjectActivityComments", "FileName");
            DropColumn("dbo.ProjectActivityComments", "LocalFilePath");
            DropColumn("dbo.ProjectDocuments", "FileName");
            DropColumn("dbo.ProjectDocuments", "LocalFilePath");
            DropColumn("dbo.ProjectComments", "FileName");
            DropColumn("dbo.ProjectComments", "LocalFilePath");
            RenameIndex(table: "dbo.MeetingAttendances", name: "IX_ProjectTeam_ID", newName: "IX_ProjectTeamID");
            RenameColumn(table: "dbo.MeetingAttendances", name: "ProjectTeam_ID", newName: "ProjectTeamID");
            CreateIndex("dbo.ProposalTemplates", "UserId");
            CreateIndex("dbo.ProposalTemplates", "OrganizationID");
            CreateIndex("dbo.CustomApplicationDetails", "OrganizationID");
            CreateIndex("dbo.CustomApplicationDetails", "ProgrammeID");
            CreateIndex("dbo.CustomApplicationDetails", "ProgApplicationID");
            CreateIndex("dbo.CustomApplicationDetails", "ProposalTemplateID");
            CreateIndex("dbo.Programmes", "ProposalTemplateID");
            AddForeignKey("dbo.CustomApplicationDetails", "ProposalTemplateID", "dbo.ProposalTemplates", "ID");
            AddForeignKey("dbo.ProposalTemplates", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Programmes", "ProposalTemplateID", "dbo.ProposalTemplates", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ProposalTemplates", "OrganizationID", "dbo.Organizations", "ID");
            AddForeignKey("dbo.CustomApplicationDetails", "ProgrammeID", "dbo.Programmes", "ID", cascadeDelete: true);
            AddForeignKey("dbo.CustomApplicationDetails", "ProgApplicationID", "dbo.ProgApplications", "ID", cascadeDelete: true);
            AddForeignKey("dbo.CustomApplicationDetails", "OrganizationID", "dbo.Organizations", "ID");
            AddForeignKey("dbo.MeetingAttendances", "ProjectTeamID", "dbo.Projects", "ID");
        }
    }
}
