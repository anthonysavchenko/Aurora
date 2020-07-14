using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.DecFormsDownloads.Queries
{
    public static class GetItemListQuery
    {
        public static DataTable GetItemList(this Entities db, int downloadId)
        {
            DataTable table = CreateDataTable();

            var items =
                db.Emails
                    .Where(x => x.DecFormsDownloads.ID == downloadId)
                    .Select(x =>
                        new
                        {
                            x.ID,
                            x.Subject,
                            x.FromAddress,
                            x.Received,
                            EmailDescription = x.ErrorDescription,
                            OuterError = !string.IsNullOrEmpty(x.ErrorDescription),
                            InnerErrors =
                                x.Attachments.Count > 0
                                    && (x.Attachments.Any(e => e.ErrorDescription != null)),
                        })
                    .ToList()
                    .OrderByDescending(x => x.Received);

            foreach (var item in items)
            {
                table.Rows.Add(
                    item.ID.ToString(),
                    item.Received,
                    item.Subject,
                    item.FromAddress,
                    item.OuterError && item.InnerErrors
                        ? $"{item.EmailDescription} А также обнаружены ошибки при скачивании некоторых файлов."
                        : item.OuterError && !item.InnerErrors
                            ? item.EmailDescription
                            : !item.OuterError && item.InnerErrors
                                ? "Обнаружены ошибки при скачивании некоторых файлов."
                                : "ОК");
            }

            DataTable subTable = CreateSubDataTable();

            var subItems =
                db.Attachments
                    .Where(x => x.Emails.DecFormsDownloads.ID == downloadId)
                    .Select(x =>
                        new
                        {
                            x.Emails.ID,
                            x.FileName,
                            AttachmentDescription = x.ErrorDescription,
                        })
                    .ToList()
                    .OrderBy(x => x.FileName);

            foreach (var subItem in subItems)
            {
                subTable.Rows.Add(
                    subItem.ID.ToString(),
                    subItem.FileName,
                    !string.IsNullOrEmpty(subItem.AttachmentDescription)
                        ? subItem.AttachmentDescription
                        : "ОК");
            }

            DataSet dataSet = new DataSet();

            dataSet.Tables.Add(table);
            dataSet.Tables.Add(subTable);

            DataColumn[] keyColumn = new DataColumn[] { dataSet.Tables[0].Columns["ID"] };
            DataColumn[] foreignKeyColumn = new DataColumn[] { dataSet.Tables[1].Columns["Email"] };
            dataSet.Relations.Add("ID_Email", keyColumn, foreignKeyColumn);

            return dataSet.Tables[0];
        }

        private static DataTable CreateDataTable()
        {
            DataTable table = new DataTable();

            table.Columns.Add("ID");
            table.Columns.Add("Received", typeof(DateTime));
            table.Columns.Add("Subject");
            table.Columns.Add("FromAddress");
            table.Columns.Add("EmailDescription");

            return table;
        }

        private static DataTable CreateSubDataTable()
        {
            DataTable table = new DataTable();

            table.Columns.Add("Email");
            table.Columns.Add("FileName");
            table.Columns.Add("AttachmentDescription");

            return table;
        }
    }
}
