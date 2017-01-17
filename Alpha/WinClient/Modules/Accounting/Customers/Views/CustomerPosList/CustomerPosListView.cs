using DevExpress.XtraGrid.Views.Grid;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using System.Drawing;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;
using DomContractor = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Contractor;
using DomService = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Service;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers
{
    [SmartPart]
    public partial class CustomerPosListView : BaseSimpleListView, ICustomerPosListView
    {
        public CustomerPosListView()
        {
            InitializeComponent();
            Initialize(gridControlOfServicesListView, gridViewOfServicesListView, "ID", true);
        }

        [CreateNew]
        public new CustomerPosListViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
            get
            {
                return (CustomerPosListViewPresenter)base.Presenter;
            }
        }

        /// <summary>
        /// Услуги
        /// </summary>
        public DataTable Services
        {
            set
            {
                GetBaseSimpleListViewMapper.DomainToView(value, gridViewOfServicesListView, "Service");
            }
        }

        /// <summary>
        /// Услуга
        /// </summary>
        public DomService Service
        {
            get
            {
                return GetBaseSimpleListViewMapper.ViewToDomain<DomService>(gridViewOfServicesListView, "Service");
            }
        }

        /// <summary>
        /// Contractors
        /// </summary>
        public DataTable Contractors
        {
            set
            {
                GetBaseSimpleListViewMapper.DomainToView(value, gridViewOfServicesListView, "Contractor");
            }
        }

        /// <summary>
        /// Contractor
        /// </summary>
        public DomContractor Contractor
        {
            get
            {
                return GetBaseSimpleListViewMapper.ViewToDomain<DomContractor>(gridViewOfServicesListView, "Contractor");
            }
        }

        /// <summary>
        /// Since
        /// </summary>
        public DateTime Since
        {
            get
            {
                DateTime _since = GetBaseSimpleListViewMapper.ViewToDomainSimpleType<DateTime>(gridViewOfServicesListView, "Since");
                return new DateTime(_since.Year, _since.Month, 1);
            }
        }

        /// <summary>
        /// Till
        /// </summary>
        public DateTime Till
        {
            get
            {
                DateTime _till = GetBaseSimpleListViewMapper.ViewToDomainSimpleType<DateTime>(gridViewOfServicesListView, "Till");
                return new DateTime(_till.Year, _till.Month, 1);
            }
        }

        /// <summary>
        /// Rate
        /// </summary>
        public decimal Rate
        {
            get
            {
                return GetBaseSimpleListViewMapper.ViewToDomainSimpleType<decimal>(gridViewOfServicesListView, "Rate");
            }
        }

        /// <summary>
        /// Признак разрешенности редактирования
        /// </summary>
        public bool IsEditingAllowed
        {
            set
            {
                gridViewOfServicesListView.OptionsBehavior.Editable = value;
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

        /// <summary>
        /// Отображает серым цветом услуги, которые уже не предоставляются
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewOfServicesListView_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                object _till = ((GridView)sender).GetRowCellValue(e.RowHandle, "Till");

                if (_till != null && _till != DBNull.Value && Presenter.IsLessThanFirstUncharged((DateTime)_till))
                {
                    e.Appearance.BackColor = Color.FromArgb(213, 213, 213);
                }
            }
        }

        private void gridViewOfServicesListView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Presenter.OnRowChanged(gridViewOfServicesListView.GetFocusedRowCellDisplayText("ID"));
        }
    }
}