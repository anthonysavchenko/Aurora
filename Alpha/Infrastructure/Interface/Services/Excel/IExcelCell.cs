namespace Taumis.Alpha.Infrastructure.Interface.Services.Excel
{
    public interface IExcelCell
    {
        void SetValue<TValue>(TValue value);
        string Value { get; set; }
        bool TryGetValue<TValue>(out TValue value);
    }
}
