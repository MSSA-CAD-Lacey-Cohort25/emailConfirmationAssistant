using EmailConfirmationServerCore.Models;
using Excel.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;


namespace EmailConfirmationServer.Models
{
    //this class does all the work for reading the spreadsheet after it is uploaded
    public class Spreadsheet
    {
        public List<Person> People { get; set; }

        public string FilePath { get; set; }

        public Spreadsheet(string filepath)
        {
            if (!File.Exists(filepath))
                throw new ArgumentNullException("The files are not in the computer.");
            
            FilePath = filepath;
            People = new List<Person>();
            ReadSheet();
        }
    
        private void ReadSheet()
        {
            var excelConverter = new ExcelConverter();
            var rows = excelConverter.Read<PersonRow>(FilePath);
            
            if (rows == null)
                throw new FileFormatException(
                    "There was an error reading the file. " +
                    "Make sure to check the property names in the row model match the column names in the spreadhseet.");

            convertRowsToPeople(rows);
        }

        private void convertRowsToPeople(IEnumerable<PersonRow> rows)
        {
            int personId = 1; 
            foreach(var row in rows)
            {
                if (row == null)
                    throw new FileFormatException(
                        "There was an error reading a row. " +
                        "Make sure to check the property names in the row model match the column names in the spreadhseet.");

                People.Add(convertRowToPerson(row));                
            }
        }

        private Person convertRowToPerson(PersonRow row)
        {
            Person person = new Person();
            person.Emails = new List<Email>();
            
            person.FirstName = row.FirstName;
            person.LastName = row.LastName;
            person.Emails.Add(new Email(row.Outlook));
            person.Emails.Add(new Email(row.StMartin));

            return person; 
        }             
    }
}
