namespace Taumis.Alpha.Infrastructure.Interface.Enums
{
    /// <summary>
    /// Тип правила начисления сборов за услугу
    /// </summary>
    public enum ChargeRuleType : byte
    {
        /// <summary>
        /// Фиксированное начиление
        /// </summary>
        FixedRate = 0,

        /// <summary>
        /// Начисление за квадратный метр площади
        /// </summary>
        SquareRate = 1,

        /// <summary>
        /// Начисление по тарифу за количество жильцов
        /// </summary>
        ResidentsRate = 2,

        /// <summary>
        /// Начисление по счетчику
        /// </summary>
        CounterRate = 3,

        /// <summary>
        /// Начиление за квадратный метр только за содержание общедомового имещества (СОД)
        /// </summary>
        PublicPlaceAreaRate = 4,

        /// <summary>
        /// Начисление банковской коммисси в виде процента от расходов СОД
        /// </summary>
        PublicPlaceBankCommission = 5,

        /// <summary>
        /// Начиление за квадратный метр только за содержание общедомового имещества (СОД) по объему потребленного ресурса услуги
        /// </summary>
        PublicPlaceVolumeAreaRate = 9
    }
}
