using ClosedXML.Excel;
using Taumis.Alpha.Infrastructure.Interface.Services.Excel;

namespace Taumis.Alpha.Infrastructure.Library.Services.Excel.ClosedXML
{
    public class ExcelCell : IExcelCell
    {
        private IXLCell _xlCell;

        public ExcelCell(IXLCell xlCell)
        {
            _xlCell = xlCell;
        }

        public string Value
        {
            get => _xlCell.GetString();
            set => _xlCell.SetValue(value);
        }

        public void SetValue<TValue>(TValue value)
        {
            _xlCell.SetValue(value);
        }

        public bool TryGetValue<TValue>(out TValue value)
        {
            return _xlCell.TryGetValue(out value);
        }
    }
}
