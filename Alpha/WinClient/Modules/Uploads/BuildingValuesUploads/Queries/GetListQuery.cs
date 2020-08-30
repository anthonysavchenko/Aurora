using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Queries
{
    public static class GetListQuery
    {
        public static DataTable GetList(this Entities db, DateTime since, DateTime till)
        {
            DataTable table = CreateDataTable();

            var items =
                db.BuildingValuesUploads
                    .Include("Author")
                    .Where(x => x.Created >= since && x.Created <= till)
                    .Select(x =>
                        new
                        {
                            x.ID,
                            x.Created,
                            x.Month,
                            Author = x.Author.Aka,
                            BuildingCounterValues = x
                                .BuildingValuesUploadPoses.Count(p => string.IsNullOrEmpty(p.ErrorDescription)),
                            Errors =
                                (!string.IsNullOrEmpty(x.ErrorDescription) ? 1 : 0) +
                                    x.BuildingValuesUploadPoses.Count(p => !string.IsNullOrEmpty(p.ErrorDescription)),
                            x.Note,
                            x.ErrorDescription,
                            OuterError = !string.IsNullOrEmpty(x.ErrorDescription),
                            InnerErrors =
                                x.BuildingValuesUploadPoses.Count > 0
                                    && x.BuildingValuesUploadPoses.Any(e => !string.IsNullOrEmpty(e.ErrorDescription)),
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
                    item.BuildingCounterValues,
                    item.Errors,
                    item.Note,
                    item.OuterError && item.InnerErrors
                        ? $"{item.ErrorDescription} А также обнаружены ошибки при распознавании и/или " +
                            "сохранении некоторых показаний ОДПУ."
                        : item.OuterError && !item.InnerErrors
                            ? item.ErrorDescription
                            : !item.OuterError && item.InnerErrors
                                ? "Обнаружены ошибки при распознавании и/или сохранении некоторых показаний ОДПУ."
                                : item.BuildingCounterValues > 0
                                    ? "Распознавание и сохранение показаний ОДПУ выполнено успешно."
                                    : "Показаний ОДПУ для распознавания и сохранения не обнаружено.");
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
            table.Columns.Add("BuildingCounterValues", typeof(int));
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
