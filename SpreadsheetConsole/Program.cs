using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.IO;
using System.Linq;

namespace SpreadsheetConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir = "C:\\Users\\izmac\\Source\\Repos\\emailConfirmationAssistant-1\\SpreadsheetConsole";
            string path = Path.Combine(dir, "TestSheet.xlsx");

            if (File.Exists(path))
            {
                using(SpreadsheetDocument doc = SpreadsheetDocument.Open(path, false))
                {
                    var workbook = doc.WorkbookPart.Workbook;
                    var sheets = workbook.Descendants<Sheet>();
                    var sheet = sheets.FirstOrDefault();
                    SharedStringTable sharedStrings = doc.WorkbookPart.SharedStringTablePart.SharedStringTable;

                    var rows = from row in sheet.Descendants<Row>()
                               select row;

                    foreach(var row in rows)
                    {
                        var cells = from cell in row.Descendants<Cell>()
                                    select cell.CellValue.InnerText;

                        Console.WriteLine(cells);

                    }
                }
            }




        }
    }
}
