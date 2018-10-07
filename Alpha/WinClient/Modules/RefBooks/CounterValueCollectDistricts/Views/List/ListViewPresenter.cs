using System.Data;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBooks.CounterValueCollectDistrict;
using DBItem = Taumis.Alpha.DataBase.CounterValueCollectDistricts;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.CounterValueCollectDistricts.Views.List
{
    public class ListViewPresenter : BaseSimpleListViewPresenter<ListView, DomItem>
    {
        public override DataTable GetElemList()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID", typeof(int));
            _table.Columns.Add("Name", typeof(string));

            using (var _db = new Entities())
            {
                foreach (DBItem _detail in _db.CounterValueCollectDistricts)
                {
                    _table.Rows.Add(
                        _detail.ID,
                        _detail.Name);
                }
            }

            return _table;
        }
        /// <summary>
        /// Проверяет текущий домена на заполненность обязательных полей.
        /// </summary>
        /// <param name="_dom">Домен</param>
        /// <param name="_message">Сообщение об ошибке</param>
        /// <returns>true, если провверка прошла успешно, иначе - false</returns>
        protected override bool CheckItem(DomItem _dom, out string _message)
        {
            bool _res = !string.IsNullOrEmpty(_dom.Name);
            _message = _res ? string.Empty : " - Не указано наименование банка";

            return _res;
        }

        /// <summary>
        /// Собирает данные вида в домен
        /// </summary>
        /// <param name="_curItem">Домен</param>
        protected override void GetItemFromView(DomItem _curItem)
        {
            _curItem.Name = View.DistrictName.Trim();
        }
    }
}