namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Newdb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CourseSession", "CourseID", "dbo.Course");
            DropForeignKey("dbo.CourseSession", "InstructorID", "dbo.Person");
            DropIndex("dbo.CourseSession", new[] { "InstructorID" });
            DropIndex("dbo.CourseSession", new[] { "CourseID" });
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
            CreateIndex("dbo.CourseSession", "CourseID");
            CreateIndex("dbo.CourseSession", "InstructorID");
            AddForeignKey("dbo.CourseSession", "InstructorID", "dbo.Person", "ID", cascadeDelete: true);
            AddForeignKey("dbo.CourseSession", "CourseID", "dbo.Course", "CourseID", cascadeDelete: true);
        }
    }
}
