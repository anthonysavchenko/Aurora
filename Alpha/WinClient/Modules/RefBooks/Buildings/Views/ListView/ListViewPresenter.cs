using System.Data;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Queries;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.List
{
    public class ListViewPresenter : BaseListViewPresenter<IBaseListView, Building>
    {
        /// <summary>
        /// Получить таблицу данных (DataTable) со списком объектов 
        /// для типа домена, указанного в параметре TBusiness класса.
        /// </summary>
        /// <returns>Таблица данных (DataTable)</returns>
        public override DataTable GetElemList()
        {
            DataTable table;

            using (var _db = new Entities())
            {
                table = _db.GetList();
            }

            return table;
        }
    }
}
