namespace Taumis.Alpha.Infrastructure.Library.Services.FormComparer.Models
{
    public enum CustomerDiffType
    {
        PrevDateTime,

        SingleCounterValue,
        DoubleCounterDayValue,
        DoubleCounterNightValue,

        SingleAndDoubleCounter,
        DoubleAndSingleCounter,

        NoPrintFormCustomer,
        NoFillFormCustomer,
    }
}
