using System;
using System.Collections.Generic;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Win.Services;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesParser.BuildingValuesFormParser
{
    static public class RowParser
    {
        public const int FIRST_ROW = 19;

        const string ADDRESS_COLUMN = "C";
        const string COUNTER_NUMBER_COLUMN = "E";
        const string CURRENT_VALUE_COLUMN = "F";
        const string PREV_VALUE_COLUMN = "G";
        const string COEFFICIENT_COLUMN = "I";
        const string CURRENT_DATE_COLUMN = "K";

        static public bool ParseRows(
            ExcelSheet source,
            out List<BuildingValuesUploadPoses> poses,
            out string message)
        {
            poses = null;
            message = null;

            for (int i = FIRST_ROW; i <= source.RowsCount; i++)
            {
                try
                {
                    ParseRow(source,
                        i,
                        out BuildingValuesUploadPoses pos,
                        out string rowMessage);

                    if (poses == null)
                    {
                        poses = new List<BuildingValuesUploadPoses>();
                    }

                    poses.Add(pos);
                }
                catch (Exception e)
                {
                    Logger.SimpleWrite($"BuildingValuesFormParser.RowParser ParseRows error (row: {i}): {e}");
                    message = $"Строка {i}. Ошибка при распознавании строки.\r\n";
                    return false;
                }
            }

            return true;
        }

        static public bool ParseRow(
            ExcelSheet source,
            int row,
            out BuildingValuesUploadPoses pos,
            out string message)
        {
            pos = new BuildingValuesUploadPoses();
            message = null;

            try
            {
                if (!BuildingValuesParser.ColumnParser.ParseAddressColumn(
                    source.GetCellText($"{ADDRESS_COLUMN}{row}"),
                    out string street,
                    out string building,
                    out message))
                {
                    pos.ErrorDescription = $"Ячейка \"{ADDRESS_COLUMN}{row}\". {message}";
                    return false;
                }
                else
                {
                    pos.Street = street;
                    pos.Building = building;
                }

                if (!ColumnParser.ParseCounterNumberColumn(
                    source.GetCellText($"{COUNTER_NUMBER_COLUMN}{row}"),
                    out string counterNumber,
                    out message))
                {
                    pos.ErrorDescription = $"Ячейка \"{COUNTER_NUMBER_COLUMN}{row}\". {message}";
                    return false;
                }
                else
                {
                    pos.CounterNumber = counterNumber;
                }

                if (!ColumnParser.ParseCurrentValueColumn(
                    source.GetCellText($"{CURRENT_VALUE_COLUMN}{row}"),
                    out decimal? currentValue,
                    out message))
                {
                    pos.ErrorDescription = $"Ячейка \"{CURRENT_VALUE_COLUMN}{row}\". {message}";
                    return false;
                }
                else
                {
                    pos.CurrentValue = currentValue;
                }

                if (!ColumnParser.ParsePrevValueColumn(
                    source.GetCellText($"{PREV_VALUE_COLUMN}{row}"),
                    out decimal? prevValue,
                    out message))
                {
                    pos.ErrorDescription = $"Ячейка \"{PREV_VALUE_COLUMN}{row}\". {message}";
                    return false;
                }
                else
                {
                    pos.PrevValue = prevValue;
                }

                if (!ColumnParser.ParseCoefficientColumn(
                    source.GetCellText($"{COEFFICIENT_COLUMN}{row}"),
                    out byte? coefficient,
                    out message))
                {
                    pos.ErrorDescription = $"Ячейка \"{COEFFICIENT_COLUMN}{row}\". {message}";
                }
                else
                {
                    pos.Coefficient = coefficient;
                }

                if (!BuildingValuesParser.ColumnParser.ParseCurrentDateColumn(
                    source.GetCellText($"{CURRENT_DATE_COLUMN}{row}"),
                    out DateTime? currentDate,
                    out message))
                {
                    pos.ErrorDescription = $"Ячейка \"{CURRENT_DATE_COLUMN}{row}\". {message}";
                    return false;
                }
                else
                {
                    pos.CurrentDate = currentDate;
                }
            }
            catch (Exception e)
            {
                pos.ErrorDescription = $"Строка {row}. Ошибка при распознавании строки.\r\n";
                pos.ExceptionMessage = e.ToString();
            }

            return true;
        }
    }
}
