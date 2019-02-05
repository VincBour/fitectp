namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_dbo_File : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.File",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Person", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            AddColumn("dbo.Person", "Login", c => c.String());
            AddColumn("dbo.Person", "Password", c => c.String());
            AddColumn("dbo.Person", "EmailAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.File", "PersonId", "dbo.Person");
            DropIndex("dbo.File", new[] { "PersonId" });
            DropColumn("dbo.Person", "EmailAddress");
            DropColumn("dbo.Person", "Password");
            DropColumn("dbo.Person", "Login");
            DropTable("dbo.File");
        }
    }
}
