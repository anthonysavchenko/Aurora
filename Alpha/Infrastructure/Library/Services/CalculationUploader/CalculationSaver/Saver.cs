using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationSaver
{
    public static class Saver
    {
        private class FileInfo
        {
            public int FormID { get; set; }
            public int FileID { get; set; }
            public byte Contract { get; set; }
        }

        public static bool TrySave(
            int uploadID,
            DateTime month,
            int progressFrom,
            int progressTill,
            List<BuildingCalculationValueHandler.BuildingInfo> buildingInfos,
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
                    files[i].Contract,
                    month,
                    buildingInfos);

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
                        db.CalculationForms
                            .Where(f =>
                                f.CalculationFiles.CalculationUploads.ID == uploadID
                                    && string.IsNullOrEmpty(f.CalculationFiles.ErrorDescription))
                            .Select(f => new FileInfo()
                            {
                                FileID = f.CalculationFiles.ID,
                                FormID = f.ID,
                                Contract = f.CalculationFiles.Contract,
                            })
                            .ToList();
                }
            }
            catch (Exception exception)
            {
                CalculationUploadHandler.UpdateSavingError(
                    uploadID,
                    exception);
                return false;
            }

            return true;
        }
    }
}
