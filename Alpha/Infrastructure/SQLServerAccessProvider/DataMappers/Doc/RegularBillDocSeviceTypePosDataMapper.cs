using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.RegularBillDocSeviceTypePoses;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.RegularBillDocSeviceTypePos;
using DomPaymentBill = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.RegularBillDoc;
using DomServiceType = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.ServiceType;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc
{
    public class RegularBillDocSeviceTypePosDataMapper : BaseDataMapper<DomItem, DBItem>
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
                    _entities.AddToRegularBillDocSeviceTypePoses(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.RegularBillDocSeviceTypePoses.First(x => x.ID == _id);
                }

                _dbItem.ServiceTypeName = domObj.ServiceTypeName;
                _dbItem.PayRate = domObj.PayRate;
                _dbItem.Charge = domObj.Charge;
                _dbItem.Benefit = domObj.Benefit;
                _dbItem.Recalculation = domObj.Recalculation;
                _dbItem.Payable = domObj.Payable;

                int _propId = Int32.Parse(domObj.RegularBillDoc.ID);
                _dbItem.RegularBillDocs = _entities.RegularBillDocs.First(p => p.ID == _propId);

                _propId = int.Parse(domObj.ServiceType.ID);
                _dbItem.ServiceTypes = _entities.ServiceTypes.First(st => st.ID == _propId);

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
                    _entities.RegularBillDocSeviceTypePoses.Include("RegularBillDocs")
                        .First(x => x.ID == _id);

                _domItem.ServiceTypeName = _dbItem.ServiceTypeName;
                _domItem.PayRate = _dbItem.PayRate;
                _domItem.Charge = _dbItem.Charge;
                _domItem.Benefit = _dbItem.Benefit;
                _domItem.Recalculation = _dbItem.Recalculation;
                _domItem.Payable = _dbItem.Payable;
                _domItem.RegularBillDoc = (DomPaymentBill)DataMapperService.get(typeof(DomPaymentBill)).find(_dbItem.RegularBillDocs.ID.ToString());
                _domItem.ServiceType = (DomServiceType)DataMapperService.get(typeof(DomServiceType)).find(_dbItem.ServiceTypeID.Value.ToString());
            }

            return _domItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.RegularBillDocSeviceTypePoses.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }

        #endregion
    }
}