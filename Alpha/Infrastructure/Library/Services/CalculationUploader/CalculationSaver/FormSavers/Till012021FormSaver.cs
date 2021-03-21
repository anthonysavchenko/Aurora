using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationSaver.FormSavers
{
    public static class Till012021FormSaver
    {
        public static void SaveForm(
            int formID,
            DateTime month)
        {
            CommonFormSaver.SaveForm(formID, month, CreateBuildingCalculationValues);
        }

        private static void CreateBuildingCalculationValues(int formID, DateTime month)
        {
            using (var db = new Entities())
            {
                var newValues =
                    db.CalculationRows
                        .Where(r =>
                            r.CalculationForms.ID == formID
                                && r.ProcessingResult == (byte)RowProcessingResult.OK
                                && r.RowType == (byte)CalculationRowType.BuildingInfo
                                && r.BuildingInfo.RowType == (byte)BuildingInfoRowType.Address)
                        .Select(r => new
                        {
                            CalculationRow = r,
                            Building = db.Buildings
                                .FirstOrDefault(b =>
                                    b.Street.Equals(
                                        r.BuildingInfo.Street,
                                        StringComparison.OrdinalIgnoreCase)
                                        && b.Number.Equals(
                                            r.BuildingInfo.Building,
                                            StringComparison.OrdinalIgnoreCase)),
                            CalculationMethodRow = db.CalculationRows
                                .FirstOrDefault(rr =>
                                    rr.CalculationForms.ID == formID
                                        && rr.ProcessingResult == (byte)RowProcessingResult.OK
                                        && rr.RowType == (byte)CalculationRowType.BuildingInfo
                                        && rr.BuildingInfo.RowType == (byte)BuildingInfoRowType.CalculationMethod
                                        && rr.BuildingAddressRow != null
                                        && rr.BuildingAddressRow.ID == r.ID),
                            DebtRow = db.CalculationRows
                                .FirstOrDefault(rr =>
                                    rr.CalculationForms.ID == formID
                                        && rr.ProcessingResult == (byte)RowProcessingResult.OK
                                        && rr.RowType == (byte)CalculationRowType.BuildingInfo
                                        && rr.BuildingInfo.RowType == (byte)BuildingInfoRowType.Debt
                                        && rr.BuildingAddressRow != null
                                        && rr.BuildingAddressRow.ID == r.ID),
                            NormRow = db.CalculationRows
                                .FirstOrDefault(rr =>
                                    rr.CalculationForms.ID == formID
                                        && rr.ProcessingResult == (byte)RowProcessingResult.OK
                                        && rr.RowType == (byte)CalculationRowType.BuildingInfo
                                        && rr.BuildingInfo.RowType == (byte)BuildingInfoRowType.Norm
                                        && rr.BuildingAddressRow != null
                                        && rr.BuildingAddressRow.ID == r.ID),
                            CollectiveVolumeRow = db.CalculationRows
                                .FirstOrDefault(rr =>
                                    rr.CalculationForms.ID == formID
                                        && rr.ProcessingResult == (byte)RowProcessingResult.OK
                                        && rr.RowType == (byte)CalculationRowType.BuildingInfo
                                        && rr.BuildingInfo.RowType == (byte)BuildingInfoRowType.CollectiveVolume
                                        && rr.BuildingAddressRow != null
                                        && rr.BuildingAddressRow.ID == r.ID),
                            CollectiveSquareRow = db.CalculationRows
                                .FirstOrDefault(rr =>
                                    rr.CalculationForms.ID == formID
                                        && rr.ProcessingResult == (byte)RowProcessingResult.OK
                                        && rr.RowType == (byte)CalculationRowType.BuildingInfo
                                        && rr.BuildingInfo.RowType == (byte)BuildingInfoRowType.CollectiveSquare
                                        && rr.BuildingAddressRow != null
                                        && rr.BuildingAddressRow.ID == r.ID),

                        })
                        .Where(p => p.Building != null)
                        .Select(r => new
                        {
                            r.CalculationRow,
                            r.Building,
                            CalculationMethod =
                                r.CalculationMethodRow != null
                                    ? r.CalculationMethodRow.BuildingInfo.CalculationMethod.Value
                                    : (byte)CalculationMethod.BuildingCounters,
                            Debt =
                                r.DebtRow != null
                                    ? r.DebtRow.BuildingInfo.Debt
                                    : null,
                            Volume =
                                r.CalculationMethodRow != null
                                    ? r.CalculationMethodRow.BuildingInfo.Volume
                                    : null,
                            r.NormRow.BuildingInfo.Norm,
                            CollectiveVolume =
                                r.CollectiveVolumeRow.BuildingInfo.CollectiveVolume.Value != 0
                                    ? r.CollectiveVolumeRow.BuildingInfo.CollectiveVolume.Value
                                    : r.CollectiveVolumeRow.BuildingInfo.NotDistributedVolume.Value,
                            r.CollectiveSquareRow.BuildingInfo.CollectiveSquare,
                        })
                        .ToList();

                newValues.ForEach(v =>
                    db.BuildingCalculationValues.AddObject(
                        new BuildingCalculationValues()
                        {
                            CalculationRows = v.CalculationRow,
                            Buildings = v.Building,
                            Month = month,
                            CalculationMethod = v.CalculationMethod,
                            Debt = v.Debt,
                            Volume = v.Volume,
                            Norm = v.Norm,
                            CollectiveVolume = v.CollectiveVolume,
                            CollectiveSquare = v.CollectiveSquare,
                        }));

                db.SaveChanges();
            }
        }
    }
}
