using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.RegularBillDocSharedCounterPoses;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.RegularBillDocSharedCounterPos;
using DomPaymentBill = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.RegularBillDoc;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc
{
    public class RegularBillDocSharedCounterPosDataMapper : BaseDataMapper<DomItem, DBItem>
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
                    _entities.AddToRegularBillDocSharedCounterPoses(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.RegularBillDocSharedCounterPoses.First(x => x.ID == _id);
                }

                _dbItem.SharedCounterValue = domObj.SharedCounterValue;
                _dbItem.SharedCharge = domObj.SharedCharge;

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
        protected override DomItem ServiceToBusiness(IDomainObject obj)
        {
            DomItem _domItem = (DomItem)obj;
            int _id = int.Parse(_domItem.ID);

            using (Entities _entities = new Entities())
            {
                DBItem _dbItem =
                    _entities.RegularBillDocSharedCounterPoses
                        .Include("RegularBillDocs")
                        .First(x => x.ID == _id);

                _domItem.SharedCounterValue = _dbItem.SharedCounterValue;
                _domItem.SharedCharge = _dbItem.SharedCharge;
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
                _result = null != _entities.RegularBillDocSharedCounterPoses.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }

        #endregion
    }
}