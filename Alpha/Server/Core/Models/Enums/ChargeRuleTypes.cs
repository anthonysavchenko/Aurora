namespace Taumis.Alpha.Server.Core.Models.Enums
{
    /// <summary>
    /// Правила начислений по услугу
    /// </summary>
    public enum ChargeRuleTypes : byte
    {
        /// <summary>
        /// Фиксированное начиление
        /// </summary>
        FixedRate,

        /// <summary>
        /// Начисление за квадратный метр площади
        /// </summary>
        SquareRate,

        /// <summary>
        /// Начисление по тарифу за количество жильцов
        /// </summary>
        ResidentsRate,

        /// <summary>
        /// Начисление по однотарифному счетчику с общедомовым начислением
        /// </summary>
        SingleRateCounter,

        /// <summary>
        /// Начисление по двухтарифному счетчику с общедомовым начислением
        /// </summary>
        DoubleRateCounter
    }
}
