using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Contractors
{
    public interface IListView : IBaseSimpleListView
    {
        /// <summary>
        /// Наименование
        /// </summary>
        string ContractorName { get; }

        /// <summary>
        /// Шифр
        /// </summary>
        string Code { get; }

        /// <summary>
        /// Контактная информация
        /// </summary>
        string ContactInfo { get; }
    }
}