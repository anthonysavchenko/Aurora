using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.PrivateCounters;
using DomCustomer = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.Customer;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.PrivateCounter;
using DomPrivateCounterValue = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.PrivateCounterValue;
using DomService = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Service;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook
{
    public class PrivateCounterDataMapper : BaseDataMapper<DomItem, DBItem>
    {
        #region Overrides of BaseDataMapper

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
                _result = null != _entities.PrivateCounters.FirstOrDefault(p => p.ID == _domainId);
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
                    _entities.AddToPrivateCounters(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.PrivateCounters.First(p => p.ID == _id);
                }

                _dbItem.Number = domObj.Number;
                _dbItem.CustomerID = int.Parse(domObj.Customer.ID);
                _dbItem.ServiceID = int.Parse(domObj.Service.ID);

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
                    _entities.PrivateCounters
                        .Include("PrivateCounterValues")
                        .Include("CustomerPoses")
                        .First(x => x.ID == _id);

                _domItem.Number = _dbItem.Number;
                _domItem.Customer = (DomCustomer)DataMapperService.get(typeof(DomCustomer)).find(_dbItem.CustomerID.ToString());
                _domItem.Service = (DomService)DataMapperService.get(typeof(DomService)).find(_dbItem.ServiceID.ToString());

                IDataMapper _dataMapper = DataMapperService.get(typeof(DomPrivateCounterValue));

                _domItem.Values.Clear();

                foreach (PrivateCounterValues _value in _dbItem.PrivateCounterValues)
                {
                    DomPrivateCounterValue _domValue =
                        (DomPrivateCounterValue)_dataMapper.find(_value.ID.ToString());
                    _domValue.PrivateCounter = _domItem;
                    _domItem.Values.Add(_domValue.ID, _domValue);
                }
            }

            return _domItem;
        }

        #endregion
    }
}