using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView
{
    /// <summary>
    /// Интерфейс главного вида юзкейса
    /// </summary>
    public interface IBaseLayoutView : IBaseView
    {
        /// <summary>
        /// Закрывает активный юзкейс
        /// </summary>
        void CloseView();
    }
}