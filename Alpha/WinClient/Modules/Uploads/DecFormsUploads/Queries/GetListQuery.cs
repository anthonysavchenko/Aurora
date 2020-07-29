using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;

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
                            Author = x.Author.Aka,

                            RouteForms = x
                                .DecFormsUploadPoses.Count(p =>
                                    (DecFormsType)p.FormType == DecFormsType.RouteForm
                                    && string.IsNullOrEmpty(p.ErrorDescription)),

                            FillForms = x
                                .DecFormsUploadPoses.Count(p =>
                                    (DecFormsType)p.FormType == DecFormsType.FillForm
                                    && string.IsNullOrEmpty(p.ErrorDescription)),
                            
                            UnknownFiles = x
                                .DecFormsUploadPoses.Count(p =>
                                    (DecFormsType)p.FormType == DecFormsType.Unknown),

                            Errors =
                                (!string.IsNullOrEmpty(x.ErrorDescription) ? 1 : 0) +
                                    x.DecFormsUploadPoses.Count(p =>
                                        !string.IsNullOrEmpty(p.ErrorDescription)
                                        && (DecFormsType)p.FormType != DecFormsType.Unknown),

                            x.Note,
                            x.ErrorDescription,
                            OuterError = !string.IsNullOrEmpty(x.ErrorDescription),
                            InnerErrors =
                                x.DecFormsUploadPoses.Count > 0
                                    && x.DecFormsUploadPoses.Any(e => e.ErrorDescription != null),

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
                    item.RouteForms,
                    item.FillForms,
                    item.UnknownFiles,
                    item.Errors,
                    item.Note,
                    item.OuterError && item.InnerErrors
                        ? $"{item.ErrorDescription} А также обнаружены ошибки при распознавании и/или " +
                            "сохранении некоторых файлов."
                        : item.OuterError && !item.InnerErrors
                            ? item.ErrorDescription
                            : !item.OuterError && item.InnerErrors
                                ? "Обнаружены ошибки при распознавании и/или сохранении некоторых файлов."
                                : item.RouteForms > 0 || item.FillForms > 0
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
            table.Columns.Add("RouteForms", typeof(int));
            table.Columns.Add("FillForms", typeof(int));
            table.Columns.Add("UnknownFiles", typeof(int));
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
