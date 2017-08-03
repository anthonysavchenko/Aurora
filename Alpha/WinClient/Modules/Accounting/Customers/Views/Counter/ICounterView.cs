using System;
using System.Data;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;
using DomService = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Service;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views.Counter
{
    public interface ICounterView : IBaseSimpleListView
    {
        /// <summary>
        /// Номер счетчика
        /// </summary>
        string Number { get; }

        DataTable Services { set; }
        DomService Service { get; }
        
        /// <summary>
        /// Подключить общий обработчик изменений
        /// </summary>
        void BindActivate(EventHandler handler);

        /// <summary>
        /// Отключить общий обработчик изменений
        /// </summary>
        void BindDeactivate(EventHandler handler);
    }
}