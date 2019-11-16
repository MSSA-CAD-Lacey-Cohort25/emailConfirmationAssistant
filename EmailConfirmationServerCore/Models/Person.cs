using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EmailConfirmationServer.Models
{
    public class Person
    {
                
        public int Id { get; set; }

        public int UploadId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Email> Emails { get; set; }

        //Used for testing purposes
        public override string ToString()
        {
            return $"FirstName: {FirstName}   LastName: {LastName}  Emails: {Emails}";
        }
    }
}