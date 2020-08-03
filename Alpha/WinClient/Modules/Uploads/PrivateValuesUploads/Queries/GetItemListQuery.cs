using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.PrivateValuesUploads.Queries
{
    public static class GetItemListQuery
    {
        public static DataTable GetItemList(this Entities db, int uploadId)
        {
            DataTable table = CreateDataTable();

            var uploadPoses =
                db.PrivateValuesForms
                    .Where(x => x.PrivateValuesUploads.ID == uploadId)
                    .Select(x =>
                        new
                        {
                            x.ID,
                            x.FileName,
                            x.ErrorDescription,
                            OuterError = !string.IsNullOrEmpty(x.ErrorDescription),
                        })
                    .ToList()
                    .OrderBy(x => x.ErrorDescription);

            foreach (var item in uploadPoses)
            {
                table.Rows.Add(
                    item.ID.ToString(),
                    item.FileName,
                    item.OuterError
                        ? item.ErrorDescription
                        : "ОК");
            }

            return table;
        }

        private static DataTable CreateDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("FileName");
            table.Columns.Add("Error");

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
