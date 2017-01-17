using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseTabbedView
{
    public interface IBaseTabbedView : IBaseView
    {
        /// <summary>
        /// Выбрать закладку
        /// </summary>
        /// <param name="_tabName">Наименование закладки</param>
        void SelectTab(string _tabName);

        /// <summary>
        /// Показывает диалог вопроса пользователю
        /// </summary>
        /// <param name="_text">Текст вопроса</param>
        /// <param name="_caption">Заголовок окна диалога</param>
        /// <returns>Возвращает результат ответа пользователя: да - true, нет - false</returns>
        bool ShowQuestionDialog(string _text, string _caption);

        /// <summary>
        /// Возвращает наименование текущей закладки
        /// </summary>
        string CurrentTab { get; }

        /// <summary>
        /// Управляет доступностью команд для закладки списка
        /// </summary>
        void ManageCommandsForListTab();

        /// <summary>
        /// Управляет доступностью команд для закладки, отличной от списка
        /// </summary>
        void ManageCommandsForNotListTab();
 
        /// <summary>
        /// Определяет наличие вкладки с указанным именем
        /// </summary>
        /// <param name="_tabName">Имя вкладки</param>
        /// <returns>true, если вкладка с указанным именем существует; иначе - false</returns>
        bool ContainsTab(string _tabName);
   }
}