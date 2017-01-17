using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.DebtAgency.Views.List
{
    public interface IListView : IBaseReportForGridView
    {
        DateTime Period { get; set; }
    }
}