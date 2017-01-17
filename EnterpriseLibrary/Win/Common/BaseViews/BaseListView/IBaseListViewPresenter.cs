using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView
{
    /// <summary>
    /// Интерфейс базового презентера вида списка
    /// </summary>
    public interface IBaseListViewPresenter : IBasePresenter
    {
        /// <summary>
        /// Выполняет действия при изменении выбранного элемента
        /// </summary>
        /// <param name="_id">Id выбранного элемента списка</param>
        void OnRowChanged(string _id);

        /// <summary>
        /// Обновить общий список элементов
        /// </summary>
        void RefreshList();

        /// <summary>
        /// Обновляет состояние глобальных кнопок, исходя из текущего выбранного элемента
        /// </summary>
        void UpdateGlobalButtonsForCurrentItem();
   }
}