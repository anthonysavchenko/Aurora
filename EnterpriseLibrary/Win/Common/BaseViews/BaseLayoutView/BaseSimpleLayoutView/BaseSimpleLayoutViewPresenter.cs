using Microsoft.Practices.CompositeUI.Commands;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;
using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleLayoutView
{
    public class BaseSimpleLayoutViewPresenter : BaseLayoutViewPresenter<BaseLayoutView.BaseLayoutView>
    {
        /// <summary>
        /// Выполняет действия при активации юз-кейса
        /// </summary>
        public override void ActivateUseCase()
        {
            // Разрешается команда "Обновить список".
            WorkItem.RootWorkItem.Commands[CommonCommandNames.RefreshItem].Status =
                CommandStatus.Enabled;
        }
    }
}