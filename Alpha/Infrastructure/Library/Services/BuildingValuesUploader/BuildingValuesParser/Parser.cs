using System;
using System.IO;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.Excel;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesParser
{
    static public class Parser
    {
        static public bool Parse(
            BuildingValuesUploads upload,
            int progressFrom,
            int progressTill,
            Action<int, string> SetProgress)
        {
            SetProgress(progressFrom, "Подготовка к началу распознавания файла...");

            if (!GetExcelWorker(upload, out Excel2007Worker worker))
            {
                return false;
            }

            if (!GetFile(upload, out string file))
            {
                return false;
            }

            SetProgress(progressFrom, "Распознавание файла...");

            if (!FileParser.ParseFile(
                upload,
                worker,
                file))
            {
                return false;
            }

            SetProgress(progressTill, "Распознавание файлов...");

            return true;
        }

        static private bool GetFile(BuildingValuesUploads upload, out string file)
        {
            file = null;

            if (File.Exists(upload.FilePath))
            {
                file = upload.FilePath;
            }
            else
            {
                BuildingValuesUploadHandler.UpdateUploadWithError(
                    upload.ID,
                    "Ошибка при поиске файлов в папке. " +
                        "Попробуйте выбрать другую папку.");
                return false;
            }

            return true;
        }

        static private bool GetExcelWorker(BuildingValuesUploads upload, out Excel2007Worker worker)
        {
            worker = null;

            try
            {
                worker = new Excel2007Worker();
            }
            catch (Exception e)
            {
                Logger.SimpleWrite($"BuildingValuesParser.Parser GetExcelWorker error: {e}");
                BuildingValuesUploadHandler.UpdateUploadWithError(
                    upload.ID,
                    "Ошибка при подготовке к работе с Excel. " +
                        "Убедитесь, что на компьютере установлен MS Excel.",
                    e.ToString());
                return false;
            }

            return true;
        }
    }
}
