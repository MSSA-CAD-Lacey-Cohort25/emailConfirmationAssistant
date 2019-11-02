using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EmailConfirmationServer.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IEmailConfirmationContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        IQueryable<Person> IEmailConfirmationContext.People
        {
            get { return People; }
        }

        public DbSet<Person> People { get; set; }

        IQueryable<Email> IEmailConfirmationContext.Emails
        {
            get { return Emails; }
        }

        public DbSet<Email> Emails { get; set; }

        Person IEmailConfirmationContext.FindPersonById(int id)
        {
            return Set<Person>().Find(id);
        }

        IQueryable<SheetUpload> IEmailConfirmationContext.Uploads
        {
            get { return Uploads; }
        }

        public DbSet<SheetUpload> Uploads { get; set; }

        IQueryable<User> IEmailConfirmationContext.Users
        {
            get { return Users; }
        }

        public DbSet<User> Users { get; set; }


        void IEmailConfirmationContext.SaveChanges()
        {
            SaveChanges();
        }

        T IEmailConfirmationContext.Add<T>(T entity)
        {
            return Set<T>().Add(entity);
        }

        IQueryable<Email> IEmailConfirmationContext.FindEmailById(int id)
        {
            var emails = from email in Emails
                         where email.Id == id
                         select email;
            return emails;
        }

        User IEmailConfirmationContext.FindUserById(string id)
        {
            //var user = (from u in Users
            //            where u.Id == id
            //            select u).FirstOrDefault();

            //var user = Users.Find(id);
            //this.Entry(user).Collection(s => s.Uploads).Load();

            var user = Users
                .Where(u => u.Id == id)
                .Include(u => u.Uploads
                    .Select(s => s.People
                    .Select(p => p.Emails)))
                .FirstOrDefault();

            return user;
        }
    }
}