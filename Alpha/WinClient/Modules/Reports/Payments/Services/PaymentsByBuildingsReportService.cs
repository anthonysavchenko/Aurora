using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.WinClient.Aurora.Modules.Reports.Payments.Views.List;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Payments.Services
{
    public class PaymentsByBuildingsReportService : IPaymentReportService
    {
        private const string INTERMEDIARY_FIELDNAME_PREFIX = "I";

        private static class ColumnNames
        {
            public const string STREET_COLUMN = "Street";
            public const string BUILDING_COLUMN = "Building";
            public const string INCOME_SUM_COLUMN = "IncomeValue";
            public const string INTERMEDIARY_TOTAL_COLUMN = "IntermediaryTotal";
            public const string WO_INTERMEDIARY_TOTAL_COLUMN = "WoIntermediaryTotal";
        }

        public List<Column> GetColumns()
        {
            List<Column> _columns = new List<Column>();

            _columns.Add(
                new Column
                {
                    Title = "Улица",
                    FieldName = ColumnNames.STREET_COLUMN,
                    FieldSortName = ColumnNames.STREET_COLUMN,
                    IsSorted = true
                });
            _columns.Add(
                new Column
                {
                    Title = "Дом",
                    FieldName = ColumnNames.BUILDING_COLUMN,
                    FieldSortName = ColumnNames.BUILDING_COLUMN,
                    IsSorted = true
                });
            _columns.Add(
                new Column
                {
                    Title = "Сумма поступления",
                    FieldName = ColumnNames.INCOME_SUM_COLUMN,
                    HasSummary = true
                });

            _columns.AddRange(GetIntermediariesColumns());

            _columns.Add(
                new Column
                {
                    Title = "С посредником",
                    FieldName = ColumnNames.INTERMEDIARY_TOTAL_COLUMN,
                    HasSummary = true
                });
            _columns.Add(
                new Column
                {
                    Title = "Без посредника",
                    FieldName = ColumnNames.WO_INTERMEDIARY_TOTAL_COLUMN,
                    HasSummary = true
                });

            return _columns;
        }

        public DataTable GetData(DateTime since, DateTime till)
        {
            DataTable _table = new DataTable();
            _table.Columns.Add(ColumnNames.STREET_COLUMN, typeof(string));
            _table.Columns.Add(ColumnNames.BUILDING_COLUMN, typeof(string));
            _table.Columns.Add(ColumnNames.INCOME_SUM_COLUMN, typeof(decimal));
            _table.Columns.Add(ColumnNames.INTERMEDIARY_TOTAL_COLUMN, typeof(decimal));
            _table.Columns.Add(ColumnNames.WO_INTERMEDIARY_TOTAL_COLUMN, typeof(decimal));

            List<Column> _cols = GetIntermediariesColumns();
            foreach (Column _col in _cols)
            {
                _table.Columns.Add(_col.FieldName, typeof(decimal));
            }

            using (var _db = new Entities())
            {
                _db.CommandTimeout = 3600;

                var _data = _db.PaymentOpers
                    .Select(p =>
                        new
                        {
                            bID = p.Customers.Buildings.ID,
                            Street = p.Customers.Buildings.Streets.Name,
                            Building = p.Customers.Buildings.Number,
                            IntermediaryID = (int?)p.PaymentSets.Intermediaries.ID,
                            Period = p.PaymentSets.Intermediaries != null ? p.PaymentSets.PaymentDate : p.CreationDateTime,
                            Value = -p.Value
                        })
                    .Where(x => x.Period >= since && x.Period <= till)
                    .GroupBy(x => x.bID)
                    .Select(g =>
                        new
                        {
                            Building = g.Key,
                            Total = g.Sum(x => x.Value),
                            ByIntermediary = g.GroupBy(x => x.IntermediaryID)
                                .Select(byInermediary =>
                                    new
                                    {
                                        ID = byInermediary.Key,
                                        Value = byInermediary.Sum(x => x.Value)
                                    })
                                .ToList()
                        })
                    .ToList()
                    .Join(
                        _db.Buildings.Select(b =>
                            new
                            {
                                b.ID,
                                Street = b.Streets.Name,
                                b.Number
                            }),
                        p => p.Building,
                        b => b.ID,
                        (p, b) =>
                            new
                            {
                                Street = b.Street,
                                Building = b.Number,
                                p.Total,
                                ByIntermediary = p.ByIntermediary.ToDictionary(i => INTERMEDIARY_FIELDNAME_PREFIX + i.ID.ToString())
                            })
                    .OrderBy(x => x.Street)
                    .ThenBy(x => x.Building)
                    .ToList();

                foreach (var _d in _data)
                {
                    DataRow _row = _table.NewRow();
                    _row[ColumnNames.STREET_COLUMN] = _d.Street;
                    _row[ColumnNames.BUILDING_COLUMN] = _d.Building;
                    _row[ColumnNames.INCOME_SUM_COLUMN] = _d.Total;
                    _row[ColumnNames.INTERMEDIARY_TOTAL_COLUMN] = _d.ByIntermediary.Values.Where(i => i.ID.HasValue).Sum(i => i.Value);
                    _row[ColumnNames.WO_INTERMEDIARY_TOTAL_COLUMN] = _d.ByIntermediary.Values.Where(i => !i.ID.HasValue).Sum(i => i.Value);

                    foreach (Column _col in _cols)
                    {
                        _row[_col.FieldName] = _d.ByIntermediary.ContainsKey(_col.FieldName)
                            ? _d.ByIntermediary[_col.FieldName].Value
                            : 0;
                    }

                    _table.Rows.Add(_row);
                }
            }

            return _table;
        }

        private List<Column> GetIntermediariesColumns()
        {
            List<Column> _columns;

            using (Entities _db = new Entities())
            {
                _columns = _db.Intermediaries
                    .Select(i =>
                        new Column
                        {
                            Title = i.Name,
                            FieldName = INTERMEDIARY_FIELDNAME_PREFIX + i.ID.ToString(),
                            HasSummary = true
                        })
                    .ToList();
            }

            return _columns;
        }
    }
}
