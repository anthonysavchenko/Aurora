using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taumis.Alpha.Infrastructure.Interface.Services.Excel
{
    public interface IExcelService
    {
        IExcelWorkbook OpenWorkbook(string file);
        IExcelWorkbook CreateWorkbook();
    }
}
