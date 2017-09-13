using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.RechargePercentCorrections;
using DomCustomerPos = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.CustomerPos;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.RechargePercentCorrection;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook
{
    public class RechargePercentCorrectionDataMapper : BaseDataMapper<DomItem, DBItem>
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
                DBItem _dbItem = _entities.RechargePercentCorrections.First(x => x.ID == _id);

                _domItem.Period = _dbItem.Period;
                _domItem.Days = _dbItem.Days;
                _domItem.Percent = _dbItem.Percent;
                _domItem.CustomerPos = (DomCustomerPos)DataMapperService.get(typeof(DomCustomerPos)).find(_dbItem.CustomerPosID.ToString());
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
                    _entities.RechargePercentCorrections.AddObject(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.RechargePercentCorrections.First(x => x.ID == _id);
                }

                _dbItem.Period = domObj.Period;
                _dbItem.Days = domObj.Days;
                _dbItem.Percent = domObj.Percent;

                int _tempId = Convert.ToInt32(domObj.CustomerPos.ID);
                _dbItem.CustomerPoses = _entities.CustomerPoses.First(s => s.ID == _tempId);

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
                _result = null != _entities.RechargePercentCorrections.FirstOrDefault(x => x.ID == _domainId);
            }

            return _result;
        }
    }
}
