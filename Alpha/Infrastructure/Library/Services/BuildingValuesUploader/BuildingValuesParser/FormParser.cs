using System.Collections.Generic;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesParser
{
    public static class FormParser
    {
        public static bool TryParseForm(
            ExcelSheet source,
            ref int rowNumber,
            int rowsCount,
            out List<BuildingValuesRows> rows)
        {
            rows = new List<BuildingValuesRows>();
            rowNumber = 1;

            if (!TryAddSkippedRows(
                18,
                ref rowNumber,
                rowsCount,
                rows))
            {
                return false;
            }

            while (rowNumber <= rowsCount)
            {
                TryParseBuildingValue(
                    source,
                    ref rowNumber,
                    rows);

                var rowsLeft = rowsCount - rowNumber + 1;

                if (rowsLeft < 8
                    && RowParser.IsAddressCellEmpty(source, rowNumber))
                {
                    if (!TryAddSkippedRows(
                        rowsLeft < 7 ? rowsLeft : 7,
                        ref rowNumber,
                        rowsCount,
                        rows))
                    {
                        break;
                    }
                }
            }

            return true;
        }

        private static bool TryParseBuildingValue(
            ExcelSheet source,
            ref int rowNumber,
            List<BuildingValuesRows> rows)
        {
            var parsedWithNoErrors =
                RowParser.TryParseRow(
                    source,
                    rowNumber,
                    out BuildingValuesRows row);

            rows.Add(row);
            rowNumber++;

            if (!parsedWithNoErrors)
            {
                return false;
            }

            return true;
        }

        private static bool TryAddSkippedRows(
            int quantity,
            ref int rowNumber,
            int rowsCount,
            List<BuildingValuesRows> rows)
        {
            for (int i = 0; i < quantity; i++)
            {
                if (rowNumber > rowsCount)
                {
                    AddMissedRow(rows);
                    return false;
                }

                rows.Add(
                    new BuildingValuesRows()
                    {
                        ProcessingResult = (byte)RowProcessingResult.Skipped,
                    });

                rowNumber++;
            }

            return true;
        }

        private static void AddMissedRow(List<BuildingValuesRows> rows)
        {
            var row = new BuildingValuesRows();
            BuildingValuesRowHandler.SetParsingError(
                row,
                "В файле отсутствует одна или несколько строк, " +
                    "которые обязательно должны быть в соответствии с форматом файла.");
            rows.Add(row);
        }
    }
}
