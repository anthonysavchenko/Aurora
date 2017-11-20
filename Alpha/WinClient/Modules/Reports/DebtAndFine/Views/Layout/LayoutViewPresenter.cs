using Microsoft.Practices.CompositeUI.Commands;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;
using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.DebtAndFine.Views.Layout
{
    public class LayoutViewPresenter : BaseLayoutForReportViewPresenter
    {
        /// <summary>
        /// Выполняет действия при активации юз-кейса
        /// </summary>
        public override void ActivateUseCase()
        {
            base.ActivateUseCase();
            // Разрешается команда "Обновить справочники".
            WorkItem.RootWorkItem.Commands[CommonCommandNames.RefreshRefBooks].Status =
                CommandStatus.Enabled;
            // Разрешается команда "Экспортировать в Excel".
            WorkItem.RootWorkItem.Commands[CommonCommandNames.ExportToExcel].Status =
                CommandStatus.Enabled;
            WorkItem.RootWorkItem.Commands[CommonCommandNames.SaveItem].Status =
                CommandStatus.Enabled;
        }
    }
}
