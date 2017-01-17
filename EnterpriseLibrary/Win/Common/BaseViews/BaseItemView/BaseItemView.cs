using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView
{
    /// <summary>
    /// Базовый класс вида для деталей домена
    /// </summary>
    public class BaseItemView : BaseView, IBaseItemView, IBaseItemViewPresenterOwner
    {
        /// <summary>
        /// Презентер
        /// </summary>
        private new IBaseItemViewPresenter Presenter
        {
            get
            {
                return (IBaseItemViewPresenter)base.Presenter;
            }
        }

        private SimpleItemViewMapper _simpleItemViewMapper;
        /// <summary>
        /// Класс для преобразования элементов домена в DevExpress элементы вида
        /// </summary>
        protected SimpleItemViewMapper GetSimpleItemViewMapper
        {
            get
            {
                if (_simpleItemViewMapper == null)
                {
                    _simpleItemViewMapper = new SimpleItemViewMapper(this);
                }

                return _simpleItemViewMapper;
            }
        }

        #region IBaseItemView Members

        /// <summary>
        /// Подключить общий обработчик изменений
        /// </summary>
        public void BindActivate()
        {
            Presenter.BindChangeHandlers(Controls);
        }

        /// <summary>
        /// Отключить общий обработчик изменений
        /// </summary>
        public void BindDeactivate()
        {
            Presenter.UnBindChangeHandlers(Controls);
        }

        /// <summary>
        /// Отобразить домен текущего элемента списка на виде
        /// </summary>
        public void ShowDomainToView()
        {
            Presenter.ShowDomainToView();
        }

        #endregion

        #region IBaseItemViewPresenterOwner Members

        /// <summary>
        /// Презентер
        /// </summary>
        IBaseItemViewPresenter IBaseItemViewPresenterOwner.Presenter
        {
            get
            {
                return (IBaseItemViewPresenter)base.Presenter;
            }
        }

        #endregion
    }
}