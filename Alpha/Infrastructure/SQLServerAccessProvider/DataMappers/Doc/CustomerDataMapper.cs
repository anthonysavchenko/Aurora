﻿using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Doc;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.Customers;
using DomBuilding = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Building;
using DomCustomerPos = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.CustomerPos;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.Customer;
using DomPrivateCounter = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.PrivateCounter;
using DomResident = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Resident;
using DomUser = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.User;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc
{
    /// <summary>
    /// Преобразователь данных для лицевого счета
    /// </summary>
    public class CustomerDataMapper : BaseDataMapper<DomItem, DBItem>, ICustomerDataMapper
    {
        #region Overrides of BaseDataMapper

        /// <summary>
        /// Преобразователь из БД в Домен
        /// с созданием нового объекта Домена.
        /// </summary>
        /// <param name="_obj"></param>
        /// <returns></returns>
        protected override DomItem ServiceToBusiness(IDomainObject _obj)
        {
            DomItem _domItem = (DomItem)_obj;
            int _id = int.Parse(_domItem.ID);
            using (Entities _db = new Entities())
            {
                var _customer =
                    _db.Customers
                        .Where(x => x.ID == _id)
                        .Select(x =>
                            new
                            {
                                x.OwnerType,
                                x.PhysicalPersonShortName,
                                x.PhysicalPersonFullName,
                                x.JuridicalPersonFullName,
                                x.Account,
                                x.IsPrivate,
                                x.RoomsCount,
                                BuildingID = x.Buildings.ID,
                                x.Floor,
                                x.Entrance,
                                x.Apartment,
                                x.Square,
                                x.Comment,
                                x.LiftPresence,
                                x.RubbishChutePresence,
                                x.BillSendingSubscription,
                                x.DebtsRepayment,
                                x.UserID
                            })
                        .First();

                _domItem.OwnerType = (OwnerType)_customer.OwnerType;
                _domItem.PhysicalPersonShortName = _customer.PhysicalPersonShortName;
                _domItem.PhysicalPersonFullName = _customer.PhysicalPersonFullName;
                _domItem.JuridicalPersonFullName = _customer.JuridicalPersonFullName;
                _domItem.Account = _customer.Account;
                _domItem.IsPrivate = _customer.IsPrivate;
                _domItem.RoomsCount = _customer.RoomsCount;
                _domItem.Building = (DomBuilding)DataMapperService.get(typeof(DomBuilding)).find(_customer.BuildingID.ToString());
                _domItem.Floor = _customer.Floor;
                _domItem.Entrance = _customer.Entrance;
                _domItem.Apartment = _customer.Apartment;
                _domItem.Square = _customer.Square;
                _domItem.Comment = _customer.Comment;
                _domItem.LiftPresence = _customer.LiftPresence;
                _domItem.RubbishChutePresence = _customer.RubbishChutePresence;
                _domItem.BillSendingSubscription = _customer.BillSendingSubscription;
                _domItem.DebtsRepayment = _customer.DebtsRepayment;
                _domItem.User =
                    _customer.UserID.HasValue
                        ? (DomUser)DataMapperService.get(typeof(DomUser)).find(_customer.UserID.Value.ToString())
                        : null;

                var _residentIDs = _db.Residents.Where(x => x.Customers.ID == _id).Select(x => x.ID).ToList();

                foreach (int _rID in _residentIDs)
                {
                    _domItem.Residents.Add(
                        _rID.ToString(),
                        (DomResident)DataMapperService.get(typeof(DomResident)).find(_rID.ToString()));
                }

                var _poses = _db.CustomerPoses.Where(x => x.Customers.ID == _id).Select(x => x.ID).ToList();

                foreach (int _posID in _poses)
                {
                    _domItem.CustomerPoses.Add(
                        _posID.ToString(),
                        (DomCustomerPos)DataMapperService.get(typeof(DomCustomerPos)).find(_posID.ToString()));
                }

                var _counters = _db.PrivateCounters.Where(x => x.CustomerID == _id).Select(x => x.ID).ToList();

                foreach (int _cID in _counters)
                {
                    _domItem.Counters.Add(
                        _cID.ToString(),
                        (DomPrivateCounter)DataMapperService.get(typeof(DomPrivateCounter)).find(_cID.ToString()));
                }
            }

            return _domItem;
        }

        /// <summary>
        /// Преобразователь из домена в БД
        /// </summary>
        /// <param name="domObj"></param>
        /// <returns></returns>
        protected override DBItem BusinessToService(DomItem domObj)
        {
            DBItem _dbItem;

            using (Entities _entities = new Entities())
            {
                if (domObj.IsNew)
                {
                    _dbItem = new DBItem();
                    _entities.AddToCustomers(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.Customers.First(x => x.ID == _id);
                }

                _dbItem.OwnerType = (int)domObj.OwnerType;
                _dbItem.PhysicalPersonShortName = domObj.PhysicalPersonShortName;
                _dbItem.PhysicalPersonFullName = domObj.PhysicalPersonFullName;
                _dbItem.JuridicalPersonFullName = domObj.JuridicalPersonFullName;
                _dbItem.Account = domObj.Account;
                _dbItem.IsPrivate = domObj.IsPrivate;
                _dbItem.RoomsCount = domObj.RoomsCount;
                _dbItem.Apartment = domObj.Apartment;
                _dbItem.Square = domObj.Square;
                _dbItem.Comment = domObj.Comment;
                _dbItem.Floor = domObj.Floor;
                _dbItem.Entrance = domObj.Entrance;
                _dbItem.LiftPresence = domObj.LiftPresence;
                _dbItem.RubbishChutePresence = domObj.RubbishChutePresence;
                _dbItem.BillSendingSubscription = domObj.BillSendingSubscription;
                _dbItem.DebtsRepayment = domObj.DebtsRepayment;

                int _tempId = int.Parse(domObj.Building.ID);
                _dbItem.Buildings = _entities.Buildings.First(b => b.ID == _tempId);

                _dbItem.UserID =
                    domObj.User != null
                        ? int.Parse(domObj.User.ID)
                        : (int?)null;

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
                _result = null != _entities.Customers.FirstOrDefault(customer => customer.ID == _domainId);
            }

            return _result;
        }

        #endregion

        #region ICustomerDataMapper Members

        public DomItem GetItem(string accountNumber)
        {
            DomItem _customer;

            using (Entities _entities = new Entities())
            {
                DBItem _dbCustomer = _entities.Customers.FirstOrDefault(customer => customer.Account == accountNumber);

                _customer = _dbCustomer != null ? (DomItem)find(_dbCustomer.ID.ToString()) : null;
            }

            return _customer;
        }

        /// <summary>
        /// Возвращает список абонентов по части номера аккаунта
        /// </summary>
        /// <param name="accountNumberPart">Часть номера аккаунта</param>
        /// <returns>Список абонентов</returns>
        public DataTable GetListByAccount(string accountNumberPart)
        {
            return GetList(c => c.Account.Contains(accountNumberPart));
        }

        /// <summary>
        /// Возвращает список абонентов по части названия улицы и части номера дома
        /// </summary>
        /// <param name="streetNamePart">Часть название улицы</param>
        /// <param name="housePart">Часть номера дома</param>
        /// <param name="ApartmentPart">Часть номера квартиры</param>
        /// <returns>Список абонентов</returns>
        public DataTable GetList(string streetNamePart, string housePart, string ApartmentPart, bool WholeWord)
        {
            return WholeWord 
                ? GetList(c =>
                    c.Buildings.Streets.Name == streetNamePart
                    && (c.Buildings.Number == housePart || String.IsNullOrEmpty(housePart))
                    && (c.Apartment == ApartmentPart || String.IsNullOrEmpty(ApartmentPart)))
                : GetList(c =>
                    c.Buildings.Streets.Name.Contains(streetNamePart)
                    && (c.Buildings.Number.Contains(housePart) || String.IsNullOrEmpty(housePart))
                    && (c.Apartment.Contains(ApartmentPart) || String.IsNullOrEmpty(ApartmentPart)));
        }

        /// <summary>
        /// Возвращает список абонентов по части номера почтового индекса
        /// </summary>
        /// <param name="zipCodePart">Часть номера почтового индекса</param>
        /// <returns>Список абонентов</returns>
        public DataTable GetListByZipCode(string zipCodePart)
        {
            return GetList(c => c.Buildings.ZipCode.Contains(zipCodePart));
        }

        /// <summary>
        /// Возвращает список всех абонентов
        /// </summary>
        /// <returns>Список абонентов</returns>
        public DataTable GetList()
        {
            return GetList(x => true);
        }

        private DataTable GetList(Expression<Func<DBItem, bool>> whereExpr)
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID", typeof(int));
            _table.Columns.Add("Account", typeof(string));
            _table.Columns.Add("Owner", typeof(string));
            _table.Columns.Add("ResidentsNumber", typeof(int));
            _table.Columns.Add("Street", typeof(string));
            _table.Columns.Add("House", typeof(string));
            _table.Columns.Add("Apartment", typeof(string));
            _table.Columns.Add("Square", typeof(string));
            _table.Columns.Add("Selected", typeof(bool));
            _table.PrimaryKey = new[] { _table.Columns["ID"] };

            using (Entities _db = new Entities())
            {
                _db.CommandTimeout = 3600;

                var _customers = _db.Customers.Where(whereExpr)
                    .Select(c =>
                        new
                        {
                            c.ID,
                            c.Account,
                            Owner = c.OwnerType == (int)OwnerType.PhysicalPerson
                                ? c.PhysicalPersonFullName
                                : c.OwnerType == (int)OwnerType.JuridicalPerson
                                      ? c.JuridicalPersonFullName
                                      : "Неизвестен",
                            ResidentsCount = c.Residents.Count,
                            Street = c.Buildings.Streets.Name,
                            Building = c.Buildings.Number,
                            c.Apartment,
                            c.Square
                        })
                    .ToList();

                foreach (var c in _customers)
                {
                    _table.Rows.Add(
                        c.ID,
                        c.Account,
                        c.Owner,
                        c.ResidentsCount,
                        c.Street,
                        c.Building,
                        c.Apartment,
                        c.Square,
                        false);
                }
            }

            return _table;
        }

        #endregion
    }
}
