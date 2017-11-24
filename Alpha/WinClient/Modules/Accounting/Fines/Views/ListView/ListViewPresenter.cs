using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.FineDoc;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Fines.View.List
{
    public class ListViewPresenter : BaseListViewPresenter<IBaseListView, DomItem>
    {
        public override void OnViewReady()
        {
            base.OnViewReady();
            RefreshList();
        }

        /// <summary>
        /// Получить таблицу данных (DataTable) со списком объектов 
        /// для типа домена, указанного в параметре TBusiness класса.
        /// </summary>
        /// <returns>Таблица данных (DataTable)</returns>
        public override DataTable GetElemList()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Period", typeof(DateTime));
            table.Columns.Add("Value", typeof(decimal));

            using (Entities _db = new Entities())
            {
                var _fineDocs = _db.FineDocs
                    .Select(f =>
                        new
                        {
                            f.ID,
                            f.Period,
                            Sum = f.FinePoses.Sum(p => p.Value)
                        })
                    .OrderByDescending(f => f.Period)
                    .ToList();

                foreach (var _fine in _fineDocs)
                {
                    table.Rows.Add(_fine.ID, _fine.Period, _fine.Sum);
                }
            }

            return table;
        }
    }
}