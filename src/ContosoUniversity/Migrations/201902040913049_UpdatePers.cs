namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePers : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Person", "Login", c => c.String());
            AlterColumn("dbo.Person", "Password", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Person", "Password", c => c.String(maxLength: 8));
            AlterColumn("dbo.Person", "Login", c => c.String(maxLength: 20));
        }
    }
}
