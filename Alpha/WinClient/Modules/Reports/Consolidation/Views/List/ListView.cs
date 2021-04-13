﻿using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using System.Windows.Forms;
using Taumis.Alpha.WinClient.Aurora.Modules.Reports.Consolidation.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Reports.Consolidation.Models;
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
        /// Колонки источника данных для таблицы
        /// </summary>
        public Column[] DataSourceColumns { get; set; }

        /// <summary>
        /// Добавляет колонку в таблицу
        /// </summary>
        public void AddGridColumn(Column column)
        {
            GridViewOfListView.Columns.AddVisible(column.FieldName, column.GridHeader);
        }

        /// <summary>
        /// Удаляет все колонки
        /// </summary>
        public void ClearGridColumns()
        {
            GridViewOfListView.Columns.Clear();
        }

        /// <summary>
        /// Возвращает данные таблицы
        /// </summary>
        /// <returns>Отображаемая таблица</returns>
        public DataTable GetDataTable()
        {
            return ((DataView)GridViewOfListView.DataSource ?? new DataView()).Table;
        }

        /// <summary>
        /// Получает от пользователя путь для сохранения файла Excel
        /// </summary>
        /// <param name="now">время экспорта</param>
        /// <returns>Полное имя файла (с путем)</returns>
        public string GetExcelFilePath(DateTime now)
        {
            SaveFileDialog _saveFileDialog = new SaveFileDialog()
            {
                Title = "Сохранить в файл",
                Filter = "Файл Excel 97-2003 (*.xls)|*.xls",
                FilterIndex = 1,
                DefaultExt = "xls",
                AddExtension = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer),
                FileName = $"{now:yyyy.MM.dd HH.mm} {ModuleUIExtensionSiteNames.TITLE} " +
                    $"c {Since:yyyy.MM} по {Since.AddMonths(DataSource.MONTH_COUNT):yyyy.MM}",
                RestoreDirectory = true,
            };

            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                return _saveFileDialog.FileName;
            }

            return string.Empty;
        }

        #endregion

        private void GridViewOfListView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            e.Appearance.ForeColor = DataSource.GetGridCellTextColor(e.CellValue);
        }

        private void GridViewOfListView_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex != GridControl.InvalidRowHandle
                && e.Value != DBNull.Value)
            {
                e.DisplayText =
                    DataSource.GetGridCellDisplayText(
                        e.Value,
                        DataSourceColumns[e.Column.AbsoluteIndex].Format,
                        (CellFormat)(((ColumnView)sender)
                            .GetListSourceRowCellValue(e.ListSourceRowIndex, DataSource.SPECIAL_CELLS_FORMAT_COLUMN)
                                ?? CellFormat.Numeric));
            }
        }
    }
}