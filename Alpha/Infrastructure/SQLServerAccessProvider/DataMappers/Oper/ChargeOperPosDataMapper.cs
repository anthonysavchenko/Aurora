using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Oper;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.ChargeOperPoses;
using DomChargeOper = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.ChargeOper;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.ChargeOperPos;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper
{
    public class ChargeOperPosDataMapper : BaseDataMapper<DomItem, DBItem>, IChargeOperPosDataMapper
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
                _result = null != _entities.ChargeOperPoses.FirstOrDefault(p => p.ID == _domainId);
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
                    _entities.AddToChargeOperPoses(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.ChargeOperPoses.First(p => p.ID == _id);
                }

                _dbItem.Value = domObj.Value;

                int _tempId = int.Parse(domObj.Service.ID);
                _dbItem.Services = _entities.Services.First(x => x.ID == _tempId);

                _tempId = int.Parse(domObj.Contractor.ID);
                _dbItem.Contractors = _entities.Contractors.First(x => x.ID == _tempId);

                _tempId = int.Parse(domObj.ChargeOper.ID);
                _dbItem.ChargeOpers = _entities.ChargeOpers.First(x => x.ID == _tempId);

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
                    _entities.ChargeOperPoses
                        .Include("Services")
                        .Include("Contractors")
                        .Include("ChargeOpers")
                        .First(x => x.ID == _id);

                _domItem.Value = _dbItem.Value;
                _domItem.Service = (Service)DataMapperService.get(typeof(Service)).find(_dbItem.Services.ID.ToString());
                _domItem.Contractor =
                    (Contractor)DataMapperService.get(typeof(Contractor)).find(_dbItem.Contractors.ID.ToString());
                _domItem.ChargeOper =
                    (DomChargeOper)DataMapperService.get(typeof(DomChargeOper)).find(_dbItem.ChargeOpers.ID.ToString());
            }

            return _domItem;
        }

        #endregion

        public DataTable GetList(DomChargeOper oper)
        {
            DataTable _table = new DataTable("PaymentOperPoses");
            _table.Columns.Add("ID", typeof(int));
            _table.Columns.Add("Contractor", typeof(string));
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

            using (Entities _db = new Entities())
            {
                _db.CommandTimeout = 3600;

                var _services = _db.Services
                    .Select(s =>
                        new
                        {
                            s.ID,
                            s.Name,
                            ServiceType = s.ServiceTypes.Name
                        })
                    .ToDictionary(s => s.ID);

                var _data =
                    _db.ChargeOperPoses
                        .Where(p => p.ChargeOpers.ID == _id)
                        .Select(p =>
                            new
                            {
                                p.ID,
                                p.Value,
                                Contractor = p.Contractors.Name,
                                ServiceID = p.Services.ID,
                                Type = "Начисление"
                            })
                        .Concat(
                            _db.BenefitOperPoses
                                .Where(b => b.BenefitOpers.ChargeOpers.ID == _id)
                                .Select(b =>
                                    new
                                    {
                                        b.ID,
                                        b.Value,
                                        Contractor = b.Contractors.Name,
                                        ServiceID = b.Services.ID,
                                        Type = "Льгота"
                                    }
                                ));

                foreach (var _pos in _data)
                {
                    _table.Rows.Add(
                        _pos.ID,
                        _pos.Contractor,
                        Math.Abs(_pos.Value),
                        _services[_pos.ServiceID].Name,
                        _services[_pos.ServiceID].ServiceType,
                        _pos.Type);
                }
            }

            return _table;
        }
    }
}