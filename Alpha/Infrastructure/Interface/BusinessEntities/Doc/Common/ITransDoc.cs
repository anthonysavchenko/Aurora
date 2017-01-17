using Taumis.Domain.Common;

namespace Taumis.Domain.Doc
{
    /// <summary>
    /// Интерфейс документа, который должен уметь проводиться
    /// </summary>
    public interface ITransDoc : IDoc, ITransactionOwner
    {
    }
}