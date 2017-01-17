namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView
{
    /// <summary>
    /// Интерфейс владельца презентера типа IBaseItemViewPresenter
    /// </summary>
    public interface IBaseItemViewPresenterOwner
    {
        /// <summary>
        /// Презентер
        /// </summary>
        IBaseItemViewPresenter Presenter { get; }
    }
}