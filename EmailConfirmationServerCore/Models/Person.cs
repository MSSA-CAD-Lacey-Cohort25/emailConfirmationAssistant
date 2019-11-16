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
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Email> Emails { get; set; } = new List<Email>();

        //Used for testing purposes
        public override string ToString()
        {
            return $"FirstName: {FirstName}   LastName: {LastName}  Emails: {Emails}";
        }
    }
}