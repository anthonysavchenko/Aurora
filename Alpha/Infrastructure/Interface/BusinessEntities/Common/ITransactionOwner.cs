namespace Taumis.Domain.Common
{
    /// <summary>
    /// Интерфейс базового документа, имеющего проводки
    /// </summary>
    public interface ITransactionOwner : IDocDateTime
    {
        /// <summary>
        /// Признак утвержденности документа
        /// </summary>
        bool IsTransacted { get; set; }
    }
}