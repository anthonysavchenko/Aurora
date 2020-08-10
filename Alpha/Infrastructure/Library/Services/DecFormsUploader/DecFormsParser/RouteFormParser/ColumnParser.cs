﻿using System.Text.RegularExpressions;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsUploader.DecFormsParser.RouteFormParser
{
    static public class ColumnParser
    {
        const int COUNTER_NUMBER_ITEMS_COUNT = 2;

        const int COUNTER_NUMBER_DB_LENGTH = 25;
        const string COUNTER_TYPE_NORM_VALUE = "по нормативу";

        static public bool ParseCounterNumberColumn(
            string source,
            out string counterNumber,
            out RouteFormCounterType counterType,
            out string message)
        {
            counterNumber = null;
            counterType = RouteFormCounterType.Unknown;
            message = null;

            string sourceNoCR = source != null ? source.Replace("\n", " ").Trim() : source;

            if (string.IsNullOrWhiteSpace(sourceNoCR))
            {
                message = "Ячейка обязательно должна быть заполнена. В данном случае она пустая.";
                return false;
            }

            if (sourceNoCR == COUNTER_TYPE_NORM_VALUE)
            {
                counterType = RouteFormCounterType.Norm;
            }
            else
            {
                string[] counterNumberItems = sourceNoCR.Split(new char[] { ' ' });

                string numberSubstring =
                    counterNumberItems.Length >= COUNTER_NUMBER_ITEMS_COUNT
                        ? string.Join(" ", counterNumberItems, 0, counterNumberItems.Length - 1).Trim()
                        : null;

                string typeSubstring =
                    counterNumberItems.Length >= COUNTER_NUMBER_ITEMS_COUNT
                        ? counterNumberItems[counterNumberItems.Length - 1]
                        : null;

                if (counterNumberItems.Length < COUNTER_NUMBER_ITEMS_COUNT
                    || string.IsNullOrEmpty(numberSubstring)
                    || (typeSubstring != "(Д/Н)"
                        && typeSubstring != "(С)"))
                {
                    message = $"Прочитано значение: \"{source.Replace("\n", "<Перенос строки>")}\". " +
                        "Предусмотрно распознавание номера счетчика в формате: \"по нормативу\" или \"<Номер " +
                        "счетчика> (Д/Н)\" или \"<Номер счетчика> (С)\". В данном случае данные не соответствуют " +
                        "этому формату, поэтому не могут быть распознаны.";
                    return false;
                }

                counterNumber = numberSubstring;

                if (counterNumber.Length > COUNTER_NUMBER_DB_LENGTH)
                {
                    message = $"Распознано значение: \"{counterNumber}\". " +
                        $"Предусмотрено сохранение номера счетчика длиной не более {COUNTER_NUMBER_DB_LENGTH} " +
                        "символов. В данном случае это ограничение превышено, поэтому данные не могут быть сохранены.";
                    return false;
                }

                counterType = typeSubstring == "(Д/Н)"
                    ? RouteFormCounterType.DayAndNight
                    : RouteFormCounterType.Common;
            }

            return true;
        }

        static public bool ParseCounterTypeColumn(
            string source,
            RouteFormCounterType counterTypeInCounterNumber,
            out RouteFormCounterType counterType,
            out string message)
        {
            counterType = RouteFormCounterType.Unknown;
            message = null;

            if (counterTypeInCounterNumber == RouteFormCounterType.Norm)
            {
                counterType = RouteFormCounterType.Norm;
            }
            else
            {
                string sourceNoCR = source != null ? source.Replace("\n", " ").Trim() : source;

                if (string.IsNullOrWhiteSpace(sourceNoCR))
                {
                    message = "Ячейка обязательно должна быть заполнена, если не указано начисление по нормативу. " +
                        "В данном случае начисление не по нормативу, а ячейка пустая.";
                    return false;
                }

                if (sourceNoCR != "Д: Н:" && sourceNoCR != "С:")
                {
                    message = $"Прочитано значение: \"{source.Replace("\n", "<Перенос строки>")}\". " +
                        "Предусмотрено распознавание типа счетчика в формате: \"Д:<Перенос строки>Н:\" или " +
                        "\"С:\". В данном случае данные не соответствуют этому формату, поэтому не могут быть " +
                        "распознаны.";
                    return false;
                }

                counterType = sourceNoCR == "Д: Н:" ? RouteFormCounterType.DayAndNight : RouteFormCounterType.Common;

                if (counterType != counterTypeInCounterNumber)
                {
                    string counterTypeString =
                        counterTypeInCounterNumber == RouteFormCounterType.Common
                            ? "однотарифный"
                            : "двухтарифный";
                    message = $"Прочитано значение: \"{source.Replace("\n", "<Перенос строки>")}\". " +
                        "Указанный тип счетчика не соответствует типу счетчика, который был указан ранее в " +
                        $"колонке с номером счетчика ({counterTypeString}), поэтому данные не могут быть распознаны " +
                        $"однозначно.";
                    return false;
                }
            }

            return true;
        }

        static public bool ParsePrevValueColumn(
            string source,
            RouteFormCounterType counterType,
            out decimal? prevValue,
            out decimal? prevDayValue,
            out decimal? prevNightValue,
            out string message)
        {
            prevValue = null;
            prevDayValue = null;
            prevNightValue = null;
            message = null;

            if (counterType != RouteFormCounterType.Norm)
            {
                string sourceNoCR = source != null ? source.Replace("\n", " ").Trim() : source;

                if (string.IsNullOrWhiteSpace(sourceNoCR))
                {
                    message = "Ячейка обязательно должна быть заполнена, если не указано начисление по нормативу. " +
                        "В данном случае начисление не по нормативу, а ячейка пустая.";
                    return false;
                }

                if (counterType == RouteFormCounterType.Common)
                {
                    if (!Regex.IsMatch(sourceNoCR, @"^\d{1,6}(,\d{1,2})?$")
                        || !decimal.TryParse(sourceNoCR, out decimal commonValue))
                    {
                        message = $"Прочитано значение: \"{source.Replace("\n", "<Перенос строки>")}\". " +
                            "Распознанный тип счетчика: однотарифный. Для однотарифного счетчика предусмотрено " +
                            "распознавание показаний в формате одного положительного десятичного числа, " +
                            "которое содержит не более 6 цифр до зяпятой и не более 2 цифр после запятой. В " +
                            "данном случае данные не соответствуют этому формату, поэтому не могут быть распознаны.";
                        return false;
                    }
                    else
                    {
                        prevValue = commonValue;
                    }
                }
                else if (counterType == RouteFormCounterType.DayAndNight)
                {
                    string[] valueItems = sourceNoCR.Split(new char[] { ' ' });

                    if (valueItems.Length != 2
                        || !Regex.IsMatch(valueItems[0], @"^\d{1,6}(,\d{1,2})?$")
                        || !decimal.TryParse(valueItems[0], out decimal dayValue)
                        || !Regex.IsMatch(valueItems[1], @"^\d{1,6}(,\d{1,2})?$")
                        || !decimal.TryParse(valueItems[1], out decimal nightValue))
                    {
                        message = $"Прочитано значение: \"{source.Replace("\n", "<Перенос строки>")}\". " +
                            "Распознанный тип счетчика: двухтарифный. Для двухтарифного счетчика предусмотрено " +
                            "распознавание показаний в формате двух положительных десятичных чисел, разделенных " +
                            "переносом строки, каждое из которых содержит не более 6 цифр до запятой и не более " +
                            "2 цифр после запятой. В данном случае данные не соответствуют этому формату, " +
                            "поэтому не могут быть распознаны.";
                        return false;
                    }
                    else
                    {
                        prevDayValue = dayValue;
                        prevNightValue = nightValue;
                    }
                }
            }

            return true;
        }
    }
}
