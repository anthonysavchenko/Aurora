using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseTabbedView
{
    /// <summary>
    /// Константы для BaseTabbedView
    /// </summary>
    public class BaseTabbedViewConstants
    {
        /// <summary>
        /// Вовращает дефолтное значение параметров работы BaseTabbedView
        /// </summary>
        public static BaseTabbedViewParams BaseTabbedViewDefaultParams = new BaseTabbedViewParams()
        {
            CurrentItemIdStateName = CommonStateNames.CurrentItemId,
            CurrentItemStateName = CommonStateNames.CurrentItem,
            EditItemStateName = CommonStateNames.EditItemState,
            LeavingTabNameStateName = CommonStateNames.LeavingTabName,
            ListViewNameStateName = "ListView",
            ItemViewNameStateName = "ItemView"
        };
    }
}