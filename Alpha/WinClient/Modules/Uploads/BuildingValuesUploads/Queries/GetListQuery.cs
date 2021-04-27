using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Queries
{
    public static class GetListQuery
    {
        public static DataTable GetList(this Entities db, DateTime since, DateTime till)
        {
            DataTable table = CreateDataTable();

            var rawItems =
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
                            Result =
                                x.ProcessingResult != (byte)UploadProcessingResult.OK
                                    ? "Ошибка"
                                    : "ОК",
                            FilesWithNoErrors =
                                db.BuildingValuesFiles
                                    .Count(f =>
                                        f.BuildingValuesUploads.ID == x.ID
                                            && f.ProcessingResult == (byte)FileProcessingResult.OK),
                            FilesWithErrors =
                                db.BuildingValuesFiles
                                    .Count(f =>
                                        f.BuildingValuesUploads.ID == x.ID
                                            && f.ProcessingResult != (byte)FileProcessingResult.OK),
                            BuildingsWithNoErrors =
                                db.BuildingValuesRows
                                    .Where(r =>
                                        r.BuildingValuesForms.BuildingValuesFiles.BuildingValuesUploads.ID == x.ID
                                            && r.BuildingValuesForms.BuildingValuesFiles.ProcessingResult ==
                                                (byte)FileProcessingResult.OK
                                            && r.ProcessingResult == (byte)RowProcessingResult.OK)
                                    .Select(r =>
                                        new
                                        {
                                            r.Street,
                                            r.Building,
                                        })
                                    .ToList(),
                            BuildingsWithErrors =
                                db.BuildingValuesRows
                                    .Where(r =>
                                        r.BuildingValuesForms.BuildingValuesFiles.BuildingValuesUploads.ID == x.ID
                                            && r.BuildingValuesForms.BuildingValuesFiles.ProcessingResult ==
                                                (byte)FileProcessingResult.OK
                                            && r.ProcessingResult != (byte)RowProcessingResult.OK
                                            && r.ProcessingResult != (byte)RowProcessingResult.Skipped)
                                    .Select(r =>
                                        new
                                        {
                                            r.Street,
                                            r.Building,
                                        })
                                    .ToList(),
                            Description =
                                x.ProcessingResult != (byte)UploadProcessingResult.OK
                                    ? string.IsNullOrEmpty(x.ErrorDescription)
                                        ? "Программная ошибка во время загрузки показаний ОДПУ при обработке данных. " +
                                            "Проверьте подключение к сети и серверу БД."
                                        : x.ErrorDescription
                                    : "ОК",
                            x.Note,
                        })
                    .ToList();

            var items =
                rawItems
                    .Select(x =>
                        new
                        {
                            x.ID,
                            x.Created,
                            x.Month,
                            x.Author,
                            x.Result,
                            x.FilesWithNoErrors,
                            x.FilesWithErrors,
                            BuildingsWithNoErrors =
                                x.BuildingsWithNoErrors
                                    .GroupBy(g =>
                                        new
                                        {
                                            Street = g.Street.ToLowerInvariant(),
                                            Building = g.Building.ToLowerInvariant(),
                                        })
                                    .Count(),
                            BuildingsWithErrors =
                                x.BuildingsWithErrors
                                    .GroupBy(g =>
                                        new
                                        {
                                            Street =
                                                !string.IsNullOrEmpty(g.Street)
                                                    ? g.Street.ToLowerInvariant()
                                                    : string.Empty,
                                            Building =
                                                !string.IsNullOrEmpty(g.Building)
                                                    ? g.Building.ToLowerInvariant()
                                                    : string.Empty,
                                        })
                                    .Count(),
                            x.Description,
                            x.Note,
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
                    item.Result,
                    item.FilesWithNoErrors,
                    item.FilesWithErrors,
                    item.BuildingsWithNoErrors,
                    item.BuildingsWithErrors,
                    item.Description,
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
            table.Columns.Add("Result");
            table.Columns.Add("FilesWithNoErrors", typeof(int));
            table.Columns.Add("FilesWithErrors", typeof(int));
            table.Columns.Add("BuildingsWithNoErrors", typeof(int));
            table.Columns.Add("BuildingsWithErrors", typeof(int));
            table.Columns.Add("Description");
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
