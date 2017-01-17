using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.OverpaymentOperPoses;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.OverpaymentOperPos;
using DomOverpaymentOper = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.OverpaymentOper;
using DomService = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Service;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper
{
    public class OverpaymentOperPosDataMapper : BaseDataMapper<DomItem, DBItem>
    {
        #region Overrides of BaseDataMapper<PaymentOperPos,PaymentOperPoses>

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
                    _entities.AddToOverpaymentOperPoses(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.OverpaymentOperPoses.First(p => p.ID == _id);
                }

                _dbItem.Period = domObj.Period;
                _dbItem.Value = domObj.Value;

                int _propId = Int32.Parse(domObj.Service.ID);
                _dbItem.Services = _entities.Services.First(s => s.ID == _propId);

                _propId = Int32.Parse(domObj.PaymentOper.ID);
                _dbItem.OverpaymentOpers = _entities.OverpaymentOpers.First(p => p.ID == _propId);

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
                    _entities.OverpaymentOperPoses
                        .Include("Services")
                        .Include("OverpaymentOpers")
                        .First(x => x.ID == _id);

                _domItem.Period = _dbItem.Period;
                _domItem.Value = _dbItem.Value;
                _domItem.Service = (DomService)DataMapperService.get(typeof(DomService)).find(_dbItem.Services.ID.ToString());
                _domItem.PaymentOper = (DomOverpaymentOper)DataMapperService.get(typeof(DomOverpaymentOper)).find(_dbItem.OverpaymentOpers.ID.ToString());
            }

            return _domItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.OverpaymentOperPoses.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }

        #endregion
    }
}