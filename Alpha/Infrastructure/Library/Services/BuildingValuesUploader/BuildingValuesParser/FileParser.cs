using System;
using System.Collections.Generic;
using System.IO;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.Excel;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesParser
{
    public static class FileParser
    {
        const string UNKNOWN_FILE_NAME = "Имя файла не распознано";

        const int MAX_FILE_NAME_LENTH = 100;

        public static void ParseFile(string filePath, Excel2007Worker worker, int uploadID)
        {
            int fileID =
                BuildingValuesFileHandler.CreateFile(
                    UNKNOWN_FILE_NAME,
                    uploadID);

            if (!TryParseFileName(filePath, fileID))
            {
                return;
            }

            if (!TryOpenFile(worker, filePath, fileID, out ExcelSheet sheet, out int rowsCount))
            {
                return;
            }

            if (rowsCount > 0)
            {
                if (!TryParseForm(sheet, rowsCount, worker, fileID, out List<BuildingValuesRows> rows))
                {
                    return;
                }

                if (!TryCreateForm(fileID, rows, worker))
                {
                    return;
                }
            }

            TryCloseFile(worker, fileID);
        }

        private static bool TryParseFileName(string filePath, int fileID)
        {
            try
            {
                if (!TryParseFileName(
                    filePath,
                    out string fileName,
                    out string description))
                {
                    BuildingValuesFileHandler.UpdateParsingError(
                        fileID,
                        $"Распознавание имени файла. {description}");
                    return false;
                }

                BuildingValuesFileHandler.UpdateFile(
                    fileName,
                    fileID);
                return true;
            }
            catch (Exception exception)
            {
                BuildingValuesFileHandler.UpdateParsingError(
                    fileID,
                    "Программная ошибка при распознавании имени файла.",
                    exception);
                return false;
            }
        }

        private static bool TryOpenFile(
            Excel2007Worker worker,
            string filePath,
            int fileID,
            out ExcelSheet sheet,
            out int rowsCount)
        {
            sheet = null;
            rowsCount = 0;

            try
            {
                worker.OpenFile(filePath);
                sheet = worker.GetSheet(1);
                rowsCount = sheet.RowsCount;
                return true;
            }
            catch (Exception exception)
            {
                TryCloseFile(worker, fileID);
                BuildingValuesFileHandler.UpdateParsingError(
                    fileID,
                    "Ошибка при открытии файла Excel. Убедитесь, что он не открыт в другой программе.",
                    exception);
                return false;
            }
        }

        private static bool TryCloseFile(Excel2007Worker worker, int fileID)
        {
            try
            {
                worker.Close();
                return true;
            }
            catch (Exception exception)
            {
                BuildingValuesFileHandler.UpdateParsingError(
                    fileID,
                    "Ошибка при закрытии файла Excel.",
                    exception);
                return false;
            }
        }

        private static bool TryParseForm(
            ExcelSheet sheet,
            int rowsCount,
            Excel2007Worker worker,
            int fileID,
            out List<BuildingValuesRows> rows)
        {
            int rowNumber = 1;
            rows = null;

            try
            {
                FormParser.TryParseForm(
                    sheet,
                    ref rowNumber,
                    rowsCount,
                    out rows);

                return true;
            }
            catch (Exception exception)
            {
                TryCloseFile(worker, fileID);
                BuildingValuesFileHandler.UpdateParsingError(
                    fileID,
                    $"Программная ошибка при распознавании строки файла с номером: {rowNumber}",
                    exception);
                return false;
            }
        }

        private static bool TryCreateForm(int fileID, List<BuildingValuesRows> rows, Excel2007Worker worker)
        {
            try
            {
                BuildingValuesFormHandler.CreateForm(fileID, rows);
                return true;
            }
            catch (Exception exception)
            {
                TryCloseFile(worker, fileID);
                BuildingValuesFileHandler.UpdateParsingError(
                    fileID,
                    $"Программная ошибка при сохранении распознанного файла",
                    exception);
                return false;
            }
        }

        private static bool TryParseFileName(
            string filePath,
            out string fileName,
            out string description)
        {
            fileName = UNKNOWN_FILE_NAME;
            description = null;

            string value = Path.GetFileName(filePath);

            if (value.Length > MAX_FILE_NAME_LENTH)
            {
                description = $"Прочитано значение: \"{value}\". Предусмотрено сохранение имени файла в формате " +
                    $"строки, которая содержит не более {MAX_FILE_NAME_LENTH} символов. В данном случае это " +
                    "ограничение превышено, поэтому файл не может быть сохранен.";
                return false;
            }

            fileName = value;

            return true;
        }
    }
}
