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

        //Properties

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Email> Emails { get; set; } = new List<Email>();
        
        //public Person()
        //{
        //    Email = new Dictionary<string, bool>();
        //}

        //public string EmailStMartins { get; set; }
        //public bool StMartinConfirm { get; set; }

        //public string EmailOutlook { get; set; }
        //public bool OutlookConfirm { get; set; }


        public override string ToString()
        {
            return $"FirstName: {FirstName}   LastName: {LastName}  Emails: {Emails}";
        }
    }
}