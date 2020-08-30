using System;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesSaver
{
    static public class FileSaver
    {
        static public void SaveFile(BuildingValuesUploads form, DateTime month)
        {
            try
            {
                BuildingValuesFormSaver.FileSaver.SaveFile(form.ID, month);
            }
            catch (Exception e)
            {
                Logger.SimpleWrite($"BuildingValuesSaver.FileSaver SaveFile error: {e}");
                BuildingValuesUploadHandler.UpdateUploadWithError(
                    form.ID,
                    "Ошибка при сохранении распознанных данных.",
                    e.ToString());
            }
        }
    }
}
