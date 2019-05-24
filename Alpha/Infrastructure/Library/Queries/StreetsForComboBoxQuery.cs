using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.Infrastructure.Library.Queries
{
    public static class StreetsForComboBoxQuery
    {
        public static DataTable GetStreetsForComboBox(this Entities db)
        {
            var values = db.Streets
                .Select(x => 
                    new
                    {
                        x.ID,
                        x.Name
                    })
                .OrderBy(x => x.Name)
                .ToArray();

            DataTable table = CreateDataTable();

            foreach (var v in values)
            {
                table.Rows.Add(
                    v.ID.ToString(),
                    v.Name);
            }

            return table;

        }

        private static DataTable CreateDataTable()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID");
            _table.Columns.Add("Name");

            DataSet _ds =
                new DataSet
                {
                    EnforceConstraints = false
                };
            _ds.Tables.Add(_table);

            return _table;
        }
    }
}
