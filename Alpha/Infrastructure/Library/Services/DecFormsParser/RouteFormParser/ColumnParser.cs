using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsParser.RouteFormParser
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

                if (counterNumberItems.Length != COUNTER_NUMBER_ITEMS_COUNT
                    || string.IsNullOrEmpty(counterNumberItems[0])
                    || (counterNumberItems[1] != "(Д/Н)"
                        && counterNumberItems[1] != "(С)"))
                {
                    message = $"Прочитано значение: \"{source.Replace("\n", "<Перенос строки>")}\". " +
                        "Предусмотрно распознавание номера счетчика в формате: \"по нормативу\" или \"<Номер " +
                        "счетчика> (Д/Н)\" или \"<Номер счетчика> (С)\". В данном случае данные не соответствуют " +
                        "этому формату, поэтому не могут быть распознаны.";
                    return false;
                }

                counterNumber = counterNumberItems[0];

                if (counterNumber.Length > COUNTER_NUMBER_DB_LENGTH)
                {
                    message = $"Распознано значение: \"{counterNumber}\". " +
                        $"Предусмотрено сохранение номера счетчика длиной не более {COUNTER_NUMBER_DB_LENGTH} " +
                        "символов. В данном случае это ограничение превышено, поэтому данные не могут быть сохранены.";
                    return false;
                }

                counterType = counterNumberItems[1] == "(Д/Н)"
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
            out int? prevValue,
            out int? prevDayValue,
            out int? prevNightValue,
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
                    if (!int.TryParse(sourceNoCR, out int commonValue) || commonValue < 0)
                    {
                        message = $"Прочитано значение: \"{source.Replace("\n", "<Перенос строки>")}\". " +
                            "Распознанный тип счетчика: однотарифный. Для однотарифного счетчика предусмотрено " +
                            $"распознавание показаний в формате одного целого числа от 0 до {int.MaxValue}. В " +
                            $"данном случае данные не соответствуют этому формату, поэтому не могут быть распознаны.";
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
                        || !int.TryParse(valueItems[0], out int dayValue)
                        || dayValue < 0
                        || !int.TryParse(valueItems[1], out int nightValue)
                        || nightValue < 0)
                    {
                        message = $"Прочитано значение: \"{source.Replace("\n", "<Перенос строки>")}\". " +
                            "Распознанный тип счетчика: двухтарифный. Для двухтарифного счетчика предусмотрено " +
                            "распознавание показаний в формате: \"<Целое число><Перенос строки><Целое число>\". " +
                            $"Целые числа должны находится в диапазоне от 0 до {int.MaxValue}. В данном случае " +
                            "данные не соответствуют этому формату, поэтому не могут быть распознаны.";
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
