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
            DateTime? _lastChargedPeriod = db.ChargeOpers
                .Where(x => x.Customers.ID == customerId && x.Value > 0)
                .OrderByDescending(x => x.ID)
                .Take(1)
                .Select(x => x.ChargeSets.Period)
                .FirstOrDefault();

            if (!_lastChargedPeriod.HasValue)
            {
                _lastChargedPeriod = db.RechargeOpers
                    .Where(x => x.Customers.ID == customerId && x.Value > 0)
                    .OrderByDescending(x => x.ID)
                    .Take(1)
                    .Select(x => x.RechargeSets.Period)
                    .FirstOrDefault();
            }

            return new Tuple<DateTime, Dictionary<int, Balance>>(
                _lastChargedPeriod.Value,
                db.GetCustomerBalancesGroupedByPeriod(customerId, x => x.Period == _lastChargedPeriod.Value)[_lastChargedPeriod.Value]);
        }
    }
}
