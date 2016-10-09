namespace MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xxx : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.cCDD_Details",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        CDDContact_Name = c.String(),
                        CDDContact_TelNo = c.String(),
                        CDDContact_Email = c.String(),
                        ComplianceSignOff_Name = c.String(),
                        ComplianceSignOff_Date = c.DateTime(nullable: false),
                        CDDStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Entity", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Entity",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.cEntityRelationship",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        entityA_ID = c.Int(nullable: false),
                        entityB_ID = c.Int(nullable: false),
                        relType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Entity", t => t.entityA_ID)
                .ForeignKey("dbo.Entity", t => t.entityB_ID)
                .Index(t => t.entityA_ID)
                .Index(t => t.entityB_ID);
            
            CreateTable(
                "dbo.cMatter",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        OfficeID = c.Int(nullable: false),
                        Team = c.String(),
                        ResponsiblePartner = c.String(),
                        MatterManager = c.String(),
                        Sec_PA = c.String(),
                        Advice_Provided_Without_CDD = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.cOffice", t => t.OfficeID, cascadeDelete: true)
                .Index(t => t.OfficeID);
            
            CreateTable(
                "dbo.cOffice",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OfficeCode = c.String(),
                        OfficeDescription = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EntityMatter",
                c => new
                    {
                        cEntityID = c.Int(nullable: false),
                        cMatterID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.cEntityID, t.cMatterID })
                .ForeignKey("dbo.Entity", t => t.cEntityID, cascadeDelete: true)
                .ForeignKey("dbo.cMatter", t => t.cMatterID, cascadeDelete: true)
                .Index(t => t.cEntityID)
                .Index(t => t.cMatterID);
            
            CreateTable(
                "dbo.Individual",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Title = c.String(),
                        Surname = c.String(),
                        Firstname = c.String(),
                        Middlename = c.String(),
                        Previousname = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        PlaceOfBirth = c.String(),
                        Nationality = c.String(),
                        CountryOfResidence = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Address3 = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Entity", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Organisation",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        registeredName = c.String(),
                        tradingName = c.String(),
                        keyClient = c.Boolean(nullable: false),
                        organisationType = c.Int(nullable: false),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Address3 = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Entity", t => t.ID)
                .Index(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Organisation", "ID", "dbo.Entity");
            DropForeignKey("dbo.Individual", "ID", "dbo.Entity");
            DropForeignKey("dbo.EntityMatter", "cMatterID", "dbo.cMatter");
            DropForeignKey("dbo.EntityMatter", "cEntityID", "dbo.Entity");
            DropForeignKey("dbo.cMatter", "OfficeID", "dbo.cOffice");
            DropForeignKey("dbo.cEntityRelationship", "entityB_ID", "dbo.Entity");
            DropForeignKey("dbo.cEntityRelationship", "entityA_ID", "dbo.Entity");
            DropForeignKey("dbo.cCDD_Details", "ID", "dbo.Entity");
            DropIndex("dbo.Organisation", new[] { "ID" });
            DropIndex("dbo.Individual", new[] { "ID" });
            DropIndex("dbo.EntityMatter", new[] { "cMatterID" });
            DropIndex("dbo.EntityMatter", new[] { "cEntityID" });
            DropIndex("dbo.cMatter", new[] { "OfficeID" });
            DropIndex("dbo.cEntityRelationship", new[] { "entityB_ID" });
            DropIndex("dbo.cEntityRelationship", new[] { "entityA_ID" });
            DropIndex("dbo.cCDD_Details", new[] { "ID" });
            DropTable("dbo.Organisation");
            DropTable("dbo.Individual");
            DropTable("dbo.EntityMatter");
            DropTable("dbo.cOffice");
            DropTable("dbo.cMatter");
            DropTable("dbo.cEntityRelationship");
            DropTable("dbo.Entity");
            DropTable("dbo.cCDD_Details");
        }
    }
}
