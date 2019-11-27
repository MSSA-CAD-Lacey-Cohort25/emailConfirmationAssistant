using EmailConfirmationServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using System.Threading.Tasks;

namespace EmailConfirmationServer.Controllers
{
    public class EmailController : Controller
    {
        private IEmailConfirmationContext context;
    
        public EmailController(IEmailConfirmationContext Context)
        {
            context = Context;
        }
    
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public async Task <ActionResult> Confirm (int id, string email)
        {                              
            //gets the person who clicks the confirm in their email (from the method's parameters)
            var person = await context.People.Include(c => c.Emails).SingleAsync(c => c.Id == id);

            //since each person has 2 emails, this determines which email the person confirmed
            foreach (var e in person.Emails)
            {
                if (e.EmailAddress == email)
                {
                    e.IsConfirmed = true;
                }
            }

            //this saves the e.IsConfirmed = true to the database
            context.SaveChanges();
           
            return View();
        }
    }
}