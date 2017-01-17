namespace Taumis.Alpha.Server.Core.Models.Enums
{
    /// <summary>
    /// Правила предоставления льготы
    /// </summary>
    public enum BenefitRuleTypes : byte
    {
        /// <summary>
        /// 50% по норме площади в зависимости от количества жильцов
        /// </summary>
        FiftyPercentBySquare = 0,

        /// <summary>
        /// Фиксированный процент
        /// </summary>
        FixedPercent = 1
    }
}
