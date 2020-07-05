using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.DecFormsUploads.Queries
{
    public static class GetItemListQuery
    {
        public static DataTable GetItemList(this Entities db, int uploadId)
        {
            DataTable table = CreateDataTable();

            var uploadPoses =
                db.DecFormsUploadPoses
                    .Where(x => x.DecFormsUploads.ID == uploadId)
                    .Select(x =>
                        new
                        {
                            x.ID,
                            x.FileName,
                            x.FormType,
                            x.Error,
                        })
                    .ToList()
                    .Select(x =>
                        new
                        {
                            x.ID,
                            x.FileName,
                            FormType = x.FormType == (byte)DecFormsType.RouteForm
                                ? "Маршрутный лист" : x.FormType == (byte)DecFormsType.FillForm
                                    ? "Форма для заполнения"
                                    : "Не определен",
                            x.Error,
                        }
                    )
                    .OrderBy(x => x.Error);

            foreach (var item in uploadPoses)
            {
                table.Rows.Add(
                    item.ID.ToString(),
                    item.FileName,
                    item.FormType,
                    item.Error);
            }

            return table;
        }

        private static DataTable CreateDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("FileName");
            table.Columns.Add("FormType");
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
