using System;
using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
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
            DataTable _table;

            try
            {
                _table = GetList<Building>();
            }
            catch (Exception)
            {
                //TODO: Log
                View.ShowMessage("Не удалось загрузить данные", "Ошибка");
                _table = new DataTable();
            }

            return _table;
        }
    }
}