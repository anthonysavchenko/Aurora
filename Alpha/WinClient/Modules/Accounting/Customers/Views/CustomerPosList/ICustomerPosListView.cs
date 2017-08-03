using System;
using System.Data;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;
using DomContractor = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Contractor;
using DomService = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Service;
using DomPrivateCounter = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.PrivateCounter;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers
{
    /// <summary>
    /// Интерфейс вида списка позиций документа.
    /// </summary>
    public interface ICustomerPosListView : IBaseSimpleListView
    {
        /// <summary>
        /// Услуги
        /// </summary>
        DataTable Services { set; }

        /// <summary>
        /// Услуга
        /// </summary>
        DomService Service { get; }

        /// <summary>
        /// Список подрядчиков
        /// </summary>
        DataTable Contractors { set; }

        /// <summary>
        /// Подрядчик
        /// </summary>
        DomContractor Contractor { get; }

        /// <summary>
        /// Таблица приборов учета, связанных с абонентом
        /// </summary>
        DataTable Counters { set; }

        /// <summary>
        /// Прибор учета
        /// </summary>
        string CounterID { get; }

        /// <summary>
        /// Since
        /// </summary>
        DateTime Since { get; }

        /// <summary>
        /// Till
        /// </summary>
        DateTime Till { get; }

        /// <summary>
        /// Rate
        /// </summary>
        decimal Rate { get; }

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