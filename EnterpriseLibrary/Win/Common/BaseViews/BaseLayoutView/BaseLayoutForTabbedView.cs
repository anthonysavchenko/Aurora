
using Microsoft.Practices.ObjectBuilder;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView
{
    /// <summary>
    /// Базовый класс вида для юз-кейса
    /// </summary>
    public class BaseLayoutForTabbedView : BaseLayoutView
    {
        /// <summary>
        /// Презентер
        /// </summary>
        [CreateNew]
        public new BaseLayoutForTabbedViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
            get
            {
                return (BaseLayoutForTabbedViewPresenter)base.Presenter;
            }
        }
    }
}