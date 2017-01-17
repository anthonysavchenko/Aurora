using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Oper;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.PaymentOperPoses;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.PaymentOperPos;
using DomPaymentOper = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.PaymentOper;
using DomService = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Service;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper
{
    public class PaymentOperPosDataMapper : BaseDataMapper<DomItem, DBItem>, IPaymentOperPosDataMapper
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
                    _entities.AddToPaymentOperPoses(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.PaymentOperPoses.First(p => p.ID == _id);
                }

                _dbItem.Period = domObj.Period;
                _dbItem.Value = domObj.Value;

                int _propId = Int32.Parse(domObj.Service.ID);
                _dbItem.Services = _entities.Services.First(s => s.ID == _propId);

                _propId = Int32.Parse(domObj.PaymentOper.ID);
                _dbItem.PaymentOpers = _entities.PaymentOpers.First(p => p.ID == _propId);

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
                    _entities.PaymentOperPoses
                        .Include("Services")
                        .Include("PaymentOpers")
                        .First(x => x.ID == _id);

                _domItem.Period = _dbItem.Period;
                _domItem.Value = _dbItem.Value;
                _domItem.Service = (DomService)DataMapperService.get(typeof(DomService)).find(_dbItem.Services.ID.ToString());
                _domItem.PaymentOper = (DomPaymentOper)DataMapperService.get(typeof(DomPaymentOper)).find(_dbItem.PaymentOpers.ID.ToString());
            }

            return _domItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.PaymentOperPoses.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }

        #endregion

        public DataTable GetList(string paymentOperId)
        {
            DataTable _table = new DataTable("PaymentOperPoses");
            _table.Columns.Add("ID", typeof(int));
            _table.Columns.Add("Period", typeof(DateTime));
            _table.Columns.Add("Value", typeof(Decimal));
            _table.Columns.Add("ServiceName", typeof(string));
            _table.Columns.Add("ServiceTypeName", typeof(string));

            DataSet _ds =
                new DataSet
                {
                    EnforceConstraints = false
                };
            _ds.Tables.Add(_table);

            int _id;
            if (int.TryParse(paymentOperId, out _id))
            {
                using (Entities _entities = new Entities())
                {
                    var _data =
                        _entities.PaymentOperPoses
                            .Include("Services")
                            .Include("Services.ServiceTypes")
                            .Where(pos => pos.PaymentOpers.ID == _id);

                    foreach (PaymentOperPoses _pos in _data)
                    {
                        _table.Rows.Add(
                            _pos.ID,
                            _pos.Period,
                            Math.Abs(_pos.Value),
                            _pos.Services != null ? _pos.Services.Name : String.Empty,
                            _pos.Services != null ? _pos.Services.ServiceTypes.Name : String.Empty);
                    }
                }
            }

            return _table;
        }
    }
}