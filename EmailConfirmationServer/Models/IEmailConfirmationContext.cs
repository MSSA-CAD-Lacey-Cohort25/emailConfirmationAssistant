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
        IQueryable<Email> Emails { get; }
        IQueryable<SheetUpload> Uploads { get; }
        IQueryable<User> Users { get; }

        void SaveChanges();
        Person FindPersonById(int id);
        IQueryable<Email> FindEmailById(int id);
        User FindUserById(int id);
        T Add<T>(T entity) where T : class;
    }
}
