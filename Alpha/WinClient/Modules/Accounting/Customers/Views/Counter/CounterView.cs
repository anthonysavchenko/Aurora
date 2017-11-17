using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;
using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views.Counter
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
            set => base.Presenter = value;
            get => (CounterViewPresenter)base.Presenter;
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
        /// Номер счетчика
        /// </summary>
        public string Number => GetBaseSimpleListViewMapper.ViewToDomain(counterGridView, "Number");

        public DataTable Services { set => GetBaseSimpleListViewMapper.DomainToView(value, counterGridView, "Service"); }

        public Service Service => GetBaseSimpleListViewMapper.ViewToDomain<Service>(counterGridView, "Service");

        /// <summary>
        /// Подключить общий обработчик изменений
        /// </summary>
        public void BindActivate(EventHandler handler)
        {
            Presenter.BindChangeHandlers(handler);
        }

        #endregion

        private void counterGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Presenter.OnRowChanged(counterGridView.GetFocusedRowCellDisplayText("ID"));
        }
    }
}