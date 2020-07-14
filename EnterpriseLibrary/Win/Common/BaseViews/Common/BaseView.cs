using System;
using System.Windows.Forms;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.Common
{
    /// <summary>
    /// Базовый класс вида
    /// </summary>
    public class BaseView : UserControl, IBaseView
    {
        private IBasePresenter _presenter;
        /// <summary>
        /// Презентер
        /// </summary>
        protected IBasePresenter Presenter
        {
            get
            {
                return _presenter;
            }
            set
            {
                _presenter = value;
                _presenter.View = this;
            }
        }

        /// <summary>
        /// Действия на загрузку вида
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (null != Presenter)
            {
                Presenter.OnViewReady();
            }
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Presenter != null)
                {
                    Presenter.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Показывает сообщение пользователю
        /// </summary>
        /// <param name="_text">Текст сообщения</param>
        /// <param name="_caption">Заголовок окна</param>
        public void ShowMessage(string _text, string _caption)
        {
            MessageBox.Show(_text, _caption);
        }

        public bool IsOk(string text, string caption)
        {
            return MessageBox.Show(text, caption, MessageBoxButtons.YesNo) == DialogResult.Yes;
        }
    }
}