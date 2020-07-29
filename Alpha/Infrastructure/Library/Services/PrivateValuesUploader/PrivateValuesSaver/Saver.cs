using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.PrivateValuesUploader.PrivateValuesSaver
{
    static public class Saver
    {
        public static bool Save(
            PrivateValuesUploads upload,
            int progressFrom,
            int progressTill,
            Action<int, string> SetProgress)
        {
            SetProgress(progressFrom, "Подготовка к началу сохранения распознанных файлов...");

            if (!GetPoses(upload, out DateTime month, out List<PrivateValuesForms> forms))
            {
                return false;
            }

            SetProgress(progressFrom, "Сохранение распознанных файлов...");

            for (int i = 0; i < forms.Count; i++)
            {
                FileSaver.SaveFile(
                    forms[i],
                    month);

                SetProgress(
                    progressFrom + (i + 1) * (progressTill - progressFrom) / upload.PrivateValuesForms.Count,
                    "Сохранение распознанных файлов...");
            }

            SetProgress(progressTill, "Сохранение распознанных файлов...");

            return true;
        }

        public static bool GetPoses(
            PrivateValuesUploads upload,
            out DateTime month,
            out List<PrivateValuesForms> forms)
        {
            forms = null;
            month = DateTime.MinValue;

            try
            {
                using (var db = new Entities())
                {
                    db.PrivateValuesUploads.Attach(upload);

                    month = upload.Month;
                    forms =
                        upload.PrivateValuesForms
                            .Where(p => string.IsNullOrEmpty(p.ErrorDescription))
                            .ToList();
                }
            }
            catch (Exception e)
            {
                Logger.SimpleWrite($"Saver GetPoses error: {e}");
                PrivateValuesUploadHandler.UpdateUploadWithError(
                    upload,
                    "Ошибка при подготовке к сохранению распознанных данных. " +
                        "Проверьте подключение к локальной сети УК ФР и серверу БД.",
                    e.ToString());
                return false;
            }

            return true;
        }
    }
}
