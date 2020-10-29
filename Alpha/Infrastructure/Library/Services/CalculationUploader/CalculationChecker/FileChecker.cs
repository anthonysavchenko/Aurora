using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationChecker
{
    public static class FileChecker
    {
        public static void CheckFile(int fileID, int formID)
        {
            try
            {
                if (!HasAnyRows(fileID))
                {
                    CalculationFileHandler.UpdateCheckingError(
                        fileID,
                        "Файл пуст - не найдено ни одной строки. " +
                            "Убедитесь, что файл составлен в правильном формате.");
                    return;
                }

                if (!HasAnyRowsParsedWithoutErrors(fileID))
                {
                    CalculationFileHandler.UpdateCheckingError(
                        fileID,
                        "Не было успешно распознано ни одной строки. " +
                            "Убедитесь, что файл составлен в правильном формате.");
                    return;
                }

                if (!HasAnyBuildingAddressesParsedWithoutErrors(fileID))
                {
                    CalculationFileHandler.UpdateCheckingError(
                        fileID,
                        "Не было успешно распознано ни одной строки с адресом дома. " +
                            "Убедитесь, что файл составлен в правильном формате.");
                    return;
                }

                FormChecker.CheckForm(formID);
            }
            catch (Exception e)
            {
                CalculationFileHandler.UpdateCheckingError(fileID, e);
            }
        }

        public static bool HasAnyRows(int fileID)
        {
            using (var db = new Entities())
            {
                return
                    db.CalculationRows
                        .Count(r =>
                            r.CalculationForms.CalculationFiles.ID == fileID) > 0;
            }
        }

        public static bool HasAnyRowsParsedWithoutErrors(int fileID)
        {
            using (var db = new Entities())
            {
                return
                    db.CalculationRows
                        .Count(r =>
                            r.CalculationForms.CalculationFiles.ID == fileID
                                && r.ProcessingResult == (byte)RowProcessingResult.OK) > 0;
            }
        }

        public static bool HasAnyBuildingAddressesParsedWithoutErrors(int fileID)
        {
            using (var db = new Entities())
            {
                return
                    db.CalculationRows
                        .Count(r =>
                            r.CalculationForms.CalculationFiles.ID == fileID
                                && r.ProcessingResult == (byte)RowProcessingResult.OK
                                && r.RowType == (byte)CalculationRowType.BuildingInfo
                                && r.BuildingInfo.RowType == (byte)BuildingInfoRowType.Address) > 0;
            }
        }
    }
}
