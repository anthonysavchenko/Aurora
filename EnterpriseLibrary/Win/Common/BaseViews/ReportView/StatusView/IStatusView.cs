namespace Taumis.EnterpriseLibrary.Win.BaseViews.ReportView.StatusView
{
    /// <summary>
    /// Интерфейс вида с бегущим прогрессбаром для отчетов
    /// </summary>
    public interface IStatusView
    {
        /// <summary>
        /// Запуск бегущей строки
        /// </summary>
        void StartMarqueProgress();

        /// <summary>
        /// Остановка бегущей строки
        /// </summary>
        void StopMarqueProgress();
    }
}
