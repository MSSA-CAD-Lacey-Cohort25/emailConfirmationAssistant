using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailConfirmationServer.Models
{
    public class SheetUpload
    {
        
        public int Id { get; set; }
   
        public List<Person> People { get; set; }

        public string UserId { get; set; }

        public SheetUpload(int id, string userId)
        {
            Id = id;
            UserId = userId; 
            People = null;
        }

        public SheetUpload()
        {
            Id = 0;
            UserId = null;
            People = null;
        }
    }
}