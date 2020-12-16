using System;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Consolidation.Queries
{
    public class Column
    {
        /// <summary>
        /// Заголовок колонки в GridView
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Формат значений в GridView
        /// </summary>
        public ColumnFormat ColumnFormat { get; set; }

        /// <summary>
        /// Название колонки в DataTable
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Тип колонки в DataTable
        /// </summary>
        public Type ColumnType { get; set; }
    }
}
