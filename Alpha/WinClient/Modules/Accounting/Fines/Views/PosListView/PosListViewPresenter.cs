using Microsoft.Practices.CompositeUI;
using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;
using Taumis.EnterpriseLibrary.Win.Constants;
using DomItemPos = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.FinePos;
using DomItemDoc = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.FineDoc;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Fines.Views.PosListView
{
    public class PosListViewPresenter : BaseSimpleListViewPresenter<IPosListView, DomItemPos>
    {
        private EventHandler _onAnyAttributeChangedEventHandler;

        /// <summary>
        /// Единица работы
        /// </summary>
        [ServiceDependency]
        public IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        /// Возвращает список элементов
        /// </summary>
        /// <returns>Список элементов</returns>
        public override DataTable GetElemList()
        {
            DataTable _table = new DataTable();

            _table.Columns.Add("ID", typeof(string));
            _table.Columns.Add("Customer", typeof(int));
            _table.Columns.Add("Value", typeof(decimal));

            if (WorkItem.State[CommonStateNames.EditItemState].ToString() == CommonEditItemStates.Edit)
            {
                int _fineDocID = int.Parse((string)WorkItem.State[CommonStateNames.CurrentItemId]);

                using (Entities _db = new Entities())
                {
                    var _poses = _db.FinePoses.Where(p => p.DocID == _fineDocID).OrderBy(p => p.Customers.Account).ToList();
                    foreach (var _pos in _poses)
                    {
                        _table.Rows.Add(
                            _pos.ID.ToString(),
                            _pos.CustomerID,
                            _pos.Value);
                    }
                }
            }

            RefreshRefBooks();

            return _table;
        }

        /// <summary>
        /// Проверяет корректность введенных данных
        /// </summary>
        /// <param name="curItem">Объект домена для проверки</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <returns>Признак успешности проверки</returns>
        protected override bool CheckItem(DomItemPos curItem, out string message)
        {
            message = string.Empty;

            if (curItem.Customer == null)
            {
                message += "- Абонент\r\n";
            }
            else if (curItem.Value <= 0)
            {
                message += "- Пеня\r\n";
            }

            return string.IsNullOrEmpty(message);
        }

        protected override void RefreshRefBooks()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID", typeof(int));
            _table.Columns.Add("Account", typeof(string));

            using (Entities _db = new Entities())
            {
                var _customers = _db.Customers
                    .Select(c =>
                        new
                        {
                            c.ID,
                            c.Account
                        })
                    .ToList();

                foreach (var _c in _customers)
                {
                    _table.Rows.Add(_c.ID, _c.Account);
                }
            }

            View.Customers = _table;
        }

        protected override bool SaveItem(DomItemPos curItem)
        {
            if(curItem.IsNew)
            {
                UnitOfWork.registerNew(curItem);
            }
            else
            {
                UnitOfWork.registerDirty(curItem);
            }

            _onAnyAttributeChangedEventHandler.Invoke(this, EventArgs.Empty);

            return true;
        }

        public override void DeleteElem()
        {
            UnitOfWork.registerRemoved(GetCurrentItem());
            _onAnyAttributeChangedEventHandler.Invoke(this, EventArgs.Empty);
        }

        protected override DomItemPos CreateNewItem()
        {
            return new DomItemPos
            {
                ID = Guid.NewGuid().ToString(),
                Doc = (DomItemDoc)WorkItem.State[CommonStateNames.CurrentItem]
            };
        }

        protected override void GetItemFromView(DomItemPos curItem)
        {
            curItem.Value = View.Value;
            curItem.Customer = View.Customer;
        }

        /// <summary>
        /// Подключить общий обработчик изменений
        /// </summary>
        public void BindChangeHandlers(EventHandler handler)
        {
            _onAnyAttributeChangedEventHandler = handler;
        }
    }
}