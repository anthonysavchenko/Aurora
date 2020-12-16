using System.Collections.Generic;
using System.Data;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.WinClient.Aurora.Modules.Reports.Consolidation.Queries;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Consolidation.Views.List
{
    public class ListViewPresenter : BaseReportForGridPresenter<IListView, EmptyReportParams>
    {
        private List<Column> Columns;

        public override void OnViewReady()
        {
            base.OnViewReady();

            DateTimeInfo dateTimeInfo = ServerTime.GetDateTimeInfo();

            View.Since = dateTimeInfo.SinceYearBeginning;
            View.Till = dateTimeInfo.TillToday;
        }

        protected override DataTable GetGridData(EmptyReportParams _params)
        {
            Columns = ListViewQuery.GetGridColumns(View.Since, View.Till);

            using (var db = new Entities())
            {
                return db.GetGridRows(Columns, View.Since, View.Till);
            }
        }

        protected override void ProcessGridData()
        {
            View.ClearColumns();

            foreach (var column in Columns)
            {
                View.AddColumn(column);
            }

            base.ProcessGridData();
        }
    }
}
