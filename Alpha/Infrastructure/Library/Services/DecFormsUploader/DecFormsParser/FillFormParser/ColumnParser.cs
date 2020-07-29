using System;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsUploader.DecFormsParser.FillFormParser
{
    static public class ColumnParser
    {
        const int COUNTER_NUMBER_DB_LENGTH = 25;
        const int COUNTER_MODEL_DB_LENGTH = 50;
        const string COUNTER_TYPE_NORM_VALUE = "<по нормативу>";

        static public bool ParseCounterModelColumn(
            string source,
            out string counterModel,
            out FillFormCounterType counterType,
            out string message)
        {
            counterModel = null;
            counterType = FillFormCounterType.Unknown;
            message = null;

            string sourceNoCR = source != null ? source.Replace("\n", " ").Trim() : source;

            if (string.IsNullOrWhiteSpace(sourceNoCR))
            {
                message = "Ячейка обязательно должна быть заполнена. В данном случае она пустая.";
                return false;
            }

            if (sourceNoCR == COUNTER_TYPE_NORM_VALUE)
            {
                counterType = FillFormCounterType.Norm;
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
            out FillFormCounterType counterType,
            out string message)
        {
            counterType = FillFormCounterType.Unknown;
            message = null;

            if (isNorm)
            {
                counterType = FillFormCounterType.Norm;
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
                    || counterType == FillFormCounterType.Unknown
                    || counterType == FillFormCounterType.Norm)
                {
                    message = $"Прочитано значение: \"{source.Replace("\n", "<Перенос строки>")}\". " +
                        "Предусмотрено распознавание типа счетчика в формате целого числа от 1 до 3." +
                        "В данном случае данные не соответствуют этому формату, поэтому не могут быть распознаны.";
                    return false;
                }
            }

            return true;
        }

        static public bool ParsePrevValueColumn(
            string source,
            FillFormCounterType counterType,
            out int? prevValue,
            out int? prevDayValue,
            out int? prevNightValue,
            out string message)
        {
            prevValue = null;
            prevDayValue = null;
            prevNightValue = null;
            message = null;

            if (counterType != FillFormCounterType.Norm)
            {
                string sourceNoCR = source != null ? source.Replace("\n", " ").Trim() : source;

                if (string.IsNullOrWhiteSpace(sourceNoCR))
                {
                    message = "Ячейка обязательно должна быть заполнена, если не указано начисление по нормативу. " +
                        "В данном случае начисление не по нормативу, а ячейка пустая.";
                    return false;
                }

                if (!int.TryParse(sourceNoCR, out int value) || value < 0)
                {
                    message = $"Прочитано значение: \"{source.Replace("\n", "<Перенос строки>")}\". " +
                        $"Предусмотрено распознавание показаний в формате целого числа от 0 до {int.MaxValue}. " +
                        "В данном случае данные не соответствуют этому формату, поэтому не могут быть распознаны.";
                    return false;
                }

                if (counterType == FillFormCounterType.Common)
                {
                    prevValue = value;
                }
                else if (counterType == FillFormCounterType.Day)
                {
                    prevDayValue = value;
                }
                else if (counterType == FillFormCounterType.Night)
                {
                    prevNightValue = value;
                }
            }

            return true;
        }
    }
}
