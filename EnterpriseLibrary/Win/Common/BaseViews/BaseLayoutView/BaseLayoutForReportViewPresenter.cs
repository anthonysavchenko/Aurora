using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView.StatusView;
using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView
{
    public class BaseLayoutForReportViewPresenter : BaseLayoutViewPresenter<BaseLayoutForReportView>
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

        /// <summary>
        /// Подписчик на запрос отображения "бегущей" строки состояния
        /// </summary>
        [EventSubscription(CommonEventNames.ShowMarqueeProgressBarFired, ThreadOption.UserInterface)]
        public virtual void OnShowMarqueeProgressBarFired(object sender, EventArgs e)
        {
            WorkItem.SmartParts.Get<IStatusView>("StatusView").StartMarqueProgress();
            View.ShowStatusBar();
        }

        /// <summary>
        /// Подписчик на запрос скрытия "бегущей" строки состояния
        /// </summary>
        [EventSubscription(CommonEventNames.HideMarqueeProgressBarFired, ThreadOption.UserInterface)]
        public virtual void OnHideMarqueeProgressBarFired(object sender, EventArgs e)
        {
            WorkItem.SmartParts.Get<IStatusView>("StatusView").StopMarqueProgress();
            View.HideStatusBar();
        }
    }
}