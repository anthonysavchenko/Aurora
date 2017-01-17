using Microsoft.Practices.CompositeUI.WinForms;
using System.Windows.Forms;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseTabbedView
{
    /// <summary>
    /// Базовый класс вида с вкладками
    /// </summary>
    public class BaseTabbedView : BaseView, IBaseTabbedView
    {
        /// <summary>
        /// Презентер
        /// </summary>
        private new IBaseTabbedViewPresenter Presenter
        {
            get
            {
                return (IBaseTabbedViewPresenter)base.Presenter;
            }
        }

        /// <summary>
        /// Вкладки
        /// </summary>
        private TabWorkspace TabWorkspace
        {
            get;
            set;
        }

        /// <summary>
        /// Выполняет инициализацию вида
        /// </summary>
        /// <param name="_tabWokrspace">Вкладки</param>
        protected void Initialize(TabWorkspace _tabWokrspace)
        {
            TabWorkspace = _tabWokrspace;
            TabWorkspace.Selecting += tabWorkspace_Selecting;
            TabWorkspace.Deselecting += tabWorkspace_Deselecting;
        }

        /// <summary>
        /// При выборе закладки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void tabWorkspace_Selecting(object sender, TabControlCancelEventArgs e)
        {
            bool _cancelAction;
            Presenter.OnEnterTabPage(e.TabPage.Name, out _cancelAction);
            e.Cancel = _cancelAction;
        }

        /// <summary>
        /// При выходе с закладки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void tabWorkspace_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            bool _cancelAction;
            Presenter.OnLeaveTabPage(e.TabPage.Name, out _cancelAction);
            e.Cancel = _cancelAction;
        }

        #region IBaseTabbedView members

        /// <summary>
        /// Показывает диалог вопроса пользователю
        /// </summary>
        /// <param name="_text">Текст вопроса</param>
        /// <param name="_caption">Заголовок окна диалога</param>
        /// <returns>Возвращает результат ответа пользователя: да - true, нет - false</returns>
        public bool ShowQuestionDialog(string _text, string _caption)
        {
            return MessageBox.Show(
                _text,
                _caption,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1) == DialogResult.Yes;
        }

        /// <summary>
        /// Выбрать закладку
        /// </summary>
        /// <param name="_tabName">Наименование закладки</param>
        public void SelectTab(string _tabName)
        {
            TabWorkspace.SelectTab(_tabName);
        }

        /// <summary>
        /// Возвращает наименование текущей закладки
        /// </summary>
        public string CurrentTab
        {
            get
            {
                return TabWorkspace.SelectedTab.Name;
            }
        }

        /// <summary>
        /// Управляет доступностью команд для закладки списка
        /// </summary>
        public void ManageCommandsForListTab()
        {
            Presenter.ManageCommandsForListTab();
        }

        /// <summary>
        /// Управляет доступностью команд для закладки, отличной от списка
        /// </summary>
        public void ManageCommandsForNotListTab()
        {
            Presenter.ManageCommandsForNotListTab();
        }

        /// <summary>
        /// Определяет наличие вкладки с указанным именем
        /// </summary>
        /// <param name="_tabName">Имя вкладки</param>
        /// <returns>true, если вкладка с указанным именем существует; иначе - false</returns>
        public bool ContainsTab(string _tabName)
        {
            bool _result = false;

            foreach(TabPage _tabPage in TabWorkspace.Pages.Values)
            {
                if (_tabPage.Name == _tabName)
                {
                    _result = true;
                    break;
                }
            }

            return _result;
        }

        #endregion
    }
}