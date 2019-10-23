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
            context = ApplicationDbContext.Create();
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
            var user = context.FindUserById(userId);

            var people = context.People.Include(c => c.Emails);

            //To do: return uploads instead of a list of people
            return View(people);
        }
        [HttpPost]
        public async Task<ActionResult> Upload(HttpPostedFileBase file)
        {
            string userId = User.Identity.GetUserId();
            var user = context.FindUserById(userId);

           

            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Files"), Path.GetFileName(file.FileName));
                    file.SaveAs(path);

                    Spreadsheet spreadsheet = new Spreadsheet(path);
                    spreadsheet.getExcelFile();

                    int sheetId = user.Uploads.Count() + 1;
                    SheetUpload upload = new SheetUpload(sheetId, userId);
                    upload.People = spreadsheet.Persons;

                    if (user == null)
                    {
                        user = new User(userId);
                        user.Uploads.Add(upload);
                        context.Add<User>(user);
                    }
                    else
                    {
                        context.Add<SheetUpload>(upload);
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