using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.TotalBillDocPoses;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.TotalBillDocPos;
using DomTotalBillDoc = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.TotalBillDoc;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc
{
    public class TotalBillDocPosDataMapper : BaseDataMapper<DomItem, DBItem>
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
                    _entities.AddToTotalBillDocPoses(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.TotalBillDocPoses.First(x => x.ID == _id);
                }

                _dbItem.ServiceTypeName = domObj.ServiceTypeName;
                _dbItem.Value = domObj.Value;
                _dbItem.TotalCharged = domObj.TotalCharged;
                _dbItem.TotalPaid = domObj.TotalPaid;

                int _propId = Int32.Parse(domObj.TotalBillDoc.ID);
                _dbItem.TotalBillDocs = _entities.TotalBillDocs.First(p => p.ID == _propId);

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
                    _entities.TotalBillDocPoses.Include("TotalBillDocs")
                        .First(x => x.ID == _id);

                _domItem.ServiceTypeName = _dbItem.ServiceTypeName;
                _domItem.Value = _dbItem.Value;
                _domItem.TotalCharged = _dbItem.TotalCharged;
                _domItem.TotalPaid = _dbItem.TotalPaid;
                _domItem.TotalBillDoc = (DomTotalBillDoc)DataMapperService.get(typeof(DomTotalBillDoc)).find(_dbItem.TotalBillDocs.ID.ToString());
            }

            return _domItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.TotalBillDocPoses.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }

        #endregion
    }
}