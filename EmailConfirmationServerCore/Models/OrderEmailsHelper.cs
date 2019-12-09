using EmailConfirmationServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailConfirmationServerCore.Models
{
    public class OrderEmailsHelper
    {
        public static List<Email> OutlookFirst(List<Email> emails)
        {
            Email[] orderedEmails = new Email[2];

            foreach(var email in emails)
            {
                if (email.EmailAddress.Contains("outlook"))
                {
                    orderedEmails[0] = email;
                }
                else
                {
                    orderedEmails[1] = email;
                }
            }

            return orderedEmails.ToList();
        }
    }
}
