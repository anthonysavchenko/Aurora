using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Payments.Views.List
{
    public interface IListView : IBaseReportForGridView
    {
        /// <summary>
        /// Добавляет колонку в таблицу
        /// </summary>
        void AddColumn(Column column);

        /// <summary>
        /// Удаляет все колонки
        /// </summary>
        void ClearColumns();

        /// <summary>
        /// Тип отчета
        /// </summary>
        ReportType ReportType { get; }

        /// <summary>
        /// Дата начала периода отчета
        /// </summary>
        DateTime Since { get; set; }

        /// <summary>
        /// Дата окончания периода отчета
        /// </summary>
        DateTime Till { get; set; }
    }

    public enum ReportType
    {
        ByContractors,
        ByBuildings
    }
}