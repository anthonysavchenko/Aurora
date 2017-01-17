using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Oper;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.RechargeOpers;
using DomCustomer = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.Customer;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.RechargeOper;
using DomRechargeSet = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.RechargeSet;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper
{
    public class RechargeOperDataMapper : BaseDataMapper<DomItem, DBItem>, IRechargeOperDataMapper
    {
        #region Overrides of BaseDataMapper<ChargeOper,ChargeOpers>

        /// <summary>
        /// Проверяет существование представления объекта в БД
        /// </summary>
        /// <param name="obj">Объект домена</param>
        /// <returns>true если объект найден в БД, иначе - false</returns>
        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.RechargeOpers.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }

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
                    _entities.AddToRechargeOpers(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.RechargeOpers.First(p => p.ID == _id);
                }

                _dbItem.CreationDateTime = domObj.CreationDateTime;
                _dbItem.Value = domObj.Value;

                int _tempId = int.Parse(domObj.Customer.ID);
                _dbItem.Customers = _entities.Customers.First(c => c.ID == _tempId);

                _tempId = int.Parse(domObj.ChargeSet.ID);
                _dbItem.RechargeSets = _entities.RechargeSets.First(x => x.ID == _tempId);

                if (domObj.ChargeOper != null)
                {
                    _tempId = int.Parse(domObj.ChargeOper.ID);
                    _dbItem.ChargeOpers = _entities.ChargeOpers.First(c => c.ID == _tempId);
                }
                else
                {
                    _dbItem.ChargeOpers = null;
                }

                if (domObj.ChargeCorrectionOper != null)
                {
                    _tempId = int.Parse(domObj.ChargeCorrectionOper.ID);
                    _dbItem.ChildChargeCorrectionOpers = _entities.ChargeCorrectionOpers.First(c => c.ID == _tempId);
                }
                else
                {
                    _dbItem.ChildChargeCorrectionOpers = null;
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
                    _entities.RechargeOpers
                        .Include("Customers")
                        .Include("RechargeSets")
                        .First(x => x.ID == _id);

                _domItem.CreationDateTime = _dbItem.CreationDateTime;
                _domItem.Value = _dbItem.Value;
                _domItem.Customer =
                    (DomCustomer)DataMapperService.get(typeof(DomCustomer)).find(_dbItem.Customers.ID.ToString());
                _domItem.ChargeSet =
                    (DomRechargeSet)DataMapperService.get(typeof(DomRechargeSet)).find(_dbItem.RechargeSets.ID.ToString());
                _domItem.ChargeOper =
                    _dbItem.ChargeOpers != null
                        ? (ChargeOper)DataMapperService.get(typeof(ChargeOper)).find(_dbItem.ChargeOpers.ID.ToString())
                        : null;
                _domItem.ChargeCorrectionOper =
                    _dbItem.ChildChargeCorrectionOpers != null
                        ? (ChargeCorrectionOper)DataMapperService.get(typeof(ChargeCorrectionOper)).find(_dbItem.ChildChargeCorrectionOpers.ID.ToString())
                        : null;
                RebenefitOpers _benefit = _dbItem.RebenefitOpers.FirstOrDefault();
                _domItem.RebenefitOper =
                    _benefit != null
                        ? (RebenefitOper)DataMapperService.get(typeof(RebenefitOper)).find(_benefit.ID.ToString())
                        : null;
            }

            return _domItem;
        }

        #endregion

        public DataTable GetList(DomRechargeSet chargeSet)
        {
            DataTable _table = new DataTable();

            _table.Columns.Add("ID", typeof(int));
            _table.Columns.Add("AccountNumber", typeof(string));
            _table.Columns.Add("Value", typeof(decimal));
            _table.Columns.Add("IsCorrected", typeof(bool));
            _table.Columns.Add("Benefit", typeof(decimal));

            DataSet _ds =
                new DataSet
                {
                    EnforceConstraints = false
                };
            _ds.Tables.Add(_table);

            int _id = int.Parse(chargeSet.ID);
            using (Entities _entities = new Entities())
            {
                var _result =
                    _entities.RechargeOpers
                        .Where(p => p.RechargeSets.ID == _id)
                        .Select(
                            p =>
                            new
                            {
                                p.ID,
                                p.Customers.Account,
                                p.Value,
                                IsCorrected = p.ChildChargeCorrectionOpers != null
                            })
                        .OrderBy(p => p.Account);

                foreach (var _oper in _result)
                {
                    _table.Rows.Add(
                        _oper.ID,
                        _oper.Account,
                        Math.Abs(_oper.Value),
                        _oper.IsCorrected,
                        0);
                }
            }

            return _table;
        }
    }
}