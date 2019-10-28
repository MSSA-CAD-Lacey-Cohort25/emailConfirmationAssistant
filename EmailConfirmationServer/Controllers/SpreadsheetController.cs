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
                    //saves the uploaded spreadsheet, reads the sheet, and adds each person to the database
                    string path = Path.Combine(Server.MapPath("~/Files"), Path.GetFileName(file.FileName));
                    file.SaveAs(path);                    
                    Spreadsheet spreadsheet = new Spreadsheet(path);
                    spreadsheet.getExcelFile();  
                    
                    //adds each person object to the database
                    foreach(Person person in spreadsheet.Persons)
                    {      
                        context.Add<Person>(person);
                    }
                    context.SaveChanges();

                    //sends the emails from the file
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
            //loads all person objects with emails from the database
            var peopleWithEmails = context.People.Include(person => person.Emails);

            //separates unconfirmed emails and sends them into partial view
            return View("_UnconfirmedTablePartial", peopleWithEmails);
        }

        
        public ActionResult LoadConfirmedTable()
        {
            //loads all person objects with emails from the database
            var peopleWithEmails = context.People.Include(person => person.Emails);

            //separates confirmed emails and sends them into partial view
            return View("_ConfirmedTablePartial", peopleWithEmails);
        }
    }
}