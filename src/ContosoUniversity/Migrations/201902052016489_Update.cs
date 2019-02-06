namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.File", "PersonId", "dbo.Person");
            DropIndex("dbo.File", new[] { "PersonId" });
            AddColumn("dbo.Person", "FilName", c => c.String());
            AddColumn("dbo.Person", "ContentType", c => c.String());
            DropTable("dbo.File");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.FileId);
            
            DropColumn("dbo.Person", "ContentType");
            DropColumn("dbo.Person", "FilName");
            CreateIndex("dbo.File", "PersonId");
            AddForeignKey("dbo.File", "PersonId", "dbo.Person", "ID", cascadeDelete: true);
        }
    }
}
