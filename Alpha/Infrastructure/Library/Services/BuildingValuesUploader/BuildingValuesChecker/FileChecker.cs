using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesChecker
{
    public static class FileChecker
    {
        public static void CheckFile(int fileID, int formID)
        {
            try
            {
                if (!HasAnyRows(fileID))
                {
                    BuildingValuesFileHandler.UpdateCheckingError(
                        fileID,
                        "Файл пуст - не найдено ни одной строки. " +
                            "Убедитесь, что файл составлен в правильном формате.");
                    return;
                }

                if (!HasAnyRowsParsedWithoutErrors(fileID))
                {
                    BuildingValuesFileHandler.UpdateCheckingError(
                        fileID,
                        "Не было успешно распознано ни одной строки. " +
                            "Убедитесь, что файл составлен в правильном формате.");
                    return;
                }

                FormChecker.CheckForm(formID);
            }
            catch (Exception e)
            {
                BuildingValuesFileHandler.UpdateCheckingError(fileID, e);
            }
        }

        public static bool HasAnyRows(int fileID)
        {
            using (var db = new Entities())
            {
                return
                    db.BuildingValuesRows
                        .Count(r =>
                            r.BuildingValuesForms.BuildingValuesFiles.ID == fileID) > 0;
            }
        }

        public static bool HasAnyRowsParsedWithoutErrors(int fileID)
        {
            using (var db = new Entities())
            {
                return
                    db.BuildingValuesRows
                        .Count(r =>
                            r.BuildingValuesForms.BuildingValuesFiles.ID == fileID
                                && r.ProcessingResult == (byte)RowProcessingResult.OK) > 0;
            }
        }
    }
}
