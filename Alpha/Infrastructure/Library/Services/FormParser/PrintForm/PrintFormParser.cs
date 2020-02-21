using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.Infrastructure.Interface.Models;
using Taumis.EnterpriseLibrary.Win.Services;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.FormParser.PrintForm
{
    static public class PrintFormParser
    {
        public const int FIRST_LINE = 14;
        const int LAST_LINE_DECRIMENT = 7;
        const int COUNTER_NUMBER_ITEMS_NUMBER = 2;
        const string ADDRESS_COLUMN = "C";
        const string COUNTER_NUMBER_COLUMN = "D";
        const string PREV_DATE_COLUMN = "H";
        const string COUNTER_TYPE_COLUMN = "I";
        const string PREV_VALUE_COLUMN = "J";

        static public bool ParseFile(ExcelSheet source, out List<Customer> customers, out string message)
        {
            message = string.Empty;
            customers = new List<Customer>();

            for (int i = FIRST_LINE; i <= source.RowsCount - LAST_LINE_DECRIMENT; i++)
            {
                if (!ParseLine(source, i, out Customer customer, out string lineMessage))
                {
                    message += $"Строка {i}. " + lineMessage + "\r\n";
                    customers = null;
                    return false;
                }
                else
                {
                    message += $"Строка {i}. Распознаны данные. " +
                        $"Адрес: \"{customer.Address.Building + ", кв. " + customer.Address.Apartment}\"; " +
                        (customer.IsNorm
                            ? $"Начисления по нормативу.\r\n"
                            : $"Дата предыдущих показаний: " +
                                $"\"{((DateTime)customer.PrevDate).ToString("dd.MM.yyyy")}\"; " +
                                (customer.Counter is SingleCounter
                                    ? $"Однотарифный счетчик: " +
                                        $"\"{(customer.Counter as SingleCounter).prevValue}\".\r\n"
                                    : $"Двухтарифный счетчик: " +
                                        $"\"{(customer.Counter as DoubleCounter).PrevDayValue}\", " +
                                        $"\"{(customer.Counter as DoubleCounter).PrevNightValue}\".\r\n"));

                    if (customers.Count != 0 && customers[0].Address.Building != customer.Address.Building)
                    {
                        message += $"Строка {i}. Распознанное название улицы и номер дома не соответствует " +
                            $"распознанному названию улицы и номеру дома в первой квартире файла. Несколько разных " +
                            $"домов в одном файле не предусмотрены форматом.\r\n";
                        customers = null;
                        return false;
                    }
                    else if (customers.Count != 0 
                        && customers.Any(c => c.Address.Apartment == customer.Address.Apartment))
                    {
                        message += $"Строка {i}. Распознанный номер квартиры уже был указан в файле ранее. " +
                            $"Дублирование номера квартиры в разных строках одного файла не предусмотрено " +
                            $"форматом.\r\n";
                        customers = null;
                        return false;
                    }
                    else
                    {
                        customers.Add(customer);
                    }
                }
            }

            return true;
        }

        static public bool ParseLine(ExcelSheet source, int line, out Customer customer, out string message)
        {
            customer = new Customer();

            try
            {
                if (!FormParser.ParseAddress(
                    source.GetCellText($"{ADDRESS_COLUMN}{line}"),
                    out Address address,
                    out message))
                {
                    message = $"Не удалось распознать значение в ячейке \"{ADDRESS_COLUMN}{line}\". " + message;
                    return false;
                }

                if (!ParseCounterNumber(
                    source.GetCellText($"{COUNTER_NUMBER_COLUMN}{line}"),
                    out bool isNorm,
                    out string counterNumber,
                    out CounterType counterTypeInCounterNumber,
                    out message))
                {
                    message = $"Не удалось распознать значение в ячейке \"{COUNTER_NUMBER_COLUMN}{line}\". " + message;
                    return false;
                }

                if (isNorm)
                {
                    customer =
                        new Customer()
                        {
                            Address = address,
                            IsNorm = isNorm,
                        };
                }
                else
                {
                    if (!FormParser.ParsePrevDate(
                        source.GetCellText($"{PREV_DATE_COLUMN}{line}"),
                        out DateTime prevDate,
                        out message))
                    {
                        message = $"Не удалось распознать значение в ячейке \"{PREV_DATE_COLUMN}{line}\". " + message;
                        return false;
                    }

                    if (!ParseCounterType(
                        source.GetCellText($"{COUNTER_TYPE_COLUMN}{line}"),
                        out CounterType counterType,
                        out message))
                    {
                        message = $"Не удалось распознать значение в ячейке \"{COUNTER_TYPE_COLUMN}{line}\". " +
                            message;
                        return false;
                    }

                    if (!ParsePrevValue(
                        source.GetCellText($"{PREV_VALUE_COLUMN}{line}"),
                        counterType,
                        out Counter counter,
                        out message))
                    {
                        message = $"Не удалось распознать значение в ячейке \"{PREV_VALUE_COLUMN}{line}\". " + message;
                        return false;
                    }

                    customer =
                        new Customer()
                        {
                            Address = address,
                            PrevDate = prevDate,
                            Counter = counter,
                        };
                }

                return true;
            }
            catch (Exception e)
            {
                Logger.SimpleWrite($"ParseLine error (line: {line}): {e}");
                message = $"Внутренняя ошибка при распознавании строки.";
                return false;
            }
        }

        static public bool ParseCounterNumber(
            string source,
            out bool isNorm,
            out string counterNumber,
            out CounterType counterType,
            out string message)
        {
            isNorm = false;
            counterNumber = string.Empty;
            counterType = CounterType.Unknown;
            message = string.Empty;

            if (source == "по нормативу")
            {
                isNorm = true;
                counterType = CounterType.Norm;
            }
            else
            {
                string[] counterNumberItems = source.Split(new char[] { ' ' });

                if (counterNumberItems.Length < COUNTER_NUMBER_ITEMS_NUMBER
                    || counterNumberItems.Length > COUNTER_NUMBER_ITEMS_NUMBER
                    || string.IsNullOrEmpty(counterNumberItems[0])
                    || (counterNumberItems[1] != "(Д/Н)"
                        && counterNumberItems[1] != "(С)"))
                {
                    message = $"Предусмотрно распознавание номера счетчика в формате: \"по нормативу\" или \"<Номер " +
                        $"счетчика> (Д/Н)\" или \"<Номер счетчика> (С)\". В данном случае данные не соответствуют " +
                        $"этому формату, поэтому не могут быть распознаны.";
                    return false;
                }

                counterNumber = counterNumberItems[0];
                counterType = counterNumberItems[1] == "(Д/Н)" ? CounterType.DayAndNight : CounterType.Common;
            }

            return true;
        }

        static public bool ParseCounterType(string source, out CounterType counterType, out string message)
        {
            counterType = CounterType.Unknown;
            message = string.Empty;

            if (source != "Д:\nН:" && source != "С:")
            {
                message = $"Предусмотрено распознавание типа счетчика в формате: \"Д:<Перенос строки>Н:\" или " +
                    $"\"С:\". В данном случае данные не соответствуют этому формату, поэтому не могут быть " +
                    $"распознаны.";
                return false;
            }

            counterType = source == "Д:\nН:" ? CounterType.DayAndNight : CounterType.Common;

            return true;
        }

        static public bool ParsePrevValue(
            string prevValueSource,
            CounterType counterType,
            out Counter counter,
            out string message)
        {
            counter = null;
            message = string.Empty;

            if (counterType == CounterType.Common)
            {
                if (!int.TryParse(prevValueSource, out int singleValue))
                {
                    message = $"Распознанный тип счетчика: однотарифный. Для однотарифного счетчика предусмотрено " +
                        $"распознавание показаний в формате одного целого числа. В данном случае данные не " +
                        $"соответствуют этому формату, поэтому не могут быть распознаны.";
                    return false;
                }
                else
                {
                    counter =
                        new SingleCounter()
                        {
                            prevValue = singleValue,
                        };
                }
            }
            else if (counterType == CounterType.DayAndNight)
            {
                string[] valueItems = prevValueSource.Split(new char[] { '\n' });

                if (valueItems.Length != 2
                    || !int.TryParse(valueItems[0], out int dayValue)
                    || !int.TryParse(valueItems[1], out int nightValue))
                {
                    message = $"Распознанный тип счетчика: двухтарифный. Для двухтарифного счетчика предусмотрено " +
                        $"распознавание показаний в формате: \"<Целое число><Перенос строки><Целое число>\". В " +
                        $"данном случае данные не соответствуют этому формату, поэтому не могут быть распознаны.";
                    return false;
                }
                else
                {
                    counter =
                        new DoubleCounter()
                        {
                            PrevDayValue = dayValue,
                            PrevNightValue = nightValue,
                        };
                }
            }

            return true;
        }
    }
}
