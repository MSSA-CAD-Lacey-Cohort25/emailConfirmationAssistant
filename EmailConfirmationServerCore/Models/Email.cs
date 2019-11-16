using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailConfirmationServer.Models
{
    public class Email
    {        
        public int Id { get; set; }

        public int PersonId { get; set; }

        public string EmailAddress { get; set; }

        public bool IsConfirmed { get; set; }

        public Email()
        {
        }

        public Email(int personId, string email)
        {
            PersonId = personId;
            EmailAddress = email;
        }
    }
}
