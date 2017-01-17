using System;

using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Domain.Common
{
    /// <summary>
    /// Интерфейс документа, имеющего дату создания
    /// </summary>
    public interface IDocDateTime : IDomainObject
    {
        /// <summary>
        /// ДатаВремя документа
        /// </summary>
        DateTime DocDateTime { get; set; }
    }
}