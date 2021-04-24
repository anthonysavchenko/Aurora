using System;
using System.Collections.Generic;
using Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationSaver.FormSavers;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationSaver
{
    public static class FileSaver
    {
        public static void SaveFile(
            int fileID,
            int formID,
            byte contract,
            DateTime month,
            List<BuildingCalculationValueHandler.BuildingInfo> buildingInfos)
        {
            try
            {
                if (month >= Constants.SINCE_012021)
                {
                    Since012021FormSaver.SaveForm(
                        formID,
                        contract,
                        month,
                        buildingInfos);
                }
                else
                {
                    Till012021FormSaver.SaveForm(
                        formID,
                        contract,
                        month,
                        buildingInfos);
                }

                CalculationFileHandler.UpdateProcessingResult(fileID);
            }
            catch (Exception exception)
            {
                CalculationFileHandler.UpdateSavingError(
                    fileID,
                    exception);
            }
        }
    }
}
