using Taumis.EnterpriseLibrary.Win.BaseViews.BaseTabbedView;
using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView
{
    public class BaseLayoutForTabbedViewPresenter : BaseLayoutViewPresenter<BaseLayoutView>
    {
        /// <summary>
        /// Выполняет действия при активации юз-кейса
        /// </summary>
        public override void ActivateUseCase()
        {
            IBaseTabbedView _tv = (IBaseTabbedView)WorkItem.SmartParts.Get("TabbedView");

            if (_tv.CurrentTab == "tabList")
            {
                _tv.ManageCommandsForListTab();
            }
            else
            {
                _tv.ManageCommandsForNotListTab();
            }
        }

        /// <summary>
        /// Выполняет действия при загрузке вью
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();

            WorkItem.State[CommonStateNames.ItemState] = CommonItemStates.NotChanged;

            WorkItem.State[CommonStateNames.EditItemState] = CommonEditItemStates.Edit;
        }
    }
}