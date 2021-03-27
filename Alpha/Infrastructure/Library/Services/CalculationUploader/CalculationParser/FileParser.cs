using System;
using System.Collections.Generic;
using System.IO;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Constants;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.FormParsers;
using Taumis.Alpha.Infrastructure.Library.Services.Excel;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser
{
    public static class FileParser
    {
        const string UNKNOWN_FILE_NAME = "Имя файла не распознано";

        const int MAX_FILE_NAME_LENTH = 100;

        public static void ParseFile(string filePath, Excel2007Worker worker, int uploadID, DateTime month)
        {
            int fileID =
                CalculationFileHandler.CreateFile(
                    UNKNOWN_FILE_NAME,
                    BuildingContract.Unknown,
                    uploadID);

            if (!TryParseFileNameAndContract(filePath, fileID))
            {
                return;
            }

            if (!TryOpenFile(worker, filePath, fileID, out ExcelSheet sheet, out int rowsCount))
            {
                return;
            }

            if (rowsCount > 0)
            {
                if (!TryParseForm(month, sheet, rowsCount, worker, fileID, out List<CalculationRows> rows))
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

        private static bool TryParseFileNameAndContract(string filePath, int fileID)
        {
            try
            {
                if (!TryParseFileName(
                    filePath,
                    out string fileName,
                    out string description))
                {
                    CalculationFileHandler.UpdateParsingError(
                        fileID,
                        $"Распознавание имени файла. {description}");
                    return false;
                }

                CalculationFileHandler.UpdateFile(
                    fileName,
                    BuildingContract.Unknown,
                    fileID);

                if (!TryParseBuildingContract(
                    fileName,
                    out BuildingContract contract,
                    out description))
                {
                    CalculationFileHandler.UpdateParsingError(
                        fileID,
                        $"Распознавание номера договора. {description}");
                    return false;
                }

                CalculationFileHandler.UpdateFile(
                    fileName,
                    contract,
                    fileID);
                return true;
            }
            catch (Exception exception)
            {
                CalculationFileHandler.UpdateParsingError(
                    fileID,
                    "Программная ошибка при распознавании имени файла и номера договора.",
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
                CalculationFileHandler.UpdateParsingError(
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
                CalculationFileHandler.UpdateParsingError(
                    fileID,
                    "Ошибка при закрытии файла Excel.",
                    exception);
                return false;
            }
        }

        private static bool TryParseForm(
            DateTime month,
            ExcelSheet sheet,
            int rowsCount,
            Excel2007Worker worker,
            int fileID,
            out List<CalculationRows> rows)
        {
            int rowNumber = 1;
            rows = null;

            try
            {
                if (month >= Constants.SINCE_012021)
                {
                    Since012021FormParser.TryParseForm(
                        sheet,
                        ref rowNumber,
                        rowsCount,
                        out rows);
                }
                else
                {
                    Till012021FormParser.TryParseForm(
                        sheet,
                        ref rowNumber,
                        rowsCount,
                        out rows);
                }

                return true;
            }
            catch (Exception exception)
            {
                TryCloseFile(worker, fileID);
                CalculationFileHandler.UpdateParsingError(
                    fileID,
                    $"Программная ошибка при распознавании строки файла с номером: {rowNumber + 1}",
                    exception);
                return false;
            }
        }

        private static bool TryCreateForm(int fileID, List<CalculationRows> rows, Excel2007Worker worker)
        {
            try
            {
                CalculationFormHandler.CreateForm(fileID, rows);
                return true;
            }
            catch (Exception exception)
            {
                TryCloseFile(worker, fileID);
                CalculationFileHandler.UpdateParsingError(
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

        private static bool TryParseBuildingContract(
            string fileName,
            out BuildingContract contract,
            out string description)
        {
            contract = BuildingContract.Unknown;
            description = null;

            if (fileName.Contains(BuildingContractNames.CONTRACT_6784))
            {
                contract = BuildingContract.Contract6784;
            }
            else if (fileName.Contains(BuildingContractNames.CONTRACT_15297))
            {
                contract = BuildingContract.Contract15297;
            }
            else
            {
                description =
                    $"Прочитано значение: \"{fileName}\". Для имени файла предусмотрено распознавание номера " +
                    $"договора в формате одного из значений: \"{BuildingContractNames.CONTRACT_6784}\", " +
                    $"\"{BuildingContractNames.CONTRACT_15297}\". В данном случае имя файла не содержит ни одно " +
                    $"из этих значений, поэтому номер договора не может быть распознан.";
                return false;
            }

            return true;
        }
    }
}
