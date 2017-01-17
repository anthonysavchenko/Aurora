using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView
{
    /// <summary>
    /// Интерфейс вида деталей
    /// </summary>
    public interface IBaseItemView : IBaseView
    {
        /// <summary>
        /// Подключить общий обработчик изменений
        /// </summary>
        void BindActivate();

        /// <summary>
        /// Отключить общий обработчик изменений
        /// </summary>
        void BindDeactivate();

        /// <summary>
        /// Отобразить домен текущего элемента списка на виде
        /// </summary>
        void ShowDomainToView();
    }
}