using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using System.Windows.Forms;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseTabbedView;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;
using Taumis.EnterpriseLibrary.Win.Constants;
using System.Diagnostics;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView
{
    /// <summary>
    /// Базовый класс презентера списка
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    /// <typeparam name="TDomEntity"></typeparam>
    public abstract class BaseListViewPresenter<TView, TDomEntity> : BaseDomainPresenter<TView>, IBaseListViewPresenter
        where TDomEntity : DomainObject
        where TView : IBaseListView
    {
        /// <summary>
        /// Конструктор с дефолтными параметрами работы BaseListView
        /// </summary>
        [InjectionConstructor]
        public BaseListViewPresenter()
        {
            Params = BaseListViewConstants.BaseListViewDefaultParams;
        }

        /// <summary>
        /// Конструктор со специфичными параметрами работы BaseListView
        /// </summary>
        /// <param name="_params">Параметры работы BaseListView</param>
        public BaseListViewPresenter(BaseListViewParams _params)
        {
            Params = _params;
        }

        /// <summary>
        /// Параметры работы BaseListView
        /// </summary>
        private readonly BaseListViewParams Params;

        /// <summary>
        /// Выполняет действия при загрузке вида
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();

            View.ClearList();
        }

        #region Подписка на глобальные события

        /// <summary>
        /// Подписка на глобальное событие - Удалить элемент.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [EventSubscription(CommonEventNames.DeleteItemFired, ThreadOption.UserInterface)]
        public virtual void OnDeleteItemFired(object sender, EventArgs eventArgs)
        {
            // Если текущий юзкейс не активен - 
            // глобальные команды обрабатывать не нужно.
            if (WorkItem.Status == WorkItemStatus.Inactive) return;

            if (MessageBox.Show("Вы уверены, что необходимо удалить выбранные элементы ?",
                            "Подтверждение удаления", System.Windows.Forms.MessageBoxButtons.YesNo,
                            System.Windows.Forms.MessageBoxIcon.Question,
                            System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                DoDelete();

                RefreshList();
            }
        }

        /// <summary>
        /// Подписка на событие "Архивировать".
        /// </summary>
        [EventSubscription(CommonEventNames.ArchivateItemFired, ThreadOption.UserInterface)]
        public virtual void OnArchivateItemFired(object sender, EventArgs eventArgs)
        {
            // Если текущий юзкейс не активен - 
            // глобальные команды обрабатывать не нужно.
            if (WorkItem.Status == WorkItemStatus.Inactive) return;

            if (MessageBox.Show("Вы уверены, что необходимо архивировать выбранные элементы ?",
                "Подтверждение архивации элементов", System.Windows.Forms.MessageBoxButtons.YesNo,
                System.Windows.Forms.MessageBoxIcon.Question,
                System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                DoArchivate();

                RefreshList();
            }
        }

        /// <summary>
        /// Подписка на событие "Разархивировать".
        /// </summary>
        [EventSubscription(CommonEventNames.UnArchivateItemFired, ThreadOption.UserInterface)]
        public virtual void OnUnArchivateItemFired(object sender, EventArgs eventArgs)
        {
            // Если текущий юзкейс не активен - 
            // глобальные команды обрабатывать не нужно.
            if (WorkItem.Status == WorkItemStatus.Inactive) return;

            if (MessageBox.Show("Вы уверены, что необходимо разархивировать выбранные элементы ?",
                "Подтверждение разархивации элементов", System.Windows.Forms.MessageBoxButtons.YesNo,
                System.Windows.Forms.MessageBoxIcon.Question,
                System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                DoUnarchivate();

                RefreshList();
            }
        }

        /// <summary>
        /// Утверждение операции.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [EventSubscription(CommonEventNames.PostItemFired, ThreadOption.UserInterface)]
        public virtual void OnPostItemFired(object sender, EventArgs eventArgs)
        {
            // Если текущий юзкейс не активен - 
            // глобальные команды обрабатывать не нужно.
            if (WorkItem.Status == WorkItemStatus.Inactive) return;

            if (System.Windows.Forms.MessageBox.Show("Вы уверены, что необходимо утвердить выбранные элементы?",
                "Утверждение", System.Windows.Forms.MessageBoxButtons.YesNo,
                System.Windows.Forms.MessageBoxIcon.Question,
                System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                DoPost();

                RefreshList();
            }
        }

        /// <summary>
        /// Откат операции.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [EventSubscription(CommonEventNames.UnPostItemFired, ThreadOption.UserInterface)]
        public virtual void OnUnPostItemFired(object sender, EventArgs eventArgs)
        {
            // Если текущий юзкейс не активен - 
            // глобальные команды обрабатывать не нужно.
            if (WorkItem.Status == WorkItemStatus.Inactive) return;

            if (MessageBox.Show("Вы уверены, что необходимо отменить утверждение выбранных элементов ?",
                "Отмена утверждения", System.Windows.Forms.MessageBoxButtons.YesNo,
                System.Windows.Forms.MessageBoxIcon.Question,
                System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                DoUnpost();

                RefreshList();
            }
        }

        /// <summary>
        /// Подписчик на событие "Обновить список".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [EventSubscription(CommonEventNames.RefreshItemFired, ThreadOption.UserInterface)]
        public virtual void OnRefreshItemFired(object sender, EventArgs eventArgs)
        {
            // Если текущий юзкейс не активен - 
            // глобальные команды обрабатывать не нужно.
            if (WorkItem.Status == WorkItemStatus.Inactive) return;

            // Обновить список элементов
            RefreshList();
        }

        /// <summary>
        /// Подписчик на событие "Изменён элемент списка"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [EventSubscription(CommonEventNames.ItemChanged, ThreadOption.UserInterface)]
        public void OnItemChangedFired(object sender, EventArgs eventArgs)
        {
            // Если текущий юзкейс не активен - 
            // глобальные команды обрабатывать не нужно.
            if (WorkItem.Status == WorkItemStatus.Inactive) return;

            OnItemChanged();
        }

        /// <summary>
        /// Подписка на глобальное событие - Выгрузить таблицу в Excel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [EventSubscription(CommonEventNames.ExportToExcelFired, ThreadOption.UserInterface)]
        public void OnExportToExcelFired(object sender, EventArgs eventArgs)
        {
            // Если текущий юзкейс не активен - 
            // глобальные команды обрабатывать не нужно.
            if (WorkItem.Status == WorkItemStatus.Inactive) return;

            SaveFileDialog _saveFileDialog = new SaveFileDialog()
            {
                Title = "Сохранить в файл",
                Filter = "Файл Excel 97-2003 (*.xls)|*.xls",
                FilterIndex = 1,
                RestoreDirectory = true,
                DefaultExt = "xls",
                FileName = "Экспорт",
                AddExtension = true,
            };

            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                View.ExportToExcel(_saveFileDialog.FileName);

                Process process = new Process();
                process.StartInfo.FileName = _saveFileDialog.FileName;
                process.Start();
            }
        }

        /// <summary>
        /// Обработчик события на заполнение списка доменом
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [EventSubscription(CommonEventNames.FILL_LIST_WITH_DOMAIN_FIRED, ThreadOption.UserInterface)]
        public void OnFillListWithDomainFired(object s, EventArgs<TDomEntity> e)
        {
            View.ElemList = GenerateDataTable(e.Data);
            View.LocateToId(e.Data.ID);
        }

        #endregion

        #region IBaseListViewPresenter

        /// <summary>
        /// Выполняет действия при изменении выбранного элемента
        /// </summary>
        /// <param name="_id">Id выбранного элемента списка</param>
        public virtual void OnRowChanged(string _id)
        {
            if (string.IsNullOrEmpty(_id))
                return;

            // Id текущего элемента списка
            WorkItem.State[Params.CurrentItemIdStateName] = _id;

            if (WorkItem.SmartParts.Get<IBaseTabbedView>("TabbedView") != null)
            {
                if (WorkItem.SmartParts.Get<IBaseTabbedView>("TabbedView").CurrentTab == "tabList")
                {
                    UpdateGlobalButtonsForCurrentItem();
                }
            }

            if (Params.UpdateWindowTitleOnRowChanged)
            {
                WorkItem.Controller.MainViewTitle = String.Format(
                    "{0} {1}",
                    WorkItem.Controller.DefaultMainViewTitle,
                    View.GetCurrentItemShortName());
            }
        }

        /// <summary>
        /// Обновить общий список элементов
        /// </summary>
        public virtual void RefreshList()
        {
            RefreshList(true);
        }

        /// <summary>
        /// Обновляет состояние глобальных кнопок, исходя из текущего выбранного элемента
        /// </summary>
        public virtual void UpdateGlobalButtonsForCurrentItem()
        {
            string _curId = (string)WorkItem.State[Params.CurrentItemIdStateName];

            ListItemStatus _isTransactedItemStatus = ListItemStatus.ListItemStatusNone;
            ListItemStatus _isArchivedItemStatus = ListItemStatus.ListItemStatusNone;

            if (!String.IsNullOrEmpty(_curId))
            {
                _isTransactedItemStatus = IsItemTransacted(_curId);
                _isArchivedItemStatus = IsItemArchived(_curId);
            }

            // Активация кнопок проведения на панели
            EnableDisablePostUnpostBtns(_isTransactedItemStatus);

            // Активация кнопок архивации на панели
            EnableDisableArchiveUnArchiveBtns(_isArchivedItemStatus);
        }

        #endregion

        #region Private and protected members

        /// <summary>
        /// Установить доступность команд "Архивировать" и "Разархивировать"
        /// </summary>
        /// <param name="_listItemStatus">Признак архивированности элемента списка</param>
        private void EnableDisableArchiveUnArchiveBtns(ListItemStatus _listItemStatus)
        {
            WorkItem.RootWorkItem.Commands[CommonCommandNames.UnarchiveItem].Status =
                (_listItemStatus == ListItemStatus.ListItemStatusOn) ? CommandStatus.Enabled : CommandStatus.Disabled;

            WorkItem.RootWorkItem.Commands[CommonCommandNames.ArchiveItem].Status =
                (_listItemStatus == ListItemStatus.ListItemStatusOff) ? CommandStatus.Enabled : CommandStatus.Disabled;
        }

        /// <summary>
        /// Установить доступность команд "Провести" и "Отменить проведение"
        /// </summary>
        /// <param name="_listItemStatus">Признак утвержденности элемента списка</param>
        private void EnableDisablePostUnpostBtns(ListItemStatus _listItemStatus)
        {
            WorkItem.RootWorkItem.Commands[CommonCommandNames.UnpostItem].Status =
                (_listItemStatus == ListItemStatus.ListItemStatusOn) ? CommandStatus.Enabled : CommandStatus.Disabled;

            WorkItem.RootWorkItem.Commands[CommonCommandNames.PostItem].Status =
                (_listItemStatus == ListItemStatus.ListItemStatusOff) ? CommandStatus.Enabled : CommandStatus.Disabled;
        }

        /// <summary>
        /// Обновить общий список элементов
        /// </summary>
        /// <param name="_forceRowChanged">Признак необходимости вызвать обработку
        /// изменения текущего элемента списка</param>
        private void RefreshList(bool _forceRowChanged)
        {
            string _curId = (string)WorkItem.State[Params.CurrentItemIdStateName];

            View.ElemList = GetElemList();

            View.LocateToId(_curId);

            if (_forceRowChanged)
            {
                OnRowChanged((string)WorkItem.State[Params.CurrentItemIdStateName]);
            }
        }

        #endregion

        #region Для наследников

        /// <summary>
        /// Обработка события "Изменён элемент списка"
        /// </summary>
        protected virtual void OnItemChanged()
        {
            RefreshList(true);
        }

        /// <summary>
        /// Получить таблицу данных (DataTable) со списком объектов 
        /// для типа домена, указанного в параметре TBusiness класса.
        /// </summary>
        /// <returns>Таблица данных (DataTable)</returns>
        public virtual DataTable GetElemList()
        {
            return GetList<TDomEntity>();
        }

        /// <summary>
        /// Удаляет объект
        /// </summary>
        /// <param name="_item">объект из списка для удаления</param>
        protected virtual void Delete(TDomEntity _item)
        {
            if (!DataMapper<TDomEntity>().delete(_item.ID))
            {
                throw new Exception("Не удалось удалить документ");
            }
        }

        /// <summary>
        /// Проводит операцию
        /// </summary>
        /// <param name="_item"></param>
        protected virtual bool Post(TDomEntity _item)
        {
            return true;
        }

        /// <summary>
        /// Откатывает операцию (должен быть переопределен)
        /// </summary>
        /// <param name="_item"></param>
        protected virtual bool UnPost(TDomEntity _item)
        {
            return true;
        }

        /// <summary>
        /// Возвращает статус утвержденности элемента по его id
        /// </summary>
        /// <param name="_id">Id элемента списка</param>
        /// <returns>Статус утвержденности элемента</returns>
        protected virtual ListItemStatus IsItemTransacted(string _id)
        {
            ListItemStatus _res = ListItemStatus.ListItemStatusNone;

            DataTable _elemList = View.ElemList;

            if (_elemList != null && _elemList.Columns.Contains("IsTransacted"))
            {
                _res = (_elemList.Rows.Find(_id)["IsTransacted"].ToString() == "1") ?
                    ListItemStatus.ListItemStatusOn : ListItemStatus.ListItemStatusOff;
            }

            return _res;
        }

        /// <summary>
        /// Возвращает статус архивированности элемента по его id
        /// </summary>
        /// <param name="_id">Id элемента списка</param>
        /// <returns>Статус архивированности элемента</returns>
        protected virtual ListItemStatus IsItemArchived(string _id)
        {
            ListItemStatus _res = ListItemStatus.ListItemStatusNone;

            DataTable _elemList = View.ElemList;

            if (_elemList != null && _elemList.Columns.Contains("IsArchived"))
            {
                _res = (_elemList.Rows.Find(_id)["IsArchived"].ToString() == "1") ?
                    ListItemStatus.ListItemStatusOn : ListItemStatus.ListItemStatusOff;
            }

            return _res;
        }

        /// <summary>
        /// Выполняет действия для удаления элемента
        /// </summary>
        protected virtual void DoDelete()
        {
            try
            {
                Delete(GetItem<TDomEntity>((string)WorkItem.State[Params.CurrentItemIdStateName]));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка удаления");
            }
        }

        /// <summary>
        /// Выполняет действия для архивации элемента
        /// </summary>
        protected virtual void DoArchivate()
        {
            if (typeof(IArchival).IsAssignableFrom(typeof(TDomEntity)))
            {
                TDomEntity _item = GetItem<TDomEntity>((string)WorkItem.State[Params.CurrentItemIdStateName]);

                ((IArchival)_item).IsArchived = true;

                UpdateItem(_item);
            }
        }

        /// <summary>
        /// Выполняет действия для разархивации элемента
        /// </summary>
        protected virtual void DoUnarchivate()
        {
            if (typeof(IArchival).IsAssignableFrom(typeof(TDomEntity)))
            {
                TDomEntity _item = GetItem<TDomEntity>((string)WorkItem.State[Params.CurrentItemIdStateName]);

                ((IArchival)_item).IsArchived = false;

                UpdateItem(_item);
            }
        }

        /// <summary>
        /// Выполняет действия для утверждения элемента
        /// </summary>
        protected virtual void DoPost()
        {
            try
            {
                Post(GetItem<TDomEntity>((string)WorkItem.State[Params.CurrentItemIdStateName]));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка утверждения");
            }

        }

        /// <summary>
        /// Выполняет действия для отмены утверждения элемента
        /// </summary>
        protected virtual void DoUnpost()
        {
            try
            {
                UnPost(GetItem<TDomEntity>((string)WorkItem.State[Params.CurrentItemIdStateName]));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка отмены утверждения");
            }
        }

        /// <summary>
        /// Генерирует таблицу из домена
        /// </summary>
        /// <param name="domain">Домен</param>
        protected virtual DataTable GenerateDataTable(TDomEntity domain)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}