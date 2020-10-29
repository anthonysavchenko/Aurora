using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesSaver.BuildingValuesFormSaver
{
    static public class FileSaver
    {
        static public void SaveFile(int formID, DateTime month)
        {
            BuildingCounterValueHandler.ClearExistedValues(month);
            BuildingCounterHandler.DeleteWithNoValues();

            FindRedundantBuildings(formID);

            CreateNewCounters(formID);
            UpdateOldCounters(formID);
            CreateNewValues(formID, month);
        }

        static private void FindRedundantBuildings(int formID)
        {
            using (var db = new Entities())
            {
                var redundantBuildings =
                    db.BuildingValuesUploadPoses
                        .Where(p =>
                            p.BuildingValuesUploads.ID == formID
                            && string.IsNullOrEmpty(p.ErrorDescription)
                            && db.Buildings
                                .Count(b =>
                                    b.Street.Equals(p.Street, StringComparison.OrdinalIgnoreCase)
                                    && b.Number.Equals(p.Building, StringComparison.OrdinalIgnoreCase)) == 0)
                        .ToList();

                redundantBuildings.ForEach(p => p.ErrorDescription =
                    "Распознанного номера дома нет в списке обслуживаемых УК домов.");
                db.SaveChanges();
            }
        }

        static private void CreateNewCounters(int formID)
        {
            using (var db = new Entities())
            {
                var newValues =
                    db.BuildingValuesUploadPoses
                        .Where(p =>
                            p.BuildingValuesUploads.ID == formID
                            && string.IsNullOrEmpty(p.ErrorDescription))
                        .Select(p => new
                        {
                            Building = db.Buildings
                                .FirstOrDefault(b =>
                                    b.Street.Equals(p.Street, StringComparison.OrdinalIgnoreCase)
                                    && b.Number.Equals(p.Building, StringComparison.OrdinalIgnoreCase)),
                            CounterNumber = p.CounterNumber.ToUpper(),
                            p.Coefficient,
                        })
                        .Where(p => p.Building != null
                            && db.BuildingCounters
                                .Count(c =>
                                    c.Buildings.ID == p.Building.ID
                                    && c.CounterNumber
                                        .Equals(p.CounterNumber, StringComparison.OrdinalIgnoreCase)) == 0)
                        .ToList();

                newValues.ForEach(c => db.BuildingCounters.AddObject(
                    new BuildingCounters()
                    {
                        Buildings = c.Building,
                        UtilityService = (byte)UtilityService.Electricity,
                        CounterNumber = c.CounterNumber,
                        Coefficient = c.Coefficient.Value,
                    }));
                db.SaveChanges();
            }
        }

        static private void UpdateOldCounters(int formID)
        {
            using (var db = new Entities())
            {
                var newValues =
                    db.BuildingValuesUploadPoses
                        .Where(p =>
                            p.BuildingValuesUploads.ID == formID
                            && string.IsNullOrEmpty(p.ErrorDescription))
                        .Select(p => new
                        {
                            Building = db.Buildings
                                .FirstOrDefault(b =>
                                    b.Street.Equals(p.Street, StringComparison.OrdinalIgnoreCase)
                                    && b.Number.Equals(p.Building, StringComparison.OrdinalIgnoreCase)),
                            CounterNumber = p.CounterNumber.ToUpper(),
                            p.Coefficient,
                        })
                        .Where(p =>
                            p.Building != null
                            && p.Coefficient != null)
                        .Select(p => new
                        {
                            Counter = db.BuildingCounters
                                .FirstOrDefault(c =>
                                    c.Buildings.ID == p.Building.ID
                                    && c.CounterNumber
                                        .Equals(p.CounterNumber, StringComparison.OrdinalIgnoreCase)
                                    && !c.Coefficient.Equals(p.Coefficient.Value)),
                            Coefficient = p.Coefficient.Value,
                        })
                        .Where(p => p.Counter != null)
                        .ToList();

                newValues.ForEach(c => c.Counter.Coefficient = c.Coefficient);
                db.SaveChanges();
            }
        }

        static private void CreateNewValues(int formID, DateTime month)
        {
            using (var db = new Entities())
            {
                var newValues =
                    db.BuildingValuesUploadPoses
                        .Where(p =>
                            p.BuildingValuesUploads.ID == formID
                            && string.IsNullOrEmpty(p.ErrorDescription))
                        .Select(p => new
                        {
                            Building = db.Buildings
                                .FirstOrDefault(b =>
                                    b.Street.Equals(p.Street, StringComparison.OrdinalIgnoreCase)
                                    && b.Number.Equals(p.Building, StringComparison.OrdinalIgnoreCase)),
                            p.CounterNumber,
                            p.PrevValue,
                            p.CurrentValue,
                            p.CurrentDate,
                            Pos = p,
                        })
                        .Where(p => p.Building != null)
                        .Select(p => new
                        {
                            Counter = db.BuildingCounters
                                .FirstOrDefault(c =>
                                    c.Buildings.ID == p.Building.ID
                                    && c.CounterNumber.Equals(p.CounterNumber)),
                            p.PrevValue,
                            p.CurrentValue,
                            p.CurrentDate,
                            p.Pos,
                        })
                        .ToList();

                newValues.ForEach(
                    c => db.BuildingCounterValues.AddObject(
                        new BuildingCounterValues()
                        {
                            Month = month,
                            PrevValue = c.PrevValue,
                            CurrentValue = c.CurrentValue,
                            CurrentDate = c.CurrentDate,
                            BuildingCounters = c.Counter,
                            BuildingValuesUploadPoses = c.Pos,
                        }));
                db.SaveChanges();
            }
        }
    }
}
