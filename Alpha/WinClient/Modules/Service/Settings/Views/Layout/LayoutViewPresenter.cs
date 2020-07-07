using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Settings.Views.Layout;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.Alpha.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Settings.Layout
{
    public class LayoutViewPresenter : BaseLayoutViewPresenter<ILayoutView>
    {
        /// <summary>
        /// Выполняет действия при загрузке вида
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();

            RefreshView();
        }

        /// <summary>
        /// Выполняет действия при активации юз-кейса
        /// </summary>
        public override void ActivateUseCase()
        {
            WorkItem.RootWorkItem.Commands[CommonCommandNames.SaveItem].Status =
                CommandStatus.Enabled;

            WorkItem.RootWorkItem.Commands[CommonCommandNames.RefreshItem].Status =
                CommandStatus.Enabled;
        }

        /// <summary>
        /// Выполняет действия при глобальной команде "Обновить"
        /// </summary>
        [EventSubscription(CommonEventNames.RefreshItemFired, ThreadOption.UserInterface)]
        public void OnRefreshItemFired(object sender, EventArgs eventArgs)
        {
            // Если текущий юзкейс не активен - 
            // глобальные команды обрабатывать не нужно.
            if (WorkItem.Status == WorkItemStatus.Inactive) return;

            RefreshView();
        }

        /// <summary>
        /// Выполняет действия при глобальной команде "Сохранить"
        /// </summary>
        [EventSubscription(CommonEventNames.SaveItemFired, ThreadOption.UserInterface)]
        public void OnSaveItemFired(object sender, EventArgs eventArgs)
        {
            // Если текущий юзкейс не активен - 
            // глобальные команды обрабатывать не нужно.
            if (this.WorkItem.Status == WorkItemStatus.Inactive) return;

            using (Entities _entities = new Entities())
            {
                var _settings = _entities.Settings.ToList();

                foreach (DataBase.Settings _setting in _settings)
                {
                    switch (_setting.Name)
                    {
                        case SettingNames.SMTP_SERVER:
                            _setting.Value = View.Server;
                            break;

                        case SettingNames.SMTP_PORT:
                            _setting.Value = View.ServerPort.ToString();
                            break;

                        case SettingNames.SMTP_LOGIN:
                            _setting.Value = View.ServerLogin;
                            break;

                        case SettingNames.SMTP_PASSWORD:
                            _setting.Value = View.ServerPassword;
                            break;

                        case SettingNames.BACKUP_PATH:
                            _setting.Value = View.BackupPath;
                            break;
                    }
                }

                _entities.SaveChanges();
            }
        }

        /// <summary>
        /// Обновляет вид
        /// </summary>
        private void RefreshView()
        {
            List<DataBase.Settings> _settings;

            using (Entities _entities = new Entities())
            {
                _settings = _entities.Settings.ToList();
            }

            foreach (DataBase.Settings _setting in _settings)
            {
                switch (_setting.Name)
                {
                    case SettingNames.SMTP_SERVER:
                        View.Server = _setting.Value;
                        break;

                    case SettingNames.SMTP_PORT:
                        View.ServerPort = int.Parse(_setting.Value);
                        break;

                    case SettingNames.SMTP_LOGIN:
                        View.ServerLogin = _setting.Value;
                        break;

                    case SettingNames.SMTP_PASSWORD:
                        View.ServerPassword = _setting.Value;
                        break;

                    case SettingNames.BACKUP_PATH:
                        View.BackupPath = _setting.Value;
                        break;
                }
            }
        }
    }
}
