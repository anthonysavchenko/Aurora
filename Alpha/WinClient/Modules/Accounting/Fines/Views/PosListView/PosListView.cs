using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Fines.Views.PosListView
{
    [SmartPart]
    public partial class PosListView : BaseSimpleListView, IPosListView
    {
        /// <summary>
        /// Презентер
        /// </summary>
        [CreateNew]
        public new PosListViewPresenter Presenter
        {
            set => base.Presenter = value;
            get => (PosListViewPresenter)base.Presenter;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public PosListView()
        {
            InitializeComponent();
            Initialize(posGridControl, posGridView, "ID", true);
        }

        #region Implementation of ICounterView

        public decimal Value => GetBaseSimpleListViewMapper.ViewToDomainSimpleType<decimal>(posGridView, "Value");
        public Customer Customer => GetBaseSimpleListViewMapper.ViewToDomain<Customer>(posGridView, "Customer");
        public DataTable Customers { set => GetBaseSimpleListViewMapper.DomainToView(value, posGridView, "Customer"); }

        /// <summary>
        /// Подключить общий обработчик изменений
        /// </summary>
        public void BindActivate(EventHandler handler)
        {
            Presenter.BindChangeHandlers(handler);
        }

        #endregion
    }
}