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
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string UserId { get; set; }

        public String Title { get; set; }

        public  ICollection<Person> People { get; set; }

        public SheetUpload(int id, string userId, string title)
        {
            Id = id;
            UserId = userId;
            Title = title;
            People = null;
        }

        public SheetUpload()
        {            
            UserId = null;
            Title = null;
            People = null;
        }
    }
}