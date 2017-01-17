using System.Collections.Generic;

using Taumis.Domain.Common;
using Taumis.Domain.DocLine;

using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Domain.Doc
{
    /// <summary>
    /// Интерфейс базового документа.
    /// </summary>
    public interface IDoc : IDocDateTime, IArchival
    {
        /// <summary>
        /// Номер документа.
        /// </summary>
        string DocNumber { get; set; }

        /// <summary>
        /// Автор документа - пользователь.
        /// </summary>
        object Author { get; set; }

        /// <summary>
        /// Владелец документа.
        /// </summary>
        object Owner { get; set; }

        /// <summary>
        /// Позиции (строки) документа.
        /// </summary>
        Dictionary<string, IDocLine> Lines { get; }
    }
}