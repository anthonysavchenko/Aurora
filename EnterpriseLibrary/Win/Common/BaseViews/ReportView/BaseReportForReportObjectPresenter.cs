namespace Taumis.EnterpriseLibrary.Win.BaseViews.ReportView
{
    /// <summary>
    /// Базовый презентер для отчетов с ReportObject
    /// </summary>
    /// <typeparam name="TView">Тип вида отчета</typeparam>
    /// <typeparam name="TParamsType">Тип параметов отчета</typeparam>
    public class BaseReportForReportObjectPresenter<TView, TParamsType> : BaseReportViewPresenter<TView, TParamsType>
        where TView : IBaseReportForReportObjectView
        where TParamsType : struct
    {
        /// <summary>
        /// Выполняет действия при загрузке вида
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();

            View.DisposeReportObject();
        }

        /// <summary>
        /// Обрабатывает данные для табличной части отчета 
        /// </summary>
        override protected void ProcessGridData()
        {
            View.ReportTableData = _gridData;
        }

        /// <summary>
        /// Обрабатывает данные для диаграммной части отчета
        /// </summary>
        override protected void ProcessDiagramData()
        {
            View.SeriesSourceForDiagramm = _diagramData;
        }

        /// <summary>
        /// Оповещает о полном окончании процесса генерации отчета
        /// </summary>
        override protected void OnReportGenerationCompleted()
        {
            View.CreateReportDocument();
        }
    }
}
