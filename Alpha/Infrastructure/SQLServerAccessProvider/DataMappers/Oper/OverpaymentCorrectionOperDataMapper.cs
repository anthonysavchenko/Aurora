using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.OverpaymentCorrectionOpers;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.OverpaymentCorrectionOper;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper
{
    public class OverpaymentCorrectionOperDataMapper : BaseDataMapper<DomItem, DBItem>
    {
        #region Overrides of BaseDataMapper<OverpaymentCorrectionOper,OverpaymentCorrectionOpers>

        /// <summary>
        /// Проверяет существование представления объекта в БД
        /// </summary>
        /// <param name="obj">Объект домена</param>
        /// <returns>true если объект найден в БД, иначе - false</returns>
        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.OverpaymentCorrectionOpers.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
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
                    _entities.AddToOverpaymentCorrectionOpers(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.OverpaymentCorrectionOpers.First(p => p.ID == _id);
                }

                _dbItem.Period = domObj.Period;
                _dbItem.Value = domObj.Value;

                int _temId = Int32.Parse(domObj.ChargeOper.ID);
                _dbItem.ChargeOpers = _entities.ChargeOpers.First(p => p.ID == _temId);

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
                DBItem _dbItem = _entities.OverpaymentCorrectionOpers.First(x => x.ID == _id);

                _domItem.Period = _dbItem.Period;
                _domItem.Value = _dbItem.Value;

                _domItem.ChargeOper = (ChargeOper)DataMapperService.get(typeof(ChargeOper)).find(_dbItem.ChargeOpers.ID.ToString());
            }

            return _domItem;
        }

        #endregion
    }
}