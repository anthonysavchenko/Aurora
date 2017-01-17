using System;
using System.Linq;
using Taumis.Alpha.Server.Infrastructure.Data;

namespace CustomerWebSite.Services.Home
{
    /// <summary>
    /// Сервис авторизации действий пользователя
    /// </summary>
    public class AuthorizationService : IAuthorizationService
    {
        private const int NOT_FOUND = -1;
        private readonly IAlphaDbContext _db;

        public AuthorizationService(IAlphaDbContext db)
        {
            _db = db;
        }

        #region Implementation of IAuthorizationService

        /// <summary>
        /// Провряет принадлежит ли лицевой счет пользователю
        /// </summary>
        /// <param name="account">Номер лицевого счета</param>
        /// <param name="userId">ID пользователя</param>
        /// <param name="customerId">ID лицевого счета, out параметр</param>
        /// <returns></returns>
        public bool AuthAccount(string account, int userId, out int customerId)
        {
            var _customerList = 
                _db.Customers
                    .Where(c => c.UserID == userId && c.Account == account)
                    .Select(c => c.ID)
                    .ToList();

            customerId = _customerList.Count > 0 ? _customerList[0] : NOT_FOUND;

            return customerId != NOT_FOUND;
        }

        /// <summary>
        /// Проверяет уполномочен ли пользователь просматривать квитанцию
        /// </summary>
        /// <param name="account">Номер лицевого счета</param>
        /// <param name="year">Год</param>
        /// <param name="month">Месяц</param>
        /// <param name="userId">ID пользователя</param>
        /// <param name="billId">ID квитанции, out параметр</param>
        /// <returns></returns>
        public bool AuthRegularBill(string account, int year, int month, int userId, out int billId)
        {
            DateTime _period = new DateTime(year, month, 1);
            
            var _billList =
                _db.RegularBillDocs
                    .Where(r =>
                        r.Customer.Account == account &&
                        r.Customer.UserID == userId &&
                        r.Period == _period)
                    .Select(r => r.ID)
                    .ToList();

            billId = _billList.Count > 0 ? _billList[0] : NOT_FOUND;

            return billId != NOT_FOUND;
        }

        #endregion
    }
}