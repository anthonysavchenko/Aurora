using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView
{
    /// <summary>
    /// Константы для BaseItemView
    /// </summary>
    public class BaseItemViewConstants
    {
        /// <summary>
        /// Вовращает дефолтное значение параметров работы BaseItemView
        /// </summary>
        public static BaseItemViewParams BaseItemViewDefaultParams = new BaseItemViewParams()
        {
            CurrentItemIdStateName = CommonStateNames.CurrentItemId,
            CurrentItemStateName = CommonStateNames.CurrentItem,
            EditItemStateName = CommonStateNames.EditItemState
        };
    }
}