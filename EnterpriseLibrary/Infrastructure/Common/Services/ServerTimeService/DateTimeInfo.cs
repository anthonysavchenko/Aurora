using System;

namespace Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService
{
    public class DateTimeInfo
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="now">Текущая дата</param>
        public DateTimeInfo(DateTime now) => Now = now;

        /// <summary>
        /// Текущая дата
        /// </summary>
        public DateTime Now { get; private set; }

        /// <summary>
        /// Дата начала текущего месяца в формате "yyyy-MM-01 00:00:00"
        /// </summary>
        public DateTime SinceMonthBeginning => new DateTime(Now.Year, Now.Month, 1);

        /// <summary>
        /// Дата начала текущего года в формате "yyyy-01-01 00:00:00"
        /// </summary>
        public DateTime SinceYearBeginning => new DateTime(Now.Year, 1, 1);

        /// <summary>
        /// Текущая дата в формате "yyyy-MM-dd 23:59:59"
        /// </summary>
        public DateTime TillToday => new DateTime(Now.Year, Now.Month, Now.Day, 23, 59, 59);
    }
}