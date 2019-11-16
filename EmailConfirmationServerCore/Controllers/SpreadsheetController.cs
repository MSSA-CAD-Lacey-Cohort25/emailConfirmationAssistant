using EmailConfirmationServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IHostingEnvironment environment;

        public SpreadsheetController(IEmailConfirmationContext Context, IHostingEnvironment Environment)
        {
            context = Context;
            environment = Environment;
        }

        // GET: Spreadsheet
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Upload()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var uploads = context.FindUploadsByUserId(userId).ToList();

            if (uploads == null)
            {
                uploads = new List<SheetUpload>();
            }

            return View(uploads);
        }
        [HttpPost]
        public async Task<ActionResult> Upload(IFormFile file)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);            
            var uploads = context.FindUploadsByUserId(userId).ToList();

            if(uploads == null)
            {
                uploads = new List<SheetUpload>();
            }
          
            if (file != null && file.Length > 0)
            {
                try
                {
                    string webRoot = environment.WebRootPath;
                    string path = Path.Combine(webRoot, Path.GetFileName(file.FileName));
                    await saveFileToRootFolder(path, file);
                    
                    Spreadsheet spreadsheet = new Spreadsheet(path);                    
                    var upload = createNewUpload(userId, spreadsheet, file.FileName);
                    uploads.Add(upload);

                    context.Add<SheetUpload>(upload);
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
            return View("Upload", uploads);
        }

        private async Task saveFileToRootFolder(string path, IFormFile file)
        {            
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
     
        private SheetUpload createNewUpload(string userId, Spreadsheet sheet, string fileName)
        {            
            SheetUpload upload = new SheetUpload(userId, fileName);
            upload.People = sheet.People;
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
            var uploads = context.FindUploadsByUserId(userId).ToList();

            if (uploads == null)
            {
                uploads = new List<SheetUpload>();
            }

            var upload = (from sheetUpload in uploads
                         where sheetUpload.Id == id
                         select sheetUpload).FirstOrDefault();

            var people = upload.People.AsQueryable();

            return View("_UnconfirmedTablePartial", people);
        }

        public ActionResult LoadConfirmedSpreadsheet(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var uploads = context.FindUploadsByUserId(userId).ToList();

            if (uploads == null)
            {
                uploads = new List<SheetUpload>();
            }

            var upload = (from sheetUpload in uploads
                          where sheetUpload.Id == id
                          select sheetUpload).FirstOrDefault();

            var people = upload.People.AsQueryable();

            return View("_ConfirmedTablePartial", people);
        }
    }
}