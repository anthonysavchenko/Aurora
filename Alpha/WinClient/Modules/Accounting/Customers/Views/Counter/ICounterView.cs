using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views.Counter
{
    public interface ICounterView : IBaseSimpleListView
    {
        /// <summary>
        /// Номер счетчика
        /// </summary>
        string Number { get; }
        
        /// <summary>
        /// Тариф
        /// </summary>
        decimal Rate { get; }

        /// <summary>
        /// Определяет доступность пользователю кнопок редактирования
        /// </summary>
        bool NavigationButtonsEnabled { set; }

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