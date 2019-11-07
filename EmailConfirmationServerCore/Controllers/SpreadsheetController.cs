using EmailConfirmationServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace EmailConfirmationServer.Controllers
{
    [Authorize]
    public class SpreadsheetController : Controller
    {
        private IEmailConfirmationContext context;

        //public SpreadsheetController()
        //{
        //    context = ApplicationDbContext.Create();
        //}

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
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = context.FindUserById(userId);

            if(user == null)
            {
                user = new User(userId);
            }

            if (user.Uploads == null)
            {
                user.Uploads = new List<SheetUpload>();
            }

            //To do: return uploads instead of a list of people
            return View(user);
        }
        [HttpPost]
        public async Task<ActionResult> Upload(IFormFile file)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = context.FindUserById(userId);
          
            if (file != null && file.Length > 0)
            {
                try
                {
                    
                    string path = Path.Combine("~/Files", Path.GetFileName(file.FileName));
                    
                    Spreadsheet spreadsheet = new Spreadsheet(path);
                    spreadsheet.getExcelFile();
            

                    if (user == null)
                    {
                        user = new User(userId);
                        addUploadToUser(user, spreadsheet, file.FileName);
                        context.Add<User>(user);
                    }
                    else
                    {
                        var upload = createNewUpload(user, spreadsheet, file.FileName);
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
            return View("Upload", user);
        }

        private void addUploadToUser(User user, Spreadsheet sheet, string fileName)
        {                        
            user.Uploads.Add(createNewUpload(user, sheet, fileName));                           
        }

        private SheetUpload createNewUpload(User user, Spreadsheet sheet, string fileName)
        {
            //This means entity framework could not find related uplaods in the DB
            if( user.Uploads == null)
            {
                user.Uploads = new List<SheetUpload>();
            }

            int sheetId = user.Uploads.Count() + 1;
            SheetUpload upload = new SheetUpload(sheetId, user.Id, fileName);
            upload.People = sheet.Persons;
            return upload;
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

        public ActionResult LoadUnconfirmedSpreadsheet(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = context.FindUserById(userId);

            var upload = (from sheetUpload in user.Uploads
                         where sheetUpload.Id == id
                         select sheetUpload).FirstOrDefault();

            var people = upload.People.AsQueryable();

            return View("_UnconfirmedTablePartial", people);
        }

        public ActionResult LoadConfirmedSpreadsheet(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = context.FindUserById(userId);

            var upload = (from sheetUpload in user.Uploads
                          where sheetUpload.Id == id
                          select sheetUpload).FirstOrDefault();

            var people = upload.People.AsQueryable();

            return View("_ConfirmedTablePartial", people);
        }
    }
}