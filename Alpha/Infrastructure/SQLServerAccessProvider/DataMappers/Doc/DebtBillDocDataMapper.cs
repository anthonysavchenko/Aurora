using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Doc;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.DebtBillDocs;
using DomBillSet = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.BillSet;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.DebtBillDoc;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc
{
    public class DebtBillDocDataMapper : BaseDataMapper<DomItem, DBItem>, IBaseBillDocDataMapper
    {
        #region Overrides of BaseDataMapper<PaymentOper,PaymentOpers>

        /// <summary>
        /// Преобразовавает объект домена в объект прокси БД
        /// </summary>
        /// <param name="domObj">Объект домена</param>
        /// <returns>Объект БД</returns>
        protected override DBItem BusinessToService(DomItem domObj)
        {
            DBItem _dbItem;

            using (Entities _entities = new Entities())
            {
                if (domObj.IsNew)
                {
                    _dbItem = new DBItem();
                    _entities.AddToDebtBillDocs(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.DebtBillDocs.First(x => x.ID == _id);
                }

                _dbItem.CreationDateTime = domObj.CreationDateTime;
                _dbItem.Period = domObj.Period;
                _dbItem.Value = domObj.Value;
                _dbItem.Account = domObj.Account;
                _dbItem.Address = domObj.Address;
                _dbItem.Owner = domObj.Owner;

                int _propId = Int32.Parse(domObj.BillSet.ID);
                _dbItem.BillSets = _entities.BillSets.First(p => p.ID == _propId);

                _entities.SaveChanges();
                domObj.ID = _dbItem.ID.ToString();
            }

            return _dbItem;
        }

        /// <summary>
        /// Преобразовывает объект прокси БД в объект домена
        /// </summary>
        /// <param name="obj">Объект домена</param>
        /// <returns>Объект домена</returns>
        protected override DomItem ServiceToBusiness(IDomainObject _obj)
        {
            DomItem _domItem = (DomItem)_obj;
            int _id = int.Parse(_domItem.ID);
            using (Entities _entities = new Entities())
            {
                DBItem _dbItem =
                    _entities.DebtBillDocs.Include("BillSets")
                        .First(x => x.ID == _id);

                _domItem.CreationDateTime = _dbItem.CreationDateTime;
                _domItem.Period = _dbItem.Period;
                _domItem.Value = _dbItem.Value;
                _domItem.Account = _dbItem.Account;
                _domItem.Address = _dbItem.Address;
                _domItem.Owner = _dbItem.Owner;
                _domItem.BillSet = (DomBillSet)DataMapperService.get(typeof(DomBillSet)).find(_dbItem.BillSets.ID.ToString());
            }

            return _domItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.DebtBillDocs.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }

        #endregion


        /// <summary>
        /// Возвращает список долговых квитанций в наборе
        /// </summary>
        /// <param name="BillSet">Набор квитанций</param>
        /// <returns>Таблица с квитанциями</returns>
        public DataTable GetList(BillSet BillSet)
        {
            DataTable _table = new DataTable();

            _table.Columns.Add("ID", typeof(int));
            _table.Columns.Add("Account", typeof(string));
            _table.Columns.Add("Address", typeof(string));
            _table.Columns.Add("Value", typeof(decimal));

            DataSet _ds =
                new DataSet
                {
                    EnforceConstraints = false
                };
            _ds.Tables.Add(_table);

            int _id;
            if (int.TryParse(BillSet.ID, out _id))
            {
                using (Entities _entities = new Entities())
                {
                    var _result = _entities.DebtBillDocs
                        .Where(bill => bill.BillSets.ID == _id)
                        .Select(bill =>
                            new
                            {
                                bill.ID,
                                bill.Account,
                                bill.Address,
                                bill.Value,
                            })
                        .ToList()
                        .OrderBy(bill => bill.Address);

                    foreach (var _DebtBill in _result)
                    {
                        _table.Rows.Add(
                            _DebtBill.ID,
                            _DebtBill.Account,
                            _DebtBill.Address,
                            _DebtBill.Value);
                    }
                }
            }

            return _table;
        }

        /// <summary>
        /// Возвращает массив ID квитанций в наборе
        /// </summary>
        /// <param name="BillSet">Набор квитанций</param>
        /// <returns>Таблица с квитанциями</returns>
        public string[] GetIdsByBillSet(BillSet BillSet)
        {
            string[] _ids = new string[0];
            int _id;
            if (int.TryParse(BillSet.ID, out _id))
            {
                using (Entities _entities = new Entities())
                {
                    var _result =
                        _entities.DebtBillDocs
                            .Where(bill => bill.BillSets.ID == _id)
                            .Select(
                                bill => bill.ID
                                );
                    _ids = _result.Cast<string>().ToArray();
                }
            }

            return _ids;
        }
    }
}