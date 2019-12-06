using EmailConfirmationServer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmailConfirmationServerCore.Models
{
    public interface IReadSheet
    {
        public SheetUpload toSheetUpload(IFormFile file, string userId);
    }
}
