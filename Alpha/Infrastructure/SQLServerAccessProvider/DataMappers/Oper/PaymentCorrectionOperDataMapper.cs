using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Oper;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.PaymentCorrectionOper;
using DomPaymentOper = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.PaymentOper;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper
{
    public class PaymentCorrectionOperDataMapper : BaseDataMapper<DomItem, PaymentCorrectionOpers>, IPaymentCorrectionOperDataMapper
    {
        #region Overrides of BaseDataMapper<PaymentCorrectionOper,PaymentCorrectionOpers>

        /// <summary>
        /// Преобразовавает объект домена в объект прокси БД
        /// </summary>
        /// <param name="domObj">Объект домена</param>
        /// <returns>Объект БД</returns>
        protected override PaymentCorrectionOpers BusinessToService(DomItem domObj)
        {
            PaymentCorrectionOpers _dbItem;

            using (Entities _entities = new Entities())
            {
                if (domObj.IsNew)
                {
                    _dbItem = new PaymentCorrectionOpers();
                    _entities.AddToPaymentCorrectionOpers(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.PaymentCorrectionOpers.First(p => p.ID == _id);
                }

                _dbItem.CreationDateTime = domObj.CreationDateTime;
                _dbItem.Period = domObj.Period;
                _dbItem.Value = domObj.Value;

                int _paymentOperId = int.Parse(domObj.PaymentOper.ID);
                _dbItem.PaymentOpers = _entities.PaymentOpers.First(p => p.ID == _paymentOperId);

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
                PaymentCorrectionOpers _dbItem =
                    _entities.PaymentCorrectionOpers
                        .Include("PaymentOpers")
                        .First(x => x.ID == _id);

                _domItem.CreationDateTime = _dbItem.CreationDateTime;
                _domItem.Period = _dbItem.Period;
                _domItem.Value = _dbItem.Value;
                _domItem.PaymentOper =
                    (DomPaymentOper)DataMapperService.get(typeof(DomPaymentOper)).find(_dbItem.PaymentOpers.ID.ToString());
            }

            return _domItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.PaymentCorrectionOpers.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }

        #endregion

        #region Implementation of IPaymentCorrectionOperDataMapper

        /// <summary>
        /// Создает позиции для операции корректировки
        /// </summary>
        /// <param name="paymentOperId">ID операции корректировки</param>
        /// <returns>Список позиций</returns>
        public DomItem Create(int paymentOperId)
        {
            int _paymentCorrectionOperId;

            using (Entities _entities = new Entities())
            {
                PaymentOpers _paymentOper = _entities.PaymentOpers.Include("Customers").First(p => p.ID == paymentOperId);

                PaymentCorrectionOpers _paymentCorrectionOper =
                    new PaymentCorrectionOpers
                    {
                        CreationDateTime = ServerTimeServiceHolder.ServerTimeService.GetDateTimeInfo().Now,
                        Period = ServerTimeServiceHolder.ServerTimeService.GetPeriodInfo().FirstUncharged,
                        Value = -1 * _paymentOper.Value,
                        PaymentOpers = _paymentOper,
                    };

                _entities.AddToPaymentCorrectionOpers(_paymentCorrectionOper);

                var _poses =
                    _entities.PaymentOperPoses
                        .Include("PaymentOpers")
                        .Include("Services")
                        .Where(pos => pos.PaymentOpers.ID == paymentOperId)
                        .GroupBy(pos => pos.Services)
                        .Select(
                            g =>
                            new
                            {
                                Service = g.Key,
                                Value = g.Sum(g1 => g1.Value)
                            });

                foreach (var _pos in _poses)
                {
                    PaymentCorrectionOperPoses _newPos =
                        new PaymentCorrectionOperPoses
                        {
                            Value = -1 * _pos.Value,
                            PaymentCorrectionOpers = _paymentCorrectionOper,
                            Services = _pos.Service
                        };

                    _entities.AddToPaymentCorrectionOperPoses(_newPos);
                }

                _entities.SaveChanges();

                _paymentCorrectionOperId = _paymentCorrectionOper.ID;
            }

            return (DomItem)find(_paymentCorrectionOperId.ToString());
        }

        #endregion
    }
}