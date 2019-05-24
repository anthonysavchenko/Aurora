using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Infrastructure.Interface.Services;

namespace Taumis.Alpha.Infrastructure.Library.Queries
{
    public static class BuildingsForComboBoxQuery
    {
        public static DataTable GetBuildingsForComboBox(this Entities db, int streetId)
        {
            var values = db.Buildings
                .Where(x => x.Streets.ID == streetId)
                .Select(x => 
                    new
                    {
                        x.ID,
                        x.Number
                    })
                .ToArray()
                .OrderBy(x => x.Number, new StringWithNumbersComparer())
                .ToArray();

            DataTable table = CreateDataTable();

            foreach (var v in values)
            {
                table.Rows.Add(v.ID, v.Number);
            }

            return table;
        }

        private static DataTable CreateDataTable()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID");
            _table.Columns.Add("Number");

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
