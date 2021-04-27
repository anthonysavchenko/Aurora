using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationChecker
{
    public static class FormChecker
    {
        public static void CheckForm(int formID)
        {
            CheckBuildingDependentRows(formID);
            CheckRedundantBuildings(formID);
        }

        private static void CheckBuildingDependentRows(int formID)
        {
            using (var db = new Entities())
            {
                var rowsWithErrors =
                    db.CalculationRows
                        .Where(r =>
                            r.CalculationForms.ID == formID
                                && r.ProcessingResult == (byte)RowProcessingResult.OK
                                && r.RowType == (byte)CalculationRowType.BuildingInfo
                                && r.BuildingInfo.RowType == (byte)BuildingInfoRowType.Address
                                && db.CalculationRows.Count(rr =>
                                    rr.BuildingAddressRow != null
                                    && rr.BuildingAddressRow.ID == r.ID
                                    && rr.ProcessingResult != (byte)RowProcessingResult.OK
                                    && rr.ProcessingResult != (byte)RowProcessingResult.Skipped) > 0)
                        .ToList();

                rowsWithErrors
                    .ForEach(r =>
                        CalculationRowHandler.SetCheckingError(
                            r,
                            "В последующих строках файла с информацией по этому дому были обнаружены ошибки, " +
                                "поэтому данные по этому дому не могут быть сохранены."));

                db.SaveChanges();
            }
        }

        private static void CheckRedundantBuildings(int formID)
        {
            using (var db = new Entities())
            {
                var rowsWithErrors =
                    db.CalculationRows
                        .Where(r =>
                            r.CalculationForms.ID == formID
                                && r.ProcessingResult == (byte)RowProcessingResult.OK
                                && r.RowType == (byte)CalculationRowType.BuildingInfo
                                && r.BuildingInfo.RowType == (byte)BuildingInfoRowType.Address
                                && db.Buildings
                                    .Count(b =>
                                        b.Street.Equals(
                                            r.BuildingInfo.Street,
                                            StringComparison.OrdinalIgnoreCase)
                                            && b.Number.Equals(
                                                r.BuildingInfo.Building,
                                                StringComparison.OrdinalIgnoreCase)) == 0)
                        .ToList();

                rowsWithErrors
                    .ForEach(r =>
                        CalculationRowHandler.SetCheckingError(
                            r,
                            "Распознанного адреса дома нет в справочнике обслуживаемых домов, " +
                                "поэтому данные по этому дому не могут быть сохранены."));

                db.SaveChanges();
            }
        }
    }
}
