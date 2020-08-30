using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.BuildingCounterValues;
using DomCounter = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.BuildingCounter;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.BuildingCounterValue;
using DomPos = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.BuildingValuesUploadPos;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook
{
    public class BuildingCounterValueDataMapper : BaseDataMapper<DomItem, DBItem>, IDataMapper
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
                    _entities.AddToBuildingCounterValues(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.BuildingCounterValues.First(p => p.ID == _id);
                }

                _dbItem.Month = domObj.Month;
                _dbItem.PrevValue = domObj.PrevValue;
                _dbItem.CurrentValue = domObj.CurrentValue;
                _dbItem.CurrentDate = domObj.CurrentDate;

                int _propId = int.Parse(domObj.BuildingCounter.ID);
                _dbItem.BuildingCounters = _entities.BuildingCounters.First(c => c.ID == _propId);

                _propId = int.Parse(domObj.BuildingValuesUploadPos.ID);
                _dbItem.BuildingValuesUploadPoses = _entities.BuildingValuesUploadPoses.First(c => c.ID == _propId);

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
                    _entities.BuildingCounterValues
                        .Include("BuildingCounters")
                        .Include("BuildingValuesUploadPoses")
                        .First(x => x.ID == _id);

                _domItem.Month = _dbItem.Month;
                _domItem.PrevValue = _dbItem.PrevValue;
                _domItem.CurrentValue = _dbItem.CurrentValue;
                _domItem.CurrentDate = _dbItem.CurrentDate;

                _domItem.BuildingCounter =
                    (DomCounter)DataMapperService.get(typeof(DomCounter))
                        .find(_dbItem.BuildingCounters.ID.ToString());

                _domItem.BuildingValuesUploadPos =
                    (DomPos)DataMapperService.get(typeof(DomPos))
                        .find(_dbItem.BuildingValuesUploadPoses.ID.ToString());
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
                _result = null != _entities.BuildingCounterValues.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }

        #endregion

    }
}
