namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseTabbedView
{
    /// <summary>
    /// Параметры работы BaseTabbedView
    /// </summary>
    public struct BaseTabbedViewParams
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

        /// <summary>
        /// Имя слота в WorkItem.State для имени покидаемой закладки
        /// </summary>
        public string LeavingTabNameStateName { get; set; }

        /// <summary>
        /// Имя слота в WorkItem.State для названия вида списка
        /// </summary>
        public string ListViewNameStateName { get; set; }

        /// <summary>
        /// Имя слота в WorkItem.State для названия вида деталей
        /// </summary>
        public string ItemViewNameStateName { get; set; }
    }
}