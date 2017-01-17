using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.ServiceTypes
{
    public interface IListView : IBaseSimpleListView
    {
        /// <summary>
        /// Наименование
        /// </summary>
        string ServiceName
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
    }
}