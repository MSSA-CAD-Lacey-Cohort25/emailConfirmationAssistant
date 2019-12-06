using EmailConfirmationServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailConfirmationServerCore.Models
{
    public interface IEmailService
    {
        public Task SendConfirmationEmails(SheetUpload sheetUpload);
    }
}
