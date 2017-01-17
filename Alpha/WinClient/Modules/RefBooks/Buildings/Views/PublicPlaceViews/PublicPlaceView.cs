using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.PublicPlaceViews
{
    [SmartPart]
    public partial class PublicPlaceView : BaseSimpleListView, IPublicPlaceView
    {
        /// <summary>
        /// Презентер
        /// </summary>
        [CreateNew]
        public new PublicPlaceViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
            get
            {
                return (PublicPlaceViewPresenter)base.Presenter;
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public PublicPlaceView()
        {
            InitializeComponent();
            Initialize(counterGridControl, counterGridView, "ID", true);
        }

        #region Implementation of ICounterView

        /// <summary>
        /// Таблица с данными услуг
        /// </summary>
        public DataTable Services
        {
            set
            {
                GetBaseSimpleListViewMapper.DomainToView(value, counterGridView, "Service");
            }
        }

        /// <summary>
        /// Услуга
        /// </summary>
        public Service Service => GetBaseSimpleListViewMapper.ViewToDomain<Service>(counterGridView, "Service");

        /// <summary>
        /// Номер счетчика
        /// </summary>
        public decimal Area => GetBaseSimpleListViewMapper.ViewToDomainSimpleType<decimal>(counterGridView, "Area");

        /// <summary>
        /// Определяет доступность пользователю кнопок редактирования
        /// </summary>
        public bool NavigationButtonsEnabled
        {
            set
            {
                counterGridControl.EmbeddedNavigator.Enabled = value;
            }
        }

        /// <summary>
        /// Подключить общий обработчик изменений
        /// </summary>
        public void BindActivate(EventHandler handler)
        {
            Presenter.BindChangeHandlers(Controls, handler);
        }

        /// <summary>
        /// Отключить общий обработчик изменений
        /// </summary>
        public void BindDeactivate(EventHandler handler)
        {
            Presenter.UnBindChangeHandlers(Controls, handler);
        }

        #endregion
    }
}