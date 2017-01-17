namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView
{
    /// <summary>
    /// Параметры работы BaseItemView
    /// </summary>
    public struct BaseItemViewParams
    {
        /// <summary>
        /// Имя слота в WorkItem.State для id текущего редактируемого элемента
        /// </summary>
        public string CurrentItemIdStateName { get; set; }

        /// <summary>
        /// Имя слота в WorkItem.State для текущего редактируемого элемента
        /// </summary>
        public string CurrentItemStateName { get; set; }

        /// <summary>
        /// Имя слота в WorkItem.State для режима редактирования элемента (создать, редактировать)
        /// </summary>
        public string EditItemStateName { get; set; }
    }
}