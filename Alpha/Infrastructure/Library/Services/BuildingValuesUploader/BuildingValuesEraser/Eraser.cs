using System;
using System.Linq;
using System.Collections.Generic;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesEraser
{
    public static class Eraser
    {
        private class FileInfo
        {
            public int FormID { get; set; }
            public int FileID { get; set; }
        }

        public static bool TryErase(
            int uploadID,
            DateTime month,
            int progressFrom,
            int progressTill,
            Action<int, string> SetProgress)
        {
            SetProgress(progressFrom, "Подготовка к началу удаления неактуальных файлов, загруженных ранее...");

            if (!TryGetFiles(uploadID, month, out List<FileInfo> files))
            {
                return false;
            }

            SetProgress(progressFrom, "Удаление неактуальных файлов, загруженных ранее...");

            for (int i = 0; i < files.Count; i++)
            {
                FileEraser.EraseFile(
                    files[i].FileID,
                    files[i].FormID);

                SetProgress(
                    progressFrom + (i + 1) * (progressTill - progressFrom) / files.Count,
                    "Удаление неактуальных файлов, загруженных ранее...");
            }

            SetProgress(progressTill, "Удаление неактуальных файлов, загруженных ранее...");

            return true;
        }

        private static bool TryGetFiles(
            int uploadID,
            DateTime month,
            out List<FileInfo> files)
        {
            files = null;

            try
            {
                using (var db = new Entities())
                {
                    files =
                        db.BuildingValuesRows
                            .Where(r =>
                                r.BuildingValuesForms.BuildingValuesFiles.BuildingValuesUploads.ID == uploadID
                                    && r.ProcessingResult == (byte)RowProcessingResult.OK)
                            .Select(r => new
                            {
                                Counter = db.BuildingCounters
                                    .FirstOrDefault(c =>
                                        c.Buildings.Street.Equals(
                                            r.Street,
                                            StringComparison.OrdinalIgnoreCase)
                                        && c.Buildings.Number.Equals(
                                            r.Building,
                                            StringComparison.OrdinalIgnoreCase)
                                        && c.CounterNumber.Equals(
                                            r.CounterNumber,
                                            StringComparison.OrdinalIgnoreCase)),
                            })
                            .Where(r => r.Counter != null)
                            .Select(r =>
                                r.Counter.BuildingCounterValues
                                    .FirstOrDefault(v => v.Month == month))
                            .Where(r => r != null)
                            .Select(r => new FileInfo
                            {
                                FileID = r.BuildingValuesRows.BuildingValuesForms.BuildingValuesFiles.ID,
                                FormID = r.BuildingValuesRows.BuildingValuesForms.ID,
                            })
                            .GroupBy(r => r)
                            .Select(g => g.Key)
                            .ToList();

                }
            }
            catch (Exception exception)
            {
                CalculationUploadHandler.UpdateErasingError(uploadID, exception);
                return false;
            }

            return true;
        }
    }
}
