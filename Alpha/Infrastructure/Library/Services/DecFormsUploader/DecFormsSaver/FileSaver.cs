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
                    var buildingID = RouteFormSaver.FileSaver.GetBuilding(pos.RouteForm);

                    if (buildingID == null)
                    {
                        DecFormsUploadPosHandler.UpdateUploadPosWithError(
                            pos,
                            "Распознанного номера дома нет в списке обслуживаемых УК домов.");
                        return;
                    }

                    RouteFormSaver.FileSaver.SaveFile(pos.RouteForm.ID, buildingID.Value, month);
                }
                else if (pos.FormType == (byte)DecFormsType.FillForm)
                {
                    var buildingID = FillFormSaver.FileSaver.GetBuilding(pos.FillForm);

                    if (buildingID == null)
                    {
                        DecFormsUploadPosHandler.UpdateUploadPosWithError(
                            pos,
                            "Распознанного номера дома нет в списке обслуживаемых УК домов.");
                        return;
                    }

                    FillFormSaver.FileSaver.SaveFile(pos.FillForm.ID, buildingID.Value, month);
                }
            }
            catch (Exception e)
            {
                Logger.SimpleWrite($"FileSaver SaveFile error (pos.ID: {pos.ID}): {e}");
                DecFormsUploadPosHandler.UpdateUploadPosWithError(
                    pos,
                    "Ошибка при сохранении распознанных данных.",
                    e.ToString());
            }
        }
    }
}
