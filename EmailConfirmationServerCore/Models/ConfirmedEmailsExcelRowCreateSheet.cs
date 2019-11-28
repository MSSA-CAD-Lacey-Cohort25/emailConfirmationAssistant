using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using EmailConfirmationServer.Models;
using Excel.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;


namespace EmailConfirmationServerCore.Models
{
    public class ConfirmedEmailsExcelRowCreateSheet : ExcelRowCreateSheet
    {
        public override void WriteToStream<T>(IEnumerable<T> rows, Stream outputStream)
        {
            base.WriteToStream(rows, outputStream);            
            HighlightEmails(rows as IEnumerable<Person>, outputStream);
           
        }

        private void HighlightEmails(IEnumerable<Person> people, Stream outputStream)
        {
            if (people == null || outputStream == null)
                return;

            using(XLWorkbook workbook = new XLWorkbook(outputStream))
            {               
                var workSheet = workbook.Worksheet(1);

                //Transpose rows to columns. This is required because opening the sheet as an XLWorkbooks switches the rows to columns.
                tranposeUsedRange(workSheet);

                var firstRow = workSheet.FirstRow();
                var rowsAfterHeader = workSheet.Rows().Skip(1);
                var rowsAndEmails = people.Zip(rowsAfterHeader, (p, r) => new { Emails = p.Emails, Row = r });

                foreach (var emailRow in rowsAndEmails)
                {
                    var emails = emailRow.Emails;
                    var row = emailRow.Row;
                    foreach (var email in emails)
                    {
                        highlightEmailCell(email, row);
                    }
                }
               
                workSheet.Columns().AdjustToContents();            
                workbook.Save();
            }
        }

        private void tranposeUsedRange(IXLWorksheet worksheet)
        {
            var rangeTable = worksheet.RangeUsed();
            rangeTable.Transpose(XLTransposeOptions.MoveCells);
        }

        private void highlightEmailCell(Email email, IXLRow row)
        {
            try
            {
                if (email.IsConfirmed)
                {
                    IXLCell cell;

                    if (email.EmailAddress.Contains("@outlook.com"))
                    {
                        cell = row.Cells().ToList()[2];
                    }
                    else
                    {
                        cell = row.Cells().ToList()[3];
                    }

                    highlightCell(cell, XLColor.LightGreen);
                }
            }
            catch(NullReferenceException) { return; }
            catch(ArgumentOutOfRangeException) { return; }            
        }

        private void highlightCell(IXLCell cell, XLColor color)
        {
            cell.Style.Fill.BackgroundColor = color;
        }
    }
}
