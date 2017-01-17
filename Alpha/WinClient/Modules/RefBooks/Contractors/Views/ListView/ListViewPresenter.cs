using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Contractor;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Contractors
{
    public class ListViewPresenter : BaseSimpleListViewPresenter<ListView, DomItem>
    {
        public override System.Data.DataTable GetElemList()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Code", typeof(string));
            table.Columns.Add("ContactInfo", typeof(string));

            using (Entities entities = new Entities())
            {
                IQueryable<Taumis.Alpha.DataBase.Contractors> _query;

                _query = from contractor in entities.Contractors
                         select contractor;

                foreach (Taumis.Alpha.DataBase.Contractors contractor in _query)
                {
                    table.Rows.Add(
                        contractor.ID,
                        contractor.Name,
                        contractor.Code,
                        contractor.ContactInfo);
                }
            }

            return table;
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
            else if (string.IsNullOrEmpty(_dom.Code.Trim()))
            {
                _message = "Не указан шифр";
                _res = false;
            }

            return _res;
        }

        /// <summary>
        /// Собирает данные вида в домен.
        /// </summary>
        /// <param name="_curItem">Домен</param>
        protected override void GetItemFromView(DomItem _curItem)
        {
            _curItem.Name = View.ContractorName;
            _curItem.Code = View.Code;
            _curItem.ContactInfo = View.ContactInfo;
        }
    }
}