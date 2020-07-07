using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.RefBook;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.PrivateCountersVolumes.Views.List
{
    public class ListViewPresenter : BaseReportForGridPresenter<IListView, EmptyReportParams>
    {
        private static class ColumnNames
        {
            public const string APARTMENT_COLUMN = "Apartment";
            public const string COUNTER_MODEL_COLUMN = "CounterModel";
            public const string COUNTER_NUMBER_COLUMN = "CounterNumber";
            public const string M1 = "M1";
            public const string N1 = "N1";
            public const string M2 = "M2";
            public const string N2 = "N2";
            public const string M3 = "M3";
            public const string N3 = "N3";
        }

        private class StringAsNumbersComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                int _x;
                int _y;

                int.TryParse(x, out _x);
                int.TryParse(y, out _y);

                return _x - _y;
            }
        }

        public override void OnViewReady()
        {
            base.OnViewReady();

            DataTable _buildings = new DataTable();

            _buildings.Columns.Add("ID", typeof(string));
            _buildings.Columns.Add("Building", typeof(string));

            using (Entities _entities = new Entities())
            {
                var _buildingList =
                    _entities.Buildings
                        .ToList()
                        .Select(x =>
                            new
                            {
                                x.ID,
                                Building = $"{x.Street}, д. {x.Number}",
                            });

                foreach (var _building in _buildingList)
                {
                    _buildings.Rows.Add(_building.ID.ToString(), _building.Building);
                }
            }

            View.Buildings = _buildings;

            DateTimeInfo _dateTimeInfo = ServerTime.GetDateTimeInfo();

            View.Since = _dateTimeInfo.SinceMonthBeginning;
            View.Till = _dateTimeInfo.TillToday;
        }

        /// <summary>
        /// Обрабатывает данные для табличной части отчета 
        /// </summary>
        protected override void ProcessGridData()
        {
            View.ClearColumns();

            DateTime april = new DateTime(2020, 04, 01);
            DateTime may = new DateTime(2020, 05, 01);
            DateTime june = new DateTime(2020, 06, 01);

            View.AddColumn(ColumnNames.APARTMENT_COLUMN, "Квартира");
            View.AddColumn(ColumnNames.COUNTER_MODEL_COLUMN, "Модель");
            View.AddColumn(ColumnNames.COUNTER_NUMBER_COLUMN, "Номер");
            if (View.Since <= april && april <= View.Till) View.AddMoneyColumn(ColumnNames.M1, "Апрель, по нашим данным");
            if (View.Since <= april && april <= View.Till) View.AddMoneyColumn(ColumnNames.N1, "Апрель, по данным ДЭК");
            if (View.Since <= may && may <= View.Till) View.AddMoneyColumn(ColumnNames.M2, "Май, по нашим данным");
            if (View.Since <= may && may <= View.Till) View.AddMoneyColumn(ColumnNames.N2, "Май, по данным ДЭК");
            if (View.Since <= june && june <= View.Till) View.AddMoneyColumn(ColumnNames.M3, "Июнь, по нашим данным");
            if (View.Since <= june && june <= View.Till) View.AddMoneyColumn(ColumnNames.N3, "Июнь, по данным ДЭК");
            base.ProcessGridData();
        }

        /// <summary>
        /// Возвращает данные для табличной части отчета
        /// </summary>
        /// <param name="_params">Параметры отчета</param>
        /// <returns>Данные табличной части отчета</returns>
        protected override DataTable GetGridData(EmptyReportParams _params)
        {
            DataTable _table = new DataTable();

            DateTime april = new DateTime(2020, 04, 01);
            DateTime may = new DateTime(2020, 05, 01);
            DateTime june = new DateTime(2020, 06, 01);

            _table.Columns.Add(ColumnNames.APARTMENT_COLUMN, typeof(string));
            _table.Columns.Add(ColumnNames.COUNTER_MODEL_COLUMN, typeof(string));
            _table.Columns.Add(ColumnNames.COUNTER_NUMBER_COLUMN, typeof(string));
            if (View.Since <= april && april <= View.Till) _table.Columns.Add(ColumnNames.M1, typeof(decimal));
            if (View.Since <= april && april <= View.Till) _table.Columns.Add(ColumnNames.N1, typeof(decimal));
            if (View.Since <= may && may <= View.Till) _table.Columns.Add(ColumnNames.M2, typeof(decimal));
            if (View.Since <= may && may <= View.Till) _table.Columns.Add(ColumnNames.N2, typeof(decimal));
            if (View.Since <= june && june <= View.Till) _table.Columns.Add(ColumnNames.M3, typeof(decimal));
            if (View.Since <= june && june <= View.Till) _table.Columns.Add(ColumnNames.N3, typeof(decimal));

            using (var _db = new Entities())
            {
                _db.CommandTimeout = 3600;

                var _privateCountersVolumes1 =
                    _db.PrivateCounterValues
                        .Where(x =>
                            x.PrivateCounters.Customers.Buildings.ID.ToString() == View.BuildingId
                            && x.Month == april.Date)
                        .Select(x =>
                            new
                            {
                                x.PrivateCounters.Customers.Apartment,
                                x.PrivateCounters.Model,
                                x.PrivateCounters.Number,
                                x.Value,
                            })
                        .ToList();

                var _privateCountersVolumes2 =
                    _db.PrivateCounterValues
                        .Where(x =>
                            x.PrivateCounters.Customers.Buildings.ID.ToString() == View.BuildingId
                            && x.Month == may.Date)
                        .Select(x =>
                            new
                            {
                                x.PrivateCounters.Customers.Apartment,
                                x.PrivateCounters.Model,
                                x.PrivateCounters.Number,
                                x.Value,
                            })
                        .ToList();

                var _privateCountersVolumes3 =
                    _db.PrivateCounterValues
                        .Where(x =>
                            x.PrivateCounters.Customers.Buildings.ID.ToString() == View.BuildingId
                            && x.Month == june.Date)
                        .Select(x =>
                            new
                            {
                                x.PrivateCounters.Customers.Apartment,
                                x.PrivateCounters.Model,
                                x.PrivateCounters.Number,
                                x.Value,
                            })
                        .ToList();


                for (int i = 0; i < _privateCountersVolumes2.Count; i++)
                {
                    var _volume = _privateCountersVolumes2[i];

                    DataRow _row = _table.NewRow();

                    _row[ColumnNames.APARTMENT_COLUMN] = _volume.Apartment;
                    _row[ColumnNames.COUNTER_MODEL_COLUMN] = _volume.Model;
                    _row[ColumnNames.COUNTER_NUMBER_COLUMN] = _volume.Number;

                    if (View.Since <= april && april <= View.Till) _row[ColumnNames.M1] = _privateCountersVolumes1.Count > i ? _privateCountersVolumes1[i].Value : 0;
                    if (View.Since <= april && april <= View.Till) _row[ColumnNames.N1] = 0;

                    if (View.Since <= may && may <= View.Till) _row[ColumnNames.M2] = _privateCountersVolumes2.Count > i ? _privateCountersVolumes2[i].Value : 0;
                    if (View.Since <= may && may <= View.Till) _row[ColumnNames.N2] = 0;

                    if (View.Since <= june && june <= View.Till) _row[ColumnNames.M3] = _privateCountersVolumes3.Count > i ? _privateCountersVolumes3[i].Value : 0;
                    if (View.Since <= june && june <= View.Till) _row[ColumnNames.N3] = 0;

                    _table.Rows.Add(_row);
                }
            }

            return _table;
        }
    }
}
