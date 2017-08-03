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
                var customerPos =
                    _entities.CustomerPoses
                        .Where(x => x.ID == _id)
                        .Select(x =>
                            new
                            {
                                CustomerID = x.Customers.ID,
                                ServiceID = x.Services.ID,
                                ContractorID = x.Contractors.ID,
                                x.PrivateCounterID,
                                x.Since,
                                x.Till,
                                x.Rate
                            })
                        .First();

                _domItem.Doc = (DomCustomer)DataMapperService.get(typeof(DomCustomer)).find(customerPos.CustomerID.ToString());
                _domItem.Service = (DomService)DataMapperService.get(typeof(DomService)).find(customerPos.ServiceID.ToString());
                _domItem.Since = customerPos.Since;
                _domItem.Till = customerPos.Till;
                _domItem.Contractor = (DomContractor)DataMapperService.get(typeof(DomContractor)).find(customerPos.ContractorID.ToString());
                _domItem.Rate = customerPos.Rate;
                _domItem.PrivateCounter = customerPos.PrivateCounterID.HasValue
                    ? (DomPrivateCounter)DataMapperService.get(typeof(DomPrivateCounter)).find(customerPos.PrivateCounterID.Value.ToString())
                    : null;
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

                _dbItem.Since = domObj.Since;
                _dbItem.Till = domObj.Till;
                _dbItem.Rate = domObj.Rate;

                int tempId = int.Parse(domObj.Service.ID);
                _dbItem.Services = _entities.Services.First(service => service.ID == tempId);

                tempId = int.Parse(domObj.Contractor.ID);
                _dbItem.Contractors = _entities.Contractors.First(contractor => contractor.ID == tempId);

                tempId = int.Parse(((DomCustomer)domObj.Doc).ID);
                _dbItem.Customers = _entities.Customers.First(customer => customer.ID == tempId);

                _dbItem.PrivateCounterID = domObj.PrivateCounter != null
                    ? int.Parse(domObj.PrivateCounter.ID)
                    : (int?)null;

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
            int _currentCustomerID = int.Parse(currentCustomer.ID);
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
                            })
                        .ToList();

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
            int _customerID = int.Parse(customer.ID);
            DataTable _table = GetTable();

            using (Entities _db = new Entities())
            {
                var _poses = _db.CustomerPoses
                    .Where(p => p.Customers.ID == _customerID)
                    .Select(p =>
                        new
                        {
                            p.ID,
                            ServiceID = p.Services.ID,
                            p.Since,
                            p.Till,
                            ContractorID = p.Contractors.ID,
                            p.Rate,
                            p.PrivateCounterID
                        });

                foreach (var _pos in _poses)
                {
                    _table.Rows.Add(
                        _pos.ID.ToString(),
                        _pos.ServiceID,
                        _pos.Since,
                        _pos.Till,
                        _pos.ContractorID,
                        _pos.Rate,
                        _pos.PrivateCounterID.HasValue ? _pos.PrivateCounterID.Value.ToString() : string.Empty);
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
            _table.Columns.Add("Service", typeof(int));
            _table.Columns.Add("Since", typeof(DateTime));
            _table.Columns.Add("Till", typeof(DateTime));
            _table.Columns.Add("Contractor", typeof(int));
            _table.Columns.Add("Rate", typeof(decimal));
            _table.Columns.Add("Counter", typeof(string));

            return _table;
        }
    }
}