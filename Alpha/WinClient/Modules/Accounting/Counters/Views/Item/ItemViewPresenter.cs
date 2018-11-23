using System.Data;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Item.Model;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Item.Queries;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;
using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Item
{
    public class ItemViewPresenter : BaseDomainPresenter<IItemView>
    {
        /// <summary>
        /// Отображает домен на view
        /// </summary>
        public void ShowDomainOnView()
        {
            int _id = int.Parse(WorkItem.State[CommonStateNames.CurrentItemId].ToString());
            DataTable _values;
            CounterInfo _counterInfo;

            using (Entities _db = new Entities())
            {
                _counterInfo = _db.GetCounterInfo(_id);
                _values = _db.GetCounterValues(_id);
            }

            View.CounterNum = _counterInfo.Number;
            View.CounterService = _counterInfo.Service;
            View.CounterModel = _counterInfo.Model;
            View.CustomerData = _counterInfo.CustomerData;
            View.CounterValueTable = _values;
        }
    }
}
