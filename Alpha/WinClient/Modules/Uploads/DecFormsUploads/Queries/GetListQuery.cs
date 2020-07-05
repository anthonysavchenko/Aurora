using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.DecFormsUploads.Queries
{
    public static class GetListQuery
    {
        public static DataTable GetList(this Entities db, DateTime since, DateTime till)
        {
            DataTable table = CreateDataTable();

            var items =
                db.DecFormsUploads
                    .Include("Author")
                    .Where(x => x.Created >= since && x.Created <= till)
                    .Select(x =>
                        new
                        {
                            x.ID,
                            x.Created,
                            x.Month,
                            x.Note,
                            Author = x.Author.Aka,
                        })
                    .OrderBy(x => x.Created)
                    .ToList();

            foreach (var item in items)
            {
                table.Rows.Add(
                    item.ID.ToString(),
                    item.ID,
                    item.Created,
                    item.Month,
                    item.Author,
                    item.Note);
            }

            return table;
        }

        private static DataTable CreateDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Number", typeof(int));
            table.Columns.Add("Created", typeof(DateTime));
            table.Columns.Add("Month", typeof(DateTime));
            table.Columns.Add("Author");
            table.Columns.Add("Note");

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
