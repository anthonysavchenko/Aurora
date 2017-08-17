namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Payments.Views.List
{
    public class Column
    {
        /// <summary>
        /// Наименование колонки в источнике данных
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Заголовок колонки
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Наименование колонки для сортировки в источнике данных
        /// </summary>
        public string FieldSortName { get; set; }

        /// <summary>
        /// Индекс группы
        /// </summary>
        public int GroupIndex { get; set; }

        /// <summary>
        /// Группировка
        /// </summary>
        public bool IsGrouped { get; set; }

        /// <summary>
        /// Сортировка
        /// </summary>
        public bool IsSorted { get; set; }

        /// <summary>
        /// Флаг добавления строки "Итого" для колонки
        /// </summary>
        public bool HasSummary { get; set; }
    }
}