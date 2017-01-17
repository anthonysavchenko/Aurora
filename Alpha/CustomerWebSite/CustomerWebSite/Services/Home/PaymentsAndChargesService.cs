using System.Collections.Generic;
using System.Linq;
using CustomerWebSite.Models;
using CustomerWebSite.Models.PaymentsAndChargesModels;
using Taumis.Alpha.Server.Core.Models.Docs;
using Taumis.Alpha.Server.Core.Models.Enums;
using Taumis.Alpha.Server.Infrastructure.Data;

namespace CustomerWebSite.Services.Home
{
    /// <summary>
    /// Cервиса предоставления данных для страницы с платежами и начислениями
    /// </summary>
    public class PaymentsAndChargesService : IPaymentsAndChargesService
    {
        private readonly IAlphaDbContext _db;

        public PaymentsAndChargesService(IAlphaDbContext db)
        {
            _db = db;
        }

        #region Implementation of IHomeService

        /// <summary>
        /// Возвращает модель с данными для страницы с платежами и начислениями
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="customerId">ID лицевого счета</param>
        /// <param name="account">Номер лицевого счета</param>
        /// <param name="year">Год</param>
        /// <returns>Модель с данными</returns>
        public PaymentsAndChargesViewModel GetPaymentsAndChargesViewModel(int userId, int customerId, string account, int? year)
        {
            int[] _years =
                _db.RegularBillDocs
                    .Where(b => b.CustomerID == customerId)
                    .Select(o => o.Period.Year)
                    .Concat(
                        _db.PaymentOpers
                            .Where(p => p.CustomerID == customerId && p.PaymentCorrectionOper == null)
                            .Select(o => o.PaymentPeriod.Year))
                    .Distinct()
                    .OrderByDescending(o => o)
                    .ToArray();

            int _currentYear = year ?? _years.Max();

            List<PeriodTotal> _opers =
                _db.RegularBillDocs
                    .Where(b => b.CustomerID == customerId && b.Period.Year == _currentYear)
                    .Select(b =>
                        new
                        {
                            b.ID,
                            b.CreationDateTime,
                            b.Period,
                            Type = CommonOperation.OperationType.Charge,
                            Value = b.MonthChargeValue,
                            Overpayment = b.OverpaymentValue
                        })
                    .Concat(
                        _db.PaymentOpers
                            .Where(p =>
                                p.CustomerID == customerId &&
                                p.PaymentCorrectionOper == null &&
                                p.PaymentPeriod.Year == _currentYear)
                            .Select(p =>
                                new
                                {
                                    p.ID,
                                    p.CreationDateTime,
                                    Period = p.PaymentPeriod,
                                    Type = CommonOperation.OperationType.Payment,
                                    p.Value,
                                    Overpayment = (decimal)0
                                }))
                    .GroupBy(c => c.Period)
                    .Select(g =>
                        new PeriodTotal
                        {
                            Period = g.Key,
                            Opers =
                                g.Select(c =>
                                    new CommonOperation
                                    {
                                        ID = c.ID,
                                        CreationDateTime = c.CreationDateTime,
                                        Period = c.Period,
                                        Type = c.Type,
                                        Value = c.Value
                                    }),
                            Overpayment = g.Sum(c => c.Type == CommonOperation.OperationType.Charge ? c.Overpayment : 0),
                            Total = g.Sum(c => c.Value)
                        })
                    .ToList();

            

            PaymentsAndChargesViewModel _result =
                new PaymentsAndChargesViewModel
                {
                    YearsViewModel =
                        new YearsViewModel
                        {
                            CurrentYear = _currentYear,
                            Years = _years,
                            Account = account
                        },
                    PeriodTotals = _opers,
                };

            return _result;
        }

        /// <summary>
        /// Проверяет принадлежит ли лицевой счет пользователю
        /// </summary>
        /// <param name="userID">ID пользователя</param>
        /// <param name="account">Номер лицевого счета</param>
        /// <returns></returns>
        public bool CheckAccountOwner(int userID, string account)
        {
            return _db.Customers.Any(c => c.UserID == userID);
        }

        /// <summary>
        /// Возвращает один из номеров лицевых счетов пользователя
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <returns>Номер лицевого счета</returns>
        public string GetFirstAccount(int userId)
        {
            return _db.Customers.Where(c => c.UserID == userId).Select(c => c.Account).First();
        }

        #endregion
    }
}