using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailConfirmationServer.Models
{
    public class SheetUpload
    {
        
        public int Id { get; set; }

        public String Title { get; set; }
   
        public List<Person> People { get; set; }

        public string UserId { get; set; }
        
        public SheetUpload(int id, string userId, string title)
        {
            Id = id;
            UserId = userId;
            Title = title;
            People = null;
        }

        public SheetUpload()
        {
            Id = 0;
            UserId = null;
            Title = null;
            People = null;
        }
    }
}