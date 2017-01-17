using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.OverpaymentOpers;
using DomCustomer = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.Customer;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.OverpaymentOper;
using DomOverpaymentCorrectionOper = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.OverpaymentCorrectionOper;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper
{
    public class OverpaymentOperDataMapper : BaseDataMapper<DomItem, DBItem>
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
                    _entities.AddToOverpaymentOpers(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.OverpaymentOpers.First(p => p.ID == _id);
                }

                _dbItem.CreationDateTime = domObj.CreationDateTime;
                _dbItem.PaymentPeriod = domObj.PaymentPeriod;
                _dbItem.Value = domObj.Value;

                int _propId = int.Parse(domObj.Customer.ID);
                _dbItem.Customers = _entities.Customers.First(c => c.ID == _propId);

                _propId = int.Parse(domObj.OverpaymentCorrectionOper.ID);
                _dbItem.OverpaymentCorrectionOpers = _entities.OverpaymentCorrectionOpers.First(x => x.ID == _propId);

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
                DBItem _dbItem = _entities.OverpaymentOpers.First(x => x.ID == _id);

                _domItem.CreationDateTime = _dbItem.CreationDateTime;
                _domItem.PaymentPeriod = _dbItem.PaymentPeriod;
                _domItem.Value = _dbItem.Value;
                _domItem.Customer = (DomCustomer)DataMapperService.get(typeof(DomCustomer)).find(_dbItem.Customers.ID.ToString());
                _domItem.OverpaymentCorrectionOper =
                    (DomOverpaymentCorrectionOper)DataMapperService.get(
                        typeof(DomOverpaymentCorrectionOper)).find(_dbItem.OverpaymentCorrectionOpers.ID.ToString());
            }

            return _domItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.OverpaymentOpers.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }

        #endregion
    }
}