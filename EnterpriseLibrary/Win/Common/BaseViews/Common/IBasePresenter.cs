using System;


using Taumis.EnterpriseLibrary.Win.Modules.CommonModule;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.Common
{
    /// <summary>
    /// Интерфейс базового презентера
    /// </summary>
    public interface IBasePresenter : IDisposable
    {
        /// <summary>
        /// Выполняет действия при загрузке вида
        /// </summary>
        void OnViewReady();

        /// <summary>
        /// Вид
        /// </summary>
        IBaseView View { set; }

        /// <summary>
        /// Юзкейс
        /// </summary>
        CommonControlledWorkItem WorkItem
        {
            get;
            set;
        }
    }
}