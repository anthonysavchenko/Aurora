using System;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationEraser
{
    public static class FileEraser
    {
        public static void EraseFile(int fileID, int formID)
        {
            try
            {
                FormEraser.EraseForm(formID);
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
