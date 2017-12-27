namespace ZenGrantService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityDocuments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DocumentName = c.String(),
                        DocumentDescription = c.String(),
                        DocumentFile = c.Binary(),
                        ProjectActivityID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        OrganizationID = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProjectActivities", t => t.ProjectActivityID, cascadeDelete: true)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ProjectActivityID)
                .Index(t => t.OrganizationID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrgName = c.String(),
                        OrgAddress = c.String(),
                        OrgState = c.Int(nullable: false),
                        OrgCountry = c.Int(nullable: false),
                        OrgPhone = c.String(),
                        OrgEmail = c.String(),
                        OrgWebsite = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        OrgLogo = c.Binary(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ActivityRisks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RiskTitle = c.String(),
                        RiskDescription = c.String(),
                        RiskMitigation = c.String(),
                        RiskStatus = c.Int(nullable: false),
                        RiskDocument = c.Binary(),
                        ProjectActivityID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        OrganizationID = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .ForeignKey("dbo.ProjectActivities", t => t.ProjectActivityID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ProjectActivityID)
                .Index(t => t.OrganizationID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProjectActivities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ActivityTitle = c.String(),
                        ActivityDescription = c.String(),
                        ActivityDocumentID = c.String(),
                        PhoneNumber = c.String(),
                        Address = c.String(),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Milestone = c.String(),
                        Priority = c.Int(nullable: false),
                        DependencyID = c.Int(),
                        ActivityStatus = c.Int(nullable: false),
                        ProjectMeetingID = c.Int(),
                        ProjectID = c.Int(nullable: false),
                        ProjectTeamID = c.Int(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        OrganizationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProjectActivities", t => t.DependencyID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .ForeignKey("dbo.ProjectMeetings", t => t.ProjectMeetingID)
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
                .ForeignKey("dbo.ProjectTeams", t => t.ProjectTeamID)
                .Index(t => t.DependencyID)
                .Index(t => t.ProjectMeetingID)
                .Index(t => t.ProjectID)
                .Index(t => t.ProjectTeamID)
                .Index(t => t.OrganizationID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProjectReference = c.String(),
                        ProjectName = c.String(),
                        ProjectDescription = c.String(),
                        ProgrammeDesc = c.String(),
                        ProjectStartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ProjectDueDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ProjectContigencyPeriod = c.String(),
                        AmountAllocated = c.Double(nullable: false),
                        BalanceAmount = c.Double(nullable: false),
                        ProjectStatus = c.String(),
                        OrganizationID = c.Int(nullable: false),
                        ProgApplicationID = c.Int(nullable: false),
                        ProgrammeID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ProjectLogo = c.Binary(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .ForeignKey("dbo.Programmes", t => t.ProgrammeID, cascadeDelete: true)
                .ForeignKey("dbo.ProgApplications", t => t.ProgApplicationID, cascadeDelete: true)
                .Index(t => t.OrganizationID)
                .Index(t => t.ProgApplicationID)
                .Index(t => t.ProgrammeID);
            
            CreateTable(
                "dbo.MeetingAttendances",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Designation = c.String(),
                        EmailAddress = c.String(),
                        PhoneNumber = c.String(),
                        AttendanceStatus = c.Int(nullable: false),
                        ProjectMeetingID = c.Int(nullable: false),
                        ProjectID = c.Int(nullable: false),
                        ProjectTeamID = c.Int(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        OrganizationID = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .ForeignKey("dbo.Projects", t => t.ProjectID)
                .ForeignKey("dbo.ProjectMeetings", t => t.ProjectMeetingID, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectTeamID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.ProjectTeams", t => t.ProjectTeamID)
                .Index(t => t.ProjectMeetingID)
                .Index(t => t.ProjectID)
                .Index(t => t.ProjectTeamID)
                .Index(t => t.OrganizationID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProjectMeetings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MeetingTitle = c.String(),
                        MeetingDetails = c.String(),
                        MeetingPurpose = c.String(),
                        MeetingMedium = c.Int(nullable: false),
                        DiscussionSummary = c.String(),
                        MeetingMinutes = c.Binary(),
                        MeetingStatus = c.Int(nullable: false),
                        ProjectID = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ProjectID)
                .Index(t => t.OrganizationID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        firstname = c.String(),
                        lastname = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.Int(nullable: false),
                        Nationality = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                        UserSummary = c.String(),
                        JobDesignation = c.String(),
                        UserImage = c.Binary(),
                        scope = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 7, storeType: "datetime2"),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ProgApplications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ApplicantReference = c.String(),
                        ApplicantName = c.String(),
                        ApplicantPhoneNumber = c.String(),
                        ApplicantEmailAddress = c.String(),
                        ApplicationSummary = c.String(),
                        Proposal = c.Binary(),
                        ApplicantPhoto = c.Binary(),
                        OrganizationID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Applicationscore = c.Double(nullable: false),
                        ProgrammeID = c.Int(nullable: false),
                        AssessorID = c.Int(),
                        applicationStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Assessors", t => t.AssessorID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .ForeignKey("dbo.Programmes", t => t.ProgrammeID)
                .Index(t => t.OrganizationID)
                .Index(t => t.ProgrammeID)
                .Index(t => t.AssessorID);
            
            CreateTable(
                "dbo.ApplicationDocuments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DocumentName = c.String(),
                        DocumentDescription = c.String(),
                        DocumentFile = c.Binary(),
                        OrganizationID = c.Int(nullable: false),
                        ProgApplicationID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .ForeignKey("dbo.ProgApplications", t => t.ProgApplicationID, cascadeDelete: true)
                .Index(t => t.OrganizationID)
                .Index(t => t.ProgApplicationID);
            
            CreateTable(
                "dbo.Assessors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AssessorCode = c.String(),
                        AssessorName = c.String(),
                        AssessorEmail = c.String(),
                        AssessorPassword = c.String(),
                        OrganizationID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.OrganizationID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SelectionAnswers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SelectionQuestionID = c.Int(nullable: false),
                        AssesorID = c.Int(nullable: false),
                        ProgApplicationID = c.Int(nullable: false),
                        AssignedScore = c.Double(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        OrganizationID = c.Int(nullable: false),
                        Assessor_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Assessors", t => t.Assessor_ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .ForeignKey("dbo.ProgApplications", t => t.ProgApplicationID, cascadeDelete: true)
                .ForeignKey("dbo.SelectionQuestions", t => t.SelectionQuestionID, cascadeDelete: true)
                .Index(t => t.SelectionQuestionID)
                .Index(t => t.ProgApplicationID)
                .Index(t => t.OrganizationID)
                .Index(t => t.Assessor_ID);
            
            CreateTable(
                "dbo.SelectionQuestions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        QuestionWeight = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        OrganizationID = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.OrganizationID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SelectionCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        CategoryDescription = c.String(),
                        CategoryWeight = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .Index(t => t.OrganizationID);
            
            CreateTable(
                "dbo.Programmes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProgrammeName = c.String(),
                        ProgrammeCode = c.String(),
                        ProgrammeDesc = c.String(),
                        ApplicationStartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ApplicationDueDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ProposalTemplateID = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        MaximumApplication = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ProgrammeLogo = c.Binary(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProposalTemplates", t => t.ProposalTemplateID, cascadeDelete: true)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ProposalTemplateID)
                .Index(t => t.OrganizationID)
                .Index(t => t.UserId);
            
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .ForeignKey("dbo.ProgApplications", t => t.ProgApplicationID, cascadeDelete: true)
                .ForeignKey("dbo.Programmes", t => t.ProgrammeID, cascadeDelete: true)
                .ForeignKey("dbo.ProposalTemplates", t => t.ProposalTemplateID)
                .Index(t => t.ProposalTemplateID)
                .Index(t => t.ProgApplicationID)
                .Index(t => t.ProgrammeID)
                .Index(t => t.OrganizationID);
            
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.OrganizationID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProjectBudgets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BudgetItem = c.String(),
                        BudgetItemDesc = c.String(),
                        PercentageAllocated = c.Double(nullable: false),
                        BudgetAmount = c.Double(nullable: false),
                        ItemActual = c.Double(nullable: false),
                        ProjectID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        OrganizationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
                .Index(t => t.ProjectID)
                .Index(t => t.OrganizationID);
            
            CreateTable(
                "dbo.ProjectTransactionHeaders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProjectBudgetID = c.Int(nullable: false),
                        TransactionRef = c.String(),
                        ShortText = c.String(),
                        FiscalYear = c.Int(nullable: false),
                        Period = c.Int(nullable: false),
                        TransactionDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EntryDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        TotalAmount = c.Double(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        OrganizationID = c.Int(nullable: false),
                        ProjectID = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .ForeignKey("dbo.Projects", t => t.ProjectID)
                .ForeignKey("dbo.ProjectBudgets", t => t.ProjectBudgetID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ProjectBudgetID)
                .Index(t => t.OrganizationID)
                .Index(t => t.ProjectID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProjectTransactionLines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProjectTransactionHeaderID = c.Int(nullable: false),
                        TransactionRef = c.String(),
                        LineNumber = c.Int(nullable: false),
                        Narration = c.String(),
                        LineAmount = c.Double(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        OrganizationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .ForeignKey("dbo.ProjectTransactionHeaders", t => t.ProjectTransactionHeaderID, cascadeDelete: true)
                .Index(t => t.ProjectTransactionHeaderID)
                .Index(t => t.OrganizationID);
            
            CreateTable(
                "dbo.ProjectComments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CommentTitle = c.String(),
                        CommentDescription = c.String(),
                        TagUser = c.String(),
                        CommentAttachment = c.Binary(),
                        OrganizationID = c.Int(nullable: false),
                        ProjectID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.OrganizationID)
                .Index(t => t.ProjectID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProjectDocuments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DocumentName = c.String(),
                        DocumentDescription = c.String(),
                        DocumentFile = c.Binary(),
                        OrganizationID = c.Int(nullable: false),
                        ProjectID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.OrganizationID)
                .Index(t => t.ProjectID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProjectRisks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RiskTitle = c.String(),
                        RiskDescription = c.String(),
                        RiskMitigation = c.String(),
                        RiskStatus = c.Int(nullable: false),
                        RiskDocument = c.Binary(),
                        ProjectID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        OrganizationID = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ProjectID)
                .Index(t => t.OrganizationID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProjectTeams",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TeamMemberReference = c.String(),
                        Fullname = c.String(),
                        EmailAddress = c.String(),
                        PhoneNumber = c.String(),
                        Address = c.String(),
                        State = c.Int(nullable: false),
                        Country = c.Int(nullable: false),
                        KPI = c.Int(nullable: false),
                        ProjectID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        OrganizationID = c.Int(nullable: false),
                        TeamMemberPhoto = c.Binary(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ProjectID)
                .Index(t => t.OrganizationID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProjectActivityComments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CommentTitle = c.String(),
                        CommentDescription = c.String(),
                        TagUser = c.String(),
                        CommentAttachment = c.Binary(),
                        ProjectActivityID = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .ForeignKey("dbo.ProjectActivities", t => t.ProjectActivityID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ProjectActivityID)
                .Index(t => t.OrganizationID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.BudgetTemplates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BudgetItem = c.String(),
                        BudgetItemDesc = c.String(),
                        PercentageAllocated = c.Double(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID, cascadeDelete: true)
                .Index(t => t.OrganizationID);
            
            CreateTable(
                "dbo.FocusAreas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FocusAreaName = c.String(),
                        FocusAreaDesc = c.String(),
                        OrganizationID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isActive = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.OrganizationID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProjectTemplates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NumberOfMilestones = c.String(),
                        ProjectReportFrequency = c.String(),
                        OrganizationID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isDeleted = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID, cascadeDelete: true)
                .Index(t => t.OrganizationID);
            
            CreateTable(
                "dbo.Renewals",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrganizationID = c.Int(nullable: false),
                        SubscriptionID = c.Int(nullable: false),
                        InvoiceNumber = c.String(),
                        PaymentReference = c.String(),
                        Status = c.Int(nullable: false),
                        PaymentMethod = c.Int(nullable: false),
                        Narration = c.String(),
                        PostingDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ExpiryDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        RenewalAmount = c.Double(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID, cascadeDelete: true)
                .ForeignKey("dbo.Subscriptions", t => t.SubscriptionID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.OrganizationID)
                .Index(t => t.SubscriptionID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        subType = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        NextExpiryDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastRenewalDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isActive = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.OrganizationID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ProgrammeSelectionCategories",
                c => new
                    {
                        Programme_ID = c.Int(nullable: false),
                        SelectionCategory_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Programme_ID, t.SelectionCategory_ID })
                .ForeignKey("dbo.Programmes", t => t.Programme_ID, cascadeDelete: true)
                .ForeignKey("dbo.SelectionCategories", t => t.SelectionCategory_ID, cascadeDelete: true)
                .Index(t => t.Programme_ID)
                .Index(t => t.SelectionCategory_ID);
            
            CreateTable(
                "dbo.SelectionCategorySelectionQuestions",
                c => new
                    {
                        SelectionCategory_ID = c.Int(nullable: false),
                        SelectionQuestion_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SelectionCategory_ID, t.SelectionQuestion_ID })
                .ForeignKey("dbo.SelectionCategories", t => t.SelectionCategory_ID, cascadeDelete: true)
                .ForeignKey("dbo.SelectionQuestions", t => t.SelectionQuestion_ID, cascadeDelete: true)
                .Index(t => t.SelectionCategory_ID)
                .Index(t => t.SelectionQuestion_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ActivityDocuments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ActivityDocuments", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.Organizations", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Renewals", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Subscriptions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Renewals", "SubscriptionID", "dbo.Subscriptions");
            DropForeignKey("dbo.Subscriptions", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.Renewals", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.ProjectTemplates", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.FocusAreas", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FocusAreas", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.BudgetTemplates", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.ActivityRisks", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectActivityComments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectActivityComments", "ProjectActivityID", "dbo.ProjectActivities");
            DropForeignKey("dbo.ProjectActivityComments", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.ProjectTeams", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectActivities", "ProjectTeamID", "dbo.ProjectTeams");
            DropForeignKey("dbo.ProjectTeams", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.ProjectTeams", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.MeetingAttendances", "ProjectTeamID", "dbo.ProjectTeams");
            DropForeignKey("dbo.ProjectRisks", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectRisks", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.ProjectRisks", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.ProjectDocuments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectDocuments", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.ProjectDocuments", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.ProjectComments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectComments", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.ProjectComments", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.ProjectTransactionHeaders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectTransactionLines", "ProjectTransactionHeaderID", "dbo.ProjectTransactionHeaders");
            DropForeignKey("dbo.ProjectTransactionLines", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.ProjectTransactionHeaders", "ProjectBudgetID", "dbo.ProjectBudgets");
            DropForeignKey("dbo.ProjectTransactionHeaders", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.ProjectTransactionHeaders", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.ProjectBudgets", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.ProjectBudgets", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.ProjectActivities", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.Projects", "ProgApplicationID", "dbo.ProgApplications");
            DropForeignKey("dbo.ProgApplications", "ProgrammeID", "dbo.Programmes");
            DropForeignKey("dbo.ProgApplications", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.Assessors", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SelectionQuestions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SelectionCategorySelectionQuestions", "SelectionQuestion_ID", "dbo.SelectionQuestions");
            DropForeignKey("dbo.SelectionCategorySelectionQuestions", "SelectionCategory_ID", "dbo.SelectionCategories");
            DropForeignKey("dbo.Programmes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProgrammeSelectionCategories", "SelectionCategory_ID", "dbo.SelectionCategories");
            DropForeignKey("dbo.ProgrammeSelectionCategories", "Programme_ID", "dbo.Programmes");
            DropForeignKey("dbo.Projects", "ProgrammeID", "dbo.Programmes");
            DropForeignKey("dbo.Programmes", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.CustomApplicationDetails", "ProposalTemplateID", "dbo.ProposalTemplates");
            DropForeignKey("dbo.ProposalTemplates", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Programmes", "ProposalTemplateID", "dbo.ProposalTemplates");
            DropForeignKey("dbo.ProposalTemplates", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.CustomApplicationDetails", "ProgrammeID", "dbo.Programmes");
            DropForeignKey("dbo.CustomApplicationDetails", "ProgApplicationID", "dbo.ProgApplications");
            DropForeignKey("dbo.CustomApplicationDetails", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.SelectionCategories", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.SelectionAnswers", "SelectionQuestionID", "dbo.SelectionQuestions");
            DropForeignKey("dbo.SelectionQuestions", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.SelectionAnswers", "ProgApplicationID", "dbo.ProgApplications");
            DropForeignKey("dbo.SelectionAnswers", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.SelectionAnswers", "Assessor_ID", "dbo.Assessors");
            DropForeignKey("dbo.ProgApplications", "AssessorID", "dbo.Assessors");
            DropForeignKey("dbo.Assessors", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.ApplicationDocuments", "ProgApplicationID", "dbo.ProgApplications");
            DropForeignKey("dbo.ApplicationDocuments", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.Projects", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.MeetingAttendances", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MeetingAttendances", "ProjectTeamID", "dbo.Projects");
            DropForeignKey("dbo.ProjectMeetings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectActivities", "ProjectMeetingID", "dbo.ProjectMeetings");
            DropForeignKey("dbo.ProjectMeetings", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.ProjectMeetings", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.MeetingAttendances", "ProjectMeetingID", "dbo.ProjectMeetings");
            DropForeignKey("dbo.MeetingAttendances", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.MeetingAttendances", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.ProjectActivities", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.ProjectActivities", "DependencyID", "dbo.ProjectActivities");
            DropForeignKey("dbo.ActivityRisks", "ProjectActivityID", "dbo.ProjectActivities");
            DropForeignKey("dbo.ActivityDocuments", "ProjectActivityID", "dbo.ProjectActivities");
            DropForeignKey("dbo.ActivityRisks", "OrganizationID", "dbo.Organizations");
            DropIndex("dbo.SelectionCategorySelectionQuestions", new[] { "SelectionQuestion_ID" });
            DropIndex("dbo.SelectionCategorySelectionQuestions", new[] { "SelectionCategory_ID" });
            DropIndex("dbo.ProgrammeSelectionCategories", new[] { "SelectionCategory_ID" });
            DropIndex("dbo.ProgrammeSelectionCategories", new[] { "Programme_ID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Subscriptions", new[] { "UserId" });
            DropIndex("dbo.Subscriptions", new[] { "OrganizationID" });
            DropIndex("dbo.Renewals", new[] { "UserId" });
            DropIndex("dbo.Renewals", new[] { "SubscriptionID" });
            DropIndex("dbo.Renewals", new[] { "OrganizationID" });
            DropIndex("dbo.ProjectTemplates", new[] { "OrganizationID" });
            DropIndex("dbo.FocusAreas", new[] { "UserId" });
            DropIndex("dbo.FocusAreas", new[] { "OrganizationID" });
            DropIndex("dbo.BudgetTemplates", new[] { "OrganizationID" });
            DropIndex("dbo.ProjectActivityComments", new[] { "UserId" });
            DropIndex("dbo.ProjectActivityComments", new[] { "OrganizationID" });
            DropIndex("dbo.ProjectActivityComments", new[] { "ProjectActivityID" });
            DropIndex("dbo.ProjectTeams", new[] { "UserId" });
            DropIndex("dbo.ProjectTeams", new[] { "OrganizationID" });
            DropIndex("dbo.ProjectTeams", new[] { "ProjectID" });
            DropIndex("dbo.ProjectRisks", new[] { "UserId" });
            DropIndex("dbo.ProjectRisks", new[] { "OrganizationID" });
            DropIndex("dbo.ProjectRisks", new[] { "ProjectID" });
            DropIndex("dbo.ProjectDocuments", new[] { "UserId" });
            DropIndex("dbo.ProjectDocuments", new[] { "ProjectID" });
            DropIndex("dbo.ProjectDocuments", new[] { "OrganizationID" });
            DropIndex("dbo.ProjectComments", new[] { "UserId" });
            DropIndex("dbo.ProjectComments", new[] { "ProjectID" });
            DropIndex("dbo.ProjectComments", new[] { "OrganizationID" });
            DropIndex("dbo.ProjectTransactionLines", new[] { "OrganizationID" });
            DropIndex("dbo.ProjectTransactionLines", new[] { "ProjectTransactionHeaderID" });
            DropIndex("dbo.ProjectTransactionHeaders", new[] { "UserId" });
            DropIndex("dbo.ProjectTransactionHeaders", new[] { "ProjectID" });
            DropIndex("dbo.ProjectTransactionHeaders", new[] { "OrganizationID" });
            DropIndex("dbo.ProjectTransactionHeaders", new[] { "ProjectBudgetID" });
            DropIndex("dbo.ProjectBudgets", new[] { "OrganizationID" });
            DropIndex("dbo.ProjectBudgets", new[] { "ProjectID" });
            DropIndex("dbo.ProposalTemplates", new[] { "UserId" });
            DropIndex("dbo.ProposalTemplates", new[] { "OrganizationID" });
            DropIndex("dbo.CustomApplicationDetails", new[] { "OrganizationID" });
            DropIndex("dbo.CustomApplicationDetails", new[] { "ProgrammeID" });
            DropIndex("dbo.CustomApplicationDetails", new[] { "ProgApplicationID" });
            DropIndex("dbo.CustomApplicationDetails", new[] { "ProposalTemplateID" });
            DropIndex("dbo.Programmes", new[] { "UserId" });
            DropIndex("dbo.Programmes", new[] { "OrganizationID" });
            DropIndex("dbo.Programmes", new[] { "ProposalTemplateID" });
            DropIndex("dbo.SelectionCategories", new[] { "OrganizationID" });
            DropIndex("dbo.SelectionQuestions", new[] { "UserId" });
            DropIndex("dbo.SelectionQuestions", new[] { "OrganizationID" });
            DropIndex("dbo.SelectionAnswers", new[] { "Assessor_ID" });
            DropIndex("dbo.SelectionAnswers", new[] { "OrganizationID" });
            DropIndex("dbo.SelectionAnswers", new[] { "ProgApplicationID" });
            DropIndex("dbo.SelectionAnswers", new[] { "SelectionQuestionID" });
            DropIndex("dbo.Assessors", new[] { "UserId" });
            DropIndex("dbo.Assessors", new[] { "OrganizationID" });
            DropIndex("dbo.ApplicationDocuments", new[] { "ProgApplicationID" });
            DropIndex("dbo.ApplicationDocuments", new[] { "OrganizationID" });
            DropIndex("dbo.ProgApplications", new[] { "AssessorID" });
            DropIndex("dbo.ProgApplications", new[] { "ProgrammeID" });
            DropIndex("dbo.ProgApplications", new[] { "OrganizationID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ProjectMeetings", new[] { "UserId" });
            DropIndex("dbo.ProjectMeetings", new[] { "OrganizationID" });
            DropIndex("dbo.ProjectMeetings", new[] { "ProjectID" });
            DropIndex("dbo.MeetingAttendances", new[] { "UserId" });
            DropIndex("dbo.MeetingAttendances", new[] { "OrganizationID" });
            DropIndex("dbo.MeetingAttendances", new[] { "ProjectTeamID" });
            DropIndex("dbo.MeetingAttendances", new[] { "ProjectID" });
            DropIndex("dbo.MeetingAttendances", new[] { "ProjectMeetingID" });
            DropIndex("dbo.Projects", new[] { "ProgrammeID" });
            DropIndex("dbo.Projects", new[] { "ProgApplicationID" });
            DropIndex("dbo.Projects", new[] { "OrganizationID" });
            DropIndex("dbo.ProjectActivities", new[] { "OrganizationID" });
            DropIndex("dbo.ProjectActivities", new[] { "ProjectTeamID" });
            DropIndex("dbo.ProjectActivities", new[] { "ProjectID" });
            DropIndex("dbo.ProjectActivities", new[] { "ProjectMeetingID" });
            DropIndex("dbo.ProjectActivities", new[] { "DependencyID" });
            DropIndex("dbo.ActivityRisks", new[] { "UserId" });
            DropIndex("dbo.ActivityRisks", new[] { "OrganizationID" });
            DropIndex("dbo.ActivityRisks", new[] { "ProjectActivityID" });
            DropIndex("dbo.Organizations", new[] { "UserId" });
            DropIndex("dbo.ActivityDocuments", new[] { "UserId" });
            DropIndex("dbo.ActivityDocuments", new[] { "OrganizationID" });
            DropIndex("dbo.ActivityDocuments", new[] { "ProjectActivityID" });
            DropTable("dbo.SelectionCategorySelectionQuestions");
            DropTable("dbo.ProgrammeSelectionCategories");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Subscriptions");
            DropTable("dbo.Renewals");
            DropTable("dbo.ProjectTemplates");
            DropTable("dbo.FocusAreas");
            DropTable("dbo.BudgetTemplates");
            DropTable("dbo.ProjectActivityComments");
            DropTable("dbo.ProjectTeams");
            DropTable("dbo.ProjectRisks");
            DropTable("dbo.ProjectDocuments");
            DropTable("dbo.ProjectComments");
            DropTable("dbo.ProjectTransactionLines");
            DropTable("dbo.ProjectTransactionHeaders");
            DropTable("dbo.ProjectBudgets");
            DropTable("dbo.ProposalTemplates");
            DropTable("dbo.CustomApplicationDetails");
            DropTable("dbo.Programmes");
            DropTable("dbo.SelectionCategories");
            DropTable("dbo.SelectionQuestions");
            DropTable("dbo.SelectionAnswers");
            DropTable("dbo.Assessors");
            DropTable("dbo.ApplicationDocuments");
            DropTable("dbo.ProgApplications");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ProjectMeetings");
            DropTable("dbo.MeetingAttendances");
            DropTable("dbo.Projects");
            DropTable("dbo.ProjectActivities");
            DropTable("dbo.ActivityRisks");
            DropTable("dbo.Organizations");
            DropTable("dbo.ActivityDocuments");
        }
    }
}
