namespace Taumis.Alpha.Infrastructure.Interface.Enums
{
    /// <summary>
    /// Тип правила начисления льготы
    /// </summary>
    public enum BenefitRuleType : byte
    {
        /// <summary>
        /// 50% по норме площади в зависимости от количества жильцов
        /// </summary>
        FiftyPercentBySquare,

        /// <summary>
        /// Фиксированный процент
        /// </summary>
        FixedPercent
    }
}
