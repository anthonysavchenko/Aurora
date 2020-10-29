using System;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    public static class CalculationRowHandler
    {
        public static void SetParsingError(
            CalculationRows row,
            string description)
        {
            row.ProcessingResult = (byte)RowProcessingResult.Error;
            row.ErrorDescription =
                $"Ошибка при распознавании строки файла. {description}";
        }

        public static void SetParsingError(
            CalculationRows row,
            string columnName,
            int rowNumber,
            string description)
        {
            SetParsingError(row, $"Ячейка \"{columnName}{rowNumber}\". {description}");
        }

        public static void SetParsingError(
            CalculationRows row,
            Exception exception)
        {
            Logger.SimpleWrite($"Программная ошибка при распознавании строки файла расшифровки. {exception}");

            row.ProcessingResult = (byte)RowProcessingResult.Exception;
            row.ExceptionMessage = exception.Message;
            row.ErrorDescription =
                "Программная ошибка при распознавании строки файла.";
        }

        public static void SetCheckingError(
            CalculationRows row,
            string description)
        {
            row.ProcessingResult = (byte)RowProcessingResult.Error;
            row.ErrorDescription =
                $"Ошибка при проверке строки распознанного файла. {description}";
        }
    }
}
