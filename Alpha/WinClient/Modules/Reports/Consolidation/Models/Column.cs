using System;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Consolidation.Models
{
    public class Column
    {
        /// <summary>
        /// Название колонки в DataTable
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Тип колонки в DataTable
        /// </summary>
        public Type ColumnType { get; set; }

        /// <summary>
        /// Отображение колонки в GridView и файле Excel
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// Отображение колонки как примечания в GridView и файле Excel
        /// </summary>
        public bool IsNote { get; set; }

        /// <summary>
        /// Формат данных в колонке
        /// </summary>
        public ColumnFormat Format { get; set; }
        
        /// <summary>
        /// Заголовок колонки в GridView
        /// </summary>
        public string GridHeader { get; set; }

        /// <summary>
        /// Название колонки в файле Excel
        /// </summary>
        public string ExcelName { get; set; }

        /// <summary>
        /// Заголовок колонки в файле Excel
        /// </summary>
        public object ExcelHeader { get; set; }

        /// <summary>
        /// Формат заголовка колонки в файле Excel
        /// </summary>
        public string ExcelHeaderFormat { get; set; }

        /// <summary>
        /// Ширина колонки в файле Excel
        /// </summary>
        public int ExcelWidth { get; set; }
    }
}
