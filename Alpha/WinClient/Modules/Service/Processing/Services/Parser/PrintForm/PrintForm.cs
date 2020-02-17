using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Processing.Services.Parser.Models;
using Taumis.EnterpriseLibrary.Win.Services;
using static Taumis.Alpha.WinClient.Aurora.Modules.Service.Processing.Services.Excel2007Worker;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Processing.Services.Parser.PrintForm
{
    static public class PrintForm
    {
        public const int FIRST_LINE = 14;
        const int LAST_LINE_DECRIMENT = 7;
        const string ADDRESS_COLUMN = "C";
        const string PREV_DATE_COLUMN = "H";
        const string COUNTER_TYPE_COLUMN = "I";
        const string PREV_VALUE_COLUMN = "J";

        static public bool ParseFile(ExcelSheet source, out List<Customer> customers, out string message)
        {
            message = string.Empty;
            customers = new List<Customer>();

            for (int i = FIRST_LINE; i <= source.RowsCount - LAST_LINE_DECRIMENT; i++)
            {
                // Пропуск квартир по нормативу. Временно.
                if (i != FIRST_LINE && source.GetCellText($"D{i}") == "по нормативу") continue;

                if (!ParseLine(source, i, out Customer customer, out string lineMessage))
                {
                    message += $"Строка {i}. " + lineMessage + "\r\n";
                    customers = null;
                    return false;
                }
                else
                {
                    message += $"Строка {i}. Распознаны данные. " +
                        $"Адрес: \"{customer.Address.Building + ", " + customer.Address.Apartment}\"; " +
                        $"Дата предыдущих показаний: \"{customer.PrevDate.ToString("dd.MM.yyyy")}\"; " +
                        (customer.Counter is SingleCounter
                            ? $"Однотарифный счетчик: \"{(customer.Counter as SingleCounter).prevValue}\".\r\n"
                            : $"Двухтарифный счетчик: \"{(customer.Counter as DoubleCounter).PrevDayValue}\", " +
                                $"\"{(customer.Counter as DoubleCounter).PrevNightValue}\".\r\n");

                    if (customers.Count != 0 && customers[0].Address.Building != customer.Address.Building)
                    {
                        message += $"Строка {i}. Распознанное название улицы и номер дома не соответствует распознанному названию улицы и номеру дома в" +
                            $" первой квартире файла. Несколько разных домов в одном файле не предусмотрены форматом.\r\n";
                        customers = null;
                        return false;
                    }
                    else if (customers.Count != 0 && customers.Any(c => c.Address.Apartment == customer.Address.Apartment))
                    {
                        message += $"Строка {i}. Распознанный номер квартиры уже был указан в файле ранее. Дублирование номера квартиры в разных строках " +
                            $"одного файла не предусмотрено форматом.\r\n";
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
            customer = null;

            try
            {
                if (!Form.ParseAddress(source.GetCellText($"{ADDRESS_COLUMN}{line}"), out Address address, out message))
                {
                    message = $"Не удалось распознать значение в ячейке \"{ADDRESS_COLUMN}{line}\". " + message;
                    return false;
                }

                if (!Form.ParsePrevDate(source.GetCellText($"{PREV_DATE_COLUMN}{line}"), out DateTime prevDate, out message))
                {
                    message = $"Не удалось распознать значение в ячейке \"{PREV_DATE_COLUMN}{line}\". " + message;
                    return false;
                }

                if (!ParseCounterType(source.GetCellText($"{COUNTER_TYPE_COLUMN}{line}"), out CounterType counterType, out message))
                {
                    message = $"Не удалось распознать значение в ячейке \"{COUNTER_TYPE_COLUMN}{line}\". " + message;
                    return false;
                }

                if (!ParsePrevValue(source.GetCellText($"{PREV_VALUE_COLUMN}{line}"), counterType, out Counter counter, out message))
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

                return true;
            }
            catch (Exception e)
            {
                Logger.SimpleWrite($"ParseLine error (line: {line}): {e}");
                message = $"Внутренняя ошибка при распознавании строки.";
                return false;
            }
        }

        static public bool ParseCounterType(string source, out CounterType counterType, out string message)
        {
            counterType = CounterType.Unknown;
            message = string.Empty;

            string normalizedSource = source.Replace(":", "").Replace("\n", "").ToUpper();

            if (normalizedSource != "ДН" && normalizedSource != "С")
            {
                message = $"Предусмотрено распознавание типа счетчика в формате: \"Д:<Перенос строки>Н:\" или \"С:\"." +
                    $"В данном случае данные не соответствуют этому формату, поэтому не могут быть распознаны.";
                return false;
            }

            counterType = normalizedSource == "ДН" ? CounterType.DayAndNight : CounterType.Common;

            return true;
        }

        static public bool ParsePrevValue(string prevValueSource, CounterType counterType, out Counter counter, out string message)
        {
            counter = null;
            message = string.Empty;

            if (counterType == CounterType.Common)
            {
                if (!int.TryParse(prevValueSource, out int singleValue))
                {
                    message = $"Распознанный тип счетчика: однотарифный. Для однотарифного счетчика предусмотрено распознавание показаний в формате одного " +
                        $"целого числа. В данном случае данные не соответствуют этому формату, поэтому не могут быть распознаны.";
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
                string[] valueItems = prevValueSource.Split(new string[] { "\n" }, StringSplitOptions.None);

                if (valueItems.Length != 2
                    || !int.TryParse(valueItems[0], out int dayValue)
                    || !int.TryParse(valueItems[1], out int nightValue))
                {
                    message = $"Распознанный тип счетчика: двухтарифный. Для двухтарифного счетчика предусмотрено распознавание показаний в формате: " +
                        $"\"<Целое число><Перенос строки><Целое число>\". В данном случае данные не соответствуют этому формату, поэтому не могут быть " +
                        $"распознаны.";
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
