using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.Counter
{
    [SmartPart]
    public partial class CounterView : BaseSimpleListView, ICounterView
    {
        /// <summary>
        /// Презентер
        /// </summary>
        [CreateNew]
        public new CounterViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
            get
            {
                return (CounterViewPresenter)base.Presenter;
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public CounterView()
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
        public Service Service
        {
            get
            {
                return GetBaseSimpleListViewMapper.ViewToDomain<Service>(counterGridView, "Service");
            } 
        }
        
        /// <summary>
        /// Номер счетчика
        /// </summary>
        public string Number
        {
            get
            {
                return GetBaseSimpleListViewMapper.ViewToDomain(counterGridView, "Number");
            }
        }

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

        private void counterGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Presenter.OnRowChanged(counterGridView.GetFocusedRowCellDisplayText("ID"));
        }
    }
}