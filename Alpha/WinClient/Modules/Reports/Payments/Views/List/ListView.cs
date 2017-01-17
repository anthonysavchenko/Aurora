using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;
using DevExpress.Data;
//using BaseReportForGridView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Payments.Views.List
{
    [SmartPart]
    public partial class ListView : BaseReportForGridView, IListView
    {
        public ListView()
        {
            InitializeComponent();
            InitReportConponents(gridControlOfListView, gridViewOfListView);
        }

        [CreateNew]
        public new ListViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
        }

        #region Implementation of IListView

        /// <summary>
        /// Добавляет колонку в таблицу
        /// </summary>
        /// <param name="fieldName">Наименование колонки в источнике данных</param>
        /// <param name="caption">Заголовок колонки</param>
        /// <param name="isSorted">Сортировка</param>
        /// <param name="sortFieldName">Наименование колонки для сортировки в источнике данных</param>
        /// <param name="isGrouped">Группировка</param>
        /// <param name="groupIndex">Индекс группы</param>
        public void AddColumn(string fieldName, string caption, bool isSorted, string sortFieldName, bool isGrouped, int groupIndex)
        {
            GridColumn _column = AddColumn(fieldName, caption, FormatType.None, String.Empty);

            if (isSorted)
            {
                _column.FieldNameSortGroup = sortFieldName;
                gridViewOfListView.SortInfo.Add(_column, ColumnSortOrder.Ascending);
            }

            if (isGrouped)
            {
                _column.GroupIndex = groupIndex;
            }
        }

        /// <summary>
        /// Добавляет колонку с саммари в таблицу
        /// </summary>
        /// <param name="fieldName">Наименование колонки в источнике данных</param>
        /// <param name="caption">Заголовок колонки</param>
        public void AddSummaryColumn(string fieldName, string caption)
        {
            GridColumn _column = AddColumn(fieldName, caption, FormatType.Numeric, "0.00");
            gridViewOfListView.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, fieldName, _column, "{0:0.00}");
        }

        /// <summary>
        /// Добавляет колонку в таблицу
        /// </summary>
        /// <param name="fieldName">Наименование колонки в источнике данных</param>
        /// <param name="caption">Заголовок колонки</param>
        /// <param name="formatType">Тип форматирования</param>
        /// <param name="formatString">Строка форматирования</param>
        private GridColumn AddColumn(string fieldName, string caption, FormatType formatType, string formatString)
        {
            GridColumn _column = gridViewOfListView.Columns.AddVisible(fieldName, caption);

            _column.DisplayFormat.FormatType = formatType;
            _column.DisplayFormat.FormatString = formatString;

            return _column;
        }

        /// <summary>
        /// Удаляет все колонки
        /// </summary>
        public void ClearColumns()
        {
            gridViewOfListView.Columns.Clear();
        }

        /// <summary>
        /// Дата начала периода отчета
        /// </summary>
        public DateTime SinceDateTime
        {
            get
            {
                return sinceDateEdit.DateTime;
            }
            set
            {
                sinceDateEdit.DateTime = value;
            }
        }

        /// <summary>
        /// Дата окончания периода отчета
        /// </summary>
        public DateTime TillDateTime
        {
            get
            {
                return tillDateEdit.DateTime;
            }
            set
            {
                tillDateEdit.DateTime = value;
            }
        }

        #endregion
    }
}