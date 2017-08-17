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
            set => base.Presenter = value;
        }

        #region Implementation of IListView

        /// <summary>
        /// Добавляет колонку в таблицу
        /// </summary>
        public void AddColumn(Column column)
        {
            GridColumn _column =  gridViewOfListView.Columns.AddVisible(column.FieldName, column.Title);

            if (column.HasSummary)
            {
                _column.DisplayFormat.FormatType = FormatType.Numeric;
                _column.DisplayFormat.FormatString = "0.00";

                _column.Summary.Add(SummaryItemType.Sum);
                gridViewOfListView.GroupSummary.Add(SummaryItemType.Sum, column.FieldName, _column, "{0:0.00}");
            }
            else
            {
                _column.DisplayFormat.FormatType = FormatType.None;
                _column.DisplayFormat.FormatString = string.Empty;
            }

            if (column.IsSorted)
            {
                _column.FieldNameSortGroup = column.FieldSortName;
                gridViewOfListView.SortInfo.Add(_column, ColumnSortOrder.Ascending);
            }

            if (column.IsGrouped)
            {
                _column.GroupIndex = column.GroupIndex;
            }
        }

        /// <summary>
        /// Удаляет все колонки
        /// </summary>
        public void ClearColumns()
        {
            gridViewOfListView.Columns.Clear();
        }

        /// <summary>
        /// Тип отчета
        /// </summary>
        public ReportType ReportType => byContractorsRadioButton.Checked ? ReportType.ByContractors : ReportType.ByBuildings;

        /// <summary>
        /// Дата начала периода отчета
        /// </summary>
        public DateTime Since
        {
            get => sinceDateEdit.DateTime;
            set => sinceDateEdit.DateTime = value;
        }

        /// <summary>
        /// Дата окончания периода отчета
        /// </summary>
        public DateTime Till
        {
            get => new DateTime(tillDateEdit.DateTime.Year, tillDateEdit.DateTime.Month, tillDateEdit.DateTime.Day, 23, 59, 59);
            set => tillDateEdit.DateTime = value;
        }

        #endregion
    }
}