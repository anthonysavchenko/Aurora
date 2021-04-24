using System;
using System.Collections.Generic;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationEraser
{
    public static class FileEraser
    {
        public static void EraseFile(
            int fileID,
            int formID,
            bool useDrafts,
            out List<BuildingCalculationValueHandler.BuildingInfo> buildingInfos)
        {
            buildingInfos = null;

            try
            {
                FormEraser.EraseForm(formID, useDrafts, out buildingInfos);
            }
            catch (Exception exception)
            {
                CalculationFileHandler.UpdateErasingError(
                    fileID,
                    exception);
            }
        }
    }
}
