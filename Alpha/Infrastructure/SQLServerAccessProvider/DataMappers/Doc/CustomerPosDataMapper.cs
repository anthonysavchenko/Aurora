using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Doc;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.CustomerPoses;
using DomContractor = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Contractor;
using DomCustomer = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.Customer;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.CustomerPos;
using DomPrivateCounter = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.PrivateCounter;
using DomService = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Service;
using DomServiceSinceTill = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.CustomerPos.ServiceSinceTill;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc
{
    /// <summary>
    /// Преобразователь данных для лицевого счета
    /// </summary>
    public class CustomerPosDataMapper : BaseDataMapper<DomItem, DBItem>, ICustomerPosDataMapper
    {
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
                CustomerPoses customerPos =
                    _entities.CustomerPoses
                        .Include("Contractors")
                        .Include("Services")
                        .Include("Customers")
                        .Include("PrivateCounters")
                        .First(x => x.ID == _id);

                _domItem.Doc = (DomCustomer)DataMapperService.get(typeof(DomCustomer)).find(customerPos.Customers.ID.ToString());
                _domItem.Service = (DomService)DataMapperService.get(typeof(DomService)).find(customerPos.Services.ID.ToString());
                _domItem.Since = customerPos.Since;
                _domItem.Till = customerPos.Till;
                _domItem.Contractor = (DomContractor)DataMapperService.get(typeof(DomContractor)).find(customerPos.Contractors.ID.ToString());
                _domItem.Rate = customerPos.Rate;

                IDataMapper _counterDataMapper = DataMapperService.get(typeof(DomPrivateCounter));
                
                _domItem.PrivateCounters.Clear();

                foreach (var _privateCounter in customerPos.PrivateCounters)
                {
                    DomPrivateCounter _domCounter =
                        (DomPrivateCounter)_counterDataMapper.find(_privateCounter.ID.ToString());
                    _domCounter.CustomerPos = _domItem;
                    _domItem.PrivateCounters.Add(_domCounter.ID, _domCounter);
                }
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
                    _entities.AddToCustomerPoses(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.CustomerPoses.First(x => x.ID == _id);
                }

                int _serviceId = int.Parse(domObj.Service.ID);
                int _contractorId = int.Parse(domObj.Contractor.ID);
                int _customerId = int.Parse(((DomCustomer)domObj.Doc).ID);

                _dbItem.Services = _entities.Services.First(service => service.ID == _serviceId);
                _dbItem.Contractors = _entities.Contractors.First(contractor => contractor.ID == _contractorId);
                _dbItem.Since = domObj.Since;
                _dbItem.Till = domObj.Till;
                _dbItem.Customers = _entities.Customers.First(customer => customer.ID == _customerId);
                _dbItem.Rate = domObj.Rate;

                _entities.SaveChanges();
                domObj.ID = _dbItem.ID.ToString();
            }

            return _dbItem;
        }

        /// <summary>
        /// Проверяет существование представления объекта в БД
        /// </summary>
        /// <param name="_obj">Объект домена</param>
        /// <returns>true если объект найден в БД, иначе - false</returns>
        public override bool checkExistance(IDomainObject _obj)
        {
            bool _result;
            int _domainId = int.Parse(_obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.CustomerPoses.FirstOrDefault(x => x.ID == _domainId);
            }

            return _result;
        }

        /// <summary>
        /// Возвращает список одинаковых услуг абонента, которые есть у всех остальных абонентов из массива их индексов
        /// </summary>
        /// <param name="customer">Абонент</param>
        /// <param name="customerIDs">Массив индексов абонентов</param>
        /// <returns>Таблица услуг</returns>
        public DataTable GetList(DomCustomer currentCustomer, string[] customerIDs, out List<DomServiceSinceTill> UniquePoses)
        {
            DataTable _table = GetTable();
            UniquePoses = new List<DomServiceSinceTill>();
            int _currentCustomerID = Int32.Parse(currentCustomer.ID);
            int _customerCount = customerIDs.Count();

            using (Entities _entities = new Entities())
            {
                _entities.CommandTimeout = 3600;

                IEnumerable<int> _customerIDs = customerIDs.Select(int.Parse);

                var _poses =
                    _entities.CustomerPoses
                        .Where(c => _customerIDs.Contains(c.Customers.ID))
                        .GroupBy(p => 
                            new
                            {
                                ServiceID = p.Services.ID,
                                Since = p.Since,
                                Till = p.Till,
                                ContractorID = p.Contractors.ID,
                                Rate = p.Rate,
                            })
                        .Select(g => 
                            new
                            {
                                ServiceID = g.Key.ServiceID,
                                Since = g.Key.Since,
                                Till = g.Key.Till,
                                ContractorID = g.Key.ContractorID,
                                Rate = g.Key.Rate,
                                GroupedByCount = g.Count(),
                                CurrentCustomerPos = g.FirstOrDefault(_x => _x.Customers.ID == _currentCustomerID),
                            });

                currentCustomer.CustomerPoses.Clear();

                foreach (var _pos in _poses)
                {
                    if (_pos.GroupedByCount == _customerCount)
                    {
                        _table.Rows.Add(
                            _pos.CurrentCustomerPos.ID,
                            _pos.ServiceID,
                            _pos.Since,
                            _pos.Till,
                            _pos.ContractorID,
                            _pos.Rate);

                        currentCustomer.CustomerPoses.Add(_pos.CurrentCustomerPos.ID.ToString(),
                            (DomItem)DataMapperService.get(typeof(DomItem)).find(_pos.CurrentCustomerPos.ID.ToString()));
                    }
                    else
                    {
                        UniquePoses.Add(new DomServiceSinceTill
                        {
                            ServiceID = _pos.ServiceID,
                            Since = _pos.Since,
                            Till = _pos.Till,
                        });
                    }
                }
            }

            return _table;
        }

        /// <summary>
        /// Возвращает список услуг абонента
        /// </summary>
        /// <param name="customer">Абонент</param>
        /// <returns>Таблица услуг</returns>
        public DataTable GetList(DomCustomer customer)
        {
            int _customerID = Int32.Parse(customer.ID);
            DataTable _table = GetTable();

            using (Entities _entities = new Entities())
            {
                var _poses = _entities.CustomerPoses
                    .Include("Contractors")
                    .Include("Services")
                    .Where(_pos => _pos.Customers.ID == _customerID);

                foreach (var _pos in _poses)
                {
                    _table.Rows.Add(
                        _pos.ID.ToString(),
                        _pos.Services.ID,
                        _pos.Since,
                        _pos.Till,
                        _pos.Contractors.ID,
                        _pos.Rate);
                }
            }

            return _table;
        }

        /// <summary>
        /// Создает таблицу для вкладки "Услуги" модуля "Абонент"
        /// </summary>
        /// <returns>Таблица услуг</returns>
        private DataTable GetTable()
        {
            DataTable _table = new DataTable();

            _table.Columns.Add("ID", typeof(string));
            _table.Columns.Add("Service", typeof(string));
            _table.Columns.Add("Since", typeof(DateTime));
            _table.Columns.Add("Till", typeof(DateTime));
            _table.Columns.Add("Contractor", typeof(string));
            _table.Columns.Add("Rate", typeof(decimal));

            return _table;
        }
    }
}