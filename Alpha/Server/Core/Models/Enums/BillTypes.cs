namespace Taumis.Alpha.Server.Core.Models.Enums
{
    /// <summary>
    /// Типы квитанций
    /// </summary>
    public enum BillTypes : byte
    {
        /// <summary>
        /// Ежемесячная
        /// </summary>
        Regular = 0,

        /// <summary>
        /// Долговая
        /// </summary>
        Debt = 1,

        /// <summary>
        /// За весь период обслуживания
        /// </summary>
        Total = 2
    }
}