using Microsoft.Practices.CompositeUI;
using System;
using System.Linq;
using DevExpress.XtraPrinting.Native;
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
                Type[] _typeArray =
                {
                    typeof(Resident),
                    typeof(CustomerPos),
                    typeof(PrivateCounter),
                    typeof(PrivateCounterValue)
                };

                IDataMapper[] _dmArray =
                {
                    DatMapServ.get(_typeArray[0]),
                    DatMapServ.get(_typeArray[1]),
                    DatMapServ.get(_typeArray[2]),
                    DatMapServ.get(_typeArray[3])
                };

                for (int i = 0; i < _typeArray.Length; i++)
                {
                    var _domObjList = newObjects.Values.Where(v => v.GetType() == _typeArray[i]);
                    foreach (DomainObject _domObj in _domObjList)
                    {
                        _result = _dmArray[i].update(_domObj);
                        if (!_result)
                        {
                            break;
                        }
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
                bool _res = true;

                Type[] _typeArray =
                {
                    typeof(PrivateCounterValue),
                    typeof(PrivateCounter),
                    typeof(CustomerPos),
                    typeof(Resident)
                };

                IDataMapper[] _dmArray = 
                {
                    DatMapServ.get(_typeArray[0]),
                    DatMapServ.get(_typeArray[1]),
                    DatMapServ.get(_typeArray[2]),
                    DatMapServ.get(_typeArray[3])
                };

                try
                {
                    for (int i = 0; i < _typeArray.Length; i++)
                    {
                        removedObjects.Values
                            .Where(o => o.GetType() == _typeArray[i])
                            .ForEach(o =>
                            {
                                if (!_dmArray[i].delete(o.ID))
                                {
                                    throw new ApplicationException("Delete failed");
                                }
                            });
                    }
                }
                catch(ApplicationException)
                {
                    _res = false;
                }

                return _res;
            }

            return _result;
        }
    }
}