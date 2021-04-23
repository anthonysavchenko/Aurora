using System;
using System.Data;
using Taumis.Alpha.WinClient.Aurora.Modules.Reports.Consolidation.Models;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Consolidation.Views.List
{
    public interface IListView : IBaseReportForGridView
    {
        /// <summary>
        /// Начальная дата периода
        /// </summary>
        DateTime Since { get; set; }

        /// <summary>
        /// Показывать архивные дома
        /// </summary>
        bool ShowArchived { get; set; }

        /// <summary>
        /// Колонки источника данных для таблицы
        /// </summary>
        Column[] DataSourceColumns { get; set; }

        /// <summary>
        /// Добавляет колонку в таблицу
        /// </summary>
        void AddGridColumn(Column column);

        /// <summary>
        /// Удаляет все колонки
        /// </summary>
        void ClearGridColumns();

        /// <summary>
        /// Возвращает данные таблицы
        /// </summary>
        /// <returns>Отображаемая таблица</returns>
        DataTable GetDataTable();

        /// <summary>
        /// Получает от пользователя путь для сохранения файла Excel
        /// </summary>
        /// <param name="now">время экспорта</param>
        /// <returns>Полное имя файла (с путем)</returns>
        string GetExcelFilePath(DateTime now);
    }
}