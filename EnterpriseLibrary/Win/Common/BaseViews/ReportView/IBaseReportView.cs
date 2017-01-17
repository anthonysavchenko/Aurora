using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.ReportView
{
    /// <summary>
    /// Интерфейс базового вида отчета
    /// </summary>
    public interface IBaseReportView : IBaseView
    {
        /// <summary>
        /// Обновляет отчет
        /// </summary>
        void UpdateReport();        
    }
}

