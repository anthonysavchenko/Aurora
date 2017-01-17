using Microsoft.Practices.CompositeUI;

using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Infrastructure.Shell
{
    /// <summary>
    /// Интерфейс доступа к сервису работы с доменами, умеющими работать с датамаппером
    /// </summary>
    public class DomainWithDataMapperHelperService : Taumis.Infrastructure.Library.Services.DomainWithDataMapperHelperService, Taumis.EnterpriseLibrary.Win.Services.IDomainWithDataMapperHelperService
    {
        /// <summary>
        /// Сервис доступа к слою преобразователей данных
        /// </summary>
        [ServiceDependency]
        override public IDataMapperService DatMapServ
        {
            set
            {
                base.DatMapServ = value;
            }
        }
    }
}