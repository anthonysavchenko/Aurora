using System;
using System.Data;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Consolidation.Views.List
{
    public interface IListView : IBaseReportForGridView
    {
        /// <summary>
        /// Добавляет колонку в таблицу
        /// </summary>
        /// <param name="fieldName">Наименование колонки в источнике данных</param>
        /// <param name="caption">Заголовок колонки</param>
        void AddColumn(string fieldName, string caption);

        /// <summary>
        /// Добавляет колонку в таблицу
        /// </summary>
        /// <param name="fieldName">Наименование колонки в источнике данных</param>
        /// <param name="caption">Заголовок колонки</param>
        void AddNumericColumn(string fieldName, string caption);

        void AddDateColumn(string fieldName, string caption);

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