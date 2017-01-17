using System.Data;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.User;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Users.Views.List
{
    public class ListViewPresenter : BaseListViewPresenter<IBaseListView, DomItem>
    {
        #region Overrides of BaseListViewPresenter<IBaseListView,User>

        /// <summary>
        /// Получить таблицу данных (DataTable) со списком объектов 
        /// для типа домена, указанного в параметре TBusiness класса.
        /// </summary>
        /// <returns>Таблица данных (DataTable)</returns>
        public override DataTable GetElemList()
        {
            return DataMapper<DomItem, IUserDataMapper>().GetList();
        }

        #endregion
    }
}