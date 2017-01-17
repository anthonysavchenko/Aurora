using Microsoft.Practices.ObjectBuilder;
using System;
using System.Windows.Forms;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView
{
    /// <summary>
    /// Базовый класс вида для юз-кейса
    /// </summary>
    public abstract class BaseLayoutForReportView : BaseLayoutView, IBaseLayoutForReportView
    {
        /// <summary>
        /// Презентер
        /// </summary>
        [CreateNew]
        public new BaseLayoutForReportViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
            get
            {
                return (BaseLayoutForReportViewPresenter)base.Presenter;
            }
        }

        /// <summary>
        /// Ссылка на SplitContainer, который должен реализовать наследник
        /// </summary>
        protected abstract SplitContainer LayoutSplitContainer
        {
            get;
        }

        /// <summary>
        /// На первую загрузку вида
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);            
        }        

        /// <summary>
        /// Отображение вида с прогрессбаром
        /// </summary>
        public void ShowStatusBar()
        {
            LayoutSplitContainer.Panel2Collapsed = false;
        }

        /// <summary>
        /// Скрытие вида с прогрессбаром
        /// </summary>
        public void HideStatusBar()
        {
            LayoutSplitContainer.Panel2Collapsed = true;
        }
    }
}