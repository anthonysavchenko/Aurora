using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.RegularBillDocPublicPlacePoses;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.RegularBillDocPublicPlacePos;


namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc
{
    public class RegularBillDocPublicPlacePosDataMapper : BaseDataMapper<DomItem, DBItem>
    {
        #region Overrides of BaseDataMapper<DomItem, DBItem>

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
                    _entities.RegularBillDocPublicPlacePoses.AddObject(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.RegularBillDocPublicPlacePoses.First(x => x.ID == _id);
                }

                _dbItem.Service = domObj.Service;
                _dbItem.Norm = domObj.Norm;
                _dbItem.NormMeasure = domObj.NormMeasure;
                _dbItem.Rate = domObj.Rate;
                _dbItem.Area = domObj.Area;
                _dbItem.ServiceVolume = domObj.ServiceVolume;
                _dbItem.Total = domObj.Total;

                int _propId = int.Parse(domObj.RegularBillDoc.ID);
                _dbItem.RegularBillDocs = _entities.RegularBillDocs.First(p => p.ID == _propId);

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
        protected override DomItem ServiceToBusiness(IDomainObject _obj)
        {
            DomItem _domItem = (DomItem)_obj;
            int _id = int.Parse(_domItem.ID);
            using (Entities _entities = new Entities())
            {
                DBItem _dbItem =
                    _entities.RegularBillDocPublicPlacePoses.Include("RegularBillDocs")
                        .First(x => x.ID == _id);


                _domItem.Service = _dbItem.Service;
                _domItem.Norm = _dbItem.Norm;
                _domItem.NormMeasure = _dbItem.NormMeasure;
                _domItem.Rate = _dbItem.Rate;
                _domItem.Area = _dbItem.Area;
                _domItem.ServiceVolume = _dbItem.ServiceVolume;
                _domItem.Total = _dbItem.Total;
                _domItem.RegularBillDoc = (RegularBillDoc)DataMapperService.get(typeof(RegularBillDoc)).find(_dbItem.RegularBillDocs.ID.ToString());
            }

            return _domItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.RegularBillDocPublicPlacePoses.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }

        #endregion
    }
}
