using System;
using System.Linq;
using System.Collections.Generic;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesSaver
{
    public static class Saver
    {
        private class FileInfo
        {
            public int FormID { get; set; }
            public int FileID { get; set; }
        }

        public static bool TrySave(
            int uploadID,
            DateTime month,
            int progressFrom,
            int progressTill,
            Action<int, string> SetProgress)
        {
            SetProgress(progressFrom, "Подготовка к началу сохранения распознанных файлов...");

            if (!TryGetFiles(uploadID, out List<FileInfo> files))
            {
                return false;
            }

            SetProgress(progressFrom, "Сохранение распознанных файлов...");

            for (int i = 0; i < files.Count; i++)
            {
                FileSaver.SaveFile(
                    files[i].FileID,
                    files[i].FormID,
                    month);

                SetProgress(
                    progressFrom + (i + 1) * (progressTill - progressFrom) / files.Count,
                    "Сохранение распознанных файлов...");
            }

            SetProgress(progressTill, "Сохранение распознанных файлов...");

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
                        db.BuildingValuesForms
                            .Where(f =>
                                f.BuildingValuesFiles.BuildingValuesUploads.ID == uploadID
                                    && string.IsNullOrEmpty(f.BuildingValuesFiles.ErrorDescription))
                            .Select(f => new FileInfo()
                            {
                                FileID = f.BuildingValuesFiles.ID,
                                FormID = f.ID,
                            })
                            .ToList();
                }
            }
            catch (Exception exception)
            {
                BuildingValuesUploadHandler.UpdateSavingError(
                    uploadID,
                    exception);
                return false;
            }

            return true;
        }
    }
}
