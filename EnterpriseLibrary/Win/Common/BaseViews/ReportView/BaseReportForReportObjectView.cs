using DevExpress.XtraPrinting.Control;
using System.Data;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.ReportView
{
    /// <summary>
    ///  Базовый вид для отчетов с ReportObject
    /// </summary>
    /// <typeparam name="TReportObject">Тип объекта зачета</typeparam>
    public abstract class BaseReportForReportObjectView<TReportObject> : BaseView, IBaseReportForReportObjectView
        where TReportObject : BaseReportObject, new()
    {
        /// <summary>
        /// Control для отображения отчета
        /// </summary>
        protected abstract PrintControl ReportPrintControl { get; }

        /// <summary>
        /// Объект отчета
        /// </summary>
        protected TReportObject _report = null;

        /// <summary>
        /// Возвращает объект отчета, создавая его, если он еще не создан
        /// </summary>
        protected TReportObject Report
        {
            get
            {
                if (_report == null)
                {
                    _report = new TReportObject();
                    ReportPrintControl.PrintingSystem = _report.PrintingSystem;
                }
                return _report;
            }
        }

        #region IBaseReportForReportObjectView members

        /// <summary>
        /// Данные для диаграммы
        /// </summary>
        public object SeriesSourceForDiagramm
        {
            set
            {
                Report.SeriesSourceForDiagramm = value;
            }
        }

        /// <summary>
        /// Данные для табличной части отчета
        /// </summary>
        public DataTable ReportTableData
        {
            set
            {
                Report.DataSource = value;
            }
        }

        /// <summary>
        /// Создает отчет на экране
        /// </summary>
        public void CreateReportDocument()
        {
            Report.CreateDocument();
        }

        /// <summary>
        /// Очищает данные на отчете
        /// </summary>
        public void DisposeReportObject()
        {
            if (_report != null)
            {
                _report.Dispose();
                _report = null;
            }
        }

        /// <summary>
        /// Обновляет отчет
        /// </summary>
        public void UpdateReport()
        {
            DisposeReportObject();

            ((IBaseReportViewPresenter)Presenter).GenerateReport();
        }

        #endregion
    }
}