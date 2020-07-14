using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.DecFormsDownloads.Queries
{
    public static class GetListQuery
    {
        public static DataTable GetList(this Entities db, DateTime since, DateTime till)
        {
            DataTable table = CreateDataTable();

            var items =
                db.DecFormsDownloads
                    .Include("Author")
                    .Where(x => x.Created >= since && x.Created <= till)
                    .Select(x =>
                        new
                        {
                            x.ID,
                            x.Created,
                            Author = x.Author.Aka,
                            Emails = x.Emails.Count(e => e.ErrorDescription == null),
                            Files = x.Emails.Count > 0
                                ? x.Emails.Sum(e => e.Attachments.Count(a => a.ErrorDescription == null))
                                : 0,
                            Errors =
                                (x.ErrorDescription != null ? 1 : 0) +
                                    x.Emails.Count(e => e.ErrorDescription != null) +
                                    (x.Emails.Count > 0
                                        ? x.Emails.Sum(e => e.Attachments.Count(a => a.ErrorDescription != null))
                                        : 0),
                            x.Note,
                            x.ErrorDescription,
                            OuterError = !string.IsNullOrEmpty(x.ErrorDescription),
                            InnerErrors =
                                x.Emails.Count > 0
                                    && (x.Emails.Any(e => e.ErrorDescription != null)
                                    || x.Emails.Any(e =>
                                        e.Attachments.Count > 0
                                        && e.Attachments.Any(a => a.ErrorDescription != null))),
                        })
                    .OrderBy(x => x.Created)
                    .ToList();

            foreach (var item in items)
            {
                table.Rows.Add(
                    item.ID.ToString(),
                    item.ID,
                    item.Created,
                    item.Author,
                    item.Emails,
                    item.Files,
                    item.Errors,
                    item.Note,
                    item.OuterError && item.InnerErrors
                        ? $"{item.ErrorDescription} А также обнаружены ошибки при чтении некоторых писем и/или скачивании некоторых файлов."
                        : item.OuterError && !item.InnerErrors
                            ? item.ErrorDescription
                            : !item.OuterError && item.InnerErrors
                                ? "Обнаружены ошибки при чтении некоторых писем и/или скачивании некоторых файлов."
                                : item.Emails > 0 && item.Files > 0
                                    ? "Скачивание выполнено успешно."
                                    : "Нескачанных файлов не обнаружено.");
            }

            return table;
        }

        private static DataTable CreateDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Number", typeof(int));
            table.Columns.Add("Created", typeof(DateTime));
            table.Columns.Add("Author");
            table.Columns.Add("Emails", typeof(int));
            table.Columns.Add("Files", typeof(int));
            table.Columns.Add("Errors", typeof(int));
            table.Columns.Add("Note");
            table.Columns.Add("Description");

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
