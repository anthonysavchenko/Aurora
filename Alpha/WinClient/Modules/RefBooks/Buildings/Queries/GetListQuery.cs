using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Infrastructure.Interface.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Queries
{
    public static class GetListQuery
    {
        public static DataTable GetList(this Entities db)
        {
            DataTable table = CreateDataTable();

            var items =
                db.Buildings
                    .Select(b => new
                    {
                        b.ID,
                        Building = b.Street + ", д. " + b.Number,
                    })
                    .ToList()
                    .OrderBy(b => b.Building, new StringWithNumbersComparer());

            foreach (var item in items)
            {
                table.Rows.Add(
                    item.ID.ToString(),
                    item.Building);
            }

            return table;
        }

        private static DataTable CreateDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Building");

            DataSet ds =
                new DataSet
                {
                    EnforceConstraints = false
                };
            ds.Tables.Add(table);

            return table;
        }
    }
}
