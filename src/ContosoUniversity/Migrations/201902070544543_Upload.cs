namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upload : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseSession",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InstructorID = c.Int(nullable: false),
                        CourseID = c.Int(nullable: false),
                        DayOfWeek = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        HourStart = c.Int(nullable: false),
                        HourEnd = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.InstructorID, cascadeDelete: true)
                .Index(t => t.InstructorID)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.FileImage",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Person", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            AddColumn("dbo.Course", "CourseSessionID", c => c.Int(nullable: false));
            AddColumn("dbo.Person", "Login", c => c.String(nullable: false));
            AddColumn("dbo.Person", "Password", c => c.String(nullable: false));
            AddColumn("dbo.Person", "EmailAddress", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FileImage", "PersonId", "dbo.Person");
            DropForeignKey("dbo.CourseSession", "InstructorID", "dbo.Person");
            DropForeignKey("dbo.CourseSession", "CourseID", "dbo.Course");
            DropIndex("dbo.FileImage", new[] { "PersonId" });
            DropIndex("dbo.CourseSession", new[] { "CourseID" });
            DropIndex("dbo.CourseSession", new[] { "InstructorID" });
            DropColumn("dbo.Person", "EmailAddress");
            DropColumn("dbo.Person", "Password");
            DropColumn("dbo.Person", "Login");
            DropColumn("dbo.Course", "CourseSessionID");
            DropTable("dbo.FileImage");
            DropTable("dbo.CourseSession");
        }
    }
}
