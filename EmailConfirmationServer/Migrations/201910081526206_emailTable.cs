namespace EmailConfirmationServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emailTable : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Emails", "Person_Id", "dbo.People");
            DropIndex("dbo.Emails", new[] { "Person_Id" });
            DropTable("dbo.Emails");
        }
    }
}
