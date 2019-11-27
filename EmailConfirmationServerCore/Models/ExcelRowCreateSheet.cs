using EmailConfirmationServer.Models;
using Excel.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmailConfirmationServerCore.Models
{
    public class ExcelRowCreateSheet : ICreateSheet
    {

        public virtual void WriteToStream<T>(IEnumerable<T> rows, Stream outputStream)
        {            
            var people = rows as IEnumerable<Person>;

            if (people == null)
                throw new ArgumentException("Rows type must implement IExcelRow", "rows");

            var excelRows = ExcelRowHelpers.convertToPersonRows(people);
            convertToExcel(excelRows, outputStream);
        }

        protected void convertToExcel(IEnumerable<IExcelRow> rows, Stream outputStream)
        {
            var excelConverter = new ExcelConverter();
            excelConverter.Write(rows, outputStream);
        }
    }
}
