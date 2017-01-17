using Microsoft.Practices.CompositeUI.Commands;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseTabbedView;
using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Users.Views.Tabbed
{
    public class TabbedViewPresenter : BaseTabbedViewPresenter<IBaseTabbedView, User>
    {
        /// <summary>
        /// Изменить доступ к глобальным командам для закладки вида списка
        /// </summary>
        public override void ManageCommandsForListTab()
        {
            base.ManageCommandsForListTab();

            WorkItem.RootWorkItem.Commands[CommonCommandNames.DeleteItem].Status =
                CommandStatus.Disabled;
        }
    }
}