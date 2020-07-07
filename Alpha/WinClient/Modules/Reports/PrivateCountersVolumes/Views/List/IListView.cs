using System;
using System.Data;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.PrivateCountersVolumes.Views.List
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
        void AddMoneyColumn(string fieldName, string caption);

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

        /// <summary>
        /// Дома
        /// </summary>
        DataTable Buildings { set; }

        /// <summary>
        /// Дом
        /// </summary>
        string BuildingId { get; }
    }
}