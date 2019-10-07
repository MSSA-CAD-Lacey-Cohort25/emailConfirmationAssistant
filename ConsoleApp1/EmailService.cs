using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Diagnostics;

namespace EmailConfirmationService
{
    public class EmailService
    {
        private string APIKey = "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";

        public EmailService(Spreadsheet sheet)
        {
            Sheet = sheet;
        }
        public Spreadsheet Sheet { get; set; }

        public async Task sendConfirmationEmails()
        {
            var msg = new SendGridMessage();

           msg.SetFrom( new EmailAddress("test.email.from.app@stmartin.com", "MSSA"));

            var recipients = new List<EmailAddress>();

            var tasks = new List<Task>();
            foreach (Person person in Sheet.Persons)
            {
                await sendConfirmationEmail(person.EmailOutlook, person.FirstName);
                await sendConfirmationEmail(person.EmailStMartins, person.FirstName);                               
            }            
        }

        public async Task<Response> sendConfirmationEmail(string email, string name)
        {
            var msg = new SendGridMessage();

            msg.SetFrom(new EmailAddress("kimberly.castro13@outlook.com", "MSSA"));

            var recipients = new List<EmailAddress>();
            recipients.Add(new EmailAddress(email, name));
            msg.AddTos(recipients);
          
            msg.SetTemplateId("d-8dbd045f80d44095b9b193bbc594706c");

            var client = new SendGridClient(APIKey);
            Response response = await client.SendEmailAsync(msg);
            Debug.WriteLine(response.StatusCode);
            return response;
        }

        public void ConfirmEmail(String email)
        {
            Sheet.ConfirmEmail(email);
        }
    }
}
