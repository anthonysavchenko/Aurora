using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.CommonCounters;
using DomBuilding = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Building;
using DomCommonCounterValue = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.CommonCounterValue;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.CommonCounter;
using DomService = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Service;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook
{
    public class CommonCounterDataMapper : BaseDataMapper<DomItem, DBItem>
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
                _result = null != _entities.CommonCounters.FirstOrDefault(p => p.ID == _domainId);
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
                    _entities.AddToCommonCounters(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.CommonCounters.First(p => p.ID == _id);
                }

                _dbItem.Number = domObj.Number;

                int _tempId = int.Parse(domObj.Building.ID);
                _dbItem.Buildings = _entities.Buildings.First(b => b.ID == _tempId);

                _tempId = int.Parse(domObj.Service.ID);
                _dbItem.Services = _entities.Services.First(s => s.ID == _tempId);

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
                    _entities.CommonCounters
                        .Include("Buildings")
                        .Include("Services")
                        .Include("CommonCounterValues")
                        .First(x => x.ID == _id);

                _domItem.Number = _dbItem.Number;
                _domItem.Building =
                    (DomBuilding)DataMapperService.get(typeof(DomBuilding)).find(_dbItem.Buildings.ID.ToString());
                _domItem.Service =
                    (DomService)DataMapperService.get(typeof(DomService)).find(_dbItem.Services.ID.ToString());

                IDataMapper _dataMapper = DataMapperService.get(typeof(DomCommonCounterValue));

                _domItem.Values.Clear();

                foreach (CommonCounterValues _value in _dbItem.CommonCounterValues)
                {
                    DomCommonCounterValue _domValue =
                        (DomCommonCounterValue)_dataMapper.find(_value.ID.ToString());
                    _domValue.CommonCounter = _domItem;
                    _domItem.Values.Add(_domValue.ID, _domValue);
                }
            }

            return _domItem;
        }

        #endregion
    }
}
