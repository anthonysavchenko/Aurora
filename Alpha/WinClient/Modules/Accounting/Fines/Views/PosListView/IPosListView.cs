using System;
using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Fines.Views.PosListView
{
    /// <summary>
    /// Интерфейс вида списка позиций документа.
    /// </summary>
    public interface IPosListView : IBaseSimpleListView
    {
        Customer Customer { get; }
        decimal Value { get; }
        DataTable Customers { set; }
        void BindActivate(EventHandler handler);
    }
}