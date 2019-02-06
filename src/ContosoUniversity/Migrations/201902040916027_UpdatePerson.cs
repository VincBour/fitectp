namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePerson : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Person", "EmailAddress");
            DropColumn("dbo.Person", "BirthDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Person", "BirthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Person", "EmailAddress", c => c.String());
        }
    }
}
