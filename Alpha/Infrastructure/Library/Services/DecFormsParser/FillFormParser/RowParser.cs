﻿using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Win.Services;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsParser.FillFormParser
{
    static public class RowParser
    {
        public const int FIRST_ROW = 1;

        const string ACCOUNT_COLUMN = "D";
        const string APARTMENT_COLUMN = "E";
        const string COUNTER_MODEL_COLUMN = "F";
        const string COUNTER_NUMBER_COLUMN = "G";
        const string COUNTER_CAPACITY_COLUMN = "H";
        const string COUNTER_TYPE_COLUMN = "I";
        const string PREV_DATE_COLUMN = "J";
        const string PREV_VALUE_COLUMN = "K";

        const int ACCOUNT_DB_LENGTH = 25;
        const int COUNTER_CAPACITY_DB_LENGTH = 2;

        static public bool ParseRows(
            ExcelSheet source,
            out List<FillFormPoses> poses,
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
                        out FillFormPoses pos,
                        out string rowStreet,
                        out string rowBuilding,
                        out string rowMessage))
                    {
                        message = $"Строка {i}. {rowMessage}";
                        return false;
                    }

                    if (pos.CounterType == (byte)FillFormCounterType.Norm)
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

                    if (poses != null)
                    {
                        FillFormPoses existed = poses.FirstOrDefault(p => p.Apartment == pos.Apartment);

                        if (existed != null)
                        {
                            if (existed.CounterType != (byte)FillFormCounterType.Day
                                || existed.PrevDayValue == null
                                || existed.PrevNightValue != null

                                || pos.CounterType != (byte)FillFormCounterType.Night
                                || pos.PrevDayValue != null
                                || pos.PrevNightValue == null

                                || existed.Account != pos.Account
                                || existed.CounterModel != pos.CounterModel
                                || existed.CounterNumber != pos.CounterNumber
                                || existed.CounterCapacity != pos.CounterCapacity
                                || existed.PrevDate != pos.PrevDate)
                            {
                                message = $"Строка {i}. Распознанный номер квартиры уже был указан в файле ранее. " +
                                    $"Дублирование номера квартиры в разных строках одного файла предусмотрено " +
                                    $"только для указания данных для двухтарифного счетчика: на первой строке - " +
                                    $"дневные данные, на второй строке - ночные. При этом значения в соответсвтующих" +
                                    $"ячейках двух строк должны быть одинаковыми, за исключением колонки с типом " +
                                    $"счетчика и колонки с предыдущими показаниями. В данном случае это не так.";
                                return false;
                            }
                            else
                            {
                                existed.PrevNightValue = pos.PrevNightValue;
                                continue;
                            }
                        }
                    }

                    if (poses == null)
                    {
                        poses = new List<FillFormPoses>();
                        street = rowStreet;
                        building = rowBuilding;
                    }

                    poses.Add(pos);
                }
                catch (Exception e)
                {
                    Logger.SimpleWrite($"FillForm ParseRows error (row: {i}): {e}");
                    message = $"Строка {i}. Внутренняя ошибка при распознавании строки.\r\n";
                    return false;
                }
            }

            return true;
        }

        static public bool ParseRow(
            ExcelSheet source,
            int row,
            out FillFormPoses pos,
            out string street,
            out string building,
            out string message)
        {
            pos = null;

            if (!DecFormsParser.ColumnParser.ParseApartmentColumn(
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
                out string counterModel,
                out FillFormCounterType counterTypeInCounterNumber,
                out message))
            {
                message = $"Ячейка \"{COUNTER_MODEL_COLUMN}{row}\". {message}";
                return false;
            }

            if (!ColumnParser.ParseCounterNumberColumn(
                source.GetCellText($"{COUNTER_NUMBER_COLUMN}{row}"),
                counterTypeInCounterNumber == FillFormCounterType.Norm,
                out string counterNumber,
                out message))
            {
                message = $"Ячейка \"{COUNTER_NUMBER_COLUMN}{row}\". {message}";
                return false;
            }

            if (!ColumnParser.ParseCounterTypeColumn(
                source.GetCellText($"{COUNTER_TYPE_COLUMN}{row}"),
                counterTypeInCounterNumber == FillFormCounterType.Norm,
                out FillFormCounterType counterType,
                out message))
            {
                message = $"Ячейка \"{COUNTER_TYPE_COLUMN}{row}\". {message}";
                return false;
            }

            if (!DecFormsParser.ColumnParser.ParsePrevDateColumn(
                source.GetCellText($"{PREV_DATE_COLUMN}{row}"),
                counterType == FillFormCounterType.Norm,
                out DateTime? prevDate,
                out message))
            {
                message = $"Ячейка \"{PREV_DATE_COLUMN}{row}\". {message}";
                return false;
            }

            if (!ColumnParser.ParsePrevValueColumn(
                source.GetCellText($"{PREV_VALUE_COLUMN}{row}"),
                counterType,
                out int? prevValue,
                out int? prevDayValue,
                out int? prevNightValue,
                out message))
            {
                message = $"Ячейка \"{PREV_VALUE_COLUMN}{row}\". {message}";
                return false;
            }

            if (!DecFormsParser.ColumnParser.ParseOptionalColumn(
                source.GetCellText($"{ACCOUNT_COLUMN}{row}"),
                ACCOUNT_DB_LENGTH,
                "лицевого счета",
                out string account,
                out message))
            {
                message = $"Ячейка \"{ACCOUNT_COLUMN}{row}\". {message}";
                return false;
            }

            if (!DecFormsParser.ColumnParser.ParseOptionalColumn(
                source.GetCellText($"{COUNTER_CAPACITY_COLUMN}{row}"),
                counterType == FillFormCounterType.Norm,
                COUNTER_CAPACITY_DB_LENGTH,
                "разрядности счетчика",
                out string counterCapacity,
                out message))
            {
                message = $"Ячейка \"{COUNTER_CAPACITY_COLUMN}{row}\". {message}";
                return false;
            }

            pos =
                new FillFormPoses()
                {
                    Apartment = apartment,
                    CounterModel = counterModel,
                    CounterType = (byte)counterType,
                    CounterNumber = counterNumber,
                    PrevDate = prevDate,
                    PrevValue = prevValue,
                    PrevDayValue = prevDayValue,
                    PrevNightValue = prevNightValue,
                    Account = account,
                    CounterCapacity = counterCapacity,
                };

            return true;
        }
    }
}
