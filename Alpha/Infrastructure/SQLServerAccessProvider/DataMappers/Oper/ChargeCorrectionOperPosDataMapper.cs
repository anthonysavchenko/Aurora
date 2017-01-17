using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.ChargeCorrectionOperPoses;
using DomChargeCorrectionOper = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.ChargeCorrectionOper;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.ChargeCorrectionOperPos;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper
{
    public class ChargeCorrectionOperPosDataMapper : BaseDataMapper<DomItem, DBItem>
    {
        #region Overrides of BaseDataMapper<ChargeOperPos,ChargeOperPoses>

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
                _result = null != _entities.ChargeCorrectionOperPoses.FirstOrDefault(p => p.ID == _domainId);
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
                    _entities.AddToChargeCorrectionOperPoses(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.ChargeCorrectionOperPoses.First(p => p.ID == _id);
                }

                _dbItem.Value = domObj.Value;

                int _tempId = int.Parse(domObj.Service.ID);
                _dbItem.Services = _entities.Services.First(x => x.ID == _tempId);

                _tempId = int.Parse(domObj.Contractor.ID);
                _dbItem.Contractors = _entities.Contractors.First(x => x.ID == _tempId);

                _tempId = int.Parse(domObj.ChargeCorrectionOper.ID);
                _dbItem.ChargeCorrectionOpers = _entities.ChargeCorrectionOpers.First(x => x.ID == _tempId);

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
                    _entities.ChargeCorrectionOperPoses
                        .Include("Services")
                        .Include("Contractors")
                        .Include("ChargeCorrectionOpers")
                        .First(x => x.ID == _id);

                _domItem.Value = _dbItem.Value;
                _domItem.Service = (Service)DataMapperService.get(typeof(Service)).find(_dbItem.Services.ID.ToString());
                _domItem.Contractor =
                    (Contractor)DataMapperService.get(typeof(Contractor)).find(_dbItem.Contractors.ID.ToString());
                _domItem.ChargeCorrectionOper =
                    (DomChargeCorrectionOper)DataMapperService.get(typeof(DomChargeCorrectionOper)).find(_dbItem.ChargeCorrectionOpers.ID.ToString());
            }

            return _domItem;
        }

        #endregion
    }
}