using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.Practices.CompositeUI;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.WinClient.Aurora.Interface.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.TotalBill.Constants;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.TotalBill.Views.Report
{
    /// <summary>
    /// Презентер вида с отчетом
    /// </summary>
    public class ReportViewPresenter : BaseReportForReportObjectPresenter<IReportView, EmptyReportParams>
    {
        /// <summary>
        /// Данные
        /// </summary>
        private DataSets.DataSet _data;

        [ServiceDependency]
        public IBillService BillService { get; set; }

        /// <summary>
        /// Выполняет действия при загрузке вида
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();
            View.UpdateReport();
        }

        /// <summary>
        /// Обрабатывает данные табличной части отчета 
        /// </summary>
        protected override void ProcessGridData()
        {
            View.DataSource = _data;
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
                _data = new DataSets.DataSet();
                DataTable _billsTable = _data.Tables["TotalBillDocs"];
                DataTable _posesTable = _data.Tables["TotalBillPoses"];
                string[] _billIDStrings = ((string[])WorkItem.State[ModuleStateNames.START_UP_PARAMS_BILL_IDS]);

                if (_billIDStrings.Length > 0)
                {
                    using (Entities _entities = new Entities())
                    {
                        int[] _billIDs = Array.ConvertAll<string, int>(_billIDStrings, Int32.Parse);

                        var _bills =
                            _entities.TotalBillDocs
                                .Where(b => _billIDs.Contains(b.ID))
                                .Select(b =>
                                    new
                                    {
                                        b.ID,
                                        b.Account,
                                        b.Owner,
                                        b.Address,
                                        b.Square,
                                        b.ResidentsCount,
                                        b.Value,
                                        b.Period,
                                        b.StartPeriod,
                                        b.CreationDateTime,
                                        b.Customers.Buildings.BankDetails,
                                        b.TotalBillDocPoses
                                    })
                                .ToList();

                        foreach (var _bill in _bills)
                        {
                            string _barcode = BillService.GenerateBarCodeString(_bill.Account, _bill.Period);

                            _billsTable.Rows.Add(
                                        _bill.ID,
                                        _bill.Account,
                                        _bill.Owner,
                                        _bill.Address,
                                        _bill.Square,
                                        _bill.ResidentsCount,
                                        _bill.Value,
                                        _barcode,
                                        BillService.FormatBarcodeString(_barcode),
                                        _bill.Period.ToString("MMMM yyyy (MM.yy)").ToUpper(),
                                        _bill.StartPeriod.HasValue
                                            ? $"{_bill.StartPeriod.Value:MMMM yyyy} - {_bill.Period:MMMM yyyy}"
                                            : $"по {_bill.Period:MMMM yyyy}",
                                        _bill.CreationDateTime,
                                        BillService.OrganizationDetails(_bill.BankDetails));

                            foreach (var _pos in _bill.TotalBillDocPoses)
                            {
                                _posesTable.Rows.Add(
                                    _pos.ServiceTypeName,
                                    _pos.Value,
                                    _pos.TotalCharged,
                                    _pos.TotalPaid,
                                    0,
                                    _bill.ID);
                            }
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