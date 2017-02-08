using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraPrinting.Control;
using DevExpress.XtraReports.UI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Taumis.Alpha.Server.PrintForms.DataSets;
using Taumis.Alpha.Server.PrintForms.Reports.DebtBills;
using Taumis.Alpha.Server.PrintForms.Reports.DebtBills;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.DebtBill.Views.Report
{
    /// <summary>
    /// Вид с отчетом
    /// </summary>
    [SmartPart]
    public partial class ReportView : /*System.Windows.Forms.UserControl//*/ 
        BaseReportForReportObjectView<ReportObject.LayoutReportObject>, 
        IReportView
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ReportView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Control для отображения отчета
        /// </summary>
        protected override PrintControl ReportPrintControl
        {
            get
            {
                return printControl1;
            }
        }

        /// <summary>
        /// Презентер
        /// </summary>
        [CreateNew]
        public new ReportViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
            get
            {
                return (ReportViewPresenter)base.Presenter;
            }
        }

        #region Implementation of IReportView

        /// <summary>
        /// Источник данных
        /// </summary>
        public DebtBillDataSet DataSource
        {
            set
            {
                Report.ReportDataSource = value;
            }
        }

        /// <summary>
        /// Печатает отчет
        /// </summary>
        public void PrintReport()
        {
            ReportPrintTool _reportPrintTool = new ReportPrintTool(Report);

            if (!String.IsNullOrEmpty(SelectedPrinter))
            {
                _reportPrintTool.PrintingSystem.PageSettings.PrinterName = SelectedPrinter;
            }

            _reportPrintTool.Print();
        }

        /// <summary>
        /// Принтеры
        /// </summary>
        public DataTable Printers
        {
            set
            {
                PrinterLookUpEdit.Properties.DataSource = value;
                PrinterLookUpEdit.Properties.ForceInitialize();
            }
        }

        /// <summary>
        /// Выбранный принтер
        /// </summary>
        public string SelectedPrinter
        {
            get
            {
                string _res = String.Empty;

                if (PrinterLookUpEdit.ItemIndex != -1)
                {
                    _res = PrinterLookUpEdit.GetColumnValue("Name").ToString();
                }

                return _res;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    ((System.ComponentModel.ISupportInitialize)(PrinterLookUpEdit.Properties)).BeginInit();
                    PrinterLookUpEdit.Properties.ValueMember = "Name";
                    PrinterLookUpEdit.EditValue = value;
                    ((System.ComponentModel.ISupportInitialize)(PrinterLookUpEdit.Properties)).EndInit();
                }
                else
                {
                    PrinterLookUpEdit.EditValue = null;
                }
            }
        }

        /// <summary>
        /// Печатать одной квитанции на листе
        /// </summary>
        public bool OneBillOnSheet
        {
            set
            {
                OneBillOnSheetCheckBox.Checked = value;
            }
            get
            {
                return OneBillOnSheetCheckBox.Checked;
            }
        }

        /// <summary>
        /// Доступность печатати одной квитанции на листе
        /// </summary>
        public bool OneBillOnSheetEnabled
        {
            set
            {
                OneBillOnSheetCheckBox.Enabled = value;
            }
        }

        /// <summary>
        /// Видимость отчета на странице
        /// </summary>
        public bool ReportVisible
        {
            set
            {
                ((ILayoutReportObject)Report).ReportVisible = value;
            }
        }

        /// <summary>
        /// Переносить каждую квитанцию на отдельную страницу
        /// </summary>
        public bool PageBreakAfterBill
        {
            set
            {
                ((ILayoutReportObject)Report).PageBreakAfterBill = value;
            }
        }

        /// <summary>
        /// Отображать линию отреза между квитанциями
        /// </summary>
        public bool ShowLineBetweenBills
        {
            set
            {
                ((ILayoutReportObject)Report).ShowLineBetweenBills = value;
            }
        }

        #endregion

        private void PrintButton_Click(object sender, System.EventArgs e)
        {
            PrintReport();
        }

        private void ExportXLSButton_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog _saveFileDialog = new SaveFileDialog()
            {
                Title = "Сохранить в файл",
                Filter = "Файл Excel 97-2003 (*.xls)|*.xls",
                FilterIndex = 1,
                RestoreDirectory = true,
                DefaultExt = "xls",
                FileName = "Экспорт",
                AddExtension = true,
            };

            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                new ReportPrintTool(Report).PrintingSystem.ExportToXls(_saveFileDialog.FileName);

                Process process = new Process();
                process.StartInfo.FileName = _saveFileDialog.FileName;
                process.Start();
            }
        }

        private void ExportPDFButton_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog _saveFileDialog = new SaveFileDialog();

            _saveFileDialog.Title = "Сохранить в файл";
            _saveFileDialog.Filter = "PDF документ (*.pdf)|*.pdf";
            _saveFileDialog.FilterIndex = 1;
            _saveFileDialog.RestoreDirectory = true;
            _saveFileDialog.DefaultExt = "pdf";

            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                new ReportPrintTool(Report).PrintingSystem.ExportToPdf(_saveFileDialog.FileName);

                Process process = new Process();
                process.StartInfo.FileName = _saveFileDialog.FileName;
                process.Start();
            }
        }

        private void OneBillOnSheetCheckBox_Click(object sender, EventArgs e)
        {
            UpdateReport();
        }
    }
}