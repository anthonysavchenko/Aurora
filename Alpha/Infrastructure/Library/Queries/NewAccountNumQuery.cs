using System;
using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.Infrastructure.Library.Queries
{
    public static class NewAccountNumQuery
    {
        public static string GetNewAccountNum(this Entities db)
        {
            string _account;
            string _lastAccountStr = db.Customers.Select(x => x.Account).OrderByDescending(x => x).FirstOrDefault();
            if (!string.IsNullOrEmpty(_lastAccountStr))
            {
                long _lastAccountNum = 
                    Convert.ToInt64($"{_lastAccountStr.Substring(0, 4)}{_lastAccountStr.Substring(5, 3)}{_lastAccountStr.Substring(9, 1)}");
                _account = (_lastAccountNum + 1).ToString().Insert(7, "-").Insert(4, "-");
            }
            else
            {
                _account = "1111-111-1";
            }

            return _account;
        }
    }
}
