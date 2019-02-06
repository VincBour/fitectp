namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_dbo_Person_v3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Person", "ConfirmPassword");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Person", "ConfirmPassword", c => c.String(nullable: false));
        }
    }
}
