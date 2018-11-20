using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Common;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.Queries
{
    public static class GetLastChargedPeriodBalanceQuery
    {
        public static Tuple<DateTime, Dictionary<int, Balance>> GetLastChargedPeriodBalance(this Entities db, int customerId)
        {
            DateTime _lastChargedPeriod = db.ChargeOpers
                .Where(x => x.Customers.ID == customerId && x.Value > 0)
                .OrderByDescending(x => x.ID)
                .Take(1)
                .Select(x => x.ChargeSets.Period)
                .FirstOrDefault();

            if (_lastChargedPeriod == DateTime.MinValue)
            {
                _lastChargedPeriod = db.RechargeOpers
                    .Where(x => x.Customers.ID == customerId && x.Value > 0)
                    .OrderByDescending(x => x.ID)
                    .Take(1)
                    .Select(x => x.RechargeSets.Period)
                    .FirstOrDefault();

                if (_lastChargedPeriod == DateTime.MinValue)
                {
                    throw new ApplicationException($"Отсутствуют начисления у абонента ID = {customerId}");
                }
            }

            return new Tuple<DateTime, Dictionary<int, Balance>>(
                _lastChargedPeriod,
                db.GetCustomerBalancesGroupedByPeriod(customerId, beforeGroupFilter: x => x.Period == _lastChargedPeriod)[_lastChargedPeriod]);
        }
    }
}
