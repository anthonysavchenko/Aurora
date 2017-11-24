using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Fines.Views.Item
{
    public interface IItemView : IBaseItemView
    {
        DateTime Period { get; set; }
    }
}