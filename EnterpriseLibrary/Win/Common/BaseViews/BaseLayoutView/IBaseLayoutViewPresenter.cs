using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView
{
    /// <summary>
    /// Интерфейс презентера главного вида
    /// </summary>
    public interface IBaseLayoutViewPresenter : IBasePresenter
    {
        /// <summary>
        /// Выполняет действия при активации юз-кейса
        /// </summary>
        void ActivateUseCase();

        /// <summary>
        /// Выполняет действия при деактивации юз-кейса
        /// </summary>
        void DeactivateUseCase();

        /// <summary>
        /// Обрабатывает закрытие юзкейса
        /// </summary>
        void CloseUseCase();
    }
}