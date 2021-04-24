using Microsoft.Practices.CompositeUI;
using System;
using System.Data;
using System.Diagnostics;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.Excel;
using Taumis.Alpha.WinClient.Aurora.Modules.Reports.Consolidation.Models;
using Taumis.Alpha.WinClient.Aurora.Modules.Reports.Consolidation.Queries;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;
using Taumis.EnterpriseLibrary.Win.Services;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Consolidation.Views.List
{
    public class ListViewPresenter : BaseReportForGridPresenter<IListView, EmptyReportParams>
    {
        public override void OnViewReady()
        {
            base.OnViewReady();

            View.Since = ServerTime.GetDateTimeInfo().SinceYearBeginning;
            View.ShowArchived = false;
        }

        protected override DataTable GetGridData(EmptyReportParams _params)
        {
            View.DataSourceColumns = DataSource.GetDataSourceColumns(View.Since);

            using (var db = new Entities())
            {
                return db.GetDataTable(View.DataSourceColumns, View.Since, View.ShowArchived);
            }
        }

        protected override void ProcessGridData()
        {
            View.ClearGridColumns();

            foreach (var column in View.DataSourceColumns)
            {
                if (column.Visible)
                {
                    View.AddGridColumn(column);
                }
            }

            base.ProcessGridData();
        }

        public override void OnExportToExcelFired(object sender, EventArgs eventArgs)
        {
            if (WorkItem.Status == WorkItemStatus.Inactive)
            {
                return;
            }

            var filePath = View.GetExcelFilePath(ServerTime.GetDateTimeInfo().Now);

            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            Excel2007Worker worker = null;

            try
            {
                worker = new Excel2007Worker();
                ExcelSheet sheet = worker.CreateFile(filePath);

                int targetRowNumber = 1;

                foreach (Column column in View.DataSourceColumns)
                {
                    if (column.Visible)
                    {
                        sheet.SetCellValue($"{column.ExcelName}{targetRowNumber}", column.ExcelHeader);
                        sheet.SetRangeFormat(
                            $"{column.ExcelName}{targetRowNumber}",
                            $"{column.ExcelName}{targetRowNumber}",
                            column.ExcelHeaderFormat);
                        sheet.SetColumnWidth(
                            $"{column.ExcelName}{targetRowNumber}",
                            $"{column.ExcelName}{targetRowNumber}",
                            column.ExcelWidth);
                    }
                }

                targetRowNumber++;

                foreach (DataRow sourceRow in View.GetDataTable().Rows)
                {
                    foreach (Column column in View.DataSourceColumns)
                    {
                        if (sourceRow[column.FieldName] != DBNull.Value)
                        {
                            string cell = $"{column.ExcelName}{targetRowNumber}";

                            if (column.Visible)
                            {
                                object value =
                                    DataSource.GetExcelCellValue(
                                        sourceRow[column.FieldName],
                                        column.Format,
                                        (CellFormat)(sourceRow[DataSource.SPECIAL_CELLS_FORMAT_COLUMN]
                                            ?? CellFormat.Numeric));
                                string format =
                                    DataSource.GetExcelCellFormat(
                                        column.Format,
                                        (CellFormat)(sourceRow[DataSource.SPECIAL_CELLS_FORMAT_COLUMN]
                                            ?? CellFormat.Numeric));

                                sheet.SetCellValue(cell, value);
                                sheet.SetRangeFormat(cell, cell, format);
                            }
                            else if (column.IsNote)
                            {
                                string noteValue =
                                    DataSource.GetNoteValue(
                                        sourceRow[column.FieldName],
                                        column.Format);

                                sheet.SetNote(cell, cell, noteValue);

                            }
                        }
                    }

                    targetRowNumber++;
                }

                worker.Save();
                worker.Close();

                Process process = new Process();
                process.StartInfo.FileName = filePath;
                process.Start();
            }
            catch (Exception exception)
            {
                Logger.SimpleWrite($"Consolidation Reoprt. Export to Excel exception. {exception}");
                View.ShowMessage("Произошла ошибка при экспорте данных в файл Excel", "Ошибка");
            }
            finally
            {
                if (worker != null)
                {
                    worker.Close();
                }
            }
        }
    }
}
