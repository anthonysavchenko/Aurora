using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.PrivateValuesUploads.Queries
{
    public static class GetListQuery
    {
        public static DataTable GetList(this Entities db, DateTime since, DateTime till)
        {
            DataTable table = CreateDataTable();

            var items =
                db.PrivateValuesUploads
                    .Include("Author")
                    .Where(x => x.Created >= since && x.Created <= till)
                    .Select(x =>
                        new
                        {
                            x.ID,
                            x.Created,
                            x.Month,
                            Author = x.Author.Aka,

                            PrivateValuesForms = x
                                .PrivateValuesForms.Count(p => string.IsNullOrEmpty(p.ErrorDescription)),
                            
                            Errors =
                                (!string.IsNullOrEmpty(x.ErrorDescription) ? 1 : 0) +
                                    x.PrivateValuesForms.Count(p => !string.IsNullOrEmpty(p.ErrorDescription)),

                            x.Note,
                            x.ErrorDescription,
                            OuterError = !string.IsNullOrEmpty(x.ErrorDescription),
                            InnerErrors =
                                x.PrivateValuesForms.Count > 0
                                    && x.PrivateValuesForms.Any(e => e.ErrorDescription != null),

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
                    item.PrivateValuesForms,
                    item.Errors,
                    item.Note,
                    item.OuterError && item.InnerErrors
                        ? $"{item.ErrorDescription} А также обнаружены ошибки при распознавании и/или " +
                            "сохранении некоторых файлов."
                        : item.OuterError && !item.InnerErrors
                            ? item.ErrorDescription
                            : !item.OuterError && item.InnerErrors
                                ? "Обнаружены ошибки при распознавании и/или сохранении некоторых файлов."
                                : item.PrivateValuesForms > 0
                                    ? "Распознавание и сохранение файлов выполнено успешно."
                                    : "Файлов для распознавания и сохранения не обнаружено.");
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
            table.Columns.Add("PrivateValuesForms", typeof(int));
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
