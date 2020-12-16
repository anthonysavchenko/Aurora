using System;
using Taumis.Alpha.WinClient.Aurora.Modules.Reports.Consolidation.Queries;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Consolidation.Views.List
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
        /// Начальная дата периода
        /// </summary>
        DateTime Since { get; set; }

        /// <summary>
        /// Конечная дата периода
        /// </summary>
        DateTime Till { get; set; }
    }
}