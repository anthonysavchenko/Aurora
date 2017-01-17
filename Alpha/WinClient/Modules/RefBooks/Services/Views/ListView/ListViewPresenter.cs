using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Service;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Services.View.List
{
    public class ListViewPresenter : BaseListViewPresenter<IBaseListView, DomItem>
    {
        /// <summary>
        /// Получить таблицу данных (DataTable) со списком объектов 
        /// для типа домена, указанного в параметре TBusiness класса.
        /// </summary>
        /// <returns>Таблица данных (DataTable)</returns>
        public override DataTable GetElemList()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Code", typeof(string));
            table.Columns.Add("ServiceTypeId", typeof(string));

            using (Entities entities = new Entities())
            {
                var _query = from services in entities.Services.Include("ServiceTypes")
                         select services;

                foreach (Taumis.Alpha.DataBase.Services service in _query)
                {
                    table.Rows.Add(
                        service.ID,
                        service.Name,
                        service.Code,
                        service.ServiceTypes.Name);
                }
            }

            return table;
        }
    }
}