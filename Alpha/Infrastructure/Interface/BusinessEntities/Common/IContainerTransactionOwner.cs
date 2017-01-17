namespace Taumis.Domain.Common
{
    /// <summary>
    /// Интерфейс документа/операции, имеющего проводки в регистрах учета КТК
    /// </summary>
    public interface IContainerTransactionOwner : ITransactionOwner, IContainerOwner
    {
    }
}