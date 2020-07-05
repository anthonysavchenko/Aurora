using Microsoft.Practices.CompositeUI.Commands;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.FillForms.Constants;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseTabbedView;
using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.FillForms.Views.Tabbed
{
    /// <summary>
    /// Презентер
    /// </summary>
    public class TabbedViewPresenter : BaseTabbedViewPresenter<ITabbedView, RouteForm>
    {
        /// <summary>
        /// Выполняет подготовительные действия перед началом редактирования домена
        /// </summary>
        /// <param name="_cancelAction">Признак отмены действия</param>
        protected override void PrepareDomainEditing(out bool _cancelAction)
        {
            string _curId = (string)WorkItem.State[Params.CurrentItemIdStateName];

            _cancelAction = string.IsNullOrEmpty(_curId);

            if (!_cancelAction)
            {
                WorkItem.State[Params.CurrentItemStateName] = GetItem<RouteForm>(_curId);
            }
        }

        /// <summary>
        /// Выполняет действия при входе на закладку
        /// </summary>
        /// <param name="_tabPageName">Имя закладки</param>
        /// <param name="_cancelAction">Признак отмены действия выхода с закладки</param>
        public override void OnEnterTabPage(string _tabPageName, out bool _cancelAction)
        {
            _cancelAction = false;

            switch (_tabPageName)
            {
                case TabNames.LIST:
                    ManageCommandsForListTab();
                    ((IBaseListView)WorkItem.SmartParts.Get(ModuleViewNames.LIST_VIEW)).RefreshList();
                    break;

                case TabNames.DETAIL:
                    if (((string)WorkItem.State[Params.LeavingTabNameStateName]) == TabNames.LIST)
                    {
                        PrepareDomainEditing(out _cancelAction);
                    }
                    if (!_cancelAction)
                    {
                        ManageCommandsForNotListTab();
                        IBaseListView _view = (IBaseListView)WorkItem.SmartParts.Get(ModuleViewNames.ITEM_VIEW);
                        _view.RefreshList();
                    }
                    break;
            }
        }

        public override void ManageCommandsForListTab()
        {
            base.ManageCommandsForListTab();

            WorkItem.RootWorkItem.Commands[CommonCommandNames.CreateNewItem].Status =
                CommandStatus.Disabled;
            WorkItem.RootWorkItem.Commands[CommonCommandNames.DeleteItem].Status =
                CommandStatus.Disabled;
            WorkItem.RootWorkItem.Commands[CommonCommandNames.ExportToExcel].Status =
                CommandStatus.Disabled;
        }

        public override void ManageCommandsForNotListTab()
        {
            base.ManageCommandsForNotListTab();

            WorkItem.RootWorkItem.Commands[CommonCommandNames.SaveItem].Status =
                CommandStatus.Disabled;
            WorkItem.RootWorkItem.Commands[CommonCommandNames.RefreshRefBooks].Status =
                CommandStatus.Disabled;
        }
    }
}
