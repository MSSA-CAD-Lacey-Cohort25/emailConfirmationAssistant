namespace EmailConfirmationServer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EmailConfirmationServer.Models.EmailConfirmationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "EmailConfirmationServer.Models.EmailConfirmationContext";
        }

        protected override void Seed(EmailConfirmationServer.Models.EmailConfirmationContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}