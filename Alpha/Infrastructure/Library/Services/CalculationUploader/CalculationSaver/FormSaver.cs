using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationSaver
{
    public static class FormSaver
    {
        public static void SaveForm(int formID, DateTime month)
        {
            CreateBuildingCalculationValues(formID, month);

            CreateBuildingCounters(formID);
            CreateBuildingCounterCalculationValues(formID, month);

            CreateCustomers(formID);
            CreateCustomerCalculationValues(formID, month);

            CreateLegalEntities(formID);
            CreateLegalEntityCalculationValues(formID, month);
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

        private static void CreateBuildingCounters(int formID)
        {
            using (var db = new Entities())
            {
                var newValues =
                    db.CalculationRows
                        .Where(r =>
                            r.CalculationForms.ID == formID
                                && r.RowType == (byte)CalculationRowType.BuildingCounter
                                && r.BuildingAddressRow != null
                                && r.BuildingAddressRow.ProcessingResult == (byte)RowProcessingResult.OK)
                        .Select(r => new
                        {
                            Building = db.Buildings
                                .FirstOrDefault(b =>
                                    b.Street.Equals(
                                        r.BuildingAddressRow.BuildingInfo.Street,
                                        StringComparison.OrdinalIgnoreCase)
                                    && b.Number.Equals(
                                        r.BuildingAddressRow.BuildingInfo.Building,
                                        StringComparison.OrdinalIgnoreCase)),
                            CounterNumber = r.BuildingCounter.CounterNumber.ToUpper(),
                            r.BuildingCounter.Coefficient,
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
                            Coefficient = v.Coefficient,
                        }));

                db.SaveChanges();
            }
        }

        private static void CreateBuildingCounterCalculationValues(int formID, DateTime month)
        {
            using (var db = new Entities())
            {
                var newValues =
                    db.CalculationRows
                        .Where(r =>
                            r.CalculationForms.ID == formID
                                && r.RowType == (byte)CalculationRowType.BuildingCounter
                                && r.BuildingAddressRow != null
                                && r.BuildingAddressRow.ProcessingResult == (byte)RowProcessingResult.OK)
                        .Select(r => new
                        {
                            CalculationRow = r,
                            Building = db.Buildings
                                .FirstOrDefault(b =>
                                    b.Street.Equals(
                                        r.BuildingAddressRow.BuildingInfo.Street,
                                        StringComparison.OrdinalIgnoreCase)
                                    && b.Number.Equals(
                                        r.BuildingAddressRow.BuildingInfo.Building,
                                        StringComparison.OrdinalIgnoreCase)),
                            r.BuildingCounter.CounterNumber,
                            r.BuildingCounter.PrevValue,
                            r.BuildingCounter.CurrentValue,
                        })
                        .Where(r => r.Building != null)
                        .Select(r => new
                        {
                            r.CalculationRow,
                            Counter = db.BuildingCounters
                                .FirstOrDefault(c =>
                                    c.Buildings.ID == r.Building.ID
                                    && c.CounterNumber.Equals(r.CounterNumber)),
                            r.PrevValue,
                            r.CurrentValue,
                        })
                        .ToList();

                newValues.ForEach(v =>
                    db.BuildingCounterCalculationValues.AddObject(
                        new BuildingCounterCalculationValues()
                        {
                            CalculationRows = v.CalculationRow,
                            BuildingCounters = v.Counter,
                            Month = month,
                            PrevValue = v.PrevValue,
                            CurrentValue = v.CurrentValue,
                        }));

                db.SaveChanges();
            }
        }

        private static void CreateCustomers(int formID)
        {
            using (var db = new Entities())
            {
                var newValues =
                    db.CalculationRows
                        .Where(r =>
                            r.CalculationForms.ID == formID
                                && r.RowType == (byte)CalculationRowType.Customer
                                && r.BuildingAddressRow != null
                                && r.BuildingAddressRow.ProcessingResult == (byte)RowProcessingResult.OK)
                        .Select(r => new
                        {
                            Building = db.Buildings
                                .FirstOrDefault(b =>
                                    b.Street.Equals(
                                        r.BuildingAddressRow.BuildingInfo.Street,
                                        StringComparison.OrdinalIgnoreCase)
                                    && b.Number.Equals(
                                        r.BuildingAddressRow.BuildingInfo.Building,
                                        StringComparison.OrdinalIgnoreCase)),
                            Apartment = r.Customer.Apartment.ToLower(),
                            Account = r.Customer.Account.ToLower(),
                        })
                        .Where(r => r.Building != null)
                        .GroupBy(r => new
                        {
                            r.Building,
                            r.Apartment,
                            r.Account,
                        })
                        .Select(g => g.Key)
                        .Where(g =>
                            db.Customers
                                .Count(c =>
                                    c.Buildings.ID == g.Building.ID
                                    && c.Apartment.Equals(g.Apartment, StringComparison.OrdinalIgnoreCase)
                                    && c.Account.Equals(g.Account, StringComparison.OrdinalIgnoreCase)) == 0)
                        .ToList();

                newValues.ForEach(v =>
                    db.Customers.AddObject(
                        new Customers()
                        {
                            Buildings = v.Building,
                            Apartment = v.Apartment,
                            Account = v.Account,
                        }));

                db.SaveChanges();
            }
        }

        private static void CreateCustomerCalculationValues(int formID, DateTime month)
        {
            using (var db = new Entities())
            {
                var newValues =
                    db.CalculationRows
                        .Where(r =>
                            r.CalculationForms.ID == formID
                                && r.RowType == (byte)CalculationRowType.Customer
                                && r.BuildingAddressRow != null
                                && r.BuildingAddressRow.ProcessingResult == (byte)RowProcessingResult.OK)
                        .Select(r => new
                        {
                            CalculationRow = r,
                            Building = db.Buildings
                                .FirstOrDefault(b =>
                                    b.Street.Equals(
                                        r.BuildingAddressRow.BuildingInfo.Street,
                                        StringComparison.OrdinalIgnoreCase)
                                    && b.Number.Equals(
                                        r.BuildingAddressRow.BuildingInfo.Building,
                                        StringComparison.OrdinalIgnoreCase)),
                            r.Customer.Apartment,
                            r.Customer.Account,
                            ValueType =
                                r.Customer.CounterType == (byte)CalculationCounterType.Common
                                    ? (byte)CalculationCustomerValueCounterType.Common
                                    : r.Customer.CounterType == (byte)CalculationCounterType.Day
                                        ? (byte)CalculationCustomerValueCounterType.Day
                                        : r.Customer.CounterType != (byte)CalculationCounterType.Night
                                            ? (byte)CalculationCustomerValueCounterType.Night
                                            : (byte)CalculationCustomerValueCounterType.Norm,
                            r.Customer.Volume,
                            r.Customer.Recalculation,
                        })
                        .Where(r => r.Building != null)
                        .Select(r => new
                        {
                            r.CalculationRow,
                            Customer = db.Customers
                                .FirstOrDefault(c =>
                                    c.Buildings.ID == r.Building.ID
                                    && c.Apartment.Equals(r.Apartment, StringComparison.OrdinalIgnoreCase)
                                    && c.Account.Equals(r.Account, StringComparison.OrdinalIgnoreCase)),
                            r.ValueType,
                            r.Volume,
                            r.Recalculation,
                        })
                        .ToList();

                newValues.ForEach(v =>
                    db.CustomerCalculationValues.AddObject(
                        new CustomerCalculationValues()
                        {
                            CalculationRows = v.CalculationRow,
                            Customers = v.Customer,
                            Month = month,
                            ValueType = v.ValueType,
                            Volume = v.Volume,
                            Recalculation = v.Recalculation,
                        }));

                db.SaveChanges();
            }
        }

        private static void CreateLegalEntities(int formID)
        {
            using (var db = new Entities())
            {
                var newValues =
                    db.CalculationRows
                        .Where(r =>
                            r.CalculationForms.ID == formID
                                && r.RowType == (byte)CalculationRowType.LegalEntity
                                && r.BuildingAddressRow != null
                                && r.BuildingAddressRow.ProcessingResult == (byte)RowProcessingResult.OK)
                        .Select(r => new
                        {
                            Building = db.Buildings
                                .FirstOrDefault(b =>
                                    b.Street.Equals(
                                        r.BuildingAddressRow.BuildingInfo.Street,
                                        StringComparison.OrdinalIgnoreCase)
                                    && b.Number.Equals(
                                        r.BuildingAddressRow.BuildingInfo.Building,
                                        StringComparison.OrdinalIgnoreCase)),
                            Contract = r.LegalEntity.Contract.ToUpper(),
                        })
                        .Where(r => r.Building != null)
                        .GroupBy(r => new
                        {
                            r.Building,
                            r.Contract,
                        })
                        .Select(g => g.Key)
                        .Where(g =>
                            db.LegalEntities
                                .Count(l =>
                                    l.Buildings.ID == g.Building.ID
                                    && l.Contract.Equals(g.Contract, StringComparison.OrdinalIgnoreCase)) == 0)
                        .ToList();

                newValues.ForEach(v =>
                    db.LegalEntities.AddObject(
                        new LegalEntities()
                        {
                            Buildings = v.Building,
                            Contract = v.Contract,
                        }));

                db.SaveChanges();
            }
        }

        private static void CreateLegalEntityCalculationValues(int formID, DateTime month)
        {
            using (var db = new Entities())
            {
                var newValues =
                    db.CalculationRows
                        .Where(r =>
                            r.CalculationForms.ID == formID
                                && r.RowType == (byte)CalculationRowType.LegalEntity
                                && r.BuildingAddressRow != null
                                && r.BuildingAddressRow.ProcessingResult == (byte)RowProcessingResult.OK)
                        .Select(r => new
                        {
                            CalculationRow = r,
                            Building = db.Buildings
                                .FirstOrDefault(b =>
                                    b.Street.Equals(
                                        r.BuildingAddressRow.BuildingInfo.Street,
                                        StringComparison.OrdinalIgnoreCase)
                                    && b.Number.Equals(
                                        r.BuildingAddressRow.BuildingInfo.Building,
                                        StringComparison.OrdinalIgnoreCase)),
                            r.LegalEntity.Contract,
                            r.LegalEntity.ChargedVolume,
                        })
                        .Where(r => r.Building != null)
                        .Select(r => new
                        {
                            r.CalculationRow,
                            LegalEntity = db.LegalEntities
                                .FirstOrDefault(l =>
                                    l.Buildings.ID == r.Building.ID
                                    && l.Contract.Equals(r.Contract, StringComparison.OrdinalIgnoreCase)),
                            r.ChargedVolume,
                        })
                        .ToList();

                newValues.ForEach(v =>
                    db.LegalEntityCalculationValues.AddObject(
                        new LegalEntityCalculationValues()
                        {
                            CalculationRows = v.CalculationRow,
                            LegalEntities = v.LegalEntity,
                            Month = month,
                            ChargedVolume = v.ChargedVolume,
                        }));

                db.SaveChanges();
            }
        }
    }
}
