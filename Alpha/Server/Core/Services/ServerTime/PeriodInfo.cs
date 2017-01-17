using System;

namespace Taumis.Alpha.Server.Core.Services.ServerTime
{
    public class PeriodInfo
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="lastChargedPeriod">Последний период с начислениями</param>
        public PeriodInfo(DateTime lastChargedPeriod)
        {
            LastCharged = lastChargedPeriod;
            FirstUncharged = lastChargedPeriod.AddMonths(1);
        }

        /// <summary>
        /// Последний период с начислениями
        /// </summary>
        public DateTime LastCharged { get; private set; }

        /// <summary>
        /// Последний период без начислений
        /// </summary>
        public DateTime FirstUncharged { get; private set; }
    }
}