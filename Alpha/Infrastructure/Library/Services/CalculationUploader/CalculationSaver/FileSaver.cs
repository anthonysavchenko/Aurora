using System;
using Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationSaver.FormSavers;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationSaver
{
    public static class FileSaver
    {
        public static void SaveFile(int fileID, int formID, DateTime month)
        {
            try
            {
                if (month >= Constants.SINCE_012021)
                {
                    Since012021FormSaver.SaveForm(formID, month);
                }
                else
                {
                    Till012021FormSaver.SaveForm(formID, month);
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
