using System;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.PrivateValuesUploader.PrivateValuesSaver
{
    static public class FileSaver
    {
        static public void SaveFile(PrivateValuesForms form, DateTime month)
        {
            try
            {
                var buildingID = PrivateValuesFormSaver.FileSaver.GetBuilding(form);

                if (buildingID == null)
                {
                    PrivateValuesFormHandler.UpdateFormWithError(
                        form,
                        "Распознанного номера дома нет в списке обслуживаемых УК домов.");
                    return;
                }

                PrivateValuesFormSaver.FileSaver.SaveFile(form.ID, buildingID.Value, month);
            }
            catch (Exception e)
            {
                Logger.SimpleWrite($"PrivateValuesSaver.FileSaver SaveFile error (pos.ID: {form.ID}): {e}");
                PrivateValuesFormHandler.UpdateFormWithError(
                    form,
                    "Ошибка при сохранении распознанных данных.",
                    e.ToString());
            }
        }
    }
}
