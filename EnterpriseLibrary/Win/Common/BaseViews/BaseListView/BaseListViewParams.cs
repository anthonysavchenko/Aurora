namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView
{
    /// <summary>
    /// Параметры работы BaseListView
    /// </summary>
    public struct BaseListViewParams
    {
        /// <summary>
        /// Имя слота в WorkItem.State для id текущего редактируемого элемента
        /// </summary>
        public string CurrentItemIdStateName { get; set; }

        /// <summary>
        /// Признак обновления заголовка окна на изменение выбранной строки
        /// </summary>
        public bool UpdateWindowTitleOnRowChanged { get; set; }
    }
}