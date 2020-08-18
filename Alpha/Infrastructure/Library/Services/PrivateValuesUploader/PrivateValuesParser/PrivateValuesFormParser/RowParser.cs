using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Win.Services;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.PrivateValuesUploader.PrivateValuesParser.PrivateValuesFormParser
{
    static public class RowParser
    {
        public const int FIRST_ROW = 1;

        const string APARTMENT_COLUMN = "E";
        const string COUNTER_MODEL_COLUMN = "F";
        const string COUNTER_NUMBER_COLUMN = "G";
        const string COUNTER_TYPE_COLUMN = "I";
        const string CURRENT_DATE_COLUMN = "L";
        const string CURRENT_VALUE_COLUMN = "M";

        static public bool ParseRows(
            ExcelSheet source,
            out List<PrivateValuesFormPoses> poses,
            out string street,
            out string building,
            out string message)
        {
            poses = null;
            street = null;
            building = null;
            message = null;

            for (int i = FIRST_ROW; i <= source.RowsCount; i++)
            {
                try
                {
                    if (!ParseRow(source,
                        i,
                        out PrivateValuesFormPoses pos,
                        out string rowStreet,
                        out string rowBuilding,
                        out string rowMessage))
                    {
                        message = $"Строка {i}. {rowMessage}";
                        return false;
                    }

                    if (pos.CounterType == (byte)PrivateFormCounterType.Norm
                        || (pos.CurrentValue == null 
                            && pos.CurrentDayValue == null
                            && pos.CurrentNightValue == null))
                    {
                        continue;
                    }

                    if (street != null && building != null && street != rowStreet && building != rowBuilding)
                    {
                        message = $"Строка {i}. Распознанное название улицы и номер дома не соответствует " +
                            $"распознанному названию улицы и номеру дома в первой квартире файла. Несколько разных " +
                            $"домов в одном файле не предусмотрены форматом.";
                        return false;
                    }

                    if (poses != null && poses.Count != 0)
                    {
                        PrivateValuesFormPoses existed =
                            poses
                                .FirstOrDefault(p =>
                                    p.Apartment == pos.Apartment
                                    && p.CounterNumber == pos.CounterNumber);

                        if (existed != null)
                        {
                            if (existed.CounterType != (byte)PrivateFormCounterType.Day
                                || existed.CurrentValue != null
                                || existed.CurrentDayValue == null
                                || existed.CurrentNightValue != null

                                || pos.CounterType != (byte)PrivateFormCounterType.Night
                                || pos.CurrentValue != null
                                || pos.CurrentDayValue != null
                                || pos.CurrentNightValue == null)
                            {
                                message = $"Строка {i}. Распознанный номер счетчика и номер квартиры уже были " +
                                    "указаны в файле ранее. Дублирование номера счетчика и номера квартиры в разных " +
                                    "строках одного файла предусмотрено только для указания данных двухтарифного " +
                                    "счетчика: на первой строке - дневные данные, на второй строке - ночные.";
                                return false;
                            }
                        }
                    }

                    if (poses == null)
                    {
                        poses = new List<PrivateValuesFormPoses>();
                        street = rowStreet;
                        building = rowBuilding;
                    }

                    poses.Add(pos);
                }
                catch (Exception e)
                {
                    Logger.SimpleWrite($"PrivateValuesFormParser.RowParser ParseRows error (row: {i}): {e}");
                    message = $"Строка {i}. Ошибка при распознавании строки.\r\n";
                    return false;
                }
            }

            return true;
        }

        static public bool ParseRow(
            ExcelSheet source,
            int row,
            out PrivateValuesFormPoses pos,
            out string street,
            out string building,
            out string message)
        {
            pos = null;

            if (!PrivateValuesParser.ColumnParser.ParseApartmentColumn(
                source.GetCellText($"{APARTMENT_COLUMN}{row}"),
                out street,
                out building,
                out string apartment,
                out message))
            {
                message = $"Ячейка \"{APARTMENT_COLUMN}{row}\". {message}";
                return false;
            }

            if (!ColumnParser.ParseCounterModelColumn(
                source.GetCellText($"{COUNTER_MODEL_COLUMN}{row}"),
                out string _,
                out PrivateFormCounterType counterTypeInCounterNumber,
                out message))
            {
                message = $"Ячейка \"{COUNTER_MODEL_COLUMN}{row}\". {message}";
                return false;
            }

            if (!ColumnParser.ParseCounterNumberColumn(
                source.GetCellText($"{COUNTER_NUMBER_COLUMN}{row}"),
                counterTypeInCounterNumber == PrivateFormCounterType.Norm,
                out string counterNumber,
                out message))
            {
                message = $"Ячейка \"{COUNTER_NUMBER_COLUMN}{row}\". {message}";
                return false;
            }

            if (!ColumnParser.ParseCounterTypeColumn(
                source.GetCellText($"{COUNTER_TYPE_COLUMN}{row}"),
                counterTypeInCounterNumber == PrivateFormCounterType.Norm,
                out PrivateFormCounterType counterType,
                out message))
            {
                message = $"Ячейка \"{COUNTER_TYPE_COLUMN}{row}\". {message}";
                return false;
            }

            if (!PrivateValuesParser.ColumnParser.ParseCurrentDateColumn(
                source.GetCellText($"{CURRENT_DATE_COLUMN}{row}"),
                counterType == PrivateFormCounterType.Norm,
                out DateTime? currentDate,
                out message))
            {
                message = $"Ячейка \"{CURRENT_DATE_COLUMN}{row}\". {message}";
                return false;
            }

            if (!ColumnParser.ParseCurrentValueColumn(
                source.GetCellText($"{CURRENT_VALUE_COLUMN}{row}"),
                counterType,
                out int? currentValue,
                out int? currentDayValue,
                out int? currentNightValue,
                out message))
            {
                message = $"Ячейка \"{CURRENT_VALUE_COLUMN}{row}\". {message}";
                return false;
            }

            pos =
                new PrivateValuesFormPoses()
                {
                    Apartment = apartment,
                    CounterType = (byte)counterType,
                    CounterNumber = counterNumber,
                    CurrentDate = currentDate,
                    CurrentValue = currentValue,
                    CurrentDayValue = currentDayValue,
                    CurrentNightValue = currentNightValue,
                };

            return true;
        }
    }
}
