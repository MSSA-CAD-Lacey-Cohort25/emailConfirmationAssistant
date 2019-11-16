using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmailConfirmationServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmailConfirmationServerCore.Data
{
    public class ApplicationDbContext : IdentityDbContext, IEmailConfirmationContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
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

        void IEmailConfirmationContext.SaveChanges()
        {
            SaveChanges();
        }

        void IEmailConfirmationContext.Add<T>(T entity)
        {
            Add<T>(entity);
        }

        IQueryable<Email> IEmailConfirmationContext.FindEmailById(int id)
        {
            var emails = from email in Emails
                         where email.Id == id
                         select email;
            return emails;
        }
     
        IQueryable<SheetUpload> IEmailConfirmationContext.FindUploadsByUserId(string id)
        {
            var uploads = Uploads
                .Where(u => u.UserId == id);
                            
            return uploads;
        }
    }
}

