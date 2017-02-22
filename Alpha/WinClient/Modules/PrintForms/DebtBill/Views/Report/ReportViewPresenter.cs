using System.Drawing.Printing;
using System;
using System.Data;
using System.Linq;
using Microsoft.Practices.CompositeUI;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Server.PrintForms.DataSets;
using Taumis.Alpha.WinClient.Aurora.Interface.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.DebtBill.Constants;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.DebtBill.Views.Report
{
    /// <summary>
    /// Презентер вида с отчетом
    /// </summary>
    public class ReportViewPresenter : BaseReportForReportObjectPresenter<IReportView, EmptyReportParams>
    {
        /// <summary>
        /// Данные
        /// </summary>
        private DebtBillDataSet _data;

        [ServiceDependency]
        public IBillService BillService { get; set; }

        /// <summary>
        /// Выполняет действия при загрузке вида
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();

            string[] _billIDStrings = ((string[])WorkItem.State[ModuleStateNames.START_UP_PARAMS_BILL_IDS]);

            if (_billIDStrings.Length > 1)
            {
                View.OneBillOnSheet = false;
                View.OneBillOnSheetEnabled = true;
            }
            else
            {
                View.OneBillOnSheet = true;
                View.OneBillOnSheetEnabled = false;
            }

            DataTable _table = new DataTable();
            _table.Columns.Add("Name", typeof(string));

            PrinterSettings.StringCollection _printerNames = PrinterSettings.InstalledPrinters;

            foreach (string _printerName in _printerNames)
            {
                _table.Rows.Add(_printerName);
            }

            View.Printers = _table;

            if (_printerNames.Count > 0)
            {
                View.SelectedPrinter = new PrinterSettings().PrinterName;
            }

            View.UpdateReport();
        }

        /// <summary>
        /// Обрабатывает данные табличной части отчета 
        /// </summary>
        protected override void ProcessGridData()
        {
            bool _showReport = _data.Tables["Bills"].Rows.Count > 0;
            View.DataSource = _data;
            View.ReportVisible = _showReport;
            View.PageBreakAfterBill = View.OneBillOnSheet;
            View.ShowLineBetweenBills = _showReport && !View.OneBillOnSheet;
        }

        /// <summary>
        /// Возвращает данные для табличной части отчета
        /// </summary>
        /// <param name="_params">Параметры отчета</param>
        /// <returns>Данные табличной части отчета</returns>
        protected override DataTable GetGridData(EmptyReportParams _params)
        {
            try
            {
                _data = new DebtBillDataSet();
                string[] _billIDStrings = ((string[])WorkItem.State[ModuleStateNames.START_UP_PARAMS_BILL_IDS]);

                if (_billIDStrings.Length > 0)
                {
                    DataTable _billsTable = _data.Tables["Bills"];
                    DateTime _now = ServerTime.GetDateTimeInfo().Now;

                    using (Entities _entities = new Entities())
                    {
                        int[] _billIDs = Array.ConvertAll<string, int>(_billIDStrings, int.Parse);

                        var _bills =
                            _entities.DebtBillDocs
                                .Where(b => _billIDs.Contains(b.ID))
                                .Select(b =>
                                    new
                                    {
                                        b.ID,
                                        b.Account,
                                        b.CreationDateTime,
                                        b.Period,
                                        b.Value,
                                        b.Address,
                                        b.Owner,
                                        b.Customers.Buildings.BankDetails
                                    })
                                .ToList();

                        foreach (var _bill in _bills)
                        {
                            string _barcode = BillService.GenerateBarCodeString(_bill.Account, _bill.Period);

                            _billsTable.Rows.Add(
                                _bill.Account,
                                BillService.FormatBarcodeString(_barcode),
                                _barcode,
                                _now.ToString("dd.MM.yyyy"),
                                _bill.CreationDateTime.ToString("dd.MM.yyyy"),
                                $"({_bill.Period:MM.yy})",
                                _bill.Value,
                                _bill.Address,
                                _bill.Owner,
                                _bill.ID,
                                BillService.OrganizationDetails(_bill.BankDetails));
                        }
                    }
                }
            }
            catch (Exception _ex)
            {
                Logger.SimpleWrite($"Не удалось загрузить данные для печати квитанции.\r\n{_ex}");
            }

            return null;
        }
    }
}