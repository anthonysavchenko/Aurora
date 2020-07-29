using System;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsUploader.DecFormsSaver
{
    static public class FileSaver
    {
        static public void SaveFile(DecFormsUploadPoses pos, DateTime month)
        {
            try
            {
                if (pos.FormType == (byte)DecFormsType.RouteForm)
                {
                    var building = RouteFormSaver.FileSaver.GetBuilding(pos.RouteForm);

                    if (building == null)
                    {
                        UploadPosHandler.UpdateUploadPosWithError(
                            pos,
                            "Распознанного номера дома нет в списке обслуживаемых УК домов.");
                        return;
                    }

                    RouteFormSaver.FileSaver.SaveFile(pos.RouteForm, building, month);
                }
                else if (pos.FormType == (byte)DecFormsType.FillForm)
                {
                    var building = FillFormSaver.FileSaver.GetBuilding(pos.FillForm);

                    if (building == null)
                    {
                        UploadPosHandler.UpdateUploadPosWithError(
                            pos,
                            "Распознанного номера дома нет в списке обслуживаемых УК домов.");
                        return;
                    }

                    FillFormSaver.FileSaver.SaveFile(pos.FillForm, building, month);
                }
            }
            catch (Exception e)
            {
                Logger.SimpleWrite($"FileSaver SaveFile error (pos.ID: {pos.ID}): {e}");
                UploadPosHandler.UpdateUploadPosWithError(
                    pos,
                    "Ошибка при сохранении распознанных данных.",
                    e.ToString());
            }
        }
    }
}
