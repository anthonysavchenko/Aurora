using System.Data;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.ReportView
{
    /// <summary>
    /// Интерфейс базового вида для отчетов с ReportObject
    /// </summary>
    public interface IBaseReportForReportObjectView : IBaseReportView
    {
        /// <summary>
        /// Данные для диаграммы
        /// </summary>
        object SeriesSourceForDiagramm { set; }

        /// <summary>
        /// Данные для табличной части отчета
        /// </summary>
        DataTable ReportTableData { set; }

        /// <summary>
        /// Создает отчет на экране
        /// </summary>
        void CreateReportDocument();

        /// <summary>
        /// Очищает данные на отчете
        /// </summary>
        void DisposeReportObject();
    }
}