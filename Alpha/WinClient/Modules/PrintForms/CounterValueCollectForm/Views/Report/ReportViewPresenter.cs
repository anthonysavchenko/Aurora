using Microsoft.Practices.CompositeUI.EventBroker;
using System;
using System.Data;
using System.Drawing.Printing;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.CounterValueCollectForm.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.CounterValueCollectForm.DataSets;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.CounterValueCollectForm.Views.Report.Queries;
using Taumis.EnterpriseLibrary.Win;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.CounterValueCollectForm.Views.Report
{
    /// <summary>
    /// Презентер вида с отчетом
    /// </summary>
    public class ReportViewPresenter : BaseReportForReportObjectPresenter<IReportView, EmptyReportParams>
    {
        /// <summary>
        /// Данные
        /// </summary>
        private CollectFormDataSet _data;

        /// <summary>
        /// Выполняет действия при загрузке вида
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();

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
                string[] _startUpParams = ((string[])WorkItem.State[ModuleStateNames.DISTRICT_IDS]);

                if (_startUpParams.Length > 0)
                {
                    int[] _ids = Array.ConvertAll(_startUpParams, int.Parse);

                    using (var _db = new Entities())
                    {
                        _db.CommandTimeout = 3600;
                        _data = _db.GetReportData(_ids[0], ServerTime.GetPeriodInfo().FirstUncharged);
                    }
                }
                else
                {
                    _data = new CollectFormDataSet();
                }
            }
            catch (Exception _ex)
            {
                Logger.SimpleWrite($"Не удалось загрузить данные для печати квитанции.\r\n{_ex}");
            }

            return null;
        }

        [EventSubscription(CommonEventNames.ON_MAIN_VIEW_SHOWN, ThreadOption.UserInterface)]
        public void ShowRegularBill(object sender, EventArgs<AnyStartUpParams> eventArgsStartUpParams)
        {
            View.UpdateReport();
        }
    }
}