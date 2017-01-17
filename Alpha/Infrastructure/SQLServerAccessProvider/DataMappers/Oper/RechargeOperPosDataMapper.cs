using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Oper;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.RechargeOperPoses;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.RechargeOperPos;
using DomRechargeOper = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.RechargeOper;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper
{
    public class RechargeOperPosDataMapper : BaseDataMapper<DomItem, DBItem>, IRechargeOperPosDataMapper
    {
        #region Overrides of BaseDataMapper<ChargeOperPos,ChargeOperPoses>

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
                _result = null != _entities.RechargeOperPoses.FirstOrDefault(p => p.ID == _domainId);
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
                    _entities.AddToRechargeOperPoses(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.RechargeOperPoses.First(p => p.ID == _id);
                }

                _dbItem.Value = domObj.Value;

                int _tempId = int.Parse(domObj.Service.ID);
                _dbItem.Services = _entities.Services.First(x => x.ID == _tempId);

                _tempId = int.Parse(domObj.Contractor.ID);
                _dbItem.Contractors = _entities.Contractors.First(x => x.ID == _tempId);

                _tempId = int.Parse(domObj.ChargeOper.ID);
                _dbItem.RechargeOpers = _entities.RechargeOpers.First(x => x.ID == _tempId);

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
                    _entities.RechargeOperPoses
                        .Include("Services")
                        .Include("Contractors")
                        .Include("RechargeOpers")
                        .First(x => x.ID == _id);

                _domItem.Value = _dbItem.Value;
                _domItem.Service = (Service)DataMapperService.get(typeof(Service)).find(_dbItem.Services.ID.ToString());
                _domItem.Contractor =
                    (Contractor)DataMapperService.get(typeof(Contractor)).find(_dbItem.Contractors.ID.ToString());
                _domItem.ChargeOper =
                    (DomRechargeOper)DataMapperService.get(typeof(DomRechargeOper)).find(_dbItem.RechargeOpers.ID.ToString());
            }

            return _domItem;
        }

        #endregion

        public DataTable GetList(DomRechargeOper oper)
        {
            DataTable _table = new DataTable("PaymentOperPoses");
            _table.Columns.Add("ID", typeof(int));
            _table.Columns.Add("Contractors", typeof(string));
            _table.Columns.Add("Value", typeof(decimal));
            _table.Columns.Add("ServiceName", typeof(string));
            _table.Columns.Add("ServiceTypeName", typeof(string));
            _table.Columns.Add("Type", typeof(string));

            DataSet _ds =
                new DataSet
                {
                    EnforceConstraints = false
                };
            _ds.Tables.Add(_table);

            int _id = int.Parse(oper.ID);

            using (Entities _entities = new Entities())
            {
                _entities.CommandTimeout = 3600;

                var _data =
                    _entities.RechargeOperPoses
                        .Where(p => p.RechargeOpers.ID == _id)
                        .Select(p =>
                            new
                            {
                                p.ID,
                                Contractor = p.Contractors.Name,
                                p.Value,
                                ServiceName = p.Services.Name,
                                ServiceTypeName = p.Services.ServiceTypes.Name,
                                Type = "Начисление"
                            });

                foreach (var _pos in _data)
                {
                    _table.Rows.Add(
                        _pos.ID,
                        _pos.Contractor,
                        Math.Abs(_pos.Value),
                        _pos.ServiceName,
                        _pos.ServiceTypeName,
                        _pos.Type);
                }
            }

            return _table;
        }
    }
}