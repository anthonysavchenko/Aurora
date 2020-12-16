using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Drawing;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.WinClient.Aurora.Modules.Reports.Consolidation.Queries;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

//using BaseReportForGridView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Consolidation.Views.List
{
    [SmartPart]
    public partial class ListView : BaseReportForGridView, IListView
    {
        public ListView()
        {
            InitializeComponent();
            InitReportComponents(GridControlOfListView, GridViewOfListView);
        }

        [CreateNew]
        public new ListViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
            get
            {
                return (ListViewPresenter)base.Presenter;
            }
        }

        #region Implementation of IListView

        /// <summary>
        /// Добавляет колонку в таблицу
        /// </summary>
        public void AddColumn(Column column)
        {
            GridColumn gridColumn = GridViewOfListView.Columns.AddVisible(column.FieldName, column.Caption);

            switch (column.ColumnFormat)
            {
                case ColumnFormat.Numeric:
                    gridColumn.DisplayFormat.FormatType = FormatType.Numeric;
                    gridColumn.DisplayFormat.FormatString = "{0:n2}";
                    break;
            }
        }

        /// <summary>
        /// Удаляет все колонки
        /// </summary>
        public void ClearColumns()
        {
            GridViewOfListView.Columns.Clear();
        }

        /// <summary>
        /// Начальная дата периода
        /// </summary>
        public DateTime Since
        {
            get
            {
                return SinceDateEdit.DateTime;
            }
            set
            {
                SinceDateEdit.DateTime = value;
            }
        }

        /// <summary>
        /// Конечная дата периода
        /// </summary>
        public DateTime Till
        {
            get
            {
                return TillDateEdit.DateTime;
            }
            set
            {
                TillDateEdit.DateTime = value;
            }
        }

        #endregion

        private void GridViewOfListView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var value = e.CellValue;

            if (value is decimal && (decimal)value < 0)
            {
                e.Appearance.ForeColor = Color.Red;
            }
        }

        private void GridViewOfListView_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex != GridControl.InvalidRowHandle
                && e.Value is decimal)
            {
                switch (((ColumnView)sender).GetListSourceRowCellValue(e.ListSourceRowIndex, "Params")?.ToString())
                {
                    case "Процент ОДН ДЭК от ИПУ ДЭК":
                        e.DisplayText = $"{e.Value:n2} %";
                        break;
                    case "Расчет ОДН":
                        e.DisplayText =
                            (decimal)e.Value == (decimal)CalculationMethod.BuildingCounters
                                ? "ОДПУ"
                                : (decimal)e.Value == (decimal)CalculationMethod.Norm
                                    ? "Норматив"
                                    : (decimal)e.Value == (decimal)CalculationMethod.Avarage
                                        ? "Среднее"
                                        : "Не определено";
                        break;
                }
            }
        }
    }
}
