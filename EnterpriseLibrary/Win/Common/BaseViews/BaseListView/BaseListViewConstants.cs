using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView
{
    /// <summary>
    /// Константы для BaseListView
    /// </summary>
    public class BaseListViewConstants
    {
        /// <summary>
        /// Вовращает дефолтное значение параметров работы BaseListView
        /// </summary>
        public static BaseListViewParams BaseListViewDefaultParams = new BaseListViewParams()
        {
            CurrentItemIdStateName = CommonStateNames.CurrentItemId,
            UpdateWindowTitleOnRowChanged = true
        };
    }
}