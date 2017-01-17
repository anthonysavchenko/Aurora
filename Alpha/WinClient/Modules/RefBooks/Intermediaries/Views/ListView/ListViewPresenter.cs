using System.Data;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Intermediary;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Intermediaries
{
    public class ListViewPresenter : BaseSimpleListViewPresenter<ListView, DomItem>
    {
        public override DataTable GetElemList()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Code", typeof(string));
            table.Columns.Add("Rate", typeof(decimal));

            using (Entities entities = new Entities())
            {
                foreach (Taumis.Alpha.DataBase.Intermediaries intermediary in entities.Intermediaries)
                {
                    table.Rows.Add(
                        intermediary.ID,
                        intermediary.Name,
                        intermediary.Code,
                        intermediary.Rate);
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
            else if (_dom.Rate <= 0)
            {
                _message = "Указан неверный взимаемый процент";
            }

            return _res;
        }

        /// <summary>
        /// Собирает данные вида в домен.
        /// </summary>
        /// <param name="_curItem">Домен</param>
        protected override void GetItemFromView(DomItem _curItem)
        {
            _curItem.Name = View.IntermediaryName;
            _curItem.Code = View.Code;
            _curItem.Rate = View.Rate;
        }
    }
}