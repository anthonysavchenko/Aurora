using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views.CounterValue
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
        /// Флаг показания по норме
        /// </summary>
        bool ByNorm { get; }

        /// <summary>
        /// Определяет доступность пользователю кнопок редактирования
        /// </summary>
        bool NavigationButtonsEnabled { set; }

        /// <summary>
        /// Подключить общий обработчик изменений
        /// </summary>
        void BindActivate(EventHandler handler);
    }
}