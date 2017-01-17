using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Street;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Streets.Views.List
{
    public class ListViewPresenter : BaseSimpleListViewPresenter<ListView, DomItem>
    {
        public override System.Data.DataTable GetElemList()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID", typeof(int));
            _table.Columns.Add("Name", typeof(string));

            using (Entities _entities = new Entities())
            {
                foreach (Taumis.Alpha.DataBase.Streets _street in _entities.Streets)
                {
                    _table.Rows.Add(
                        _street.ID,
                        _street.Name);
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
            _message = string.Empty;
            bool _res = true;

            if (string.IsNullOrEmpty(_dom.Name.Trim()))
            {
                _message = "Не указано полное название";
                _res = false;
            }
            else
            {
                using (Entities _entities = new Entities())
                {
                    if (_entities.Streets.Where(street => street.Name == _dom.Name).FirstOrDefault() != null)
                    {
                        _message = "Улица с указанным наименованием уже существует";
                        _res = false;
                    }
                }
            }

            return _res;
        }

        /// <summary>
        /// Собирает данные вида в домен.
        /// </summary>
        /// <param name="_curItem">Домен</param>
        protected override void GetItemFromView(DomItem _curItem)
        {
            _curItem.Name = View.StreetName;
        }
    }
}