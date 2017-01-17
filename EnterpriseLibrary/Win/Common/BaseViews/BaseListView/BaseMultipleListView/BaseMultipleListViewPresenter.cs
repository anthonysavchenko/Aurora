using Microsoft.Practices.CompositeUI;
using System;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.ObjectBuilder;


namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView.BaseMultipleListView
{
    /// <summary>
    /// Базовый класс обработчика списка со множественным выбором
    /// </summary>
    /// <typeparam name="TView">Тип вью</typeparam>
    /// <typeparam name="TDomEntity">Тип домена</typeparam>
    public abstract class BaseMultipleListViewPresenter<TView, TDomEntity> : BaseListViewPresenter<TView, TDomEntity>
        where TDomEntity : DomainObject
        where TView : IBaseMultipleListView
    {
        /// <summary>
        /// Конструктор с дефолтными параметрами работы BaseListView
        /// </summary>
        [InjectionConstructor]
        public BaseMultipleListViewPresenter()
        {
        }

        /// <summary>
        /// Конструктор со специфичными параметрами работы BaseListView
        /// </summary>
        /// <param name="parameter">Параметры работы BaseListView</param>
        public BaseMultipleListViewPresenter(BaseListViewParams parameter)
            : base(parameter)
        {
        }

        /// <summary>
        /// Удалить элемент
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public override void OnDeleteItemFired(object sender, EventArgs eventArgs)
        {
            // Если текущий юзкейс не активен - 
            // глобальные команды обрабатывать не нужно
            if (WorkItem.Status == WorkItemStatus.Inactive) return;

            if (MessageBox.Show("Вы уверены, что необходимо удалить выбранные элементы ?",
                            "Подтверждение удаления", System.Windows.Forms.MessageBoxButtons.YesNo,
                            System.Windows.Forms.MessageBoxIcon.Question,
                            System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                DoMultipleDelete();

                RefreshList();
            }
        }

        /// <summary>
        /// Подписка на событие "Архивировать"
        /// </summary>
        public override void OnArchivateItemFired(object sender, EventArgs eventArgs)
        {
            // Если текущий юзкейс не активен - 
            // глобальные команды обрабатывать не нужно
            if (WorkItem.Status == WorkItemStatus.Inactive) return;

            if (MessageBox.Show("Вы уверены, что необходимо архивировать выбранные элементы ?",
                "Подтверждение архивации элементов", System.Windows.Forms.MessageBoxButtons.YesNo,
                System.Windows.Forms.MessageBoxIcon.Question,
                System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                DoMultipleArchivate();

                RefreshList();
            }
        }

        /// <summary>
        /// Подписка на событие "Разархивировать"
        /// </summary>
        public override void OnUnArchivateItemFired(object sender, EventArgs eventArgs)
        {
            // Если текущий юзкейс не активен - 
            // глобальные команды обрабатывать не нужно
            if (WorkItem.Status == WorkItemStatus.Inactive) return;

            if (MessageBox.Show("Вы уверены, что необходимо разархивировать выбранные элементы ?",
                "Подтверждение разархивации элементов", System.Windows.Forms.MessageBoxButtons.YesNo,
                System.Windows.Forms.MessageBoxIcon.Question,
                System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                DoMultipleUnarchivate();

                RefreshList();
            }
        }

        /// <summary>
        /// Утверждение операции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public override void OnPostItemFired(object sender, EventArgs eventArgs)
        {
            // Если текущий юзкейс не активен - 
            // глобальные команды обрабатывать не нужно
            if (WorkItem.Status == WorkItemStatus.Inactive) return;

            if (System.Windows.Forms.MessageBox.Show("Вы уверены, что необходимо утвердить выбранные элементы?",
                "Утверждение", System.Windows.Forms.MessageBoxButtons.YesNo,
                System.Windows.Forms.MessageBoxIcon.Question,
                System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                DoMultiplePost();

                RefreshList();
            }
        }

        /// <summary>
        /// Откат операции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public override void OnUnPostItemFired(object sender, EventArgs eventArgs)
        {
            // Если текущий юзкейс не активен - 
            // глобальные команды обрабатывать не нужно
            if (WorkItem.Status == WorkItemStatus.Inactive) return;

            if (MessageBox.Show("Вы уверены, что необходимо отменить утверждение выбранных элементов ?",
                "Отмена утверждения", System.Windows.Forms.MessageBoxButtons.YesNo,
                System.Windows.Forms.MessageBoxIcon.Question,
                System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                DoMultipleUnpost();

                RefreshList();
            }
        }

        #region Для наследников

        /// <summary>
        /// Выполняет действия для множественного утверждения
        /// </summary>
        protected virtual void DoMultiplePost()
        {
            string[] _selRowsIds = View.SelectedIds;

            StringBuilder _errMsg = new StringBuilder();
            int _unCount = 0;

            foreach (string _ind in _selRowsIds)
            {
                try
                {
                    Post(GetItem<TDomEntity>(_ind));
                }
                catch (Exception ex)
                {
                    _errMsg.AppendLine(ex.Message);
                    _unCount++;
                }
            }

            if (_unCount > 0)
            {
                MessageBox.Show(
                    string.Format("Утверждено: {0}\nНе удалось утвердить: {1}\n\nПодробности:\n{2}",
                        _selRowsIds.Length - _unCount,
                        _unCount,
                        _errMsg),
                    "Ошибка утверждения"
                );
            }
        }

        /// <summary>
        /// Выполняет действия для множественной отмены утверждения
        /// </summary>
        protected virtual void DoMultipleUnpost()
        {
            string[] _selRowsIds = View.SelectedIds;

            StringBuilder _errMsg = new StringBuilder();
            int _unCount = 0;

            foreach (string _ind in _selRowsIds)
            {
                try
                {
                    UnPost(GetItem<TDomEntity>(_ind));
                }
                catch (Exception ex)
                {
                    _errMsg.AppendLine(ex.Message);
                    _unCount++;
                }
            }

            if (_unCount > 0)
            {
                MessageBox.Show(
                    string.Format("Отменено: {0}\nНе удалось отменить: {1}\n\nПодробности:\n{2}",
                        _selRowsIds.Length - _unCount,
                        _unCount,
                        _errMsg),
                    "Ошибка отмены утверждения"
                );
            }
        }

        /// <summary>
        /// Выполняет действия для множественного архивирования
        /// </summary>
        protected virtual void DoMultipleArchivate()
        {
            if (typeof(IArchival).IsAssignableFrom(typeof(TDomEntity)))
            {
                string[] _selRowsIds = View.SelectedIds;

                foreach (string _ind in _selRowsIds)
                {
                    TDomEntity _item = GetItem<TDomEntity>(_ind);
                    
                    ((IArchival)_item).IsArchived = true;
                    
                    UpdateItem(_item);
                }
            }
        }

        /// <summary>
        /// Выполняет действия для множественной отмены архивирования
        /// </summary>
        protected virtual void DoMultipleUnarchivate()
        {
            if (typeof(IArchival).IsAssignableFrom(typeof(TDomEntity)))
            {
                string[] _selRowsIds = View.SelectedIds;

                foreach (string _ind in _selRowsIds)
                {
                    TDomEntity _item = GetItem<TDomEntity>(_ind);

                    ((IArchival)_item).IsArchived = false;

                    UpdateItem(_item);
                }
            }
        }

        /// <summary>
        /// Выполняет действия для множественного удаления
        /// </summary>
        protected virtual void DoMultipleDelete()
        {
            string[] _selRowsIds = View.SelectedIds;

            StringBuilder _errMsg = new StringBuilder();
            int _unCount = 0;

            foreach (string _ind in _selRowsIds)
            {
                try
                {
                    Delete(GetItem<TDomEntity>(_ind));
                }
                catch (Exception ex)
                {
                    _errMsg.AppendLine(ex.Message);
                    _unCount++;
                }
            }

            if (_unCount > 0)
            {
                MessageBox.Show(
                    string.Format("Удалено: {0}\nНе удалось удалить: {1}\n\nПодробности:\n{2}",
                        _selRowsIds.Length - _unCount,
                        _unCount,
                        _errMsg),
                    "Ошибка удаления"
                );
            }
        }

        #endregion
    }
}