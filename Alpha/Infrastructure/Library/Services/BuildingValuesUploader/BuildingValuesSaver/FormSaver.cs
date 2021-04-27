using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesSaver
{
    public static class FormSaver
    {
        static public void SaveForm(int formID, DateTime month)
        {
            CreateBuildingCounters(formID);
            CreateBuildingCounterValues(formID, month);
        }

        private static void CreateBuildingCounters(int formID)
        {
            using (var db = new Entities())
            {
                var newValues =
                    db.BuildingValuesRows
                        .Where(r =>
                            r.BuildingValuesForms.ID == formID
                                && r.ProcessingResult == (byte)RowProcessingResult.OK)
                        .Select(r => new
                        {
                            Building = db.Buildings
                                .FirstOrDefault(b =>
                                    b.Street.Equals(
                                        r.Street,
                                        StringComparison.OrdinalIgnoreCase)
                                    && b.Number.Equals(
                                        r.Building,
                                        StringComparison.OrdinalIgnoreCase)),
                            CounterNumber = r.CounterNumber.ToUpper(),
                            r.Coefficient,
                        })
                        .Where(r => r.Building != null
                            && db.BuildingCounters
                                .Count(c =>
                                    c.Buildings.ID == r.Building.ID
                                    && c.CounterNumber
                                        .Equals(r.CounterNumber, StringComparison.OrdinalIgnoreCase)) == 0)
                        .ToList();

                newValues.ForEach(v =>
                    db.BuildingCounters.AddObject(
                        new BuildingCounters()
                        {
                            Buildings = v.Building,
                            UtilityService = (byte)UtilityService.Electricity,
                            CounterNumber = v.CounterNumber,
                            Coefficient = v.Coefficient.Value,
                        }));

                db.SaveChanges();
            }
        }

        private static void CreateBuildingCounterValues(int formID, DateTime month)
        {
            using (var db = new Entities())
            {
                var newValues =
                    db.BuildingValuesRows
                        .Where(r =>
                            r.BuildingValuesForms.ID == formID
                                && r.ProcessingResult == (byte)RowProcessingResult.OK)
                        .Select(r => new
                        {
                            BuildingValuesRow = r,
                            Building = db.Buildings
                                .FirstOrDefault(b =>
                                    b.Street.Equals(
                                        r.Street,
                                        StringComparison.OrdinalIgnoreCase)
                                    && b.Number.Equals(
                                        r.Building,
                                        StringComparison.OrdinalIgnoreCase)),
                            r.CounterNumber,
                            r.PrevValue,
                            r.CurrentValue,
                        })
                        .Where(r => r.Building != null)
                        .Select(r => new
                        {
                            r.BuildingValuesRow,
                            Counter = db.BuildingCounters
                                .FirstOrDefault(c =>
                                    c.Buildings.ID == r.Building.ID
                                    && c.CounterNumber.Equals(r.CounterNumber)),
                            r.PrevValue,
                            r.CurrentValue,
                        })
                        .ToList();

                newValues.ForEach(v =>
                    db.BuildingCounterValues.AddObject(
                        new BuildingCounterValues()
                        {
                            BuildingValuesRows = v.BuildingValuesRow,
                            BuildingCounters = v.Counter,
                            Month = month,
                            PrevValue = v.PrevValue,
                            CurrentValue = v.CurrentValue,
                        }));

                db.SaveChanges();
            }
        }
    }
}
