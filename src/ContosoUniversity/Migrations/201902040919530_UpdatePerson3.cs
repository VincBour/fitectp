namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePerson3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Person", "BirthDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Person", "BirthDate", c => c.DateTime(nullable: false));
        }
    }
}
