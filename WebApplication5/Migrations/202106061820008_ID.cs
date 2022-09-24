namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Editors", "id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Viewers", "id", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Viewers", "id");
            DropColumn("dbo.Editors", "id");
            DropColumn("dbo.Admins", "id");
        }
    }
}
