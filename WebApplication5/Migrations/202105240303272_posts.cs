namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class posts : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "ArticleTitle", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "ArticleTitle", c => c.String(maxLength: 100));
        }
    }
}
