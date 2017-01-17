using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Streets.Views.List
{
    public interface IListView : IBaseSimpleListView
    {
        /// <summary>
        /// Наименование
        /// </summary>
        string StreetName
        {
            get;
        }
    }
}