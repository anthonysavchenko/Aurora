using System;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    public static class BuildingValuesRowHandler
    {
        public static void SetParsingError(
            BuildingValuesRows row,
            string description)
        {
            row.ProcessingResult = (byte)RowProcessingResult.Error;
            row.ErrorDescription = $"Ошибка при распознавании строки файла. {description}";
        }

        public static void SetParsingError(
            BuildingValuesRows row,
            string columnName,
            int rowNumber,
            string description)
        {
            SetParsingError(row, $"Ячейка \"{columnName}{rowNumber}\". {description}");
        }

        public static void SetParsingError(
            BuildingValuesRows row,
            Exception exception)
        {
            Logger.SimpleWrite($"Программная ошибка при распознавании строки файла c показаниями ОДПУ. {exception}");

            row.ProcessingResult = (byte)RowProcessingResult.Exception;
            row.ExceptionMessage = exception.Message;
            row.ErrorDescription = "Программная ошибка при распознавании строки файла.";
        }

        public static void SetCheckingError(
            BuildingValuesRows row,
            string description)
        {
            row.ProcessingResult = (byte)RowProcessingResult.Error;
            row.ErrorDescription = $"Ошибка при проверке строки распознанного файла. {description}";
        }
    }
}
