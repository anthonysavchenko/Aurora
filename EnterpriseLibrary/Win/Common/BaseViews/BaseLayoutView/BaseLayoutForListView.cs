
using Microsoft.Practices.ObjectBuilder;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView
{
    /// <summary>
    /// Базовый класс вида для юз-кейса
    /// </summary>
    public class BaseLayoutForListView : BaseLayoutView
    {
        /// <summary>
        /// Презентер
        /// </summary>
        [CreateNew]
        public new BaseLayoutForListViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
        }
    }
}