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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}