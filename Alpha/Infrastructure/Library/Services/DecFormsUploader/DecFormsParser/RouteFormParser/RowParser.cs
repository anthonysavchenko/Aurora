using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Win.Services;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsUploader.DecFormsParser.RouteFormParser
{
    static public class RowParser
    {
        public const int FIRST_ROW = 14;
        const int UNNECESSARY_LAST_ROWS = 7;

        const string APARTMENT_COLUMN = "C";
        const string COUNTER_NUMBER_COLUMN = "D";
        const string PREV_DATE_COLUMN = "H";
        const string COUNTER_TYPE_COLUMN = "I";
        const string PREV_VALUE_COLUMN = "J";
        const string ACCOUNT_COLUMN = "A";
        const string OWNER_COLUMN = "B";
        const string COUNTER_CAPACITY_COLUMN = "E";
        const string DEBT_COLUMN = "F";
        const string PAYED_COLUMN = "G";
        const string PHONE_COLUMN = "M";
        const string NOTE_COLUMN = "N";

        const int OWNER_DB_LENGTH = 50;
        const int COUNTER_CAPACITY_DB_LENGTH = 2;
        const int DEBT_DB_LENGTH = 25;
        const int PAYED_DB_LENGTH = 25;
        const int PHONE_DB_LENGTH = 25;
        const int NOTE_DB_LENGTH = 5;

        static public bool ParseRows(
            ExcelSheet source,
            out List<RouteFormPoses> poses,
            out string street,
            out string building,
            out string message)
        {
            poses = null;
            street = null;
            building = null;
            message = null;

            for (int i = FIRST_ROW; i <= source.RowsCount - UNNECESSARY_LAST_ROWS; i++)
            {
                try
                {
                    if (!ParseRow(source,
                        i,
                        out RouteFormPoses pos,
                        out string rowStreet,
                        out string rowBuilding,
                        out string rowMessage))
                    {
                        message = $"Строка {i}. {rowMessage}";
                        return false;
                    }

                    if (!string.IsNullOrEmpty(street)
                        && !string.IsNullOrEmpty(building)
                        && !street.Equals(rowStreet, StringComparison.OrdinalIgnoreCase)
                        && !building.Equals(rowBuilding, StringComparison.OrdinalIgnoreCase))
                    {
                        message = $"Строка {i}. Распознанное название улицы и номер дома не соответствует " +
                            "распознанному названию улицы и номеру дома в первой квартире файла. Несколько разных " +
                            "домов в одном файле не предусмотрены форматом.";
                        return false;
                    }

                    if (poses != null
                        && poses.Count != 0
                        && poses.Any(p =>
                            p.Apartment.Equals(pos.Apartment, StringComparison.OrdinalIgnoreCase)
                            && p.Account.Equals(pos.Account, StringComparison.OrdinalIgnoreCase)
                            && p.CounterNumber.Equals(pos.CounterNumber, StringComparison.OrdinalIgnoreCase)))
                    {
                        message = $"Строка {i}. Распознанный номер счетчика, лицевой счет и номер квартиры уже " +
                            "были указаны в файле ранее. Дублирование номера счетчика, лицевого счета и номера " +
                            "квартиры в разных строках одного файла не предусмотрено форматом.";
                        return false;
                    }

                    if (poses == null)
                    {
                        poses = new List<RouteFormPoses>();
                        street = rowStreet;
                        building = rowBuilding;
                    }

                    poses.Add(pos);
                }
                catch (Exception e)
                {
                    Logger.SimpleWrite($"RouteForm ParseRows error (row: {i}): {e}");
                    message = $"Строка {i}. Ошибка при распознавании строки.";
                    return false;
                }
            }

            return true;
        }

        static public bool ParseRow(
            ExcelSheet source,
            int row,
            out RouteFormPoses pos,
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

            if (!DecFormsParser.ColumnParser.ParseAccountColumn(
                source.GetCellText($"{ACCOUNT_COLUMN}{row}"),
                out string account,
                out message))
            {
                message = $"Ячейка \"{ACCOUNT_COLUMN}{row}\". {message}";
                return false;
            }

            if (!ColumnParser.ParseCounterNumberColumn(
                source.GetCellText($"{COUNTER_NUMBER_COLUMN}{row}"),
                out string counterNumber,
                out RouteFormCounterType counterTypeInCounterNumber,
                out message))
            {
                message = $"Ячейка \"{COUNTER_NUMBER_COLUMN}{row}\". {message}";
                return false;
            }

            if (!ColumnParser.ParseCounterTypeColumn(
                source.GetCellText($"{COUNTER_TYPE_COLUMN}{row}"),
                counterTypeInCounterNumber,
                out RouteFormCounterType counterType,
                out message))
            {
                message = $"Ячейка \"{COUNTER_TYPE_COLUMN}{row}\". {message}";
                return false;
            }

            if (!DecFormsParser.ColumnParser.ParsePrevDateColumn(
                source.GetCellText($"{PREV_DATE_COLUMN}{row}"),
                counterType == RouteFormCounterType.Norm,
                out DateTime? prevDate,
                out message))
            {
                message = $"Ячейка \"{PREV_DATE_COLUMN}{row}\". {message}";
                return false;
            }

            if (!ColumnParser.ParsePrevValueColumn(
                source.GetCellText($"{PREV_VALUE_COLUMN}{row}"),
                counterType,
                out decimal? prevValue,
                out decimal? prevDayValue,
                out decimal? prevNightValue,
                out message))
            {
                message = $"Ячейка \"{PREV_VALUE_COLUMN}{row}\". {message}";
                return false;
            }

            if (!DecFormsParser.ColumnParser.ParseOptionalColumn(
                source.GetCellText($"{OWNER_COLUMN}{row}"),
                OWNER_DB_LENGTH,
                "имени абонента",
                out string owner,
                out message))
            {
                message = $"Ячейка \"{OWNER_COLUMN}{row}\". {message}";
                return false;
            }

            if (!DecFormsParser.ColumnParser.ParseOptionalColumn(
                source.GetCellText($"{COUNTER_CAPACITY_COLUMN}{row}"),
                counterType == RouteFormCounterType.Norm,
                COUNTER_CAPACITY_DB_LENGTH,
                "разрядности счетчика",
                out string counterCapacity,
                out message))
            {
                message = $"Ячейка \"{COUNTER_CAPACITY_COLUMN}{row}\". {message}";
                return false;
            }

            if (!DecFormsParser.ColumnParser.ParseOptionalColumn(
                source.GetCellText($"{DEBT_COLUMN}{row}"),
                DEBT_DB_LENGTH,
                "задолженности",
                out string debt,
                out message))
            {
                message = $"Ячейка \"{DEBT_COLUMN}{row}\". {message}";
                return false;
            }

            if (!DecFormsParser.ColumnParser.ParseOptionalColumn(
                source.GetCellText($"{PAYED_COLUMN}{row}"),
                PAYED_DB_LENGTH,
                "даты платежа",
                out string payed,
                out message))
            {
                message = $"Ячейка \"{PAYED_COLUMN}{row}\". {message}";
                return false;
            }

            if (!DecFormsParser.ColumnParser.ParseOptionalColumn(
                source.GetCellText($"{PHONE_COLUMN}{row}"),
                PHONE_DB_LENGTH,
                "телефона",
                out string phone,
                out message))
            {
                message = $"Ячейка \"{PHONE_COLUMN}{row}\". {message}";
                return false;
            }

            if (!DecFormsParser.ColumnParser.ParseOptionalColumn(
                source.GetCellText($"{NOTE_COLUMN}{row}"),
                NOTE_DB_LENGTH,
                "примечания",
                out string note,
                out message))
            {
                message = $"Ячейка \"{NOTE_COLUMN}{row}\". {message}";
                return false;
            }

            pos =
                new RouteFormPoses()
                {
                    Account = account,
                    Apartment = apartment,
                    CounterType = (byte)counterType,
                    CounterNumber = counterNumber,
                    PrevDate = prevDate,
                    PrevValue = prevValue,
                    PrevDayValue = prevDayValue,
                    PrevNightValue = prevNightValue,
                    Owner = owner,
                    CounterCapacity = counterCapacity,
                    Debt = debt,
                    Payed = payed,
                    Phone = phone,
                    Note = note,
                };

            return true;
        }
    }
}
