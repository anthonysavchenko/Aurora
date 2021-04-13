using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Constants;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.WinClient.Aurora.Modules.Reports.Consolidation.Models;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Consolidation.Queries
{
    public static class ListViewQuery
    {
        public static DataTable GetDataTable(this Entities db, Column[] columns, DateTime since)
        {
            DateTime till = since.AddMonths(DataSource.MONTH_COUNT);
            DateTime sinceMinusOneMonth = since.AddMonths(-1);

            var dbQueriedItems =
                db.Buildings
                    .Select(b =>
                        new
                        {
                            Address = b.Street + ", д. " + b.Number,
                            b.NormCoefficient,
                            b.CollectiveSquare,

                            BuildingCounterValueVolumes =
                                db.BuildingCounterValues
                                    .Where(v =>
                                        v.BuildingCounters.Buildings.ID == b.ID
                                            && v.Month >= since
                                            && v.Month <= till)
                                    .GroupBy(g => g.Month)
                                    .Select(g =>
                                        new
                                        {
                                            Month = g.Key,
                                            Value = g.Sum(vv => (vv.CurrentValue - vv.PrevValue) *
                                                vv.BuildingCounters.Coefficient),
                                        })
                                    .ToList(),

                            BuildingCounterCalculationValueVolumes =
                                db.BuildingCounterCalculationValues
                                    .Where(v =>
                                        v.BuildingCounters.Buildings.ID == b.ID
                                            && v.Month >= since
                                            && v.Month <= till)
                                    .GroupBy(g => g.Month)
                                    .Select(g =>
                                        new
                                        {
                                            Month = g.Key,
                                            Value = g.Sum(vv => (vv.CurrentValue - vv.PrevValue) *
                                                vv.BuildingCounters.Coefficient),
                                        })
                                    .ToList(),

                            LegalEntityCalculationValueChargedVolumes =
                                db.LegalEntityCalculationValues
                                    .Where(v =>
                                        v.LegalEntities.Buildings.ID == b.ID
                                            && v.Month >= since
                                            && v.Month <= till)
                                    .GroupBy(g => g.Month)
                                    .Select(g =>
                                        new
                                        {
                                            Month = g.Key,
                                            Value = g.Sum(vv => vv.ChargedVolume),
                                        })
                                    .ToList(),

                            /*FillFormValues =
                                db.FillFormValues
                                    .Where(v =>
                                        v.PrivateCounters.Customers.Buildings.ID == b.ID
                                            && v.Month >= sinceMinusOneMonth
                                            && v.Month <= till)
                                    .GroupBy(g => g.Month)
                                    .Select(g =>
                                        new
                                        {
                                            Month = g.Key,
                                            Values = g.Select(vv =>
                                                new
                                                {
                                                    Counter = vv.PrivateCounters.ID,
                                                    vv.ValueType,
                                                    vv.Value,
                                                })
                                            .ToList(),
                                        })
                                    .ToList(),*/

                            CustomerCalculationValueVolumes =
                                db.CustomerCalculationValues
                                    .Where(v =>
                                        v.Customers.Buildings.ID == b.ID
                                            && v.Month >= since
                                            && v.Month <= till)
                                    .GroupBy(g => g.Month)
                                    .Select(g =>
                                        new
                                        {
                                            Month = g.Key,
                                            Value = g.Sum(vv => vv.Volume),
                                        })
                                    .ToList(),

                            CustomerCalculationValueRecalculations =
                                db.CustomerCalculationValues
                                    .Where(v =>
                                        v.Customers.Buildings.ID == b.ID
                                            && v.Month >= since
                                            && v.Month <= till)
                                    .GroupBy(g => g.Month)
                                    .Select(g =>
                                        new
                                        {
                                            Month = g.Key,
                                            Value = g.Sum(vv => vv.Recalculation),
                                        })
                                    .ToList(),

                            BuildingCalculationValues =
                                db.BuildingCalculationValues
                                    .Where(v =>
                                        v.Buildings.ID == b.ID
                                            && v.Month >= since
                                            && v.Month <= till)
                                    .GroupBy(g => g.Month)
                                    .Select(g =>
                                        new
                                        {
                                            Month = g.Key,
                                            Value = g.FirstOrDefault(),
                                        })
                                    .ToList(),
                        })
                    .Where(i =>
                        i.BuildingCounterValueVolumes.Count() != 0
                            || i.BuildingCounterCalculationValueVolumes.Count() != 0
                            || i.LegalEntityCalculationValueChargedVolumes.Count() != 0
                            //|| i.FillFormValues.Count() != 0
                            || i.CustomerCalculationValueVolumes.Count() != 0
                            || i.CustomerCalculationValueRecalculations.Count() != 0
                            || i.BuildingCalculationValues.Count() != 0)
                    .ToList();

            var toDictionariesItems =
                dbQueriedItems
                    .Select(i =>
                        new
                        {
                            i.Address,
                            i.NormCoefficient,
                            i.CollectiveSquare,

                            MaxBuildingCalculationValue =
                                i.BuildingCalculationValues
                                    .FirstOrDefault(v =>
                                        v.Month ==
                                            i.BuildingCalculationValues
                                                .Max(vv => vv.Month)),

                            BuildingCounterVolumes =
                                i.BuildingCounterValueVolumes
                                    .ToDictionary(
                                        v => $"{v.Month:MM.yyyy}",
                                        vv => vv.Value),

                            DecBuildingCounterVolumes =
                                i.BuildingCounterCalculationValueVolumes
                                    .ToDictionary(
                                        v => $"{v.Month:MM.yyyy}",
                                        vv => (decimal?)vv.Value),

                            DecLegalEntityVolumes =
                                i.LegalEntityCalculationValueChargedVolumes
                                    .ToDictionary(
                                        v => $"{v.Month:MM.yyyy}",
                                        vv => (decimal?)vv.Value),

                            /*FillFormValues =
                                i.FillFormValues
                                    .ToDictionary(
                                        v => v.Month,
                                        vv => vv.Values
                                            .ToDictionary(
                                                vvv => $"{vvv.Counter}-{vvv.ValueType}",
                                                vvv => vvv.Value)),*/

                            DecCustomerVolumes =
                                i.CustomerCalculationValueVolumes
                                    .ToDictionary(
                                        v => $"{v.Month:MM.yyyy}",
                                        vv => (decimal?)vv.Value),

                            DecCustomerRecalculations =
                                i.CustomerCalculationValueRecalculations
                                    .ToDictionary(
                                        v => $"{v.Month:MM.yyyy}",
                                        vv => (decimal?)vv.Value),

                            DecCollectiveVolumes =
                                i.BuildingCalculationValues
                                    .ToDictionary(
                                        v => $"{v.Month:MM.yyyy}",
                                        vv => vv.Value?.CollectiveVolume),

                            DecCollectiveSquares =
                                i.BuildingCalculationValues
                                    .ToDictionary(
                                        v => $"{v.Month:MM.yyyy}",
                                        vv => vv.Value?.CollectiveSquare),

                            DecCalculationMethods =
                                i.BuildingCalculationValues
                                    .ToDictionary(
                                        v => $"{v.Month:MM.yyyy}",
                                        vv => (decimal?)vv.Value?.CalculationMethod),
                        })
                    .ToList();

            var processedItems =
                toDictionariesItems
                    .Select(i =>
                        new
                        {
                            i.Address,
                            i.NormCoefficient,
                            i.CollectiveSquare,

                            Contract =
                                i.MaxBuildingCalculationValue != null
                                    ? (BuildingContract)i.MaxBuildingCalculationValue.Value.Contract
                                        == BuildingContract.Contract6784
                                        ? BuildingContractNames.CONTRACT_6784
                                        : (BuildingContract)i.MaxBuildingCalculationValue.Value.Contract
                                            == BuildingContract.Contract15297
                                            ? BuildingContractNames.CONTRACT_15297
                                            : null
                                    : null,

                            i.BuildingCounterVolumes,
                            i.DecBuildingCounterVolumes,
                            i.DecLegalEntityVolumes,

                            /*FillFormVolumes =
                                i.FillFormValues
                                    .Select(v =>
                                        new
                                        {
                                            Month = v.Key,
                                            Volumes =
                                                v.Value
                                                    .Select(vv =>
                                                        vv.Value.HasValue
                                                            ? i.FillFormValues.ContainsKey(v.Key.AddMonths(-1))
                                                                ? i.FillFormValues[v.Key.AddMonths(-1)].ContainsKey(vv.Key)
                                                                    ? i.FillFormValues[v.Key.AddMonths(-1)][vv.Key].HasValue
                                                                        ? vv.Value - 
                                                                            i.FillFormValues[v.Key.AddMonths(-1)][vv.Key]
                                                                        : null
                                                                    : null
                                                                : null
                                                            : null)
                                                .ToList(),
                                        })
                                    .ToDictionary(
                                        v => $"{v.Month:MM.yyyy}",
                                        vv =>
                                            vv.Volumes.Count(vvv => !vvv.HasValue) == 0
                                                ? (decimal?)vv.Volumes.Sum()
                                                : null),*/

                            i.DecCustomerVolumes,
                            i.DecCustomerRecalculations,
                            i.DecCollectiveVolumes,
                            i.DecCollectiveSquares,
                            i.DecCalculationMethods,
                        })
                        .OrderBy(b => b.Contract)
                        .ThenBy(b => b.Address)
                        .ToList();

            var table = new DataTable();

            DataSource.CreateDataTableColumns(table, columns);

            foreach (var item in processedItems)
            {
                table.Rows.Add(
                    DataSource.CreateDataTableRow(
                        table,
                        columns,
                        item.Contract,
                        item.Address,
                        "Расчет ОДН",
                        specialCellsFormat: CellFormat.CalculationMethod,
                        values: item.DecCalculationMethods,
                        calculateAvarageAndSum: false));

                table.Rows.Add(
                    DataSource.CreateDataTableRow(
                        table,
                        columns,
                        item.Contract,
                        item.Address,
                        "ОДПУ УК",
                        values: item.BuildingCounterVolumes));

                table.Rows.Add(
                    DataSource.CreateDataTableRow(
                        table,
                        columns,
                        item.Contract,
                        item.Address,
                        "ОДПУ ДЭК (объем для норм. и сред.)",
                        values: item.DecBuildingCounterVolumes,
                        checkValues: item.DecCalculationMethods,
                        alternativeValues: item.DecCollectiveVolumes,
                        comparison: (x, y, z) =>
                        {
                            return
                                x == (decimal?)CalculationMethod.BuildingCounters
                                    ? y
                                    : z;
                        },
                        replaceNegativeValues: true));

                table.Rows.Add(
                    DataSource.CreateDataTableRow(
                        table,
                        columns,
                        item.Contract,
                        item.Address,
                        "Показания юр. лиц",
                        values: item.DecLegalEntityVolumes));

                table.Rows.Add(
                    DataSource.CreateDataTableRow(
                        table,
                        columns,
                        item.Contract,
                        item.Address,
                        "К распределению ДЭК",
                        values: item.DecBuildingCounterVolumes,
                        secondValues: item.DecLegalEntityVolumes,
                        operation: (x, y) =>
                        {
                            return
                                x.HasValue
                                    ? x - (y ?? 0)
                                    : null;
                        },
                        checkValues: item.DecCalculationMethods,
                        alternativeValues: item.DecCollectiveVolumes,
                        comparison: (x, y, z) =>
                        {
                            return
                                x == (decimal?)CalculationMethod.BuildingCounters
                                    ? y
                                    : z;
                        },
                        replaceNegativeValues: true));

                /*table.Rows.Add(
                    DataSourceTable.CreateRow(
                        table,
                        columns,
                        item.Contract,
                        item.Address,
                        "ИПУ УК",
                        values: item.FillFormVolumes));*/

                table.Rows.Add(
                    DataSource.CreateDataTableRow(
                        table,
                        columns,
                        item.Contract,
                        item.Address,
                        "ИПУ ДЭК",
                        values: item.DecCustomerVolumes));

                table.Rows.Add(
                    DataSource.CreateDataTableRow(
                        table,
                        columns,
                        item.Contract,
                        item.Address,
                        "Перерасчет ИПУ ДЭК",
                        norm: item.NormCoefficient,
                        values: item.DecCustomerRecalculations));

                table.Rows.Add(
                    DataSource.CreateDataTableRow(
                        table,
                        columns,
                        item.Contract,
                        item.Address,
                        "Процент ОДН ДЭК от ИПУ ДЭК",
                        norm: item.CollectiveSquare,
                        specialCellsFormat: CellFormat.Percent,
                        values: item.DecCollectiveVolumes,
                        secondValues: item.DecCustomerVolumes,
                        operation: (x, y) =>
                        {
                            return
                                x.HasValue && y.HasValue && y != 0
                                    ? x / y * 100
                                    : null;
                        },
                        replaceNegativeValues: true));

                table.Rows.Add(
                    DataSource.CreateDataTableRow(
                        table,
                        columns,
                        item.Contract,
                        item.Address,
                        "ОДН ДЭК",
                        norm: item.NormCoefficient * item.CollectiveSquare,
                        values: item.DecCollectiveVolumes,
                        replaceNegativeValues: true));

                table.Rows.Add(
                    DataSource.CreateDataTableRow(
                        table,
                        columns,
                        item.Contract,
                        item.Address,
                        "Баланс",
                        values: item.DecCollectiveVolumes,
                        replacePositiveValues: true));

                table.Rows.Add(
                    DataSource.CreateDataTableRow(
                        table,
                        columns,
                        item.Contract,
                        item.Address,
                        "Проверка",
                        values: item.DecBuildingCounterVolumes,
                        secondValues: item.DecLegalEntityVolumes,
                        thirdValues: item.DecCustomerVolumes,
                        operation: (x, y) =>
                        {
                            return
                                x.HasValue
                                    ? x - (y ?? 0)
                                    : null;
                        },
                        secondOperation: (x, y) =>
                        {
                            return
                                x.HasValue
                                    ? x - (y ?? 0)
                                    : null;
                        }));

                table.Rows.Add(
                    DataSource.CreateDataTableRow(
                        table,
                        columns,
                        item.Contract,
                        item.Address,
                        "Площадь МОП",
                        values: item.DecCollectiveSquares));

                table.Rows.Add(
                    DataSource.CreateDataTableRow(
                        table,
                        columns,
                        item.Contract,
                        item.Address,
                        null));
            }

            return table;
        }
    }
}
