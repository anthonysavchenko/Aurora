using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.RouteFormValues;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.RouteFormValue;
using DomPrivateCounter = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.PrivateCounter;
using DomRouteFormPos = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.RouteFormPos;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook
{
    public class RouteFormValueDataMapper : BaseDataMapper<DomItem, DBItem>
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
                    _entities.AddToRouteFormValues(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.RouteFormValues.First(p => p.ID == _id);
                }

                _dbItem.Month = domObj.Month;
                _dbItem.ValueType = (byte)domObj.ValueType;
                _dbItem.Value = domObj.Value;

                int _propId = int.Parse(domObj.PrivateCounter.ID);
                _dbItem.PrivateCounters = _entities.PrivateCounters.First(c => c.ID == _propId);

                _propId = int.Parse(domObj.RouteFormPos.ID);
                _dbItem.RouteFormPoses = _entities.RouteFormPoses.First(c => c.ID == _propId);

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
                    _entities.RouteFormValues
                        .Include("PrivateCounters")
                        .Include("RouteFormPoses")
                        .First(x => x.ID == _id);

                _domItem.Month = _dbItem.Month;
                _domItem.ValueType = (PrivateCounterValueType)_dbItem.ValueType;
                _domItem.Value = _dbItem.Value;

                _domItem.PrivateCounter =
                    (DomPrivateCounter)DataMapperService.get(typeof(DomPrivateCounter))
                        .find(_dbItem.PrivateCounters.ID.ToString());

                _domItem.RouteFormPos =
                    (DomRouteFormPos)DataMapperService.get(typeof(DomRouteFormPos))
                        .find(_dbItem.RouteFormPoses.ID.ToString());
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
                _result = null != _entities.RouteFormValues.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }

        #endregion
    }
}
