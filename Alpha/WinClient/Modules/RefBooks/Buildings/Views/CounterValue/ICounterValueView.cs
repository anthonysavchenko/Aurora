using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.CounterValue
{
    public interface ICounterValueView : IBaseSimpleListView
    {
        /// <summary>
        /// Период
        /// </summary>
        DateTime Period { get; }

        /// <summary>
        /// Значение/показание
        /// </summary>
        decimal Value { get; }

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