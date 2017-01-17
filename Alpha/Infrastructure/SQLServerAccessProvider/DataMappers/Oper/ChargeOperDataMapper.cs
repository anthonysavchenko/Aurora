using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Oper;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.ChargeOpers;
using DomChargeSet = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.ChargeSet;
using DomCustomer = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.Customer;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.ChargeOper;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper
{
    public class ChargeOperDataMapper : BaseDataMapper<DomItem, DBItem>, IChargeOperDataMapper
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
                _result = null != _entities.ChargeOpers.FirstOrDefault(p => p.ID == _domainId);
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
                    _entities.AddToChargeOpers(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.ChargeOpers.First(p => p.ID == _id);
                }

                _dbItem.CreationDateTime = domObj.CreationDateTime;
                _dbItem.Value = domObj.Value;

                int _temId = int.Parse(domObj.Customer.ID);
                _dbItem.Customers = _entities.Customers.First(c => c.ID == _temId);

                _temId = int.Parse(domObj.ChargeSet.ID);
                _dbItem.ChargeSets = _entities.ChargeSets.First(x => x.ID == _temId);

                _temId = int.Parse(domObj.RegularBillDoc.ID);
                _dbItem.RegularBillDocs = _entities.RegularBillDocs.First(p => p.ID == _temId);

                if (domObj.ChargeCorrectionOper != null)
                {
                    _temId = int.Parse(domObj.ChargeCorrectionOper.ID);
                    _dbItem.ChargeCorrectionOpers = _entities.ChargeCorrectionOpers.First(p => p.ID == _temId);
                }
                else
                {
                    _dbItem.ChargeCorrectionOpers = null;
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
                    _entities.ChargeOpers
                        .Include("ChargeSets")
                        .Include("ChargeCorrectionOpers")
                        .Include("RegularBillDocs")
                        .Include("Customers")
                        .Include("BenefitOpers")
                        .First(x => x.ID == _id);

                _domItem.CreationDateTime = _dbItem.CreationDateTime;
                _domItem.Value = _dbItem.Value;
                _domItem.Customer =
                    (DomCustomer)DataMapperService.get(typeof(DomCustomer)).find(_dbItem.Customers.ID.ToString());
                _domItem.ChargeSet =
                    (DomChargeSet)DataMapperService.get(typeof(DomChargeSet)).find(_dbItem.ChargeSets.ID.ToString());
                _domItem.ChargeCorrectionOper =
                    _dbItem.ChargeCorrectionOpers != null
                        ? (ChargeCorrectionOper)DataMapperService.get(typeof(ChargeCorrectionOper)).find(_dbItem.ChargeCorrectionOpers.ID.ToString())
                        : null;
                _domItem.RegularBillDoc =
                    _dbItem.RegularBillDocs != null
                        ? (RegularBillDoc)DataMapperService.get(typeof(RegularBillDoc)).find(_dbItem.RegularBillDocs.ID.ToString())
                        : null;

                BenefitOpers _benefit = _dbItem.BenefitOpers.FirstOrDefault();
                _domItem.BenefitOper =
                    _benefit != null
                        ? (BenefitOper)DataMapperService.get(typeof(BenefitOper)).find(_benefit.ID.ToString())
                        : null;
            }

            return _domItem;
        }

        #endregion

        public DataTable GetList(DomChargeSet chargeSet)
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
                    _entities.ChargeOpers
                        .Where(p => p.ChargeSets.ID == _id)
                        .Select(
                            p =>
                            new
                            {
                                p.ID,
                                p.Customers.Account,
                                p.Value,
                                IsCorrected = p.ChargeCorrectionOpers != null,
                                Benefit = p.BenefitOpers.Sum(b => (decimal?)b.Value) ?? 0
                            })
                        .OrderBy(p => p.Account);

                foreach (var _oper in _result)
                {
                    _table.Rows.Add(
                        _oper.ID,
                        _oper.Account,
                        Math.Abs(_oper.Value),
                        _oper.IsCorrected,
                        Math.Abs(_oper.Benefit));
                }
            }

            return _table;
        }
    }
}