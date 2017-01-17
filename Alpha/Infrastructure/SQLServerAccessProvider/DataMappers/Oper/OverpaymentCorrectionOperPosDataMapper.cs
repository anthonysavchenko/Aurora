using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.OverpaymentCorrectionOperPoses;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.OverpaymentCorrectionOperPos;
using DomOverpaymentCorrectionOper = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.OverpaymentCorrectionOper;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper
{
    public class OverpaymentCorrectionOperPosDataMapper : BaseDataMapper<DomItem, DBItem>
    {
        #region Overrides of BaseDataMapper<OverpaymentCorrectionOperPos,OverpaymentCorrectionOperPoses>

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
                _result = null != _entities.OverpaymentCorrectionOperPoses.FirstOrDefault(p => p.ID == _domainId);
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
                    _entities.AddToOverpaymentCorrectionOperPoses(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.OverpaymentCorrectionOperPoses.First(p => p.ID == _id);
                }

                _dbItem.Value = domObj.Value;

                int _tempId = int.Parse(domObj.Service.ID);
                _dbItem.Services = _entities.Services.First(x => x.ID == _tempId);

                _tempId = int.Parse(domObj.OverpaymentCorrectionOper.ID);
                _dbItem.OverpaymentCorrectionOpers = _entities.OverpaymentCorrectionOpers.First(x => x.ID == _tempId);

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
                DBItem _dbItem = _entities.OverpaymentCorrectionOperPoses.First(x => x.ID == _id);

                _domItem.Value = _dbItem.Value;
                _domItem.Service = (Service)DataMapperService.get(typeof(Service)).find(_dbItem.Services.ID.ToString());
                _domItem.OverpaymentCorrectionOper =
                    (DomOverpaymentCorrectionOper)DataMapperService.get(typeof(DomOverpaymentCorrectionOper)).find(_dbItem.OverpaymentCorrectionOpers.ID.ToString());
            }

            return _domItem;
        }

        #endregion
    }
}