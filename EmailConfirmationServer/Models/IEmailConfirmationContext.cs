using EmailConfirmationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailConfirmationServer.Models
{
    public interface IEmailConfirmationContext
    {
        IQueryable<Person> People { get; }
        void SaveChanges();
        Person FindPersonByEmail(string email);
        T Add<T>(T entity) where T : class;
    }
}
