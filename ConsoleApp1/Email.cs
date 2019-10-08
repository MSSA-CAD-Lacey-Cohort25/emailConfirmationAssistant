using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailConfirmationService
{
    public class Email
    {
        public int Id { get; set; }

        public string EmailAddress { get; set; }

        public bool IsConfirmed { get; set; }

        public Email(int id, string email)
        {
            Id = id;
            EmailAddress = email;
        }
    }
}
