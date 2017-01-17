namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView.BaseMultipleListView
{
    public interface IBaseMultipleListView : IBaseListView
    {
        /// <summary>
        /// Возвращает массив ID выбранных строк.
        /// </summary>
        string[] SelectedIds { get; }
    }
}