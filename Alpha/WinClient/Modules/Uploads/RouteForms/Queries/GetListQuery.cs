using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.RouteForms.Queries
{
    public static class GetListQuery
    {
        public static DataTable GetList(this Entities db, DateTime since, DateTime till)
        {
            DataTable table = CreateDataTable();

            var items =
                db.RouteForms
                    .Where(x =>
                        x.DecFormsUploadPoses.DecFormsUploads.Created >= since
                        && x.DecFormsUploadPoses.DecFormsUploads.Created <= till)
                    .Select(x =>
                        new
                        {
                            x.ID,
                            x.Street,
                            x.Building,
                            Upload = x.DecFormsUploadPoses.DecFormsUploads.ID,
                            x.DecFormsUploadPoses.DecFormsUploads.Created,
                            x.DecFormsUploadPoses.DecFormsUploads.Month
                        })
                    .OrderBy(x => x.Created)
                    .ToList()
                    .Select(x =>
                        new
                        {
                            x.ID,
                            x.Upload,
                            Building = $"{x.Street}, д. {x.Building}",
                            x.Created,
                            x.Month,
                        });

            foreach (var item in items)
            {
                table.Rows.Add(
                    item.ID.ToString(),
                    item.Upload,
                    item.Created,
                    item.Month,
                    item.Building);
            }

            return table;
        }

        private static DataTable CreateDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Upload");
            table.Columns.Add("Created", typeof(DateTime));
            table.Columns.Add("Month", typeof(DateTime));
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
