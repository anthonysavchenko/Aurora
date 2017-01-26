using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Doc;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using Taumis.Infrastructure.Interface.Services;
using DBItem = Taumis.Alpha.DataBase.RegularBillDocs;
using DomBillSet = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.BillSet;
using DomChargeData = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.RegularBillDocSeviceTypePos;
using DomCounterData = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.RegularBillDocCounterPos;
using DomCustomer = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.Customer;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.RegularBillDoc;
using DomSharedCounterData = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.RegularBillDocSharedCounterPos;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc
{
    public class RegularBillDocDataMapper : BaseDataMapper<DomItem, DBItem>, IBaseBillDocDataMapper
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
                    _entities.AddToRegularBillDocs(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.RegularBillDocs.First(x => x.ID == _id);
                }

                _dbItem.CreationDateTime = domObj.CreationDateTime;
                _dbItem.Period = domObj.Period;
                _dbItem.Account = domObj.Account;
                _dbItem.Address = domObj.Address;
                _dbItem.Owner = domObj.Owner;
                _dbItem.PayBeforeDateTime = domObj.PayBeforeDateTime;
                _dbItem.Square = domObj.Square;
                _dbItem.ResidentsCount = domObj.ResidentsCount;
                _dbItem.OverpaymentValue = domObj.OverpaymentValue;
                _dbItem.MonthChargeValue = domObj.MonthChargeValue;
                _dbItem.Value = domObj.Value;
                _dbItem.ContractorContactInfo = domObj.ContractorContactInfo;

                int _propId = Int32.Parse(domObj.BillSet.ID);
                _dbItem.BillSets = _entities.BillSets.First(p => p.ID == _propId);

                _propId = Int32.Parse(domObj.Customer.ID);
                _dbItem.Customers = _entities.Customers.First(p => p.ID == _propId);

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
                    _entities.RegularBillDocs
                        .Include("BillSets")
                        .Include("Customers")
                        .Include("RegularBillDocSeviceTypePoses")
                        .Include("RegularBillDocCounterPoses")
                        .Include("RegularBillDocSharedCounterPoses")
                        .First(x => x.ID == _id);

                _domItem.CreationDateTime = _dbItem.CreationDateTime;
                _domItem.Period = _dbItem.Period;
                _domItem.Account = _dbItem.Account;
                _domItem.Address = _dbItem.Address;
                _domItem.Owner = _dbItem.Owner;
                _domItem.PayBeforeDateTime = _dbItem.PayBeforeDateTime;
                _domItem.Square = _dbItem.Square;
                _domItem.ResidentsCount = _dbItem.ResidentsCount;
                _domItem.OverpaymentValue = _dbItem.OverpaymentValue;
                _domItem.MonthChargeValue = _dbItem.MonthChargeValue;
                _domItem.Value = _dbItem.Value;
                _domItem.ContractorContactInfo = _dbItem.ContractorContactInfo;
                _domItem.BillSet = (DomBillSet)DataMapperService.get(typeof(DomBillSet)).find(_dbItem.BillSets.ID.ToString());
                _domItem.Customer = (DomCustomer)DataMapperService.get(typeof(DomCustomer)).find(_dbItem.Customers.ID.ToString());

                foreach (var _item in _dbItem.RegularBillDocSeviceTypePoses)
                {
                    _domItem.RegularBillDocSeviceTypePoses.Add(
                        _item.ID.ToString(),
                        (DomChargeData)DataMapperService.get(typeof(DomChargeData)).find(_item.ID.ToString()));
                }

                foreach (var _item in _dbItem.RegularBillDocCounterPoses)
                {
                    _domItem.RegularBillDocCounterPoses.Add(
                        _item.ID.ToString(),
                        (DomCounterData)DataMapperService.get(typeof(DomCounterData)).find(_item.ID.ToString()));
                }

                foreach (var _item in _dbItem.RegularBillDocSharedCounterPoses)
                {
                    _domItem.RegularBillDocSharedCounterPoses.Add(
                        _item.ID.ToString(),
                        (DomSharedCounterData)DataMapperService.get(typeof(DomSharedCounterData)).find(_item.ID.ToString()));
                }
            }

            return _domItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.RegularBillDocs.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }

        #endregion


        /// <summary>
        /// Возвращает список квитанций в наборе
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
                    var _result = _entities.RegularBillDocs
                        .Where(bill => bill.BillSets.ID == _id)
                        .Select(bill =>
                            new
                            {
                                bill.ID,
                                bill.Account,
                                bill.Customers.Buildings.Streets.Name,
                                bill.Customers.Buildings.Number,
                                bill.Customers.Apartment,
                                bill.Value,
                            })
                        .ToList()
                        .OrderBy(bill => bill.Name)
                        .ThenBy(bill => bill.Number, new StringWithNumbersComparer())
                        .ThenBy(bill => bill.Apartment, new StringWithNumbersComparer());

                    foreach (var _bill in _result)
                    {
                        _table.Rows.Add(
                            _bill.ID,
                            _bill.Account,
                            $"ул. {_bill.Name}, д. {_bill.Number}, кв. {_bill.Apartment}",
                            _bill.Value);
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
            int _id = int.Parse(BillSet.ID);

            using (Entities _entities = new Entities())
            {
                return _entities.RegularBillDocs
                    .Where(bill => bill.BillSets.ID == _id)
                    .Select(bill => bill.ID)
                    .Cast<string>()
                    .ToArray();
            }
        }
    }
}