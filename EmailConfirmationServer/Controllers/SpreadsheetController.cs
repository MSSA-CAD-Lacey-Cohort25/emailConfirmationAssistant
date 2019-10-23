using EmailConfirmationServer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace EmailConfirmationServer.Controllers
{
    public class SpreadsheetController : Controller
    {
        private IEmailConfirmationContext context;

        public SpreadsheetController()
        {
            context = new EmailConfirmationContext();
        }

        public SpreadsheetController(IEmailConfirmationContext Context)
        {
            context = Context;
        }

        // GET: Spreadsheet
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Upload()
        {
            string userId = User.Identity.GetUserId();

            var user = context.FindUserById(id);
            var people = context.People.Include(c => c.Emails);

            return View(people);
        }
        [HttpPost]
        public async Task<ActionResult> Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Files"), Path.GetFileName(file.FileName));
                    file.SaveAs(path);                    
                    Spreadsheet spreadsheet = new Spreadsheet(path);
                    spreadsheet.getExcelFile();            
                    foreach(Person person in spreadsheet.Persons)
                    {      
                        context.Add<Person>(person);
                    }
                    context.SaveChanges();

                    var emailService = new Models.EmailService(spreadsheet);
                    await emailService.sendConfirmationEmails();                                                           
                    ViewBag.Message = "File uploaded successfully";

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return View("Upload");
        }

        public ActionResult LoadUnconfirmedTable()
        {
            var people = context.People.Include(c => c.Emails);

            return View("_UnconfirmedTablePartial", people);
        }

        public ActionResult LoadConfirmedTable()
        {
            var people = context.People.Include(c => c.Emails);

            return View("_ConfirmedTablePartial", people);
        }
    }
}