using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.CalculationUploads.Queries
{
    public static class GetItemListQuery
    {
        public static DataTable GetItemList(this Entities db, int uploadId)
        {
            DataTable table = CreateDataTable();

            var uploadFiles =
                db.CalculationFiles
                    .Where(x => x.CalculationUploads.ID == uploadId)
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
                                x.ProcessingResult == (byte)FileProcessingResult.OK
                                    ? db.CalculationRows
                                        .Count(r =>
                                            r.CalculationForms.CalculationFiles.ID == x.ID
                                                && r.ProcessingResult == (byte)RowProcessingResult.OK
                                                && r.RowType == (byte)CalculationRowType.BuildingInfo
                                                && r.BuildingInfo.RowType == (byte)BuildingInfoRowType.Address)
                                    : 0,
                            BuildingsWithErrors =
                                x.ProcessingResult == (byte)FileProcessingResult.OK
                                    ? db.CalculationRows
                                        .Count(r =>
                                            r.CalculationForms.CalculationFiles.ID == x.ID
                                                && r.ProcessingResult != (byte)RowProcessingResult.OK
                                                && r.RowType == (byte)CalculationRowType.BuildingInfo
                                                && r.BuildingInfo.RowType == (byte)BuildingInfoRowType.Address)
                                    : 0,
                            Description =
                                x.ProcessingResult != (byte)FileProcessingResult.OK
                                    ? string.IsNullOrEmpty(x.ErrorDescription)
                                        ? "Программная ошибка при обработке файла. " +
                                            "Проверьте подключение к сети и серверу БД."
                                        : x.ErrorDescription
                                    : "ОК",
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
