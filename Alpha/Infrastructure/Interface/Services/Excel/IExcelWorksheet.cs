namespace Taumis.Alpha.Infrastructure.Interface.Services.Excel
{
    public interface IExcelWorksheet
    {
        IExcelCell Cell(int row, int column);
        IExcelCell Cell(int row, string column);

        int GetRowCount();
    }
}