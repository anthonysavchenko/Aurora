using System;
using System.Data;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.Counter
{
    public interface ICounterView : IBaseSimpleListView
    {
        /// <summary>
        /// Таблица с данными услуг
        /// </summary>
        DataTable UtilityServices { set; }

        /// <summary>
        /// Услуга
        /// </summary>
        UtilityService UtilityService { get; }

        /// <summary>
        /// Номер счетчика
        /// </summary>
        string CounterNumber { get; }

        /// <summary>
        /// Коэффициент
        /// </summary>
        byte Coefficient { get; }

        DateTime? CheckedSince { get; }

        DateTime? CheckedTill { get; }

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
