using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmailConfirmationServer.Models
{
    public class SheetUpload
    {
        
        public int Id { get; set; }

        public string UserId { get; set; }

        public String Title { get; set; }

        public  ICollection<Person> People { get; set; }

        public SheetUpload()
        {
        }

        public SheetUpload(string userId, string title)
        {            
            UserId = userId;
            Title = title;
            People = null;
        }        
    }
}