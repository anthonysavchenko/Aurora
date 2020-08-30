using System;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesSaver
{
    static public class Saver
    {
        public static bool Save(
            BuildingValuesUploads upload,
            int progressFrom,
            int progressTill,
            Action<int, string> SetProgress)
        {
            SetProgress(progressFrom, "Подготовка к началу сохранения распознанных файлов...");

            if (!string.IsNullOrEmpty(upload.ErrorDescription))
            {
                return false;
            }

            SetProgress(progressFrom, "Сохранение распознанных файлов...");

            FileSaver.SaveFile(
                upload,
                upload.Month);

            SetProgress(progressTill, "Сохранение распознанных файлов...");

            return true;
        }
    }
}
