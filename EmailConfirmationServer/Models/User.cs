using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailConfirmationServer.Models
{
    public class User
    {
        public int Id { get; set; }

        public List<SheetUpload> Uploads { get; set; } = new List<SheetUpload>();
    }
}