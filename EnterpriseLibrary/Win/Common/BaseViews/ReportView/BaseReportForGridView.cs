using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;
using DevExpress.XtraPrinting;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.ReportView
{
    public class BaseReportForGridView : BaseView, IBaseReportForGridView
    {
        private GridControl _gridControl;
        private GridView _gridView;

        /// <summary>
        /// Инициализирует ссылки на компоненты вида
        /// </summary>
        /// <param name="_gc">Ссылка на экземпляр GridControl </param>
        /// <param name="_gv">Ссылка на экземпляр GridView</param>
        protected void InitReportComponents(GridControl _gc, GridView _gv)
        {
            _gridControl = _gc;
            _gridView = _gv;
        }

        /// <summary>
        /// Данные отчета
        /// </summary>
        public DataTable GridData
        {
            set
            {
                _gridControl.DataSource = value;
                _gridView.BestFitColumns();
            }
        }

        /// <summary>
        /// Обновляет отчет
        /// </summary>
        public void UpdateReport()
        {
            ((IBaseReportViewPresenter)Presenter).GenerateReport();
        }

        /// <summary>
        /// Экспортирует данные отчета в Excel
        /// </summary>
        /// <param name="_filename">Имя (с путем) файла, в который экспортируются данные</param>
        public void ExportToExcel(string _filename)
        {
            _gridView.OptionsPrint.AutoWidth = false;
            _gridView.ExportToXls(_filename, new XlsExportOptions(TextExportMode.Value, true));
        }
    }
}
