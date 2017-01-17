using Microsoft.Practices.CompositeUI;
using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.WinClient.Aurora.Library;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Constants;
using Taumis.EnterpriseLibrary.Win;
using DomServiceSinceTill = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.CustomerPos.ServiceSinceTill;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Services
{
    [Service]
    public class CustomersUnitOfWork : UnitOfWork
    {
        /// <summary>
        /// Режим редактирования элементов списка
        /// </summary>
        public string EditMode
        {
            set;
            get;
        }

        /// <summary>
        /// Идентификаторы выбранных элементов для множественного редактирования
        /// </summary>
        public string[] SelectedIDs
        {
            set;
            get;
        }

        /// <summary>
        /// Добавление новых элементов имеет особый порядок.
        /// </summary>
        override protected bool insertNew()
        {
            bool _result = true;

            if (EditMode == ModuleEditItemModes.Multiple)
            {
                foreach (DomainObject _domObj in newObjects.Values)
                {
                    if (_domObj.GetType() == typeof(CustomerPos))
                    {
                        using (Entities _entities = new Entities())
                        {
                            CustomerPos _afterEditPos = ((CustomerPos)_domObj);
                            int _serviceID = Int32.Parse(_afterEditPos.Service.ID);
                            int _contractorID = Int32.Parse(_afterEditPos.Contractor.ID);
                            DataBase.Services _service = _entities.Services.First(_x => _x.ID == _serviceID);
                            Contractors _contractor = _entities.Contractors.First(_x => _x.ID == _contractorID);

                            foreach (string _idString in SelectedIDs)
                            {
                                int _customerID = Int32.Parse(_idString);

                                CustomerPoses _currentPos = new CustomerPoses();
                                _entities.AddToCustomerPoses(_currentPos);

                                _currentPos.Customers = _entities.Customers.First(_x => _x.ID == _customerID);
                                _currentPos.Services = _service;
                                _currentPos.Contractors = _contractor;
                                _currentPos.Since = _afterEditPos.Since;
                                _currentPos.Till = _afterEditPos.Till;
                                _currentPos.Rate = _afterEditPos.Rate;
                            }

                            _entities.SaveChanges();
                        }
                    }
                }
            }
            else
            {
                Type _residentType = typeof(Resident);
                Type _customerPosType = typeof(CustomerPos);
                Type _privateCounterType = typeof(PrivateCounter);
                Type _privateCounterValueType = typeof(PrivateCounterValue);

                IDataMapper _dmResident = DatMapServ.get(_residentType);
                IDataMapper _dmCustomerPos = DatMapServ.get(_customerPosType);
                IDataMapper _dmPrivateCounter = DatMapServ.get(_privateCounterType);
                IDataMapper _dmPrivateCounterValue = DatMapServ.get(_privateCounterValueType);

                foreach (DomainObject _domObj in newObjects.Values)
                {
                    Type _type = _domObj.GetType();

                    if (_type == _residentType)
                    {
                        _result = _dmResident.update(_domObj);
                    }
                    else if (_type == _customerPosType)
                    {
                        _result = _dmCustomerPos.update(_domObj);
                    }
                    else if (_type == _privateCounterType)
                    {
                        _result = _dmPrivateCounter.update(_domObj);
                    }
                    else if (_type == _privateCounterValueType)
                    {
                        _result = _dmPrivateCounterValue.update(_domObj);
                    }

                    if (!_result)
                    {
                        break;
                    }
                }
            }

            return _result;
        }

        protected override bool updateDirty()
        {
            bool _result = true;

            if (EditMode == ModuleEditItemModes.Multiple)
            {
                foreach (DomainObject _domObj in dirtyObjects.Values)
                {
                    if (_domObj.GetType() == typeof(CustomerPos))
                    {
                        using (Entities _entities = new Entities())
                        {
                            int _beforeEditPosID = Int32.Parse(_domObj.ID);
                            DomServiceSinceTill _beforeEditPos = _entities.CustomerPoses
                                .Where(_pos => _pos.ID == _beforeEditPosID)
                                .Select(_pos => new DomServiceSinceTill
                                {
                                    ServiceID = _pos.Services.ID,
                                    Since = _pos.Since,
                                    Till = _pos.Till,
                                })
                                .First();

                            CustomerPos _afterEditPos = ((CustomerPos)_domObj);
                            int _serviceID = Int32.Parse(_afterEditPos.Service.ID);
                            int _contractorID = Int32.Parse(_afterEditPos.Contractor.ID);
                            DataBase.Services _service = _entities.Services.First(_x => _x.ID == _serviceID);
                            Contractors _contractor = _entities.Contractors.First(_x => _x.ID == _contractorID);

                            foreach (string _idString in SelectedIDs)
                            {
                                int _customerID = Int32.Parse(_idString);

                                CustomerPoses _currentPos = _entities.CustomerPoses
                                        .First(_pos => _pos.Customers.ID == _customerID &&
                                            _pos.Services.ID == _beforeEditPos.ServiceID &&
                                            _pos.Since == _beforeEditPos.Since &&
                                            _pos.Till == _beforeEditPos.Till);

                                _currentPos.Services = _service;
                                _currentPos.Contractors = _contractor;
                                _currentPos.Since = _afterEditPos.Since;
                                _currentPos.Till = _afterEditPos.Till;
                                _currentPos.Rate = _afterEditPos.Rate;
                            }

                            _entities.SaveChanges();
                        }
                    }
                }
            }
            else
            {
                _result = base.updateDirty();
            }

            return _result;
        }

        protected override bool deleteRemoved()
        {
            bool _result = true;

            if (EditMode == ModuleEditItemModes.Multiple)
            {
                foreach (DomainObject _domObj in removedObjects.Values)
                {
                    if (_domObj.GetType() == typeof(CustomerPos))
                    {
                        using (Entities _entities = new Entities())
                        {
                            int _beforeEditPosID = Int32.Parse(_domObj.ID);
                            DomServiceSinceTill _beforeEditPos = _entities.CustomerPoses
                                .Where(_pos => _pos.ID == _beforeEditPosID)
                                .Select(_pos => new DomServiceSinceTill
                                {
                                    ServiceID = _pos.Services.ID,
                                    Since = _pos.Since,
                                    Till = _pos.Till,
                                })
                                .First();

                            foreach (string _idString in SelectedIDs)
                            {
                                int _customerID = Int32.Parse(_idString);

                                CustomerPoses _currentPos = _entities.CustomerPoses
                                        .First(_pos => _pos.Customers.ID == _customerID &&
                                            _pos.Services.ID == _beforeEditPos.ServiceID &&
                                            _pos.Since == _beforeEditPos.Since &&
                                            _pos.Till == _beforeEditPos.Till);

                                _entities.DeleteObject(_currentPos);
                            }

                            _entities.SaveChanges();
                        }
                    }
                }
            }
            else
            {
                _result = base.deleteRemoved();
            }

            return _result;
        }
    }
}