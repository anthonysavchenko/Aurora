using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Benefits.Views.List
{
    public interface IListView : IBaseReportForGridView
    {
        /// <summary>
        /// Период отчета
        /// </summary>
        DateTime Period { get; set; }

        /// <summary>
        /// Показывать только федеральные льготы (по норме площади)
        /// </summary>
        bool ShowOnlyFederalBenefits { get; }
    }
}