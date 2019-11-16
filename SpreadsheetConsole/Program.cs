using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Excel.IO;
using System;
using System.IO;
using System.Linq;

namespace SpreadsheetConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //string dir = "C:\\Users\\izmac\\Source\\Repos\\emailConfirmationAssistant-1\\SpreadsheetConsole";
            string dir = "C:\\Users\\izmac\\Downloads";
            string path = Path.Combine(dir, "TestSheet.xlsx");

            if (File.Exists(path))
            {
                var excelConverter = new ExcelConverter();
                var people = excelConverter.Read<PersonRow>(path);

                foreach (var person in people)
                {
                    Console.WriteLine(person);
                }

            }
        }
    }

    public class PersonRow : IExcelRow
    {
        public string SheetName { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Outlook { get; set; }
        
        public string StMartin{ get; set; }

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
