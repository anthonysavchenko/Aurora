using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Win.Modules.CommonModule;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.Common
{
    public class BasePresenter<TView> : IBasePresenter
        where TView : IBaseView
    {
        private TView _view;
        /// <summary>
        /// Вид
        /// </summary>
        IBaseView IBasePresenter.View
        {
            set
            {
                _view = (TView)value;
                OnViewSet();
            }
        }

        /// <summary>
        /// Вид
        /// </summary>
        public TView View
        {
            get
            {
                return _view;
            }
        }

        /// <summary>
        /// Юзкейс
        /// </summary>
        [ServiceDependency]
        public CommonControlledWorkItem WorkItem
        {
            set;
            get;
        }

        /// <summary>
        /// Сервис получения серверного времени
        /// </summary>
        [ServiceDependency]
        public IServerTimeService ServerTime { set; get; }

        /// <summary>
        /// Выполняет действия при загрузке вида
        /// </summary>
        public virtual void OnViewReady()
        {
        }

        /// <summary>
        /// Выполняет действия при установке вида
        /// </summary>
        protected virtual void OnViewSet()
        {
        }

        /// <summary>
        /// Деструктор
        /// </summary>
        ~BasePresenter()
        {
            Dispose(false);
        }

        /// <summary>
        /// See <see cref="System.IDisposable.Dispose"/> for more information.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Called when the object is being disposed or finalized.
        /// </summary>
        /// <param name="disposing">True when the object is being disposed (and therefore can
        /// access managed members); false when the object is being finalized without first
        /// having been disposed (and therefore can only touch unmanaged members).</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                }
            }
        }

        /// <summary>
        /// Выполняет действия при закрытии вида
        /// </summary>
        protected virtual void CloseView()
        {
            IWorkspaceLocatorService locator = WorkItem.Services.Get<IWorkspaceLocatorService>();
            IWorkspace wks = locator.FindContainingWorkspace(WorkItem, View);
            if (wks != null)
            {
                wks.Close(View);
            }
        }
    }
}