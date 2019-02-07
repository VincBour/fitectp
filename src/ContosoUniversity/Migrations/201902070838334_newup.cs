namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newup : DbMigration
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
            
            AddColumn("dbo.Course", "CourseSessionID", c => c.Int(nullable: false));
            AlterColumn("dbo.Person", "Login", c => c.String(nullable: false));
            AlterColumn("dbo.Person", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Person", "EmailAddress", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FileImage", "PersonId", "dbo.Person");
            DropIndex("dbo.FileImage", new[] { "PersonId" });
            AlterColumn("dbo.Person", "EmailAddress", c => c.String());
            AlterColumn("dbo.Person", "Password", c => c.String());
            AlterColumn("dbo.Person", "Login", c => c.String());
            DropColumn("dbo.Course", "CourseSessionID");
            DropTable("dbo.FileImage");
        }
    }
}
