using EmailConfirmationServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmailConfirmationServerCore.Models
{
    public class ExcelRowHelpers
    {
        public static IEnumerable<Person> convertRowsToPeople(IEnumerable<PersonRow> rows)
        {
            var people = new List<Person>();
            int personId = 1;

            foreach (var row in rows)
            {
                if (row == null)
                    throw new FileFormatException(
                        "There was a null person row. Make sure to check the property names in the row model match the column names in the spreadhseet.");

                people.Add(convertRowToPerson(row));
            }

            return people; 
        }

        private static Person convertRowToPerson(PersonRow row)
        {
            Person person = new Person();
            person.Emails = new List<Email>();

            person.FirstName = row.FirstName;
            person.LastName = row.LastName;
            person.Emails.Add(new Email(row.Outlook));
            person.Emails.Add(new Email(row.StMartin));

            return person;
        }

        public static IEnumerable<PersonRow> convertToPersonRows(IEnumerable<Person> people)
        {
            var personRows = new List<PersonRow>();

            foreach(var person in people)
            {
                personRows.Add(converToPersonRow(person));
            }
            
            return personRows; 
        }

        public static PersonRow converToPersonRow(Person person)
        {
            if( person == null)
            {
                return createEmptyRow();
            }

            var personRow = new PersonRow();

            personRow.SheetName = "Sheet1";
            personRow.FirstName = person.FirstName;
            personRow.LastName = person.LastName;
            var emails = person.Emails.ToList();
            personRow.Outlook = emails[0].EmailAddress;
            personRow.StMartin = emails[1].EmailAddress;

            return personRow; 
        }

        private static PersonRow createEmptyRow()
        {
            var personRow = new PersonRow();
            personRow.SheetName = "Sheet1";
            personRow.FirstName = String.Empty;
            personRow.LastName = String.Empty;
            personRow.Outlook = String.Empty;
            personRow.StMartin = String.Empty;

            return personRow;
        }

    }
}
