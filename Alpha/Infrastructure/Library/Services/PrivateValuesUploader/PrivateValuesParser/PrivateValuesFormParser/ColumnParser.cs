using System;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.Infrastructure.Library.Services.PrivateValuesUploader.PrivateValuesParser.PrivateValuesFormParser
{
    static public class ColumnParser
    {
        const int COUNTER_NUMBER_DB_LENGTH = 25;
        const int COUNTER_MODEL_DB_LENGTH = 50;
        const string COUNTER_TYPE_NORM_VALUE = "<по нормативу>";

        static public bool ParseCounterModelColumn(
            string source,
            out string counterModel,
            out PrivateFormCounterType counterType,
            out string message)
        {
            counterModel = null;
            counterType = PrivateFormCounterType.Unknown;
            message = null;

            string sourceNoCR = source != null ? source.Replace("\n", " ").Trim() : source;

            if (string.IsNullOrWhiteSpace(sourceNoCR))
            {
                message = "Ячейка обязательно должна быть заполнена. В данном случае она пустая.";
                return false;
            }

            if (sourceNoCR == COUNTER_TYPE_NORM_VALUE)
            {
                counterType = PrivateFormCounterType.Norm;
            }
            else
            {
                counterModel = sourceNoCR;

                if (counterModel.Length > COUNTER_MODEL_DB_LENGTH)
                {
                    message = $"Распознано значение: \"{counterModel}\". " +
                        $"Предусмотрено сохранение модели счетчика длиной не более {COUNTER_MODEL_DB_LENGTH} " +
                        "символов. В данном случае это ограничение превышено, поэтому данные не могут быть сохранены.";
                    return false;
                }
            }

            return true;
        }

        static public bool ParseCounterNumberColumn(
            string source,
            bool isNorm,
            out string counterNumber,
            out string message)
        {
            counterNumber = null;
            message = null;

            if (!isNorm)
            {
                string sourceNoCR = source != null ? source.Replace("\n", " ").Trim() : source;

                if (string.IsNullOrWhiteSpace(sourceNoCR))
                {
                    message = "Ячейка обязательно должна быть заполнена, если не указано начисление по нормативу. " +
                        "В данном случае начисление не по нормативу, а ячейка пустая.";
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
            }

            return true;
        }

        static public bool ParseCounterTypeColumn(
            string source,
            bool isNorm,
            out PrivateFormCounterType counterType,
            out string message)
        {
            counterType = PrivateFormCounterType.Unknown;
            message = null;

            if (isNorm)
            {
                counterType = PrivateFormCounterType.Norm;
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

                if (!Enum.TryParse(sourceNoCR, out counterType)
                    || counterType == PrivateFormCounterType.Unknown
                    || counterType == PrivateFormCounterType.Norm)
                {
                    message = $"Прочитано значение: \"{source.Replace("\n", "<Перенос строки>")}\". " +
                        "Предусмотрено распознавание типа счетчика в формате целого числа от 1 до 3." +
                        "В данном случае данные не соответствуют этому формату, поэтому не могут быть распознаны.";
                    return false;
                }
            }

            return true;
        }

        static public bool ParseCurrentValueColumn(
            string source,
            PrivateFormCounterType counterType,
            out int? currentValue,
            out int? currentDayValue,
            out int? currentNightValue,
            out string message)
        {
            currentValue = null;
            currentDayValue = null;
            currentNightValue = null;
            message = null;

            if (counterType != PrivateFormCounterType.Norm)
            {
                string sourceNoCR = source != null ? source.Replace("\n", " ").Trim() : source;

                if (!string.IsNullOrWhiteSpace(sourceNoCR))
                {
                    if (!int.TryParse(sourceNoCR, out int value) || value < 0 || value > 99999999)
                    {
                        message = $"Прочитано значение: \"{source.Replace("\n", "<Перенос строки>")}\". " +
                            $"Предусмотрено распознавание показаний в формате одного положительного целого числа, " +
                            $"которое содержит не более 8 цифр. В данном случае данные не соответствуют этому " +
                            $"формату, поэтому не могут быть распознаны.";
                        return false;
                    }

                    if (counterType == PrivateFormCounterType.Common)
                    {
                        currentValue = value;
                    }
                    else if (counterType == PrivateFormCounterType.Day)
                    {
                        currentDayValue = value;
                    }
                    else if (counterType == PrivateFormCounterType.Night)
                    {
                        currentNightValue = value;
                    }
                }
            }

            return true;
        }
    }
}
