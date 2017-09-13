using Microsoft.Practices.CompositeUI;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.EnterpriseLibrary.Win.Services;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.RechargePercentCorrection;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers
{
    public class RechargePercentCorrectionListViewPresenter : BaseSimpleListViewPresenter<IRechargePercentCorrectionListView, DomItem>
    {
        /// <summary>
        /// Единица работы
        /// </summary>
        [ServiceDependency]
        public IUnitOfWork UOW { protected get; set; }

        public override void OnViewReady()
        {
        }

        /// <summary>
        /// Возвращает список элементов
        /// </summary>
        /// <returns>Список элементов</returns>
        public override DataTable GetElemList()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID");
            _table.Columns.Add("ServiceName");
            _table.Columns.Add("Period", typeof(DateTime));
            _table.Columns.Add("Days", typeof(int));
            _table.Columns.Add("Percent", typeof(int));

            string _idStr = (string)WorkItem.State[CommonStateNames.CurrentItemId];
            if(!string.IsNullOrEmpty(_idStr))
            {
                int _customerID = int.Parse(_idStr);
                using (Entities _db = new Entities())
                {
                    var _list = _db.RechargePercentCorrections
                        .Where(rpc => rpc.CustomerPoses.Customers.ID == _customerID)
                        .Select(rpc =>
                            new
                            {
                                rpc.ID,
                                ServiceName = rpc.CustomerPoses.Services.Name,
                                rpc.Period,
                                rpc.Days,
                                rpc.Percent
                            })
                        .OrderBy(rpc => rpc.ServiceName)
                        .ToList();

                    foreach (var _rpc in _list)
                    {
                        _table.Rows.Add(
                            _rpc.ID,
                            _rpc.ServiceName,
                            _rpc.Period,
                            _rpc.Days,
                            _rpc.Percent);
                    }
                }
            }
            return _table;
        }

        public override void DeleteElem()
        {
            UOW.registerRemoved(GetCurrentItem());
            WorkItem.State[CommonStateNames.ItemState] = CommonItemStates.Modified;
        }

        /// <summary>
        /// Отключить общий обработчик изменений
        /// </summary>
        public void UnBindChangeHandlers(Control.ControlCollection _coll, EventHandler handler)
        {
            WorkItem.RootWorkItem.Services.Get<IChangeEventHandlerService>().UnBind(_coll, handler);
        }

        /// <summary>
        /// Подключить общий обработчик изменений
        /// </summary>
        public void BindChangeHandlers(Control.ControlCollection _coll, EventHandler handler)
        {
            WorkItem.RootWorkItem.Services.Get<IChangeEventHandlerService>().Bind(_coll, handler);
        }
    }
}