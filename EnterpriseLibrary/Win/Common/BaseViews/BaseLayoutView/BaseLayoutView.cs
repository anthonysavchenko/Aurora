using System;
using System.Windows.Forms;


using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView
{
    /// <summary>
    /// Базовый класс вида для юз-кейса
    /// </summary>
    public class BaseLayoutView : BaseView, IBaseLayoutView
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BaseLayoutView()
        {
            Dock = System.Windows.Forms.DockStyle.Fill;
        }

        /// <summary>
        /// Презентер
        /// </summary>
        private new IBaseLayoutViewPresenter Presenter
        {
            get
            {
                return (IBaseLayoutViewPresenter)base.Presenter;
            }
        }

        /// <summary>
        /// Действия на загрузку вида
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            if (null != ParentForm)
            {
                // Обработчик активации вида управляет доступностью глобальных комманд
                ParentForm.Activated += new EventHandler(ParentForm_Activated);

                // Обработчик деактивации вида управляет доступностью глобальных комманд
                ParentForm.Deactivate += new EventHandler(ParentForm_Deactivate);

                ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_OnClosing);
            }

            base.OnLoad(e);
        }

        /// <summary>
        /// Обработчик активации формы юзкейса
        /// </summary>
        void ParentForm_Activated(object sender, EventArgs e)
        {
            Presenter.ActivateUseCase();
        }

        /// <summary>
        /// Обработчик деактивации формы юзкейса
        /// </summary>
        void ParentForm_Deactivate(object sender, EventArgs e)
        {
            Presenter.DeactivateUseCase();
        }

        /// <summary>
        /// Обработчик закрытия формы юзкейса
        /// </summary>
        protected void ParentForm_OnClosing(object sender, FormClosingEventArgs e)
        {
            Presenter.CloseUseCase();
        }

        #region IBaseLayoutView Members

        /// <summary>
        /// Закрывает активный юзкейс
        /// </summary>
        public virtual void CloseView()
        {
            Presenter.CloseUseCase();
        }

        #endregion
    }
}