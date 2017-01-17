using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Bills.Views.List
{
    public interface IListView : IBaseListView
    {
        DateTime Since { get; set; }
        DateTime Till { get; set; }

        /// <summary>
        /// Получить ID текущего элемента списка.
        /// </summary>
        string GetCurrentItemId();
    }
}