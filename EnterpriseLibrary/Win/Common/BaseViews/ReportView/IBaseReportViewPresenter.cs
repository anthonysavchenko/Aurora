using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.ReportView
{
    public interface IBaseReportViewPresenter : IBasePresenter
    {
        /// <summary>
        /// Запускает генерацию данных для табличной и диаграммной частей отчета
        /// </summary>
        void GenerateReport();
    }
}
