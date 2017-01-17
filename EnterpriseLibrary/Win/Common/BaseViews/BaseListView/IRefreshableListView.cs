namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView
{
    /// <summary>
    /// Интерфейс списка с возможностью запуска обновления
    /// из другого по отношению списка юзкейса.
    /// </summary>
    public interface IRefreshableListView
    {
        /// <summary>
        /// Обновить данные списка.
        /// </summary>
        void RefreshElemList();
    }
}