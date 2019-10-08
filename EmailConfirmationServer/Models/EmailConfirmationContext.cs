using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using EmailConfirmationService;

namespace EmailConfirmationServer.Models
{
    public class EmailConfirmationContext : DbContext, IEmailConfirmationContext
    {
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
    }
}