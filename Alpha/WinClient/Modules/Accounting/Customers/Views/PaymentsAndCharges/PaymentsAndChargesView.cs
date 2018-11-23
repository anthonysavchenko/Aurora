using DevExpress.XtraGrid.Views.Base;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System.Data;
using System.Drawing;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Constants;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views.PaymentsAndCharges
{
    /// <summary>
    /// Вид деталей
    /// </summary>
    [SmartPart]
    public partial class PaymentsAndChargesView : BaseSimpleListView
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public PaymentsAndChargesView()
        {
            InitializeComponent();
            Initialize(GridControlOfListView, GridViewOfListView, PaymentAndChargesColumnNames.COLUMN_ID, false);
        }

        /// <summary>
        /// Создание презентера для формы
        /// </summary>
        [CreateNew]
        public new PaymentsAndChargesViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
            get
            {
                return (PaymentsAndChargesViewPresenter)base.Presenter;
            }
        }

        public override DataTable ElemList
        {
            set
            {
                if (value != null)
                {
                    value.PrimaryKey = new DataColumn[] { value.Columns[PaymentAndChargesColumnNames.COLUMN_PERIOD] };
                }
                GridControlOfListView.DataSource = value;
                GridViewOfListView.BestFitColumns();
                SubGridViewOfListView.BestFitColumns();
                GridControlOfListView.LevelTree.Nodes.Add(PaymentAndChargesColumnNames.RELATION_PERIOD, SubGridViewOfListView);
            }
        }

        private void SubGridViewOfListView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Column.FieldName == PaymentAndChargesColumnNames.COLUMN_OPERATION_NAME)
            {
                Presenter.SelectOperationLink(
                    (int)((ColumnView)GridControlOfListView.FocusedView).GetRowCellValue(e.RowHandle, PaymentAndChargesColumnNames.COLUMN_LINK_OPERATION_ID),
                    (int)((ColumnView)GridControlOfListView.FocusedView).GetRowCellValue(e.RowHandle, PaymentAndChargesColumnNames.COLUMN_OPERATION_TYPE));
            }
        }

        private void SubGridViewOfListView_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                switch ((OperationType)(int)((DevExpress.XtraGrid.Views.Grid.GridView)sender).GetRowCellValue(e.RowHandle, PaymentAndChargesColumnNames.COLUMN_OPERATION_TYPE))
                {
                    case OperationType.Charge:
                    case OperationType.PaymentCorrection:
                    case OperationType.Recharge:
                    case OperationType.BenefitCorrection:
                    case OperationType.ActCorrection:
                        e.Appearance.BackColor = Color.FromArgb(250, 200, 200);
                        break;

                    case OperationType.Payment:
                    case OperationType.Benefit:
                    case OperationType.ChargeCorrection:
                    case OperationType.RechargeCorrection:
                    case OperationType.Rebenefit:
                    case OperationType.Act:
                        e.Appearance.BackColor = Color.FromArgb(200, 250, 200);
                        break;
                }
            }
        }

        private void TotalBillLinkLabel_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            Presenter.SelectTotalBillLink();
        }
    }
}