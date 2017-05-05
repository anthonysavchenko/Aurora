using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using System;
using System.Data;
using System.Windows.Forms;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseTabbedView;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;
using Taumis.EnterpriseLibrary.Win.Constants;
using System.Diagnostics;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView
{
    /// <summary>
    /// Базовый класс обработчика списка типа простой справочник.
    /// </summary>
    /// <typeparam name="TView">Вид</typeparam>
    /// <typeparam name="TDomEntity">Домен</typeparam>
    public class BaseSimpleListViewPresenter<TView, TDomEntity> : BaseDomainPresenter<TView>, IBaseSimpleListViewPresenter
        where TView : IBaseSimpleListView
        where TDomEntity : DomainObject, new()
    {
        /// <summary>
        /// На загрузку вью
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();

            // Обновляем справочники
            RefreshRefBooks();

            // Обновляем список
            RefreshList();
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
        /// Создает новый объект домена
        /// </summary>
        /// <returns>Новый объект домена</returns>
        protected virtual TDomEntity CreateNewItem()
        {
            TDomEntity curItem = new TDomEntity
            {
                ID = Guid.NewGuid().ToString()
            };

            return curItem;
        }

        /// <summary>
        /// Собрать данные с вида в домен.
        /// </summary>
        /// <param name="curItem">Домен</param>
        protected virtual void GetItemFromView(TDomEntity curItem)
        {
        }

        /// <summary>
        /// Проверка текущего домена на заполненность обязательных полей.
        /// </summary>
        /// <param name="curItem">Домен</param>
        /// <param name="message">сообщение</param>
        /// <returns>true, false</returns>
        protected virtual bool CheckItem(TDomEntity curItem, out string message)
        {
            message = string.Empty;
            return true;
        }

        /// <summary>
        /// Обновить общий список элементов.
        /// </summary>
        public virtual void RefreshList()
        {
            RefreshRefBooks();
            string curItemId = View.GetCurrentItemId();

            View.ElemList = GetElemList();

            // Устанавливает указатель на строку с ID.
            if ((curItemId != "") && (curItemId != "0"))
            {
                View.LocateToId(curItemId);
            }
        }

        /// <summary>
        /// Обновить справочные данные в комбобоксах таблицы
        /// </summary>
        protected virtual void RefreshRefBooks()
        {
        }

        #region Подписчики
        /// <summary>
        /// Подписчик на событие "Изменён элемент списка".
        /// </summary>
        /// <param name="sender">Не исп.</param>
        /// <param name="eventArgs">Не исп.</param>
        [EventSubscription(CommonEventNames.RefreshItemFired, ThreadOption.UserInterface)]
        public void OnRefreshItemFired(object sender, EventArgs eventArgs)
        {
            // Если текущий юзкейс не активен - 
            // глобальные команды обрабатывать не нужно.
            if (WorkItem.Status == WorkItemStatus.Inactive) return;

            // Обновить список элементов, за исключением случая, когда список находится на невидимой в данный момент вкладке
            if (!CheckIfOwnerTabPresentsAndHidden())
            {
                RefreshList();
            }
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
            if (WorkItem.Status == WorkItemStatus.Inactive || CheckIfOwnerTabPresentsAndHidden()) return;

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
        /// Подписка на глобальную команду "Обновить справочники".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [EventSubscription(CommonEventNames.RefreshRefBooksFired, ThreadOption.UserInterface)]
        public void OnRefreshRefBooksFired(object sender, EventArgs eventArgs)
        {
            // Если текущий юзкейс не активен - 
            // глобальные команды обрабатывать не нужно.
            if (this.WorkItem.Status == WorkItemStatus.Inactive) return;

            // Обновляем списки всех справочников на таблице.
            RefreshRefBooks();
        }

        #endregion

        #region IBaseSimpleListViewPresenter Members

        /// <summary>
        /// Добавление/обновление элемента.
        /// </summary>
        public virtual void AddOrUpdateElem()
        {
            string _checkStr;

            TDomEntity curItem = GetCurrentItem();

            // Если элемента нет - добавляем, иначе - обновляем.
            if (curItem == null)
            {
                curItem = CreateNewItem();
            }

            // Наполнить данными домен.
            GetItemFromView(curItem);

            // Проверяем на полноту и непротиворечивость заполнения.
            if (!CheckItem(curItem, out _checkStr))
            {
                MessageBox.Show(_checkStr);
            }
            else
            {
                try
                {
                    SaveItem(curItem);

                    // Значение невидимого столбца обновляем вручную.
                    View.ID = curItem.ID;
                }
                catch (Exception _ex)
                {
                    MessageBox.Show(
                        String.Format(
                            "Ошибка сохранения.\n{0}",
                            _ex.Message));
                }
            }
        }

        /// <summary>
        /// Возвращает текущий объект
        /// </summary>
        /// <returns></returns>
        protected virtual TDomEntity GetCurrentItem()
        {
            return GetItem<TDomEntity>(View.GetCurrentItemId());
        }

        /// <summary>
        /// Удаление текущего элемента домена из базы данных.
        /// </summary>
        public virtual void DeleteElem()
        {
            DataMapper<TDomEntity>().delete(View.GetCurrentItemId());
        }

        /// <summary>
        /// Производит сохранение элемента в БД
        /// </summary>
        /// <param name="curItem">Объект домена</param>
        /// <returns>Признак успешности изменения</returns>
        protected virtual bool SaveItem(TDomEntity curItem)
        {
            return UpdateItem(curItem);
        }

        /// <summary>
        /// Обработка нажатия на кнопку навигатора "Добавить"
        /// </summary>
        public virtual bool NavigatorBtnAppend()
        {
            return false;
        }

        /// <summary>
        /// Обработка нажатия на кнопку навигатора "Удалить"
        /// </summary>
        public virtual bool NavigatorBtnRemove()
        {
            bool _doDelete =
                MessageBox.Show("Вы уверены, что необходимо удалить выбранный элемент?",
                                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button1) == DialogResult.Yes;
            if (_doDelete)
            {
                DeleteElem();
            }

            // Если удаление выполнено, то необходима встроенная обработка для удаление из грида. 
            // Если же не выполнено удаление, тогда глушим встроенную обработку.
            return !_doDelete;
        }

        /// <summary>
        /// Обработка нажатия на кнопку навигатора "Закончить редактирование"
        /// </summary>
        /// <returns>Возвращает true, если событие обработано</returns>
        public virtual bool NavigatorBtnEndEdit()
        {
            // Получить текущее значение последнего редактируемого поля.
            View.PostEditor();

            // Обновить / добавить элемент в БД.
            AddOrUpdateElem();
            return false;
        }

        #endregion

        #region Private members

        /// <summary>
        /// Проверяет находится ли список на невидимой (невыбранной) вкладке
        /// </summary>
        /// <returns>true, если список находится на невидимой (невыбранной) вкладке; иначе - false</returns>
        private bool CheckIfOwnerTabPresentsAndHidden()
        {
            bool _res = false;

            IBaseTabbedView _iTabbedView = null;

            // Проверка для новых списков, находящихся на "старых" вкладках, которые не являются IBaseTabbedView
            if (WorkItem.SmartParts.Get("TabbedView") is IBaseTabbedView)
            {
                _iTabbedView = (IBaseTabbedView)WorkItem.SmartParts.Get("TabbedView");
            }

            if (_iTabbedView != null)
            {
                string _ownerTabbedViewName = GetOwnerTabbedViewNameIfExists();
                _res = (!String.IsNullOrEmpty(_ownerTabbedViewName) && _iTabbedView.CurrentTab != _ownerTabbedViewName);
            }

            return _res;
        }

        /// <summary>
        /// Возвращает имя вкладки, на которой находится список (если он находится на вкладке)
        /// </summary>
        /// <returns>имя вкладки, на которой находится список (если он находится на вкладке); String.Empty, если список не находится на вкладке</returns>
        private string GetOwnerTabbedViewNameIfExists()
        {
            string _res = String.Empty;

            Control _owner = View.Parent;

            while (_owner != null)
            {
                if (_owner is TabPage)
                {
                    _res = _owner.Name;
                    break;
                }

                _owner = _owner.Parent;
            }

            return _res;
        }

        #endregion
    }
}
