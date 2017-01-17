using System.Data;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.ReportView
{
    /// <summary>
    /// Интерфейс базового вида для отчетов с ReportObject
    /// </summary>
    public interface IBaseReportForGridView : IBaseReportView
    {
        /// <summary>
        /// Данные отчета
        /// </summary>
        DataTable GridData { set; }

        /// <summary>
        /// Экспортирует в Excel
        /// </summary>
        /// <param name="_filename">Имя (с путем) файла, в который экспортируются данные</param>
        void ExportToExcel(string _filename);
    }
}

