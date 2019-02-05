namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_dbo_Person_For_UploadImg : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CourseSession", "CourseID", "dbo.Course");
            DropForeignKey("dbo.CourseSession", "InstructorID", "dbo.Person");
            DropIndex("dbo.CourseSession", new[] { "InstructorID" });
            DropIndex("dbo.CourseSession", new[] { "CourseID" });
            AddColumn("dbo.Person", "Login", c => c.String());
            AddColumn("dbo.Person", "Password", c => c.String());
            AddColumn("dbo.Person", "EmailAddress", c => c.String());
            AddColumn("dbo.Person", "FileName", c => c.String());
            AddColumn("dbo.Person", "ContentType", c => c.String());
            AddColumn("dbo.Person", "Content", c => c.Binary());
            DropColumn("dbo.Course", "CourseSessionID");
            DropTable("dbo.CourseSession");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Course", "CourseSessionID", c => c.Int(nullable: false));
            DropColumn("dbo.Person", "Content");
            DropColumn("dbo.Person", "ContentType");
            DropColumn("dbo.Person", "FileName");
            DropColumn("dbo.Person", "EmailAddress");
            DropColumn("dbo.Person", "Password");
            DropColumn("dbo.Person", "Login");
            CreateIndex("dbo.CourseSession", "CourseID");
            CreateIndex("dbo.CourseSession", "InstructorID");
            AddForeignKey("dbo.CourseSession", "InstructorID", "dbo.Person", "ID", cascadeDelete: true);
            AddForeignKey("dbo.CourseSession", "CourseID", "dbo.Course", "CourseID", cascadeDelete: true);
        }
    }
}
