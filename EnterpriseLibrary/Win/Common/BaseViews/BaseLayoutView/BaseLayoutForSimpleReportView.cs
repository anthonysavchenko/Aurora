

using Microsoft.Practices.ObjectBuilder;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView
{
    /// <summary>
    /// Базовый класс вида для юз-кейса
    /// </summary>
    public class BaseLayoutForSimpleReportView : BaseLayoutView
    {
        /// <summary>
        /// Презентер
        /// </summary>
        [CreateNew]
        public new BaseLayoutForSimpleReportViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
            get
            {
                return (BaseLayoutForSimpleReportViewPresenter)base.Presenter;
            }
        }       
    }
}