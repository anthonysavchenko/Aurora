using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Top
{
    public class TopViewPresenter : BasePresenter<ITopView>
    {
        public override void OnViewReady()
        {
            base.OnViewReady();

            View.Districts = GetDistricts();
        }

        private DataTable GetDistricts()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID");
            _table.Columns.Add("Name");

            DataSet _ds =
                new DataSet
                {
                    EnforceConstraints = false
                };
            _ds.Tables.Add(_table);

            using (var _db = new Entities())
            {
                _db.CounterValueCollectDistricts
                    .ToList()
                    .ForEach(x => _table.Rows.Add(x.ID, x.Name));
            }

            return _table;
        }

        public void PrintCollectForm(int districtId)
        {

        }
    }
}
