namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "Editor_username", "dbo.Editors");
            DropForeignKey("dbo.Questions", "Viewer_username", "dbo.Viewers");
            DropIndex("dbo.Questions", new[] { "Editor_username" });
            DropIndex("dbo.Questions", new[] { "Viewer_username" });
            AddColumn("dbo.Questions", "Editor_username1", c => c.String(maxLength: 20));
            AddColumn("dbo.Questions", "Viewer_username1", c => c.String(maxLength: 20));
            AlterColumn("dbo.Questions", "Editor_username", c => c.String());
            AlterColumn("dbo.Questions", "Viewer_username", c => c.String());
            CreateIndex("dbo.Questions", "Editor_username1");
            CreateIndex("dbo.Questions", "Viewer_username1");
            AddForeignKey("dbo.Questions", "Editor_username1", "dbo.Editors", "username");
            AddForeignKey("dbo.Questions", "Viewer_username1", "dbo.Viewers", "username");
         
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "EditorID", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "ViewerID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Questions", "Viewer_username1", "dbo.Viewers");
            DropForeignKey("dbo.Questions", "Editor_username1", "dbo.Editors");
            DropIndex("dbo.Questions", new[] { "Viewer_username1" });
            DropIndex("dbo.Questions", new[] { "Editor_username1" });
            AlterColumn("dbo.Questions", "Viewer_username", c => c.String(maxLength: 20));
            AlterColumn("dbo.Questions", "Editor_username", c => c.String(maxLength: 20));
            DropColumn("dbo.Questions", "Viewer_username1");
            DropColumn("dbo.Questions", "Editor_username1");
            CreateIndex("dbo.Questions", "Viewer_username");
            CreateIndex("dbo.Questions", "Editor_username");
            AddForeignKey("dbo.Questions", "Viewer_username", "dbo.Viewers", "username");
            AddForeignKey("dbo.Questions", "Editor_username", "dbo.Editors", "username");
        }
    }
}
