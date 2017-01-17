using Taumis.EnterpriseLibrary.Win;
using DomContainer = Taumis.Domain.Cargo.Container;

namespace Taumis.Domain.Common
{
    /// <summary>
    /// Интерфейс документа/операции, относящегося к КТК
    /// </summary>
    public interface IContainerOwner : IDomainObject
    {
        /// <summary>
        /// Контейнер
        /// </summary>
        DomContainer Container { get; }
    }
}