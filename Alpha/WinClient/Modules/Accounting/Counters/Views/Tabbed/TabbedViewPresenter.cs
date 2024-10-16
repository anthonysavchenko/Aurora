﻿using Microsoft.Practices.CompositeUI.Commands;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Wizard;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseTabbedView;
using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Tabbed
{
    /// <summary>
    /// Презентер
    /// </summary>
    public class TabbedViewPresenter : BaseTabbedViewPresenter<ITabbedView, PrivateCounter>
    {
        /// <summary>
        /// Обрабатывает событие создания нового элемента
        /// </summary>
        protected override void OnCreateNewItem()
        {
            View.ShowWizardTab();
            View.SelectTab(TabNames.WIZARD);
            ((IWizardView)WorkItem.SmartParts.Get(ModuleViewNames.WIZARD_VIEW)).StartWizard();
        }

        /// <summary>
        /// Выполняет действия при загрузке вида
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();
            View.HideWizardTab();
        }

        /// <summary>
        /// Обрабатывает событие покидания закладки
        /// </summary>
        public override void OnLeaveTabPage(string _tabPageName, out bool _cancelAction)
        {
            _cancelAction = false;
            if (_tabPageName == TabNames.WIZARD)
            {
                IWizardView _wizard = ((IWizardView)WorkItem.SmartParts.Get(ModuleViewNames.WIZARD_VIEW));
                if (_wizard.IsMasterInProgress)
                {
                    _cancelAction = true;
                }
                else
                {
                    if (!_wizard.IsMasterCompleted)
                    {
                        _cancelAction = !View.ShowQuestionDialog(@"Все несохраненные данные будут утеряны. 
Вы действительно хотите продолжить?", "Прекращение работы мастера");
                    }
                }

                if (!_cancelAction && !_wizard.IsMasterCompleted)
                {
                    _cancelAction = true;
                    _wizard.IsMasterCompleted = true;
                    View.SelectTab(TabNames.LIST);
                }
            }
            else
            {
                base.OnLeaveTabPage(_tabPageName, out _cancelAction);
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

            if (_tabPageName != TabNames.WIZARD)
            {
                View.HideWizardTab();
                base.OnEnterTabPage(_tabPageName, out _cancelAction);
            }
        }

        /// <summary>
        /// Изменяет доступ к глобальным командам для закладки со списком наборов платежей
        /// </summary>
        private void ManageCommandsForPaymentSetListTab()
        {
            base.ManageCommandsForListTab();

            WorkItem.RootWorkItem.Commands[CommonCommandNames.DeleteItem].Status =
                CommandStatus.Disabled;
            WorkItem.RootWorkItem.Commands[CommonCommandNames.ExportToExcel].Status =
                CommandStatus.Disabled;
        }
        /// <summary>
        /// Изменяет доступ к глобальным командам для закладки с деталями платежа
        /// </summary>
        private void ManageCommandsForPaymentDetailTab()
        {
            base.ManageCommandsForNotListTab();

            WorkItem.RootWorkItem.Commands[CommonCommandNames.SaveItem].Status =
                CommandStatus.Disabled;
            WorkItem.RootWorkItem.Commands[CommonCommandNames.RefreshRefBooks].Status =
                CommandStatus.Disabled;
            WorkItem.RootWorkItem.Commands[CommonCommandNames.ExportToExcel].Status =
                CommandStatus.Disabled;
        }

        /// <summary>
        /// Изменить доступ к глобальным командам для закладки вида списка
        /// </summary>
        public override void ManageCommandsForListTab()
        {
            base.ManageCommandsForListTab();

            WorkItem.RootWorkItem.Commands[CommonCommandNames.ExportToExcel].Status =
                CommandStatus.Disabled;
            WorkItem.RootWorkItem.Commands[CommonCommandNames.DeleteItem].Status =
                CommandStatus.Disabled;
        }
    }
}