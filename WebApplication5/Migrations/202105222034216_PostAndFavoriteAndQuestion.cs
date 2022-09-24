namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostAndFavoriteAndQuestion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        post_id = c.Int(nullable: false),
                        ArticleTitle = c.String(maxLength: 20),
                        ArticleBody = c.String(maxLength: 20),
                        Date = c.DateTime(storeType: "date"),
                        image = c.String(),
                        Type = c.String(maxLength: 20),
                        Approve = c.Boolean(),
                        Likes = c.Int(nullable: false, identity: true),
                        Dislikes = c.Int(),
                        Viewers = c.Int(),
                    })
                .PrimaryKey(t => t.post_id);
            
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        fav_id = c.Int(nullable: false),
                        username_Viewer = c.String(maxLength: 20),
                        post_id = c.Int(),
                        Viewer_username = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.fav_id)
                .ForeignKey("dbo.Posts", t => t.post_id)
                .ForeignKey("dbo.Viewers", t => t.Viewer_username)
                .Index(t => t.post_id)
                .Index(t => t.Viewer_username);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionNumber = c.Int(nullable: false, identity: true),
                        Question = c.String(unicode: false, storeType: "text"),
                        post_id = c.Int(),
                        username_Viewer = c.String(maxLength: 20),
                        Reply = c.String(unicode: false, storeType: "text"),
                        username_Editor = c.String(maxLength: 20),
                        Editor_username = c.String(maxLength: 20),
                        Viewer_username = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.QuestionNumber)
                .ForeignKey("dbo.Editors", t => t.Editor_username)
                .ForeignKey("dbo.Posts", t => t.post_id)
                .ForeignKey("dbo.Viewers", t => t.Viewer_username)
                .Index(t => t.post_id)
                .Index(t => t.Editor_username)
                .Index(t => t.Viewer_username);
            
            AddColumn("dbo.Editors", "post_id", c => c.Int());
            CreateIndex("dbo.Editors", "post_id");
            AddForeignKey("dbo.Editors", "post_id", "dbo.Posts", "post_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "Viewer_username", "dbo.Viewers");
            DropForeignKey("dbo.Questions", "post_id", "dbo.Posts");
            DropForeignKey("dbo.Questions", "Editor_username", "dbo.Editors");
            DropForeignKey("dbo.Favorites", "Viewer_username", "dbo.Viewers");
            DropForeignKey("dbo.Favorites", "post_id", "dbo.Posts");
            DropForeignKey("dbo.Editors", "post_id", "dbo.Posts");
            DropIndex("dbo.Questions", new[] { "Viewer_username" });
            DropIndex("dbo.Questions", new[] { "Editor_username" });
            DropIndex("dbo.Questions", new[] { "post_id" });
            DropIndex("dbo.Favorites", new[] { "Viewer_username" });
            DropIndex("dbo.Favorites", new[] { "post_id" });
            DropIndex("dbo.Editors", new[] { "post_id" });
            DropColumn("dbo.Editors", "post_id");
            DropTable("dbo.Questions");
            DropTable("dbo.Favorites");
            DropTable("dbo.Posts");
        }
    }
}
