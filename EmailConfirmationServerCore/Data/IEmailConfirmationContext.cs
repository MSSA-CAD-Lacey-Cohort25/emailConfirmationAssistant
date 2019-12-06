using EmailConfirmationServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailConfirmationServer.Data
{
    public interface IEmailConfirmationContext
    {
        IQueryable<Person> People { get; }
        IQueryable<Email> Emails { get; }
        IQueryable<SheetUpload> Uploads { get; }        

        void SaveChanges();
        Person FindPersonById(int id);
        IQueryable<Email> FindEmailById(int id);
        IQueryable<SheetUpload> FindUploadsByUserId(string id);
        void Add<T>(T entity) where T : class;

        public void Delete(SheetUpload upload);
    }
}
