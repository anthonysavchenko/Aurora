using System;
using System.Data;
using System.Linq;
using System.Text;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.BankDetails.Constants;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.BankDetail;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.BankDetails.Views.List
{
    public class ListViewPresenter : BaseSimpleListViewPresenter<ListView, DomItem>
    {
        public override System.Data.DataTable GetElemList()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add(ColumnNames.ID, typeof(int));
            _table.Columns.Add(ColumnNames.ACCOUNT, typeof(string));
            _table.Columns.Add(ColumnNames.NAME, typeof(string));
            _table.Columns.Add(ColumnNames.BIK, typeof(string));
            _table.Columns.Add(ColumnNames.CORR_ACCOUNT, typeof(string));
            _table.Columns.Add(ColumnNames.KPP, typeof(string));
            _table.Columns.Add(ColumnNames.INN, typeof(string));

            using (Entities _entities = new Entities())
            {
                foreach (Taumis.Alpha.DataBase.BankDetails _detail in _entities.BankDetails)
                {
                    _table.Rows.Add(
                        _detail.ID,
                        _detail.Account,
                        _detail.Name,
                        _detail.BIK,
                        _detail.CorrAccount,
                        _detail.KPP,
                        _detail.INN);
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

            StringBuilder _errors = new StringBuilder();

            if (string.IsNullOrEmpty(_dom.Name))
            {
                _errors.AppendLine(" - Не указано наименование банка");
            }

            if (string.IsNullOrEmpty(_dom.Account))
            {
                _errors.AppendLine(" - Не указан расчетный счет");
            }

            if (string.IsNullOrEmpty(_dom.BIK))
            {
                _errors.AppendLine(" - Не указан БИК");
            }

            if (_errors.Length > 0)
            {
                _message = _errors.ToString();
                _res = false;
            }
            else
            {
                int _id = _dom.IsNew ? 0 : Convert.ToInt32(_dom.ID);

                using (Entities _entities = new Entities())
                {
                    if (_entities.BankDetails.FirstOrDefault(b => (_id == 0 || b.ID != _id) && b.BIK == _dom.BIK && b.Account == _dom.Account) != null)
                    {
                        _message = "Реквизиты с указанным расчетным счетом и банком уже существуют";
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
            _curItem.Name = View.BankName.Trim();
            _curItem.BIK = View.BIK.Trim();
            _curItem.Account = View.Account.Trim();
            _curItem.CorrAccount = View.CorrAccount.Trim();
            _curItem.KPP = View.KPP.Trim();
            _curItem.INN = View.INN.Trim();
        }
    }
}