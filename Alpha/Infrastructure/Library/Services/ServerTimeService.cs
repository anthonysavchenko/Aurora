﻿using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;

namespace Taumis.Alpha.Infrastructure.Library.Services
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

            using (Entities _entities = new Entities())
            {
                _now = _entities.CreateQuery<DateTime>("CurrentDateTime()").AsEnumerable().First();
            }

            return new DateTimeInfo(_now);
        }

        /// <summary>
        /// Возвращает информацию о последних начисленном и неначисленном периоде
        /// </summary>
        public PeriodInfo GetPeriodInfo()
        {
            DateTime _lastChagedPeriod;

            using (Entities _entities = new Entities())
            {
                if (_entities.ChargeSets.Any())
                {
                    _lastChagedPeriod = _entities.ChargeSets.Max(c => c.Period);
                }
                else
                {
                    DateTime _now = _entities.CreateQuery<DateTime>("CurrentDateTime()").AsEnumerable().First();
                    _lastChagedPeriod = new DateTime(_now.Year, _now.Month, 1);
                }
            }

            return new PeriodInfo(_lastChagedPeriod);
        }

        #endregion
    }
}