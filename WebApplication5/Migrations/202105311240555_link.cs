namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class link : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Favorites", "ViewerID", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "ViewerID", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "EditorID", c => c.Int(nullable: false));
            DropColumn("dbo.Favorites", "username_Viewer");
            DropColumn("dbo.Questions", "username_Viewer");
            DropColumn("dbo.Questions", "username_Editor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "username_Editor", c => c.String(maxLength: 20));
            AddColumn("dbo.Questions", "username_Viewer", c => c.String(maxLength: 20));
            AddColumn("dbo.Favorites", "username_Viewer", c => c.String(maxLength: 20));
            DropColumn("dbo.Questions", "EditorID");
            DropColumn("dbo.Questions", "ViewerID");
            DropColumn("dbo.Favorites", "ViewerID");
        }
    }
}
