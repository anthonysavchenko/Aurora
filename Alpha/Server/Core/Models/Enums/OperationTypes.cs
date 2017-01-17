namespace Taumis.Alpha.Server.Core.Models.Enums
{
    /// <summary>
    /// Типы операций
    /// </summary>
    public enum OperationTypes : int
    {
        /// <summary>
        /// Ежемесячное начисление
        /// </summary>
        Charge = 0,

        /// <summary>
        /// Платеж
        /// </summary>
        Payment = 1,

        /// <summary>
        /// Корректировка платежа
        /// </summary>
        PaymentCorrection = 2,

        /// <summary>
        /// Предоставление льготы
        /// </summary>
        Benefit = 3,

        /// <summary>
        /// Корректировка переплаты
        /// </summary>
        OverpaymentCorrection = 4,

        /// <summary>
        /// Внесение переплаты
        /// </summary>
        Overpayment = 5,

        /// <summary>
        /// Дополнительное начисление
        /// </summary>
        Recharge = 6,

        /// <summary>
        /// Дополнительное предоставление льготы
        /// </summary>
        Rebenefit = 7,

        /// <summary>
        /// Корректировка начисления
        /// </summary>
        ChargeCorrection = 8,

        /// <summary>
        /// Корректировка предоставления льготы
        /// </summary>
        BenefitCorrection = 9
    }
}
