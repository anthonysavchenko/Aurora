using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.BankDetails;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.BankDetail;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook
{
    public class BankDetailDataMapper : BaseDataMapper<DomItem, DBItem>
    {
        public override object doLoad()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID");
            _table.Columns.Add("Account");
            _table.Columns.Add("Name");
            _table.Columns.Add("AccountAndBank");

            using (Entities _db = new Entities())
            {
                var _bankDetails = _db.BankDetails
                    .Select(b =>
                        new
                        {
                            b.ID,
                            b.Account,
                            b.Name
                        })
                    .ToList();

                foreach (var _bankDetail in _bankDetails)
                {
                    _table.Rows.Add(
                        _bankDetail.ID.ToString(), 
                        _bankDetail.Account, 
                        _bankDetail.Name,
                        $"{_bankDetail.Account}, {_bankDetail.Name}");
                }
            }

            return _table;
        }

        /// <summary>
        /// Преобразовывает объект прокси БД в объект домена
        /// </summary>
        /// <param name="obj">Объект домена</param>
        /// <returns>Объект домена</returns>
        protected override DomItem ServiceToBusiness(IDomainObject obj)
        {
            DomItem _domItem = (DomItem)obj;
            int _id = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                DBItem _dbItem = _entities.BankDetails.First(x => x.ID == _id);

                _domItem.Name = _dbItem.Name;
                _domItem.BIK = _dbItem.BIK;
                _domItem.KPP = _dbItem.KPP;
                _domItem.CorrAccount = _dbItem.CorrAccount;
                _domItem.Account = _dbItem.Account;
                _domItem.INN = _dbItem.INN;
            }

            return _domItem;
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
                    _entities.AddToBankDetails(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.BankDetails.First(x => x.ID == _id);
                }

                _dbItem.Name = domObj.Name;
                _dbItem.BIK = domObj.BIK;
                _dbItem.KPP = domObj.KPP;
                _dbItem.CorrAccount = domObj.CorrAccount;
                _dbItem.Account = domObj.Account;
                _dbItem.INN = domObj.INN;

                _entities.SaveChanges();
                domObj.ID = _dbItem.ID.ToString();
            }

            return _dbItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.BankDetails.FirstOrDefault(x => x.ID == _domainId);
            }

            return _result;
        }
    }
}