using Microsoft.Practices.CompositeUI.Commands;

using Taumis.EnterpriseLibrary.Win.BaseViews.Common;
using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView
{
    public abstract class BaseLayoutViewPresenter<TView> : BasePresenter<TView>, IBaseLayoutViewPresenter
        where TView : IBaseLayoutView
    {
        /// <summary>
        /// Выполняет действия при активации юз-кейса
        /// </summary>
        public abstract void ActivateUseCase();

        /// <summary>
        /// Выполняет действия при деактивации юз-кейса
        /// </summary>
        public virtual void DeactivateUseCase()
        {
            // Запрещается команда "Создать".
            WorkItem.RootWorkItem.Commands[CommonCommandNames.CreateNewItem].Status =
                CommandStatus.Disabled;

            // Запрещается команда "Удалить".
            WorkItem.RootWorkItem.Commands[CommonCommandNames.DeleteItem].Status =
                CommandStatus.Disabled;

            // Запрещается команда "Сохранить".
            WorkItem.RootWorkItem.Commands[CommonCommandNames.SaveItem].Status =
                CommandStatus.Disabled;

            // Запрещается команда "Обновить список".
            WorkItem.RootWorkItem.Commands[CommonCommandNames.RefreshItem].Status =
                CommandStatus.Disabled;

            // Запрещается команда "Обновить справочники".
            WorkItem.RootWorkItem.Commands[CommonCommandNames.RefreshRefBooks].Status =
                CommandStatus.Disabled;

            // Запрещается команда "Провести".
            WorkItem.RootWorkItem.Commands[CommonCommandNames.PostItem].Status =
                CommandStatus.Disabled;

            // Запрещается команда "Отменить проведение".
            WorkItem.RootWorkItem.Commands[CommonCommandNames.UnpostItem].Status =
                CommandStatus.Disabled;

            // Запрещается команда "Архивировать".
            WorkItem.RootWorkItem.Commands[CommonCommandNames.ArchiveItem].Status =
                CommandStatus.Disabled;

            // Запрещается команда "Разархивировать".
            WorkItem.RootWorkItem.Commands[CommonCommandNames.UnarchiveItem].Status =
                CommandStatus.Disabled;

            // Запрещается команда "Копировать в Excel".
            WorkItem.RootWorkItem.Commands[CommonCommandNames.ExportToExcel].Status =
                CommandStatus.Disabled;

            // Запрещается команда "Печать".
            WorkItem.RootWorkItem.Commands[CommonCommandNames.PrintItem].Status =
                CommandStatus.Disabled;
        }

        /// <summary>
        /// Обрабатывает закрытие юзкейса
        /// </summary>
        public virtual void CloseUseCase()
        {
            WorkItem.State[CommonStateNames.ItemState] = CommonItemStates.NotChanged;      
            CloseView();
        }
    }
}