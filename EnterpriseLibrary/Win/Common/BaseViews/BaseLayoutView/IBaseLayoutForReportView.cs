namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView
{
    public interface IBaseLayoutForReportView
    {
        /// <summary>
        /// Отображение вида с прогрессбаром
        /// </summary>
        void ShowStatusBar();

        /// <summary>
        /// Скрытие вида с прогрессбаром
        /// </summary>
        void HideStatusBar();
    }
}

