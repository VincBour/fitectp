namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_db : DbMigration
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
                        FileType = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Person", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            AlterColumn("dbo.Person", "Login", c => c.String());
            AlterColumn("dbo.Person", "Password", c => c.String());
            AlterColumn("dbo.Person", "EmailAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.File", "PersonId", "dbo.Person");
            DropIndex("dbo.File", new[] { "PersonId" });
            AlterColumn("dbo.Person", "EmailAddress", c => c.String(nullable: false));
            AlterColumn("dbo.Person", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Person", "Login", c => c.String(nullable: false));
            DropTable("dbo.File");
        }
    }
}
