using System;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationSaver
{
    public static class FileSaver
    {
        public static void SaveFile(int fileID, int formID, DateTime month)
        {
            try
            {
                FormSaver.SaveForm(formID, month);

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
