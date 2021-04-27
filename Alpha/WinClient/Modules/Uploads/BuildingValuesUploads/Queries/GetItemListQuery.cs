using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Queries
{
    public static class GetItemListQuery
    {
        public static DataTable GetItemList(this Entities db, int uploadId)
        {
            DataTable table = CreateDataTable();

            var rawUploadFiles =
                db.BuildingValuesFiles
                    .Where(x => x.BuildingValuesUploads.ID == uploadId)
                    .Select(x =>
                        new
                        {
                            x.ID,
                            x.FileName,
                            Result =
                                x.ProcessingResult != (byte)FileProcessingResult.OK
                                    ? "Ошибка"
                                    : "ОК",
                            BuildingsWithNoErrors =
                                db.BuildingValuesRows
                                    .Where(r =>
                                        x.ProcessingResult == (byte)FileProcessingResult.OK
                                            && r.BuildingValuesForms.BuildingValuesFiles.ID == x.ID
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
                                        x.ProcessingResult == (byte)FileProcessingResult.OK
                                            && r.BuildingValuesForms.BuildingValuesFiles.ID == x.ID
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
                                x.ProcessingResult != (byte)FileProcessingResult.OK
                                    ? string.IsNullOrEmpty(x.ErrorDescription)
                                        ? "Программная ошибка при обработке файла. " +
                                            "Проверьте подключение к сети и серверу БД."
                                        : x.ErrorDescription
                                    : "ОК",
                        })
                    .ToList();

            var uploadFiles =
                rawUploadFiles
                    .Select(x =>
                        new
                        {
                            x.ID,
                            x.FileName,
                            x.Result,
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
                        })
                    .ToList()
                    .OrderBy(x => x.ID);

            foreach (var item in uploadFiles)
            {
                table.Rows.Add(
                    item.ID.ToString(),
                    item.FileName,
                    item.Result,
                    item.BuildingsWithNoErrors,
                    item.BuildingsWithErrors,
                    item.Description);
            }

            return table;
        }

        private static DataTable CreateDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("FileName");
            table.Columns.Add("Result");
            table.Columns.Add("BuildingsWithNoErrors");
            table.Columns.Add("BuildingsWithErrors");
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
