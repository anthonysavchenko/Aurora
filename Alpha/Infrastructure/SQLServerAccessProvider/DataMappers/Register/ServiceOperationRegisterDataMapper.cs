using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.ServiceOperationRegister;
using DomCustomer = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.Customer;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Register.ServiceOperationRegister;
using DomService = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Service;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Rregister
{
    /// <summary>
    /// Register datamapper
    /// </summary>
    public class ServiceOperationRegisterDataMapper : BaseDataMapper<DomItem, DBItem>
    {
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
                ServiceOperationRegister _dbItem =
                    _entities.ServiceOperationRegister
                        .Include("Services")
                        .Include("Customers")
                        .First(x => x.ID == _id);

                _domItem.OperationDateTime = _dbItem.OperationDateTime;
                _domItem.OperationID = _dbItem.OperationID;
                _domItem.OperationType = (DomItem.OperationTypes)_dbItem.OperationType;
                _domItem.Service = (DomService)DataMapperService.get(typeof(DomService)).find(_dbItem.Services.ID.ToString());
                _domItem.ServicePeriod = _dbItem.ServicePeriod;
                _domItem.Customer = (DomCustomer)DataMapperService.get(typeof(DomCustomer)).find(_dbItem.Customers.ID.ToString());
                _domItem.Value = _dbItem.Value;
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
                    _entities.AddToServiceOperationRegister(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.ServiceOperationRegister.First(x => x.ID == _id);
                }

                int _serviceId = Convert.ToInt32(domObj.Service.ID);
                int _customerId = Convert.ToInt32(domObj.Customer.ID);

                _dbItem.OperationDateTime = domObj.OperationDateTime;
                _dbItem.OperationID = domObj.OperationID;
                _dbItem.OperationType = (int)domObj.OperationType;
                _dbItem.Services = _entities.Services.First(service => service.ID == _serviceId);
                _dbItem.ServicePeriod = domObj.ServicePeriod;
                _dbItem.Customers = _entities.Customers.First(customer => customer.ID == _customerId);
                _dbItem.Value = domObj.Value;

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
                _result = null != _entities.ServiceOperationRegister.FirstOrDefault(x => x.ID == _domainId);
            }

            return _result;

        }
    }
}