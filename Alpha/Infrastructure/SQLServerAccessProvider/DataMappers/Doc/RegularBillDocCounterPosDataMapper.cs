using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.RegularBillDocCounterPoses;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.RegularBillDocCounterPos;
using DomPaymentBill = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.RegularBillDoc;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc
{
    public class RegularBillDocCounterPosDataMapper : BaseDataMapper<DomItem, DBItem>
    {
        #region Overrides of BaseDataMapper<PaymentOper,PaymentOpers>

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
                    _entities.AddToRegularBillDocCounterPoses(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.RegularBillDocCounterPoses.First(x => x.ID == _id);

                }

                _dbItem.Number = domObj.Number;
                _dbItem.PrevValue = domObj.PrevValue;
                _dbItem.CurValue = domObj.CurValue;
                _dbItem.Consumption = domObj.Consumption;
                _dbItem.Rate = domObj.Rate;
                _dbItem.Measure = domObj.Measure;
                _dbItem.ServiceName = domObj.ServiceName;

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
                    _entities.RegularBillDocCounterPoses.Include("RegularBillDocs")
                        .First(x => x.ID == _id);

                _domItem.Number = _dbItem.Number;
                _domItem.PrevValue = _dbItem.PrevValue;
                _domItem.CurValue = _dbItem.CurValue;
                _domItem.Consumption = _dbItem.Consumption;
                _domItem.Rate = _dbItem.Rate;
                _domItem.Measure = _dbItem.Measure;
                _domItem.ServiceName = _dbItem.ServiceName;
                _domItem.RegularBillDoc = (DomPaymentBill)DataMapperService.get(typeof(DomPaymentBill)).find(_dbItem.RegularBillDocs.ID.ToString());
            }

            return _domItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.RegularBillDocCounterPoses.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }

        #endregion
    }
}