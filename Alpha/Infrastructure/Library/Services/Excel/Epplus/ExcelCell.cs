using Taumis.Alpha.Infrastructure.Interface.Services.Excel;

namespace Taumis.Alpha.Infrastructure.Library.Services.Excel.Epplus
{
    public class ExcelCell : IExcelCell
    {
        private OfficeOpenXml.ExcelRange _cell;

        public ExcelCell(OfficeOpenXml.ExcelRange cell)
        {
            _cell = cell;
        }

        public string Value
        {
            get => _cell.Value?.ToString() ?? string.Empty;
            set => _cell.Value = value;
        }

        public void SetValue<TValue>(TValue value)
        {
            _cell.Value = value;
        }

        public bool TryGetValue<TValue>(out TValue value)
        {
            bool _result = true;
            try
            {
                value = _cell.GetValue<TValue>();
            }
            catch
            {
                _result = false;
                value = default(TValue);
            }

            return _result;
        }
    }
}