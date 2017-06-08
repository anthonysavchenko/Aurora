using System;

namespace Taumis.Alpha.Infrastructure.Interface.Services.Excel
{
    public interface IExcelWorkbook : IDisposable
    {
        IExcelWorksheet Worksheet(string name);
        IExcelWorksheet Worksheet(int position);

        void Save();
        void SaveAs(string file);
    }
}
