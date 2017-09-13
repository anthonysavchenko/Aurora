using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers
{
    [SmartPart]
    public partial class RechargePercentCorrectionListView : BaseSimpleListView, IRechargePercentCorrectionListView
    {
        public RechargePercentCorrectionListView()
        {
            InitializeComponent();
            Initialize(gridControlOfServicesListView, gridViewOfServicesListView, "ID", true);
        }

        [CreateNew]
        public new RechargePercentCorrectionListViewPresenter Presenter
        {
            set => base.Presenter = value;
            get => (RechargePercentCorrectionListViewPresenter)base.Presenter;
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
    }
}