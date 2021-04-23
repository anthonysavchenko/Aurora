using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.Buildings;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Building;
using DomCounter = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.BuildingCounter;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook
{
    public class BuildingDataMapper : BaseDataMapper<DomItem, DBItem>, IDataMapper
    {
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
                    _entities.AddToBuildings(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.Buildings.First(x => x.ID == _id);
                }

                _dbItem.Street = domObj.Street;
                _dbItem.Number = domObj.Number;
                _dbItem.BuildingContract = (byte)domObj.BuildingContract;
                _dbItem.NormCoefficient = domObj.NormCoefficient;
                _dbItem.CollectiveSquare = domObj.CollectiveSquare;
                _dbItem.IsArchived = domObj.IsArchived;
                _dbItem.Note = domObj.Note;

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
            int _id = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                DBItem _dbItem =
                    _entities.Buildings
                        .Include("BuildingCounters")
                        .First(x => x.ID == _id);

                _domItem.Number = _dbItem.Number;
                _domItem.Street = _dbItem.Street;
                _domItem.BuildingContract = (BuildingContract)_dbItem.BuildingContract;
                _domItem.NormCoefficient = _dbItem.NormCoefficient;
                _domItem.CollectiveSquare = _dbItem.CollectiveSquare;
                _domItem.IsArchived = _dbItem.IsArchived;
                _domItem.Note = _dbItem.Note;

                IDataMapper counterDataMapper = DataMapperService.get(typeof(DomCounter));
                _domItem.Counters.Clear();

                foreach (var counter in _dbItem.BuildingCounters)
                {
                    DomCounter domCounter = (DomCounter)counterDataMapper.find(counter.ID.ToString());
                    domCounter.Building = _domItem;
                    _domItem.Counters.Add(domCounter.ID, domCounter);
                }
            }

            return _domItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.Buildings.FirstOrDefault(x => x.ID == _domainId);
            }

            return _result;
        }
    }
}
