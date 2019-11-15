using Excel.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailConfirmationServerCore.Models
{
    public class PersonRow : IExcelRow
    {
        public string SheetName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Outlook { get; set; }

        public string StMartin { get; set; }

        public PersonRow()
        {
            SheetName = "Sheet1";
        }

        public PersonRow(string sheetName)
        {
            SheetName = sheetName;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} : {Outlook}, {StMartin}";
        }
    }
}
