using System.Data;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.PreChargeReport.Views.List
{
    public interface IListView : IBaseReportForGridView
    {
        /// <summary>
        /// Улицы
        /// </summary>
        DataTable Streets { set; }

        /// <summary>
        /// Дома
        /// </summary>
        DataTable Buildings { set; }

        /// <summary>
        /// Улица
        /// </summary>
        string StreetId { get; }

        /// <summary>
        /// Дом
        /// </summary>
        string BuildingId { get; }
    }
}