namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_dbo_Person : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Person", "Login", c => c.String());
            AddColumn("dbo.Person", "Password", c => c.String());
            AddColumn("dbo.Person", "EmailAddress", c => c.String());
            AddColumn("dbo.Person", "FilName", c => c.String());
            AddColumn("dbo.Person", "ContentType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Person", "ContentType");
            DropColumn("dbo.Person", "FilName");
            DropColumn("dbo.Person", "EmailAddress");
            DropColumn("dbo.Person", "Password");
            DropColumn("dbo.Person", "Login");
        }
    }
}
