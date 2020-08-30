using System.Text.RegularExpressions;

namespace Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesParser.BuildingValuesFormParser
{
    static public class ColumnParser
    {
        const int COUNTER_NUMBER_DB_LENGTH = 25;

        static public bool ParseCounterNumberColumn(
            string source,
            out string counterNumber,
            out string message)
        {
            counterNumber = null;
            message = null;

            string sourceNoCR = source != null ? source.Replace("\n", " ").Trim() : source;

            if (string.IsNullOrWhiteSpace(sourceNoCR))
            {
                message = "Ячейка обязательно должна быть заполнена. В данном случае она пустая.";
                return false;
            }

            counterNumber = sourceNoCR;

            if (counterNumber.Length > COUNTER_NUMBER_DB_LENGTH)
            {
                message = $"Распознано значение: \"{counterNumber}\". " +
                    $"Предусмотрено сохранение номера счетчика длиной не более {COUNTER_NUMBER_DB_LENGTH} " +
                    "символов. В данном случае это ограничение превышено, поэтому данные не могут быть сохранены.";
                return false;
            }

            return true;
        }

        static public bool ParseCurrentValueColumn(
            string source,
            out decimal? currentValue,
            out string message)
        {
            currentValue = null;
            message = null;

            string sourceNoCR = source != null ? source.Replace("\n", " ").Trim() : source;

            if (!string.IsNullOrWhiteSpace(sourceNoCR))
            {
                if (!Regex.IsMatch(sourceNoCR, @"^\d{1,8}(,\d{1,3})?$")
                    || !decimal.TryParse(sourceNoCR, out decimal value))
                {
                    message = $"Прочитано значение: \"{source.Replace("\n", "<Перенос строки>")}\". " +
                        $"Предусмотрено распознавание показаний в формате одного положительного десятичного числа, " +
                        "которое содержит не более 8 цифр до запятой и не более 3 цифр после запятой. В " +
                        "данном случае данные не соответствуют этому формату, поэтому не могут быть распознаны.";
                    return false;
                }
                else
                {
                    currentValue = value;
                }
            }

            return true;
        }

        static public bool ParsePrevValueColumn(
            string source,
            out decimal? prevValue,
            out string message)
        {
            prevValue = null;
            message = null;

            string sourceNoCR = source != null ? source.Replace("\n", " ").Trim() : source;

            if (!string.IsNullOrWhiteSpace(sourceNoCR))
            {
                if (!Regex.IsMatch(sourceNoCR, @"^\d{1,8}(,\d{1,3})?$")
                    || !decimal.TryParse(sourceNoCR, out decimal value))
                {
                    message = $"Прочитано значение: \"{source.Replace("\n", "<Перенос строки>")}\". " +
                        $"Предусмотрено распознавание показаний в формате одного положительного десятичного числа, " +
                        "которое содержит не более 8 цифр до запятой и не более 3 цифр после запятой. В " +
                        "данном случае данные не соответствуют этому формату, поэтому не могут быть распознаны.";
                    return false;
                }
                else
                {
                    prevValue = value;
                }
            }

            return true;
        }

        static public bool ParseCoefficientColumn(
            string source,
            out byte? coefficient,
            out string message)
        {
            coefficient = null;
            message = null;

            string sourceNoCR = source != null ? source.Replace("\n", " ").Trim() : source;

            if (string.IsNullOrWhiteSpace(sourceNoCR))
            {
                message = "Ячейка обязательно должна быть заполнена. В данном случае она пустая.";
                return false;
            }

            if (!byte.TryParse(sourceNoCR, out byte value) || value < 1 || value > 100)
            {
                message = $"Прочитано значение: \"{source.Replace("\n", "<Перенос строки>")}\". " +
                    $"Предусмотрено распознавание коэффициента в формате целого числа от 1 до 100. " +
                    "В данном случае данные не соответствуют этому формату, поэтому не могут быть распознаны.";
                return false;
            }
            else
            {
                coefficient = value;
            }

            return true;
        }
    }
}
