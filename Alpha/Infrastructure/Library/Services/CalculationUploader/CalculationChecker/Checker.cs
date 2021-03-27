using System;
using System.Linq;
using System.Collections.Generic;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationChecker
{
    public static class Checker
    {
        private class FileInfo
        {
            public int FormID { get; set; }
            public int FileID { get; set; }
        }

        public static bool TryCheck(
            int uploadID,
            int progressFrom,
            int progressTill,
            Action<int, string> SetProgress)
        {
            SetProgress(progressFrom, "Подготовка к началу проверки распознанных файлов...");

            if (!TryGetFiles(uploadID, out List<FileInfo> files))
            {
                return false;
            }

            SetProgress(progressFrom, "Проверка распознанных файлов...");

            for (int i = 0; i < files.Count; i++)
            {
                FileChecker.CheckFile(
                    files[i].FileID,
                    files[i].FormID);

                SetProgress(
                    progressFrom + (i + 1) * (progressTill - progressFrom) / files.Count,
                    "Проверка распознанных файлов...");
            }

            SetProgress(progressTill, "Проверка распознанных файлов...");

            return true;
        }

        private static bool TryGetFiles(
            int uploadID,
            out List<FileInfo> files)
        {
            files = null;

            try
            {
                using (var db = new Entities())
                {
                    files =
                        db.CalculationForms
                            .Where(f =>
                                f.CalculationFiles.CalculationUploads.ID == uploadID
                                    && string.IsNullOrEmpty(f.CalculationFiles.ErrorDescription))
                            .Select(f => new FileInfo()
                            {
                                FileID = f.CalculationFiles.ID,
                                FormID = f.ID,
                            })
                            .ToList();
                }
            }
            catch (Exception exception)
            {
                CalculationUploadHandler.UpdateCheckingError(
                    uploadID,
                    exception);
                return false;
            }

            return true;
        }
    }
}
