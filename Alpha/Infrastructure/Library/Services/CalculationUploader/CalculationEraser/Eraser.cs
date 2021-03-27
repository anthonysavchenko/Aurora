using System;
using System.Linq;
using System.Collections.Generic;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationEraser
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
                        db.CalculationRows
                            .Where(r =>
                                r.CalculationForms.CalculationFiles.CalculationUploads.ID == uploadID
                                    && r.ProcessingResult == (byte)RowProcessingResult.OK
                                    && r.RowType == (byte)CalculationRowType.BuildingInfo
                                    && r.BuildingInfo.RowType == (byte)BuildingInfoRowType.Address)
                            .Select(r => new
                            {
                                Building = db.Buildings
                                    .FirstOrDefault(b =>
                                        b.Street.Equals(
                                            r.BuildingInfo.Street,
                                            StringComparison.OrdinalIgnoreCase)
                                        && b.Number.Equals(
                                            r.BuildingInfo.Building,
                                            StringComparison.OrdinalIgnoreCase)),
                            })
                            .Where(r => r.Building != null)
                            .Select(r =>
                                r.Building.BuildingCalculationValues
                                    .FirstOrDefault(v => v.Month == month))
                            .Where(r => r != null)
                            .Select(r => new FileInfo
                            {
                                FileID = r.CalculationRows.CalculationForms.CalculationFiles.ID,
                                FormID = r.CalculationRows.CalculationForms.ID,
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
