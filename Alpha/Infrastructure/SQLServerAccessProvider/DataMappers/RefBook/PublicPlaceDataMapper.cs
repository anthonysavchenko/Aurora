using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.PublicPlaces;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.PublicPlace;
using DomBuilding = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Building;
using DomService = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Service;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook
{
    public class PublicPlaceDataMapper: BaseDataMapper<DomItem, DBItem>
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
                DBItem _dbItem = _entities.PublicPlaces.First(x => x.ID == _id);

                _domItem.Area = _dbItem.Area;
                _domItem.Building = (DomBuilding)DataMapperService.get(typeof(DomBuilding)).find(_dbItem.BuildingID.ToString());
                _domItem.Service = (DomService)DataMapperService.get(typeof(DomService)).find(_dbItem.ServiceID.ToString());
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
                    _entities.PublicPlaces.AddObject(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.PublicPlaces.First(x => x.ID == _id);
                }

                _dbItem.Area = domObj.Area;

                int _tempId = Convert.ToInt32(domObj.Building.ID);
                _dbItem.Buildings = _entities.Buildings.First(s => s.ID == _tempId);

                _tempId = Convert.ToInt32(domObj.Service.ID);
                _dbItem.Services = _entities.Services.First(s => s.ID == _tempId);

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
                _result = null != _entities.PublicPlaces.FirstOrDefault(x => x.ID == _domainId);
            }

            return _result;
        }
    }
}