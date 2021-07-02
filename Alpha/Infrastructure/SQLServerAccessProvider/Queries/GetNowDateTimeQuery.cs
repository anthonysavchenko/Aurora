using System;
using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.Queries
{
    public static class GetNowDateTimeQuery
    {
        public static DateTime GetNowDateTime(this Entities db)
        {
            return db.CreateQuery<DateTime>("CurrentDateTime()").AsEnumerable().First();
        }
    }
}
