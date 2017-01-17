using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Oper;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.PaymentOpers;
using DomCustomer = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.Customer;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.PaymentOper;
using DomPaymentCorrectionOper = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.PaymentCorrectionOper;
using DomPaymentSet = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.PaymentSet;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper
{
    public class PaymentOperDataMapper : BaseDataMapper<DomItem, DBItem>, IPaymentOperDataMapper
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
                    _entities.AddToPaymentOpers(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.PaymentOpers.First(p => p.ID == _id);
                }

                _dbItem.CreationDateTime = domObj.CreationDateTime;
                _dbItem.PaymentPeriod = domObj.PaymentPeriod;
                _dbItem.Value = domObj.Value;

                int _propId = Int32.Parse(domObj.Customer.ID);
                _dbItem.Customers = _entities.Customers.First(c => c.ID == _propId);

                _propId = Int32.Parse(domObj.PaymentSet.ID);
                _dbItem.PaymentSets = _entities.PaymentSets.First(p => p.ID == _propId);

                if (domObj.PaymentCorrectionOper != null)
                {
                    _propId = Int32.Parse(domObj.PaymentCorrectionOper.ID);
                    _dbItem.PaymentCorrectionOper = _entities.PaymentCorrectionOpers.First(p => p.ID == _propId);
                }
                else
                {
                    _dbItem.PaymentCorrectionOper = null;
                }

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
        protected override DomItem ServiceToBusiness(IDomainObject obj)
        {
            DomItem _domItem = (DomItem)obj;
            int _id = int.Parse(_domItem.ID);

            using (Entities _entities = new Entities())
            {
                DBItem _dbItem =
                    _entities.PaymentOpers
                        .Include("Customers")
                        .Include("PaymentSets")
                        .Include("PaymentCorrectionOpers")
                        .First(x => x.ID == _id);

                _domItem.CreationDateTime = _dbItem.CreationDateTime;
                _domItem.PaymentPeriod = _dbItem.PaymentPeriod;
                _domItem.Value = _dbItem.Value;
                _domItem.Customer = (DomCustomer)DataMapperService.get(typeof(DomCustomer)).find(_dbItem.Customers.ID.ToString());
                _domItem.PaymentSet = (DomPaymentSet)DataMapperService.get(typeof(DomPaymentSet)).find(_dbItem.PaymentSets.ID.ToString());
                _domItem.PaymentCorrectionOper =
                    _dbItem.PaymentCorrectionOper != null
                        ? (DomPaymentCorrectionOper)DataMapperService.get(typeof(DomPaymentCorrectionOper)).find(_dbItem.PaymentCorrectionOper.ID.ToString())
                        : null;
            }

            return _domItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.PaymentOpers.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }

        #endregion

        public DataTable GetList(string paymentSetId)
        {
            DataTable _table = new DataTable();

            _table.Columns.Add("ID", typeof(int));
            _table.Columns.Add("Period", typeof(DateTime));
            _table.Columns.Add("AccountNumber", typeof(string));
            _table.Columns.Add("Value", typeof(decimal));
            _table.Columns.Add("IsCorrected", typeof(bool));

            DataSet _ds =
                new DataSet
                {
                    EnforceConstraints = false
                };
            _ds.Tables.Add(_table);

            int _id;
            if (int.TryParse(paymentSetId, out _id))
            {
                using (Entities _entities = new Entities())
                {
                    var _result =
                        _entities.PaymentOpers
                            .Where(p => p.PaymentSets.ID == _id)
                            .Select(
                                p =>
                                new
                                {
                                    p.ID,
                                    p.PaymentPeriod,
                                    p.Customers.Account,
                                    p.Value,
                                    CorrectionValue = p.PaymentCorrectionOper != null
                                });

                    foreach (var _paymentOper in _result)
                    {
                        _table.Rows.Add(
                            _paymentOper.ID,
                            _paymentOper.PaymentPeriod,
                            _paymentOper.Account,
                            Math.Abs(_paymentOper.Value*(-1)),
                            _paymentOper.CorrectionValue);
                    }
                }
            }

            return _table;
        }
    }
}