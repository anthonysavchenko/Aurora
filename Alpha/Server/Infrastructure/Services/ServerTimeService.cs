using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Taumis.Alpha.Server.Core.Services.ServerTime;
using Taumis.Alpha.Server.Infrastructure.Data;

namespace Taumis.Alpha.Server.Infrastructure.Services
{
    public class ServerTimeService : IServerTimeService
    {
        #region Implementation of IServerTimeService

        /// <summary>
        /// Возвращает информацию о текущем периоде, дате-времени
        /// </summary>
        public DateTimeInfo GetDateTimeInfo()
        {
            DateTime _now;

            using (AlphaDbContext _dbContext = new AlphaDbContext())
            {
                _now = ((IObjectContextAdapter)_dbContext).ObjectContext.CreateQuery<DateTime>("CurrentDateTime()").AsEnumerable().First();
            }

            return new DateTimeInfo(_now);
        }

        /// <summary>
        /// Возвращает информацию о последних начисленном и неначисленном периоде
        /// </summary>
        public PeriodInfo GetPeriodInfo()
        {
            DateTime _lastChagedPeriod;

            using (AlphaDbContext _dbContext = new AlphaDbContext())
            {
                if (_dbContext.ChargeSets.Any())
                {
                    _lastChagedPeriod = _dbContext.ChargeSets.Max(c => c.Period);
                }
                else
                {
                    DateTime _now = ((IObjectContextAdapter)_dbContext).ObjectContext.CreateQuery<DateTime>("CurrentDateTime()").AsEnumerable().First().AddMonths(-1);
                    _lastChagedPeriod = new DateTime(_now.Year, _now.Month, 1);
                }
            }

            return new PeriodInfo(_lastChagedPeriod);
        }

        #endregion
    }
}