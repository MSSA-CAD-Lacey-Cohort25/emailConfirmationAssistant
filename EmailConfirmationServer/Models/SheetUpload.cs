using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailConfirmationServer.Models
{
    public class SheetUpload
    {
        public int Id { get; set; }
   
        public List<Person> People { get; set; } = new List<Person>();
    }
}