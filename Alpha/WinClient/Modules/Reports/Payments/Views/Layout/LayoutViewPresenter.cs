using Microsoft.Practices.CompositeUI.Commands;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;
using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Payments.Views.Layout
{
    public class LayoutViewPresenter : BaseLayoutForReportViewPresenter
    {
        /// <summary>
        /// Выполняет действия при активации юз-кейса
        /// </summary>
        public override void ActivateUseCase()
        {
            base.ActivateUseCase();
            // Разрешается команда "Экспортировать в Excel".
            WorkItem.RootWorkItem.Commands[CommonCommandNames.ExportToExcel].Status =
                CommandStatus.Enabled;
        }
    }
}
