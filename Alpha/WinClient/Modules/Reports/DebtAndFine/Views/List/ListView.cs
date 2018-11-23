using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using System.Windows.Forms;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

//using BaseReportForGridView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.DebtAndFine.Views.List
{
    [SmartPart]
    public partial class ListView : BaseReportForGridView, IListView
    {
        public ListView()
        {
            InitializeComponent();
            InitReportComponents(gridControlOfListView, gridViewOfListView);
        }

        [CreateNew]
        public new ListViewPresenter Presenter
        {
            get => (ListViewPresenter)base.Presenter;
            set => base.Presenter = value;
        }

        #region Implementation of IListView

        public DateTime Since
        {
            get
            {
                DateTime _temp = DateTime.MinValue;
                sinceDateEdit.Invoke(new MethodInvoker(() => _temp = sinceDateEdit.DateTime));
                return new DateTime(_temp.Year, _temp.Month, 1);
            }
            set
            {
                sinceDateEdit.DateTime = value;
            }
        }

        public DateTime Till
        {
            get
            {
                DateTime _temp = DateTime.MinValue;
                tillDateEdit.Invoke(new MethodInvoker(() => _temp = tillDateEdit.DateTime));
                return new DateTime(_temp.Year, _temp.Month, 1);
            }
            set
            {
                tillDateEdit.DateTime = value;
            }
        }

        public DataTable Streets { set => streetLookUpEdit.Properties.DataSource = value; }
        public DataTable Buildings { set => buildingLookUpEdit.Properties.DataSource = value; }
        public DataTable Apartments { set => apartmentsLookUpEdit.Properties.DataSource = value; }
        public string StreetId => (string)streetLookUpEdit.EditValue;
        public string BuildingId => (string)buildingLookUpEdit.EditValue;
        public string Apartment => (string)apartmentsLookUpEdit.EditValue;
        public string Account => accountTextEdit.Text;
        public decimal FineRate => fineRateNumericUpDown.Value;
        public CustomerSearchType SearchType => addressRadioButton.Checked ? CustomerSearchType.Address : CustomerSearchType.Account;

        public DataTable RepairServices
        {
            get => (DataTable)repairGridControl.DataSource;
            set => repairGridControl.DataSource = value;
        }

        public DataTable MaintenanceServices
        {
            get => (DataTable)maintenanceGridControl.DataSource;
            set => maintenanceGridControl.DataSource = value;
        }

        public DataTable RecyclingServices
        {
            get => (DataTable)recyclingGridControl.DataSource;
            set => recyclingGridControl.DataSource = value;
        }

        public DataTable Services
        {
            set
            {
                repairRepItemLookUpEdit.DataSource = value;
                maintenanceRepItemLookUpEdit.DataSource = value;
                recyclingRepItemLookUpEdit.DataSource = value;
            }
        }

        #endregion

        private void filterLookUpEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                ((LookUpEdit)sender).EditValue = null;
            }
        }

        private void streetLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (streetLookUpEdit.ItemIndex != -1)
            {
                Presenter.FillBuildingList();
            }
        }

        private void buildingLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if(buildingLookUpEdit.ItemIndex != -1)
            {
                Presenter.FillApartmentsList();
            }
        }

        private void AccountTextBox_Enter(object sender, EventArgs e)
        {
            accountRadioButton.Checked = true;
        }

        private void AddressControl_Enter(object sender, EventArgs e)
        {
            addressRadioButton.Checked = true;
        }

        private void repairRepItemLookUpEdit_Closed(object sender, ClosedEventArgs e)
        {
            repairGridView.CloseEditor();
            repairGridView.UpdateCurrentRow();
        }

        private void maintenanceRepItemLookUpEdit_Closed(object sender, ClosedEventArgs e)
        {
            maintenanceGridView.CloseEditor();
            maintenanceGridView.UpdateCurrentRow();
        }

        private void recyclingRepItemLookUpEdit_Closed(object sender, ClosedEventArgs e)
        {
            recyclingGridView.CloseEditor();
            recyclingGridView.UpdateCurrentRow();
        }
    }
}