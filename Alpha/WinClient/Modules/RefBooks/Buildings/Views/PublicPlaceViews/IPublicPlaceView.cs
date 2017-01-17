using System;
using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.PublicPlaceViews
{
    public interface IPublicPlaceView : IBaseSimpleListView
    {
        /// <summary>
        /// Таблица с данными услуг
        /// </summary>
        DataTable Services { set; }

        /// <summary>
        /// Услуга
        /// </summary>
        Service Service { get; }

        /// <summary>
        /// Номер счетчика
        /// </summary>
        decimal Area { get; }
        
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