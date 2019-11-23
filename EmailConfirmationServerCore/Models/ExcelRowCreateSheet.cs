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
        public void Create<T>(IEnumerable<T> rows, string path)
        {
            var excelRows = rows as IEnumerable<IExcelRow>;
            if (excelRows == null)
                throw new ArgumentException("rows type must implement IExcelRow", "rows");

            CreateExcelRowSheet(excelRows, path);
        }

        public void Create<T>(IEnumerable<T> rows, Stream outputStream)
        {
            var excelRows = rows as IEnumerable<IExcelRow>;
            if (excelRows == null)
                throw new ArgumentException("rows type must implement IExcelRow", "rows");

            CreateExcelRowSheet(excelRows, outputStream);
        }

        protected void CreateExcelRowSheet(IEnumerable<IExcelRow> rows, string path)
        {                      
            var excelConverter = new ExcelConverter();
            excelConverter.Write(rows, path);
        }

        protected void CreateExcelRowSheet(IEnumerable<IExcelRow> rows, Stream outputStream)
        {
            var excelConverter = new ExcelConverter();
            excelConverter.Write(rows, outputStream);
        }
    }
}
