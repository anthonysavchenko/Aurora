using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.FinePoses;
using DomCustomer = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.Customer;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.FinePos;
using DomDoc = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.FineDoc;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc
{
    public class FinePosDataMapper : BaseDataMapper<DomItem, DBItem>
    {
        #region Overrides of BaseDataMapper

        /// <summary>
        /// Преобразователь из БД в Домен
        /// с созданием нового объекта Домена.
        /// </summary>
        /// <param name="_obj"></param>
        /// <returns></returns>
        protected override DomItem ServiceToBusiness(IDomainObject _obj)
        {
            DomItem _domItem = (DomItem)_obj;
            int _id = int.Parse(_domItem.ID);
            using (Entities _db = new Entities())
            {
                var _dbItem = _db.FinePoses.Where(x => x.ID == _id).First();

                _domItem.Value = _dbItem.Value;
                _domItem.Customer = (DomCustomer)DataMapperService.get(typeof(DomCustomer)).find(_dbItem.CustomerID.ToString());
                _domItem.Doc = (DomDoc)DataMapperService.get(typeof(DomDoc)).find(_dbItem.DocID.ToString());
            }

            return _domItem;
        }

        /// <summary>
        /// Преобразователь из домена в БД
        /// </summary>
        /// <param name="domObj"></param>
        /// <returns></returns>
        protected override DBItem BusinessToService(DomItem domObj)
        {
            DBItem _dbItem;

            using (Entities _db = new Entities())
            {
                if (domObj.IsNew)
                {
                    _dbItem = new DBItem();
                    _db.AddToFinePoses(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _db.FinePoses.First(x => x.ID == _id);
                }

                _dbItem.Value = domObj.Value;

                _dbItem.CustomerID = int.Parse(domObj.Customer.ID);
                _dbItem.DocID = int.Parse(domObj.Doc.ID);

                _db.SaveChanges();
                domObj.ID = _dbItem.ID.ToString();
            }

            return _dbItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _id = int.Parse(obj.ID);

            using (Entities _db = new Entities())
            {
                _result = null != _db.FinePoses.FirstOrDefault(x => x.ID == _id);
            }

            return _result;
        }

        #endregion
    }
}
