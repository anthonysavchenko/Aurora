using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseTabbedView
{
    public interface IBaseTabbedViewPresenter : IBaseDomainPresenter
    {
        /// <summary>
        /// Выполняет действия при входе на закладку
        /// </summary>
        /// <param name="_tabPageName">Имя закладки</param>
        /// <param name="_cancelAction">Признак отмены действия выхода с закладки</param>
        void OnEnterTabPage(string _tabPageName, out bool _cancelAction);

        /// <summary>
        /// Выполняет действия при выходе с закладки
        /// </summary>
        /// <param name="_tabPageName">Имя закладки</param>
        /// <param name="_cancelAction">Признак отмены действия выхода с закладки</param>
        void OnLeaveTabPage(string _tabPageName, out bool _cancelAction);

        /// <summary>
        /// Управляет доступностью команд для закладки списка
        /// </summary>
        void ManageCommandsForListTab();

        /// <summary>
        /// Управляет доступностью команд для закладки, отличной от списка
        /// </summary>
        void ManageCommandsForNotListTab();
    }
}