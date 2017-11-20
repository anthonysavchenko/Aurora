using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.Services;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Service;
using DomServiceType = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.ServiceType;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook
{
    /// <summary>
    /// Преобразователь данных для лицевого счета
    /// </summary>
    public class ServiceDataMapper : BaseDataMapper<DomItem, DBItem>
    {
        public override object doLoad()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID", typeof(string));
            _table.Columns.Add("Name", typeof(string));

            using (Entities _entities = new Entities())
            {
                foreach (var _service in _entities.Services.OrderBy(s => s.Name))
                {
                    _table.Rows.Add(
                        _service.ID.ToString(),
                        _service.Name);
                }
            }

            return _table;
        }

        /// <summary>
        /// Преобразовывает объект прокси БД в объект домена
        /// </summary>
        /// <param name="obj">Объект домена</param>
        /// <returns>Объект домена</returns>
        protected override DomItem ServiceToBusiness(IDomainObject obj)
        {
            DomItem _domItem = (DomItem)obj;
            int _id = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                Services service = _entities.Services.Include("ServiceTypes").First(x => x.ID == _id);

                _domItem.Name = service.Name;
                _domItem.Code = service.Code;
                _domItem.ServiceType = (DomServiceType)DataMapperService.get(typeof(DomServiceType)).find(service.ServiceTypes.ID.ToString());
                _domItem.ChargeRule = (DomItem.ChargeRuleType)service.ChargeRule;
                _domItem.Norm = service.Norm;
                _domItem.Measure = service.Measure;
            }

            return _domItem;
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
                    _entities.AddToServices(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.Services.First(x => x.ID == _id);
                }

                int _serviceTypeId = Convert.ToInt32(domObj.ServiceType.ID);

                _dbItem.Name = domObj.Name;
                _dbItem.Code = domObj.Code;
                _dbItem.ServiceTypes = _entities.ServiceTypes.First(serviceType => serviceType.ID == _serviceTypeId);
                _dbItem.ChargeRule = (byte)domObj.ChargeRule;
                _dbItem.Norm = domObj.Norm;
                _dbItem.Measure = domObj.Measure;

                _entities.SaveChanges();
                domObj.ID = _dbItem.ID.ToString();
            }

            return _dbItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.Services.FirstOrDefault(x => x.ID == _domainId);
            }

            return _result;
        }
    }
}
