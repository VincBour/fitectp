namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_dbo_Person : DbMigration
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
            
            AddColumn("dbo.Course", "CourseSessionID", c => c.Int(nullable: false));
            DropColumn("dbo.Person", "Login");
            DropColumn("dbo.Person", "Password");
            DropColumn("dbo.Person", "EmailAddress");
            DropColumn("dbo.Person", "FilName");
            DropColumn("dbo.Person", "ContentType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Person", "ContentType", c => c.String());
            AddColumn("dbo.Person", "FilName", c => c.String());
            AddColumn("dbo.Person", "EmailAddress", c => c.String());
            AddColumn("dbo.Person", "Password", c => c.String());
            AddColumn("dbo.Person", "Login", c => c.String());
            DropForeignKey("dbo.CourseSession", "InstructorID", "dbo.Person");
            DropForeignKey("dbo.CourseSession", "CourseID", "dbo.Course");
            DropIndex("dbo.CourseSession", new[] { "CourseID" });
            DropIndex("dbo.CourseSession", new[] { "InstructorID" });
            DropColumn("dbo.Course", "CourseSessionID");
            DropTable("dbo.CourseSession");
        }
    }
}
