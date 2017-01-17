using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.DataMappers.Oper
{
    public interface IPaymentCorrectionOperDataMapper : IDataMapper
    {
        /// <summary>
        /// Создает позиции для операции корректировки
        /// </summary>
        /// <param name="paymentOperId">ID операции корректировки</param>
        /// <returns>Список позиций</returns>
        PaymentCorrectionOper Create(int paymentOperId);
    }
}
