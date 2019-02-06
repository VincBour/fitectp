namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_dbo_Person_v1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Person", "FilName");
            DropColumn("dbo.Person", "ContentType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Person", "ContentType", c => c.String());
            AddColumn("dbo.Person", "FilName", c => c.String());
        }
    }
}
