using System.Data;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Debtors.Views.List
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
        /// Нижняя граница суммы долга
        /// </summary>
        decimal DebtMinSum { get; set; }

        /// <summary>
        /// Нижняя граница количества месяцев долга
        /// </summary>
        int DebtMonthCount { get; set; }

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