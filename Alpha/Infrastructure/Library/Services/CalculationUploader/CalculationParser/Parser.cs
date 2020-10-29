using System;
using System.IO;
using System.Linq;
using Taumis.Alpha.Infrastructure.Library.Services.Excel;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser
{
    public static class Parser
    {
        public static bool Parse(
            int uploadID,
            string directoryPath,
            int progressFrom,
            int progressTill,
            Action<int, string> SetProgress)
        {
            SetProgress(progressFrom, "Подготовка к началу распознавания файлов...");

            if (!GetExcelWorker(uploadID, out Excel2007Worker worker))
            {
                return false;
            }

            if (!GetFiles(uploadID, directoryPath, out string[] files))
            {
                return false;
            }

            SetProgress(progressFrom, "Распознавание файлов...");

            for (int i = 0; i < files.Length; i++)
            {
                FileParser.ParseFile(
                    uploadID,
                    worker,
                    files[i]);

                SetProgress(
                    progressFrom + (i + 1) * (progressTill - progressFrom) / files.Length,
                    "Распознавание файлов...");
            }

            SetProgress(progressTill, "Распознавание файлов...");

            return true;
        }

        private static bool GetFiles(int uploadID, string directoryPath, out string[] files)
        {
            files = null;

            try
            {
                files = Directory
                    .GetFiles(directoryPath, "*.xls", SearchOption.TopDirectoryOnly)
                    .Where(f => f.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
                    .ToArray();
            }
            catch (Exception exception)
            {
                CalculationUploadHandler.UpdateParsingError(
                    uploadID,
                    "Ошибка при поиске файлов в папке. Попробуйте выбрать другую папку.",
                    exception);
                return false;
            }

            return true;
        }

        private static bool GetExcelWorker(int uploadID, out Excel2007Worker worker)
        {
            worker = null;

            try
            {
                worker = new Excel2007Worker();
            }
            catch (Exception exception)
            {
                CalculationUploadHandler.UpdateParsingError(
                    uploadID,
                    "Ошибка при подготовке к работе с Excel. Убедитесь, что на компьютере установлен MS Excel.",
                    exception);
                return false;
            }

            return true;
        }
    }
}
