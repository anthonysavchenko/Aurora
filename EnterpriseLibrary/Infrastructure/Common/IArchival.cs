namespace Taumis.EnterpriseLibrary.Win
{
    /// <summary>
    /// Интерфейс документа, имеющего признак архивированности
    /// </summary>
    public interface IArchival : IDomainObject
    {
        /// <summary>
        /// Признак архивированности документа
        /// </summary>
        bool IsArchived { get; set; }
    }
}