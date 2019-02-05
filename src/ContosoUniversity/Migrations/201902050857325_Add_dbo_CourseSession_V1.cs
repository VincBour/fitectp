namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_dbo_CourseSession_V1 : DbMigration
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
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseSession", "InstructorID", "dbo.Person");
            DropForeignKey("dbo.CourseSession", "CourseID", "dbo.Course");
            DropIndex("dbo.CourseSession", new[] { "CourseID" });
            DropIndex("dbo.CourseSession", new[] { "InstructorID" });
            DropColumn("dbo.Course", "CourseSessionID");
            DropTable("dbo.CourseSession");
        }
    }
}
