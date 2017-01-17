using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Intermediaries
{
    public interface IListView : IBaseSimpleListView
    {
        /// <summary>
        /// Наименование
        /// </summary>
        string IntermediaryName
        {
            get;
        }

        /// <summary>
        /// Шифр
        /// </summary>
        string Code
        {
            get;
        }

        /// <summary>
        /// Процент
        /// </summary>
        decimal Rate { get; }
    }
}