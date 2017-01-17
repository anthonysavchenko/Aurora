using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using System;
using System.Windows.Forms;
using Taumis.EnterpriseLibrary.Win.Constants;
using System.Diagnostics;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.ReportView
{
    /// <summary>
    /// Базовый презентер для отчетов с Grid
    /// </summary>
    /// <typeparam name="TView">Тип вида отчета</typeparam>
    /// <typeparam name="TParamsType">Тип параметов отчета</typeparam>
    public class BaseReportForGridPresenter<TView, TParamsType> : BaseReportViewPresenter<TView, TParamsType>
        where TView : IBaseReportForGridView
        where TParamsType : struct
    {
        /// <summary>
        /// Обрабатывает данные для табличной части отчета 
        /// </summary>
        override protected void ProcessGridData()
        {
            View.GridData = _gridData;
        }

        /// <summary>
        /// Обрабатывает глобальное событие - "Экспорт в Excel"
        /// </summary>
        [EventSubscription(CommonEventNames.ExportToExcelFired, ThreadOption.UserInterface)]
        public virtual void OnExportToExcelFired(object sender, EventArgs eventArgs)
        {
            if (WorkItem.Status == WorkItemStatus.Inactive) return;

            SaveFileDialog _saveFileDialog = new SaveFileDialog()
            {
                Title = "Сохранить в файл",
                Filter = "Файл Excel 97-2003 (*.xls)|*.xls",
                FilterIndex = 1,
                RestoreDirectory = true,
                DefaultExt = "xls",
                FileName = "Экспорт",
                AddExtension = true,
            };

            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                View.ExportToExcel(_saveFileDialog.FileName);

                Process process = new Process();
                process.StartInfo.FileName = _saveFileDialog.FileName;
                process.Start();
            }
        }
    }
}
