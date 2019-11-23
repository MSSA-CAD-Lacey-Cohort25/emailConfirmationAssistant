using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmailConfirmationServerCore.Models
{
    public interface ICreateSheet
    {
        public void Create<T>(IEnumerable<T> rows, string path);

        public void Create<T>(IEnumerable<T> rows, Stream outputStream);

    }
}
