using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.Customers;
using DomBuilding = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Building;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.Customer;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc
{
    /// <summary>
    /// Преобразователь данных для лицевого счета
    /// </summary>
    public class CustomerDataMapper : BaseDataMapper<DomItem, DBItem>, IDataMapper
    {
        #region Overrides of BaseDataMapper

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

                _dbItem.Apartment = domObj.Apartment;

                int _propId = int.Parse(domObj.Building.ID);
                _dbItem.Buildings = _entities.Buildings.First(b => b.ID == _propId);

                _entities.SaveChanges();
                domObj.ID = _dbItem.ID.ToString();
            }

            return _dbItem;
        }

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
                        .First(x => x.ID == _id);

                _domItem.Apartment = _customer.Apartment;

                _domItem.Building = (DomBuilding)DataMapperService.get(typeof(DomBuilding)).find(_customer.Buildings.ID.ToString());
            }

            return _domItem;
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

    }
}
