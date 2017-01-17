using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Payments.Views.List
{
    public interface IListView : IBaseReportForGridView
    {
        /// <summary>
        /// Добавляет колонку в таблицу
        /// </summary>
        /// <param name="fieldName">Наименование колонки в источнике данных</param>
        /// <param name="caption">Заголовок колонки</param>
        /// <param name="isSorted">Сортировка</param>
        /// <param name="sortFieldName">Наименование колонки для сортировки в источнике данных</param>
        /// <param name="isGrouped">Группировка</param>
        /// <param name="groupIndex">Индекс группы</param>
        void AddColumn(string fieldName, string caption, bool isSorted, string sortFieldName, bool isGrouped, int groupIndex);

        /// <summary>
        /// Добавляет колонку с саммари в таблицу
        /// </summary>
        /// <param name="fieldName">Наименование колонки в источнике данных</param>
        /// <param name="caption">Заголовок колонки</param>
        void AddSummaryColumn(string fieldName, string caption);

        /// <summary>
        /// Удаляет все колонки
        /// </summary>
        void ClearColumns();

        /// <summary>
        /// Дата начала периода отчета
        /// </summary>
        DateTime SinceDateTime { get; set; }

        /// <summary>
        /// Дата окончания периода отчета
        /// </summary>
        DateTime TillDateTime { get; set; }
    }
}