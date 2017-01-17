using Microsoft.Practices.CompositeUI;
using Taumis.Alpha.Infrastructure.Library.Services;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Library
{
    /// <summary>
    /// Единица работы
    /// </summary>
    public class UnitOfWork : BaseUnitOfWork, ITestUnitOfWork
    {
        /// <summary>
        /// Сервис доступа к слою преобразователей данных.
        /// </summary>
        [ServiceDependency]
        public override IDataMapperService DatMapServ { protected get; set; }
        /*
        /// <summary>
        /// Сервис для работы с транзакциями БД
        /// </summary>
        [ServiceDependency]
        public override IDbTransactionService DbTransactionService { protected get; set; }*/
    }
}