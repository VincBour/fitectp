namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApiInstru3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CourseSession", "Duration");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourseSession", "Duration", c => c.Int(nullable: false));
        }
    }
}
