using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EmailConfirmationServer.Models
{
    public class Person
    {
    
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Email> Emails { get; set; } = new List<Email>();

        public override string ToString()
        {
            return $"FirstName: {FirstName}   LastName: {LastName}  Emails: {Emails}";
        }
    }
}