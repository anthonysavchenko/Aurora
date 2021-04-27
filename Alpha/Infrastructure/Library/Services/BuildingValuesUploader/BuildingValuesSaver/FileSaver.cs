using System;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesSaver
{
    public static class FileSaver
    {
        public static void SaveFile(int fileID, int formID, DateTime month)
        {
            try
            {
                FormSaver.SaveForm(formID, month);
                BuildingValuesFileHandler.UpdateProcessingResult(fileID);
            }
            catch (Exception exception)
            {
                BuildingValuesFileHandler.UpdateSavingError(
                    fileID,
                    exception);
            }
        }
    }
}
