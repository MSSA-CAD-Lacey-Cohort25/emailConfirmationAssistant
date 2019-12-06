using EmailConfirmationServerCore.Models;
using Excel.IO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;


namespace EmailConfirmationServer.Models
{
    //this class does all the work for reading the spreadsheet after it is uploaded
    public class ReadSheet : IReadSheet
    {
        public List<Person> People { get; set; }

        public string FilePath { get; set; }

        public ReadSheet()
        {
            People = new List<Person>();
        }
   
        public SheetUpload toSheetUpload(IFormFile file, string userId)
        {
            if( file == null)
            {
                throw new ArgumentNullException("file", "Argument file cannot be null.");
            }

            MemoryStream memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);

            var people = convertToPersonCollection(memoryStream);

            SheetUpload upload = new SheetUpload(userId, file.FileName);
            upload.People = people;
            return upload;
        }

        private ICollection<Person> convertToPersonCollection(Stream inputStream)
        {
            var excelConverter = new ExcelConverter();
            var rows = excelConverter.Read<PersonRow>(inputStream);

            if (rows == null)
                throw new FileFormatException(
                    "There was an error reading the file. " +
                    "Make sure to check the property names in the row model match the column names in the spreadhseet.");

            return convertRowsToPeople(rows);
        }

        private ICollection<Person> convertRowsToPeople(IEnumerable<PersonRow> rows)
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

            return People;
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
