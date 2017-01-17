using System.Data;

namespace Taumis.EnterpriseLibrary.Infrastructure.Common.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы с транзакциями БД
    /// </summary>
    public interface IDbTransactionService
    {
        /// <summary>
        /// Начинает новую транзакцию
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Фиксирует текущую транзакцию
        /// </summary>
        void Commit();

        /// <summary>
        /// Откатывает текущую транзакцию
        /// </summary>
        void Rollback();
    }
}