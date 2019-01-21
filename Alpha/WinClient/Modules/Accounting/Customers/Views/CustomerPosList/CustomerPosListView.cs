using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
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
    public partial class CustomerPosListView : /*System.Windows.Forms.UserControl//*/BaseSimpleListView, ICustomerPosListView
    {
        public CustomerPosListView()
        {
            InitializeComponent();
            Initialize(gridControlOfServicesListView, gridViewOfServicesListView, "ID", true);
        }

        [CreateNew]
        public new CustomerPosListViewPresenter Presenter
        {
            set => base.Presenter = value;
            get => (CustomerPosListViewPresenter)base.Presenter;
        }
        
        public DataTable Services { set => GetBaseSimpleListViewMapper.DomainToView(value, gridViewOfServicesListView, "Service"); }
        public DataTable Contractors { set => GetBaseSimpleListViewMapper.DomainToView(value, gridViewOfServicesListView, "Contractor"); }
        public DataTable Counters { set => GetBaseSimpleListViewMapper.DomainToView(value, gridViewOfServicesListView, "Counter"); }

        public DomService Service => GetBaseSimpleListViewMapper.ViewToDomain<DomService>(gridViewOfServicesListView, "Service");
        public DomContractor Contractor => GetBaseSimpleListViewMapper.ViewToDomain<DomContractor>(gridViewOfServicesListView, "Contractor");
        public string CounterID => GetBaseSimpleListViewMapper.ViewToDomainID(gridViewOfServicesListView, "Counter");

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
        public decimal Rate => GetBaseSimpleListViewMapper.ViewToDomainSimpleType<decimal>(gridViewOfServicesListView, "Rate");

        /// <summary>
        /// Признак разрешенности редактирования
        /// </summary>
        public bool IsEditingAllowed { set => gridViewOfServicesListView.OptionsBehavior.Editable = value; }

        public bool ShowAll => showAllCheckBox.Checked;

        /// <summary>
        /// Подключить общий обработчик изменений
        /// </summary>
        public void BindActivate(EventHandler handler)
        {
            Presenter.BindChangeHandlers(Controls, handler);
            showAllCheckBox.CheckedChanged -= handler;
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

        private void gridViewOfServicesListView_ShownEditor(object sender, EventArgs e)
        {
            ColumnView _cv = (ColumnView)sender;
            if(_cv.FocusedColumn.FieldName == "Counter")
            {
                string _serviceId = gridViewOfServicesListView.GetFocusedRowCellValue("Service").ToString();
                if (!string.IsNullOrEmpty(_serviceId))
                {
                    LookUpEdit _edit = (LookUpEdit)_cv.ActiveEditor;
                    DataTable _dt = (DataTable)_edit.Properties.DataSource;
                    DataView _dv = new DataView(_dt)
                    {
                        RowFilter = $"ServiceID = {_serviceId}"
                    };
                    _edit.Properties.DataSource = _dv;
                }
            }
        }

        private void ServiceRepositoryItemLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            gridViewOfServicesListView.PostEditor();
            gridViewOfServicesListView.SetFocusedRowCellValue("Counter", null);
        }

        private void showAllCheckBox_Click(object sender, EventArgs e)
        {
            Presenter.RefreshList();
        }
    }
}