
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView
{
    /// <summary>
    /// Интерфейс базового презентера 
    /// </summary>
    public interface IBaseSimpleListViewPresenter : IBaseDomainPresenter
    {
        /// <summary>
        /// Добавить/Обновить элемент.
        /// </summary>
        void AddOrUpdateElem();

        /// <summary>
        /// Удалить текущий элемент.
        /// </summary>
        void DeleteElem();

        /// <summary>
        /// Обновить список.
        /// </summary>
        void RefreshList();

        /// <summary>
        /// Обработка нажатия на кнопку навигатора "Добавить"
        /// </summary>
        /// <returns>Возвращает, обработано ли событие</returns>
        bool NavigatorBtnAppend();

        /// <summary>
        /// Обработка нажатия на кнопку навигатора "Удалить"
        /// </summary>
        /// <returns>Возвращает, обработано ли событие</returns>
        bool NavigatorBtnRemove();

        /// <summary>
        /// Обработка нажатия на кнопку навигатора "Закончить редактирование"
        /// </summary>
        /// <returns>Возвращает, обработано ли событие</returns>
        bool NavigatorBtnEndEdit();
    }
}
