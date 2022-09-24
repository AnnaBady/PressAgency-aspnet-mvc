namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class p : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Editors", "Post_post_id", "dbo.Posts");
            DropForeignKey("dbo.Favorites", "Post_post_id", "dbo.Posts");
            DropForeignKey("dbo.Questions", "Post_post_id", "dbo.Posts");
            DropIndex("dbo.Editors", new[] { "Post_post_id" });
            DropIndex("dbo.Favorites", new[] { "Post_post_id" });
            DropIndex("dbo.Questions", new[] { "Post_post_id" });
            DropColumn("dbo.Editors", "post_id");
            DropColumn("dbo.Favorites", "post_id");
            DropColumn("dbo.Questions", "post_id");
            RenameColumn(table: "dbo.Editors", name: "Post_post_id", newName: "post_id");
            RenameColumn(table: "dbo.Favorites", name: "Post_post_id", newName: "post_id");
            RenameColumn(table: "dbo.Questions", name: "Post_post_id", newName: "post_id");
            DropPrimaryKey("dbo.Posts");
            AlterColumn("dbo.Editors", "post_id", c => c.Int());
            AlterColumn("dbo.Posts", "post_id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Favorites", "post_id", c => c.Int());
            AlterColumn("dbo.Questions", "post_id", c => c.Int());
            AddPrimaryKey("dbo.Posts", "post_id");
            CreateIndex("dbo.Editors", "post_id");
            CreateIndex("dbo.Favorites", "post_id");
            CreateIndex("dbo.Questions", "post_id");
            AddForeignKey("dbo.Editors", "post_id", "dbo.Posts", "post_id");
            AddForeignKey("dbo.Favorites", "post_id", "dbo.Posts", "post_id");
            AddForeignKey("dbo.Questions", "post_id", "dbo.Posts", "post_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "post_id", "dbo.Posts");
            DropForeignKey("dbo.Favorites", "post_id", "dbo.Posts");
            DropForeignKey("dbo.Editors", "post_id", "dbo.Posts");
            DropIndex("dbo.Questions", new[] { "post_id" });
            DropIndex("dbo.Favorites", new[] { "post_id" });
            DropIndex("dbo.Editors", new[] { "post_id" });
            DropPrimaryKey("dbo.Posts");
            AlterColumn("dbo.Questions", "post_id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Favorites", "post_id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Posts", "post_id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Editors", "post_id", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Posts", "post_id");
            RenameColumn(table: "dbo.Questions", name: "post_id", newName: "Post_post_id");
            RenameColumn(table: "dbo.Favorites", name: "post_id", newName: "Post_post_id");
            RenameColumn(table: "dbo.Editors", name: "post_id", newName: "Post_post_id");
            AddColumn("dbo.Questions", "post_id", c => c.Int());
            AddColumn("dbo.Favorites", "post_id", c => c.Int());
            AddColumn("dbo.Editors", "post_id", c => c.Int());
            CreateIndex("dbo.Questions", "Post_post_id");
            CreateIndex("dbo.Favorites", "Post_post_id");
            CreateIndex("dbo.Editors", "Post_post_id");
            AddForeignKey("dbo.Questions", "Post_post_id", "dbo.Posts", "post_id");
            AddForeignKey("dbo.Favorites", "Post_post_id", "dbo.Posts", "post_id");
            AddForeignKey("dbo.Editors", "Post_post_id", "dbo.Posts", "post_id");
        }
    }
}
