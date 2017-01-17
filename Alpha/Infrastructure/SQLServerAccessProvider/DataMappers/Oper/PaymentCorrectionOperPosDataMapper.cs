using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.PaymentCorrectionOperPos;
using DomPaymentCorrectionOper = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.PaymentCorrectionOper;
using DomService = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Service;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper
{
    public class PaymentCorrectionOperPosDataMapper : BaseDataMapper<DomItem, PaymentCorrectionOperPoses>
    {
        #region Overrides of BaseDataMapper<PaymentCorrectionOperPos,PaymentCorrectionOperPoses>

        /// <summary>
        /// Преобразовавает объект домена в объект прокси БД
        /// </summary>
        /// <param name="domObj">Объект домена</param>
        /// <returns>Объект БД</returns>
        protected override PaymentCorrectionOperPoses BusinessToService(DomItem domObj)
        {
            PaymentCorrectionOperPoses _dbItem;

            using (Entities _entities = new Entities())
            {
                if (domObj.IsNew)
                {
                    _dbItem = new PaymentCorrectionOperPoses();
                    _entities.AddToPaymentCorrectionOperPoses(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.PaymentCorrectionOperPoses.First(p => p.ID == _id);
                }

                _dbItem.Value = domObj.Value;

                int _serviceId = int.Parse(domObj.Service.ID);
                _dbItem.Services = _entities.Services.First(s => s.ID == _serviceId);

                int _paymentCorrectionOperId = int.Parse(domObj.PaymentCorrectionOper.ID);
                _dbItem.PaymentCorrectionOpers =
                    _entities.PaymentCorrectionOpers.First(p => p.ID == _paymentCorrectionOperId);

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
                PaymentCorrectionOperPoses _dbItem =
                    _entities.PaymentCorrectionOperPoses
                        .Include("PaymentOpers")
                        .First(x => x.ID == _id);

                _domItem.Value = _dbItem.Value;
                _domItem.Service =
                    (DomService)DataMapperService.get(typeof(DomService)).find(_dbItem.Services.ID.ToString());
                _domItem.PaymentCorrectionOper =
                    (DomPaymentCorrectionOper)DataMapperService.get(typeof(DomPaymentCorrectionOper)).find(_dbItem.PaymentCorrectionOpers.ID.ToString());
            }

            return _domItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.PaymentCorrectionOperPoses.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }

        #endregion
    }
}