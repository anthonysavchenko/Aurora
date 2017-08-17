using System.Collections.Generic;
using System.Data;
using Taumis.Alpha.WinClient.Aurora.Modules.Reports.Payments.Services;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Payments.Views.List
{
    public class ListViewPresenter : BaseReportForGridPresenter<IListView, EmptyReportParams>
    {
        private IPaymentReportService _srvPaymentsByContractors = new PaymentsByContractorsReportService();
        private IPaymentReportService _srvPaymentsByBuildings = new PaymentsByBuildingsReportService();

        private IPaymentReportService ReportService => View.ReportType == ReportType.ByContractors 
            ? _srvPaymentsByContractors 
            : _srvPaymentsByBuildings;

        /// <summary>
        /// Выполняет действия при загрузке вида
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();
            DateTimeInfo _dateTimeInfo = ServerTime.GetDateTimeInfo();
            View.Since = _dateTimeInfo.SinceMonthBeginning;
            View.Till = _dateTimeInfo.TillToday;
        }

        /// <summary>
        /// Обрабатывает данные для табличной части отчета 
        /// </summary>
        override protected void ProcessGridData()
        {
            View.ClearColumns();

            List<Column> _columns = ReportService.GetColumns();

            foreach(Column _col in _columns)
            {
                View.AddColumn(_col);
            }

            base.ProcessGridData();
        }

        /// <summary>
        /// Возвращает данные для табличной части отчета
        /// </summary>
        /// <param name="_params">Параметры отчета</param>
        /// <returns>Данные табличной части отчета</returns>
        protected override DataTable GetGridData(EmptyReportParams _params)
        {
            return ReportService.GetData(View.Since, View.Till);
        }
    }
}