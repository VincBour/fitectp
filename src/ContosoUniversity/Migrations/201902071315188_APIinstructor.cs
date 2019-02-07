namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class APIinstructor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseSession", "Duration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourseSession", "Duration");
        }
    }
}
