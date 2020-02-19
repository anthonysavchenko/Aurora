using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.Infrastructure.Library.Services.FormParser.Models;
using Taumis.EnterpriseLibrary.Win.Services;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.FormParser.FillForm
{
    static public class FillFormParser
    {
        public const int FIRST_LINE = 1;
        const string ADDRESS_COLUMN = "E";
        const string COUNTER_TYPE_COLUMN = "I";
        const string PREV_DATE_COLUMN = "J";
        const string PREV_VALUE_COLUMN = "K";

        static public bool ParseFile(ExcelSheet source, out List<Customer> customers, out string message)
        {
            message = string.Empty;
            customers = new List<Customer>();

            for (int i = FIRST_LINE; i <= source.RowsCount; i++)
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
                    else
                    {
                        Customer existed = customers.FirstOrDefault(c => c.Address.Apartment == customer.Address.Apartment);

                        if (existed != null)
                        {
                            if (!(customer.Counter is DoubleCounter
                                && (customer.Counter as DoubleCounter).PrevDayValue == null
                                && (customer.Counter as DoubleCounter).PrevNightValue != null
                                && existed.Counter is DoubleCounter
                                && (existed.Counter as DoubleCounter).PrevDayValue != null
                                && (existed.Counter as DoubleCounter).PrevNightValue == null))
                            {
                                message += $"Строка {i}. Распознанный номер квартиры уже был указан в файле ранее. Дублирование номера квартиры в разных " +
                                    $"строках одного файла предусмотрено только для указания данных для двухтарифного счетчика: на первой строке - дневные " +
                                    $"данные, на второй строке - ночные. В данном случае это не так.\r\n";
                                customers = null;
                                return false;
                            }
                            else
                            {
                                (existed.Counter as DoubleCounter).PrevNightValue = (customer.Counter as DoubleCounter).PrevNightValue;
                            }
                        }
                        else
                        {
                            customers.Add(customer);
                        }
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
                if (!FormParser.ParseAddress(source.GetCellText($"{ADDRESS_COLUMN}{line}"), out Address address, out message))
                {
                    message = $"Не удалось распознать значение в ячейке \"{ADDRESS_COLUMN}{line}\". " + message;
                    return false;
                }

                if (!ParseCounterType(source.GetCellText($"{COUNTER_TYPE_COLUMN}{line}"), out CounterType counterType, out message))
                {
                    message = $"Не удалось распознать значение в ячейке \"{COUNTER_TYPE_COLUMN}{line}\". " + message;
                    return false;
                }

                if (!FormParser.ParsePrevDate(source.GetCellText($"{PREV_DATE_COLUMN}{line}"), out DateTime prevDate, out message))
                {
                    message = $"Не удалось распознать значение в ячейке \"{PREV_DATE_COLUMN}{line}\". " + message;
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
                message = $"Внутренняя ошибка при распознавании строки {line}.";
                return false;
            }
        }

        static public bool ParseCounterType(string source, out CounterType counterType, out string message)
        {
            counterType = CounterType.Unknown;
            message = string.Empty;

            if (!Enum.TryParse(source, out counterType) || counterType == CounterType.Unknown)
            {
                message = "Предусмотрено распознавание типа счетчика в формате целого числа от 1 до 3." +
                    "В данном случае данные не соответствуют этому формату, поэтому не могут быть распознаны.";
                return false;
            }

            return true;
        }

        static public bool ParsePrevValue(string prevValueSource, CounterType counterType, out Counter counter, out string message)
        {
            counter = null;
            message = string.Empty;

            if (!int.TryParse(prevValueSource, out int value))
            {
                message = $"Предусмотрено распознавание показаний в формате целого числа. " +
                    $"В данном случае данные не соответствуют этому формату, поэтому не могут быть распознаны.";
                return false;
            }

            if (counterType == CounterType.Common)
            {
                counter =
                    new SingleCounter()
                    {
                        prevValue = value,
                    };
            }
            else if (counterType == CounterType.Day)
            {
                counter =
                    new DoubleCounter()
                    {
                        PrevDayValue = value,
                    };
            }
            else if (counterType == CounterType.Night)
            {
                counter =
                    new DoubleCounter()
                    {
                        PrevNightValue = value,
                    };
            }

            return true;
        }
    }
}
