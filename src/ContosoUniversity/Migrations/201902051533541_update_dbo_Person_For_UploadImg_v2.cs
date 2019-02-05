namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_dbo_Person_For_UploadImg_v2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Person", "ContentType");
            DropColumn("dbo.Person", "Content");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Person", "Content", c => c.Binary());
            AddColumn("dbo.Person", "ContentType", c => c.String());
        }
    }
}
