using EmailConfirmationServer.Models;
using EmailConfirmationService;
using System.Web.Hosting;
using System.Web.Mvc;

namespace EmailConfirmationServer.Controllers
{
    public class EmailController : Controller
    {
        private IEmailConfirmationContext context;

        public EmailController()
        {
            context = new EmailConfirmationContext();
        }
        public EmailController(IEmailConfirmationContext Context)
        {
            context = Context;
        }

        // GET: Confirm
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Confirm (int id, string email)
        {
            
            string path = HostingEnvironment.ApplicationPhysicalPath + "/Files/TestSheet.xlsx";

            //Calls the constructor.
            Spreadsheet spreadsheet = new Spreadsheet(path);
            spreadsheet.getExcelFile();
            spreadsheet.ConfirmEmail(email);
            var person = context.FindPersonById(id);
            //Track saved emails

            return View();
        }
    }
}