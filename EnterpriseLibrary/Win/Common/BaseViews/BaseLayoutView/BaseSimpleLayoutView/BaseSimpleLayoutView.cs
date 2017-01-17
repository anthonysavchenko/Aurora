using Microsoft.Practices.ObjectBuilder;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleLayoutView
{
    /// <summary>
    /// Базовый класс вида для юз-кейса
    /// </summary>
    public class BaseSimpleLayoutView : Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView.BaseLayoutView
    {
        /// <summary>
        /// Презентер
        /// </summary>
        [CreateNew]
        public new BaseSimpleLayoutViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
        }
    }
}