namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Constants
{
    internal static class ModuleStateNames
    {
        /// <summary>
        /// Выбранна позиция абонента
        /// </summary>
        public const string CURRENT_CUSTOMER_POS = "state://CurrentCustomerPos";

        /// <summary>
        /// Выбранный прибор учета
        /// </summary>
        public const string CURRENT_PRIVATE_COUNTER = "state://CurrentPrivateCounter";

        /// <summary>
        /// Идентификаторы выбранных элементов
        /// </summary>
        public const string SELECTED_ITEM_IDS = "state://Customers/SelectedItemIDs";

        /// <summary>
        /// Режим редактирования элементов списка
        /// </summary>
        public const string EDIT_ITEM_MODE = "state://Customers/EditItemMode";
    }
}