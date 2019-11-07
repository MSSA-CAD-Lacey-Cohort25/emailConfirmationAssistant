using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailConfirmationServer.Models
{
    public class User
    {
        public string Id { get; set; }

        public List<SheetUpload> Uploads { get; set; }

        public User(string id)
        {
            Id = id;
            Uploads = new List<SheetUpload>(); 
        }

        public User()
        {
            Id = null;
            Uploads = null;
        }
    }
}