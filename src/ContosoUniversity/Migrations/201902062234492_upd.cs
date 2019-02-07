namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileImage",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
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
            DropForeignKey("dbo.FileImage", "PersonId", "dbo.Person");
            DropIndex("dbo.FileImage", new[] { "PersonId" });
            DropColumn("dbo.Person", "EmailAddress");
            DropColumn("dbo.Person", "Password");
            DropColumn("dbo.Person", "Login");
            DropTable("dbo.FileImage");
        }
    }
}
