using System.Windows.Forms;
using DevExpress.Data.Mask;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

//using BaseReportForGridView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.PaymentsAndCharges.Views.List
{
    [SmartPart]
    public partial class ListView : BaseReportForGridView, IListView
    {
        public ListView()
        {
            InitializeComponent();
            InitReportConponents(gridControlOfListView, gridViewOfListView);
        }

        [CreateNew]
        public new ListViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
            get
            {
                return (ListViewPresenter)base.Presenter;
            }
        }

        #region Implementation of IListView

        /// <summary>
        /// Дата начала периода отчета
        /// </summary>
        public DateTime SincePeriod
        {
            get
            {
                DateTime _temp = sinceDateEdit.DateTime;
                return new DateTime(_temp.Year, _temp.Month, 1);
            }
            set
            {
                sinceDateEdit.DateTime = value;
            }
        }

        /// <summary>
        /// Дата окончания периода отчета
        /// </summary>
        public DateTime TillPeriod
        {
            get
            {
                DateTime _temp = tillDateEdit.DateTime;
                return new DateTime(_temp.Year, _temp.Month, 1);
            }
            set
            {
                tillDateEdit.DateTime = value;
            }
        }

        /// <summary>
        /// Виды услуг
        /// </summary>
        public DataTable ServiceTypes
        {
            set
            {
                serviceTypeLookUpEdit.Properties.DataSource = value;
            }
        }

        /// <summary>
        /// Подуслуги
        /// </summary>
        public DataTable Services
        {
            set
            {
                serviceLookUpEdit.Properties.DataSource = value;
            }
        }

        /// <summary>
        /// Улицы
        /// </summary>
        public DataTable Streets
        {
            set
            {
                streetLookUpEdit.Properties.DataSource = value;
            }
        }

        /// <summary>
        /// Дома
        /// </summary>
        public DataTable Buildings
        {
            set
            {
                buildingLookUpEdit.Properties.DataSource = value;
            }
        }

        /// <summary>
        /// Вид услуг
        /// </summary>
        public string ServiceTypeId
        {
            get
            {
                return (string)serviceTypeLookUpEdit.EditValue;
            }
        }

        /// <summary>
        /// Услуга
        /// </summary>
        public string ServiceId
        {
            get
            {
                return (string)serviceLookUpEdit.EditValue;
            }
        }

        /// <summary>
        /// Улица
        /// </summary>
        public string StreetId
        {
            get
            {
                return (string)streetLookUpEdit.EditValue;
            }
        }


        /// <summary>
        /// Дом
        /// </summary>
        public string BuildingId
        {
            get
            {
                return (string)buildingLookUpEdit.EditValue;
            }
        }

        /// <summary>
        /// Признак разбивки по подуслугам
        /// </summary>
        public bool SplitByServices
        {
            get
            {
                return splitByServicesCheckBox.Checked;
            }
        }

        /// <summary>
        /// Флаг группировки по абоненту (по услугам, в противном случае)
        /// </summary>
        public bool GroupByCustomer
        {
            get
            {
                return byCustomerRadioButton.Checked;
            }
        }

        /// <summary>
        /// Устанавливает колонки грида в соответствии с выбранным вариантом отчета
        /// </summary>
        public void SetGridColumns()
        {
            gridViewOfListView.Columns["Street"].VisibleIndex = 0;
            gridViewOfListView.Columns["Building"].VisibleIndex = 1;
            gridViewOfListView.Columns["Service"].VisibleIndex = 2;
            gridViewOfListView.Columns["Customer"].VisibleIndex = 3;
            gridViewOfListView.Columns["Apartment"].VisibleIndex = 4;
            gridViewOfListView.Columns["Charges"].VisibleIndex = 5;
            gridViewOfListView.Columns["Acts"].VisibleIndex = 6;
            gridViewOfListView.Columns["Recharges"].VisibleIndex = 7;
            gridViewOfListView.Columns["Benefits"].VisibleIndex = 8;
            gridViewOfListView.Columns["Payable"].VisibleIndex = 9;
            gridViewOfListView.Columns["Paid"].VisibleIndex = 10;
            gridViewOfListView.Columns["OverpaymentDebt"].VisibleIndex = 11;

            gridViewOfListView.Columns["Customer"].Visible = GroupByCustomer;
            gridViewOfListView.Columns["Apartment"].Visible = GroupByCustomer;
            gridViewOfListView.Columns["Service"].Visible = !GroupByCustomer;
        }

        #endregion

        /// <summary>
        /// Обрабатывает очистку фильтров
        /// </summary>
        private void filterLookUpEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                ((LookUpEdit)sender).EditValue = null;
            }
        }

        /// <summary>
        /// Обработка выбора улицы
        /// </summary>
        private void streetLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (streetLookUpEdit.ItemIndex != -1)
            {
                Presenter.FillBuildingList();
            }
        }

        private void byCustomerRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            splitByServicesCheckBox.Enabled = !byCustomerRadioButton.Checked;
        }
    }
}