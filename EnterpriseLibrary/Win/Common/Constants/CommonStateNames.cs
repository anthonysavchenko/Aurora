namespace Taumis.EnterpriseLibrary.Win.Constants
{
    /// <summary>
    /// Константы состояний юзкейса. 
    /// </summary>
    public static class CommonStateNames
    {
        /// <summary>
        /// Состояние изменённости элемента. (Изменён, не изменён);
        /// </summary>
        public const string ItemState = "state://CommonStateName/ItemState";

        /// <summary>
        /// Режим редактирования элемента/документа (создать, редактировать).
        /// </summary>
        public const string EditItemState = "state://CommonStateName/EditItemState";

        /// <summary>
        /// Id текущего элемента списка элементов.
        /// </summary>
        public const string CurrentItemId = "state://CommonStateName/CurrentItemId";

        /// <summary>
        /// Текущий обрабатываемый элемент.
        /// </summary>
        public const string CurrentItem = "state://CommonStateName/CurrentItem";

        /// <summary>
        /// Имя покидаемой закладки
        /// </summary>
        public const string LeavingTabName = "state://CommonStateName/LastTabName";

        /// <summary>
        /// Признак успешного сохранения редактируемого элемента
        /// </summary>
        public const string IsSaveSucceeded = "state://CommonStateName/IsSaveSucceeded";
    }
}