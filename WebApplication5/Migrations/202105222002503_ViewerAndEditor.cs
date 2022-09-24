namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ViewerAndEditor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Editors",
                c => new
                    {
                        username = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 20),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 20),
                        PhoneNO = c.Int(),
                        Role = c.String(nullable: false, maxLength: 10),
                        Photo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.username);
            
            CreateTable(
                "dbo.Viewers",
                c => new
                    {
                        username = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 20),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 30),
                        PhoneNO = c.Int(),
                        Role = c.String(nullable: false, maxLength: 10),
                        Photo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.username);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Viewers");
            DropTable("dbo.Editors");
        }
    }
}
