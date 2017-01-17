using Microsoft.Practices.CompositeUI.Commands;

using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView
{
    public class BaseLayoutForSimpleReportViewPresenter : BaseLayoutViewPresenter<BaseLayoutView>
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