using System;
using System.IO;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.Excel;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsUploader.DecFormsParser
{
    static public class Parser
    {
        static public bool Parse(
            DecFormsUploads upload,
            int progressFrom,
            int progressTill,
            Action<int, string> SetProgress)
        {
            SetProgress(progressFrom, "Подготовка к началу распознавания файлов...");

            if (!GetExcelWorker(upload, out Excel2007Worker worker))
            {
                return false;
            }

            if (!GetFiles(upload, out string[] files))
            {
                return false;
            }

            SetProgress(progressFrom, "Распознавание файлов...");

            for (int i = 0; i < files.Length; i++)
            {
                FileParser.ParseFile(
                    upload,
                    worker,
                    files[i]);

                SetProgress(
                    progressFrom + (i + 1) * (progressTill - progressFrom) / files.Length,
                    "Распознавание файлов...");
            }

            SetProgress(progressTill, "Распознавание файлов...");

            return true;
        }

        static private bool GetFiles(DecFormsUploads upload, out string[] files)
        {
            files = null;

            try
            {
                files = Directory
                    .GetFiles(upload.Directory, "*.xls", SearchOption.TopDirectoryOnly)
                    .Where(f => f.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
                    .ToArray();
            }
            catch(Exception e)
            {
                Logger.SimpleWrite($"Parser GetFiles error: {e}");
                DecFormsUploadHandler.UpdateUploadWithError(
                    upload,
                    "Ошибка при поиске файлов в папке. " +
                        "Попробуйте выбрать другую папку.",
                    e.ToString());
                return false;
            }

            return true;
        }

        static private bool GetExcelWorker(DecFormsUploads upload, out Excel2007Worker worker)
        {
            worker = null;
            
            try
            {
                worker = new Excel2007Worker();
            }
            catch (Exception e)
            {
                Logger.SimpleWrite($"Parser GetExcelWorker error: {e}");
                DecFormsUploadHandler.UpdateUploadWithError(
                    upload,
                    "Ошибка при подготовке к работе с Excel. " +
                        "Убедитесь, что на компьютере установлен MS Excel.",
                    e.ToString());
                return false;
            }
            
            return true;
        }
    }
}
