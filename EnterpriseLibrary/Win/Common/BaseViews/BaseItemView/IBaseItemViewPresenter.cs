using System.Windows.Forms;

using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView
{
    public interface IBaseItemViewPresenter : IBaseDomainPresenter
    {
        /// <summary>
        /// Подключить общий обработчик изменений
        /// </summary>
        void BindChangeHandlers(Control.ControlCollection _coll);

        /// <summary>
        /// Отключить общий обработчик изменений
        /// </summary>
        void UnBindChangeHandlers(Control.ControlCollection _coll);

        /// <summary>
        /// Отобразить домен текущего элемента списка на виде
        /// </summary>
        void ShowDomainToView();
    }
}