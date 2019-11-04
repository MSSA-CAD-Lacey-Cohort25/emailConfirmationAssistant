namespace EmailConfirmationServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmailAddress = c.String(),
                        IsConfirmed = c.Boolean(nullable: false),
                        Person_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .Index(t => t.Person_Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        SheetUpload_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SheetUploads", t => t.SheetUpload_Id)
                .Index(t => t.SheetUpload_Id);
            
            CreateTable(
                "dbo.SheetUploads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SheetUploads", "User_Id", "dbo.Users");
            DropForeignKey("dbo.People", "SheetUpload_Id", "dbo.SheetUploads");
            DropForeignKey("dbo.Emails", "Person_Id", "dbo.People");
            DropIndex("dbo.SheetUploads", new[] { "User_Id" });
            DropIndex("dbo.People", new[] { "SheetUpload_Id" });
            DropIndex("dbo.Emails", new[] { "Person_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.SheetUploads");
            DropTable("dbo.People");
            DropTable("dbo.Emails");
        }
    }
}
