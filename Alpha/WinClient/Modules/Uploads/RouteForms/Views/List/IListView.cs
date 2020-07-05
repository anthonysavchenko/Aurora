using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView.BaseMultipleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.RouteForms.Views.List
{
    public interface IListView : IBaseMultipleListView
    {
        /// <summary>
        /// Начальная дата периода
        /// </summary>
        DateTime Since { get; set; }
        
        /// <summary>
        /// Конечная дата периода
        /// </summary>
        DateTime Till { get; set; }
    }
}
