using EmailConfirmationServer.Models;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;

namespace EmailConfirmationServer.Controllers
{
    public class EmailController : Controller
    {
        private IEmailConfirmationContext context;

        public EmailController()
        {
            context = ApplicationDbContext.Create();            
        }

        public EmailController(IEmailConfirmationContext Context)
        {
            context = Context;
        }

        // GET: Confirm
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public async Task <ActionResult> Confirm (int id, string email)
        {            
            string path = HostingEnvironment.ApplicationPhysicalPath + "/Files/TestSheet.xlsx";

            //Calls the constructor.
            Spreadsheet spreadsheet = new Spreadsheet(path);
            spreadsheet.getExcelFile();
            spreadsheet.ConfirmEmail(email);

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