namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class up : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Person", "Login");
            DropColumn("dbo.Person", "Password");
            DropColumn("dbo.Person", "EmailAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Person", "EmailAddress", c => c.String(nullable: false));
            AddColumn("dbo.Person", "Password", c => c.String(nullable: false));
            AddColumn("dbo.Person", "Login", c => c.String(nullable: false));
        }
    }
}
