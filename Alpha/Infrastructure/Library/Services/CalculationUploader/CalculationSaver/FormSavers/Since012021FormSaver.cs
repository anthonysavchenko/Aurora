using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationSaver.FormSavers
{
    public static class Since012021FormSaver
    {
        public static void SaveForm(
            int formID,
            byte contract,
            DateTime month,
            List<BuildingCalculationValueHandler.BuildingInfo> buildingInfos)
        {
            CommonFormSaver.SaveForm(
                formID,
                contract,
                month,
                buildingInfos,
                CreateBuildingCalculationValues);
        }

        private static void CreateBuildingCalculationValues(
            int formID,
            byte contract,
            DateTime month,
            List<BuildingCalculationValueHandler.BuildingInfo> buildingInfos)
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
                            PeriodVolumeRow = db.CalculationRows
                                .FirstOrDefault(rr =>
                                    rr.CalculationForms.ID == formID
                                        && rr.ProcessingResult == (byte)RowProcessingResult.OK
                                        && rr.RowType == (byte)CalculationRowType.BuildingInfo
                                        && rr.BuildingInfo.RowType == (byte)BuildingInfoRowType.PeriodVolume
                                        && rr.BuildingAddressRow != null
                                        && rr.BuildingAddressRow.ID == r.ID),
                            RestRow = db.CalculationRows
                                .FirstOrDefault(rr =>
                                    rr.CalculationForms.ID == formID
                                        && rr.ProcessingResult == (byte)RowProcessingResult.OK
                                        && rr.RowType == (byte)CalculationRowType.BuildingInfo
                                        && rr.BuildingInfo.RowType == (byte)BuildingInfoRowType.Rest
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
                                r.PeriodVolumeRow.BuildingInfo.PeriodVolume.Value +
                                    r.RestRow.BuildingInfo.Rest.Value,
                            r.CollectiveSquareRow.BuildingInfo.CollectiveSquare,
                        })
                        .ToList();

                foreach (var value in newValues)
                {
                    decimal? collectiveVolumeDraft =
                        buildingInfos != null
                            ? buildingInfos
                                .FirstOrDefault(i => i.BuildingID == value.Building.ID)
                                ?.CollectiveVolume
                            : null;

                    db.BuildingCalculationValues.AddObject(
                        new BuildingCalculationValues()
                        {
                            CalculationRows = value.CalculationRow,
                            Buildings = value.Building,
                            Contract = contract,
                            Month = month,
                            CalculationMethod = value.CalculationMethod,
                            Debt = value.Debt,
                            Volume = value.Volume,
                            Norm = value.Norm,
                            CollectiveVolume = value.CollectiveVolume,
                            CollectiveSquare = value.CollectiveSquare,
                            CollectiveVolumeDraft =
                                collectiveVolumeDraft.HasValue
                                    && collectiveVolumeDraft.Value != value.CollectiveVolume
                                    ? collectiveVolumeDraft
                                    : null,
                        });
                }

                db.SaveChanges();
            }
        }
    }
}
