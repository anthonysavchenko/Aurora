using System;
using System.Data;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.DebtAndFine.Views.List
{
    public enum CustomerSearchType
    {
        Address,
        Account
    }

    public interface IListView : IBaseReportForGridView
    {
        DateTime Since { get; set; }
        DateTime Till { get; set; }
        DataTable Streets { set; }
        DataTable Buildings { set; }
        DataTable Apartments { set; }
        DataTable Services { set; }
        CustomerSearchType SearchType { get; }
        string StreetId { get; }
        string BuildingId { get; }
        string Apartment { get; }
        string Account { get; }
        decimal FineRate { get; }
        DataTable RepairServices { get; set; }
        DataTable MaintenanceServices { get; set; }
        DataTable RecyclingServices { get; set; }
    }
}