using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.BuildingCounters;
using DomBuilding = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Building;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.BuildingCounter;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook
{
    public class BuildingCounterDataMapper : BaseDataMapper<DomItem, DBItem>
    {
        #region Overrides of BaseDataMapper

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
                    _entities.AddToBuildingCounters(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.BuildingCounters.First(p => p.ID == _id);
                }

                _dbItem.CounterNumber = domObj.CounterNumber;
                _dbItem.UtilityService = (byte)domObj.UtilityService;
                _dbItem.Coefficient = domObj.Coefficient;
                _dbItem.CheckedSince = domObj.CheckedSince;
                _dbItem.CheckedTill = domObj.CheckedTill;

                int _propId = int.Parse(domObj.Building.ID);
                _dbItem.Buildings = _entities.Buildings.First(p => p.ID == _propId);

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
                    _entities.BuildingCounters
                        .Include("Buildings")
                        .First(x => x.ID == _id);

                _domItem.CounterNumber = _dbItem.CounterNumber;
                _domItem.UtilityService = (UtilityService)_dbItem.UtilityService;
                _domItem.Coefficient = _dbItem.Coefficient;
                _domItem.CheckedSince = _dbItem.CheckedSince;
                _domItem.CheckedTill = _dbItem.CheckedTill;

                _domItem.Building =
                    (DomBuilding)DataMapperService.get(typeof(DomBuilding)).find(_dbItem.Buildings.ID.ToString());
            }

            return _domItem;
        }

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
                _result = null != _entities.BuildingCounters.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }

        #endregion
    }
}
