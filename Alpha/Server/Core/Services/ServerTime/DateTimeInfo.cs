using System;

namespace Taumis.Alpha.Server.Core.Services.ServerTime
{
    public class DateTimeInfo
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="now">Текущая дата</param>
        public DateTimeInfo(DateTime now)
        {
            Now = now;
            SinceMonthBeginning = new DateTime(now.Year, now.Month, 1);
            TillToday = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
        }

        /// <summary>
        /// Текущая дата
        /// </summary>
        public DateTime Now { get; private set; }

        /// <summary>
        /// Дата начала текущего месяца в формате "yyyy-MM-dd 00:00:00"
        /// </summary>
        public DateTime SinceMonthBeginning { get; private set; }

        /// <summary>
        /// Текущая дата в формате "yyyy-MM-dd 23:59:59"
        /// </summary>
        public DateTime TillToday { get; private set; }
    }
}