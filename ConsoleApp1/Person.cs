using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EmailConfirmationService
{
    public class Person
    {

        //Properties
        
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string EmailStMartins { get; set; }
        public bool StMartinConfirm { get; set; }

        public string EmailOutlook { get; set; }
        public bool OutlookConfirm { get; set; }


        public override string ToString()
        {
            return $"FirstName: {FirstName}   LastName: {LastName}  EmailStMartins: {EmailStMartins}  EmailOutlook: {EmailOutlook}";
        }
    }
}