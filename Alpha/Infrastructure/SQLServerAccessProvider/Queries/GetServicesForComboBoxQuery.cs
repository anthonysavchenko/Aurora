using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.Queries
{
    public static class GetServicesForComboBoxQuery
    {
        public static DataTable GetServicesForComboBox(this Entities db, Expression<Func<Services, bool>> whereExpression = null)
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID", typeof(int));
            _table.Columns.Add("Name", typeof(string));

            var _services = 
                (whereExpression != null
                    ? db.Services.Where(whereExpression)
                    : db.Services)
                .Select(x =>
                    new
                    {
                        x.ID,
                        x.Name
                    })
                .ToList();

            foreach (var _s in _services)
            {
                _table.Rows.Add(_s.ID, _s.Name);
            }

            return _table;
        }
    }
}
