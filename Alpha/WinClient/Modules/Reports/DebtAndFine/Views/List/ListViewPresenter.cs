using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.Constants;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.RefBook;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.DebtAndFine.Views.List
{
    public class ListViewPresenter : BaseReportForGridPresenter<IListView, EmptyReportParams>
    {
        private const int FINE_130 = 130;
        private const int FINE_300 = 300;
        /// <summary>
        /// Коичество дней, на которые не начисляется пеня при просрочке платежа
        /// </summary>
        private const int FREE_DAYS = 30;

        /// <summary>
        /// Количество дней, на которые начисляется пеня по ставке 1/300
        /// </summary>
        private const int DAYS_300 = 60;
        private const int PAY_DAY = 10;
        private const int YEAR = 2016;


        private static class Columns
        {
            public static class Main
            {
                public const string YEAR = "Year";
                public const string DEBT = "DebtSum";
                public const string FINE = "FineSum";
            }

            public static class Level1
            {
                public const string YEAR = "Year";
                public const string PERIOD = "Period";
                public const string MAINTENANCE_RATE = "MaintenanceRate";
                public const string REPAIR_RATE = "RepairRate";
                public const string RECYCLING_RATE = "RecyclingRate";
                public const string AREA = "Area";
                public const string DEBT = "Debt";
                public const string DAYS = "Days";
                public const string FINE_RATE = "FineRate";
                public const string FINE = "Fine";
            }

            public static class Level2
            {
                public const string YEAR = "Year";
                public const string PERIOD = "Period";
                public const string MAINTENANCE_RATE = "MaintenanceRate";
                public const string REPAIR_RATE = "RepairRate";
                public const string RECYCLING_RATE = "RecyclingRate";
                public const string AREA = "Area";
                public const string DEBT = "Debt";
                public const string DAYS_300 = "Days300";
                public const string DAYS_130 = "Days130";
                public const string FINE_RATE = "FineRate";
                public const string FINE = "Fine";
            }

            public static class Level3
            {
                public const string YEAR = "Year";
                public const string PERIOD = "Period";
                public const string MAINTENANCE_RATE = "MaintenanceRate";
                public const string ELECTR_RATE = "ElectrRate";
                public const string HOT_WATER_RATE = "HotWaterRate";
                public const string WATER_RATE = "WaterRate";
                public const string AREA = "Area";
                public const string DEBT = "Debt";
                public const string DAYS_300 = "Days300";
                public const string DAYS_130 = "Days130";
                public const string FINE_RATE = "FineRate";
                public const string FINE = "Fine";
            }
        }

        private class Record
        {
            public int ServiceID { get; set; }
            public int Year { get; set; }
            public DateTime Period { get; set; }
            public decimal Debt { get; set; }
            public decimal Days { get; set; }
            public decimal Days300 { get; set; }
            public decimal Days130 { get; set; }
            public decimal Fine { get; set; }
            public decimal RepairRate { get; set;}
            public decimal OldMaintenanceRate { get; set; }
            public decimal RecyclingRate { get; set; }
            public decimal NewMaintenanceRate { get; set; }
            public decimal ElectrRate { get; set; }
            public decimal HotWaterRate { get; set; }
            public decimal WaterRate { get; set; }
        }

        private class ServiceTotal
        {
            public int ID { get; set; }
            public decimal Rate { get; set; }
            public decimal Total { get; set; }
        }

        private class PeriodTotal
        {
            public decimal Total { get; set; }
            public Dictionary<int, ServiceTotal> ServiceBalances { get; set; }
        }

        /// <summary>
        /// Выполняет действия при загрузке вида
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();
            RefreshRefBooks();

            PeriodInfo _periodInfo = ServerTime.GetPeriodInfo();

            View.Since = _periodInfo.LastCharged;
            View.Till = _periodInfo.LastCharged;

            RestoreServiceSettings();
        }

        /// <summary>
        /// Выполняет действия при глобальной команде "Сохранить"
        /// </summary>
        [EventSubscription(CommonEventNames.SaveItemFired, ThreadOption.UserInterface)]
        public void OnSaveItemFired(object sender, EventArgs eventArgs)
        {
            // Если текущий юзкейс не активен - 
            // глобальные команды обрабатывать не нужно.
            if (this.WorkItem.Status == WorkItemStatus.Inactive) return;

            List<int> _repair = ConvertServices(View.RepairServices);
            List<int> _maintenance = ConvertServices(View.MaintenanceServices);
            List<int> _recycling = ConvertServices(View.RecyclingServices);

            string _repairStr = string.Join(",", _repair.ToArray());
            string _maintenanceStr = string.Join(",", _maintenance.ToArray());
            string _recyclingStr = string.Join(",", _recycling.ToArray());

            using (Entities _db = new Entities())
            {
                try
                {
                    var _repairSet = _db.Settings.First(x => x.Name == SettingNames.REPAIR_SERVICE_LIST);
                    var _maintenanceSet = _db.Settings.First(x => x.Name == SettingNames.MAINTENANCE_SERVICE_LIST);
                    var _recyclingSet = _db.Settings.First(x => x.Name == SettingNames.RECYCLING_SERVICE_LIST);

                    _repairSet.Value = _repairStr;
                    _maintenanceSet.Value = _maintenanceStr;
                    _recyclingSet.Value = _recyclingStr;

                    _db.SaveChanges();

                    View.ShowMessage("Изменения успешно сохранены", "");
                }
                catch (Exception _ex)
                {
                    View.ShowMessage($"Не удалось сохранить изменения. Ошибка: {_ex}", "Ошибка сохранения");
                }
            }
        }

        private void RestoreServiceSettings()
        {
            int[] _repair,
                  _maintenance,
                  _recycling;

            using (Entities _db = new Entities())
            {
                try
                {
                    var _repairSet = _db.Settings.First(x => x.Name == SettingNames.REPAIR_SERVICE_LIST);
                    var _maintenanceSet = _db.Settings.First(x => x.Name == SettingNames.MAINTENANCE_SERVICE_LIST);
                    var _recyclingSet = _db.Settings.First(x => x.Name == SettingNames.RECYCLING_SERVICE_LIST);

                    _repair = string.IsNullOrEmpty(_repairSet.Value) 
                        ? new int[0] 
                        : _repairSet.Value.Split(',').Select(id => int.Parse(id)).ToArray();

                    _maintenance = string.IsNullOrEmpty(_maintenanceSet.Value) 
                        ? new int[0] 
                        : _maintenanceSet.Value.Split(',').Select(id => int.Parse(id)).ToArray();

                    _recycling = string.IsNullOrEmpty(_recyclingSet.Value)
                        ? new int[0] 
                        : _recyclingSet.Value.Split(',').Select(id => int.Parse(id)).ToArray();

                    View.RepairServices = GetServicesByIds(_repair, _db);
                    View.MaintenanceServices = GetServicesByIds(_maintenance, _db);
                    View.RecyclingServices = GetServicesByIds(_recycling, _db);
                }
                catch (Exception _ex)
                {
                    View.ShowMessage($"Не удалось загрузить данные. Ошибка: {_ex}", "Ошибка");
                }
            }
        }

        private DataTable GetServicesByIds(int[] ids, Entities db)
        {
            var _list = db.Services.Where(s => ids.Contains(s.ID)).ToList();

            DataTable _dt = new DataTable();
            _dt.Columns.Add("ID");

            foreach(var _srv in _list)
            {
                _dt.Rows.Add(_srv.ID);
            }

            return _dt;
        }

        /// <summary>
        /// Обновляет справочники
        /// </summary>
        private void RefreshRefBooks()
        {
            View.Streets = GetList<Street>();
            View.Services = GetList<Service>();
        }

        /// <summary>
        /// Заполняет список домов
        /// </summary>
        public void FillBuildingList()
        {
            View.Buildings = DataMapper<Building, IBuildingDataMapper>().GetBuildingsOnStreet(GetItem<Street>(View.StreetId));
        }

        public void FillApartmentsList()
        {
            DataTable _dt = new DataTable();
            _dt.Columns.Add("Number");

            int _bID = int.Parse(View.BuildingId);

            using (Entities _db = new Entities())
            {
                var _aps = _db.Customers.Where(c => c.Buildings.ID == _bID).Select(c => c.Apartment).OrderBy(a => a).ToList();
                foreach (var _ap in _aps)
                {
                    _dt.Rows.Add(_ap);
                }
            }

            View.Apartments = _dt;
        }

        /// <summary>
        /// Возвращает данные для табличной части отчета
        /// </summary>
        /// <param name="_params">Параметры отчета</param>
        /// <returns>Данные табличной части отчета</returns>
        protected override DataTable GetGridData(EmptyReportParams _params)
        {
            if (int.Parse(UserHolder.User.ID) != 2)
            {
                return new DataTable();
            }

            DataSet _dataSet = GetDataSet();
            DataTable _dtMain = _dataSet.Tables[0];
            DataTable _dtLevel1 = _dataSet.Tables[1];
            DataTable _dtLevel2 = _dataSet.Tables[2];
            DataTable _dtLevel3 = _dataSet.Tables[3];

            List<int> _repair = ConvertServices(View.RepairServices);
            List<int> _oldMaintenance = ConvertServices(View.MaintenanceServices);
            List<int> _recycling = ConvertServices(View.RecyclingServices);
            //List<int> _newMaintenance = new List<int> { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 31, 32, 33, 36, 40, 62 };
            /*List<int> _electr = new List<int> { 58, 65, 72, 73 };
            List<int> _hotWater = new List<int> { 59 };
            List<int> _water = new List<int> { 60, 66, 67 };*/

            decimal _fineRate = View.FineRate;

            using (var _db = new Entities())
            {
                try
                {
                    string _msg;
                    if (CheckParameters(_db, out _msg))
                    {
                        _db.CommandTimeout = 3600;

                        DateTime _now = ServerTime.GetDateTimeInfo().Now.Date;

                        Tuple<int, decimal> _custInfo = GetCustomerInfo(_db);
                        decimal _area = _custInfo.Item2;
                        int _customerID = _custInfo.Item1;

                        List<int> _newMaintenance = _db.Services.Where(s => s.ServiceTypes.Code == ServiceTypeConstants.MAINTENANCE).Select(s => s.ID).ToList();
                        List<int> _electr = _db.Services.Where(s => s.ServiceTypes.Code == ServiceTypeConstants.PP_ELECTRICITY).Select(s => s.ID).ToList();
                        List<int> _hotWater = _db.Services.Where(s => s.ServiceTypes.Code == ServiceTypeConstants.PP_HOT_WATER).Select(s => s.ID).ToList();
                        List<int> _water = _db.Services.Where(s => s.ServiceTypes.Code == ServiceTypeConstants.PP_WATER).Select(s => s.ID).ToList();

                        List<int> _all = _repair
                            .Concat(_oldMaintenance)
                            .Concat(_recycling)
                            .Concat(_newMaintenance)
                            .Concat(_electr)
                            .Concat(_hotWater)
                            .Concat(_water)
                            .Distinct()
                            .ToList();

                        var _raw = GetBalance(_customerID, View.Since, View.Till, _all, _db)
                            .Select(b => GetRecord(b, _now, Math.Round(_fineRate, 2 , MidpointRounding.AwayFromZero), _repair, _oldMaintenance, _recycling, _newMaintenance, _electr, _hotWater, _water))
                            .GroupBy(b => b.Year)
                            .Select(byYear =>
                                new
                                {
                                    Year = byYear.Key,
                                    DebtSum = Math.Round(byYear.Sum(p => p.Debt), 2, MidpointRounding.AwayFromZero),
                                    FineSum = Math.Round(byYear.Sum(p => p.Fine), 2, MidpointRounding.AwayFromZero),
                                    ByPeriod = byYear.GroupBy(b => b.Period).Select(byPeriod =>
                                        new
                                        {
                                            Period = byPeriod.Key,
                                            RepairRate = Math.Round(byPeriod.Sum(b => b.RepairRate), 2, MidpointRounding.AwayFromZero),
                                            OldMaintenanceRate = Math.Round(byPeriod.Sum(b => b.OldMaintenanceRate), 2, MidpointRounding.AwayFromZero),
                                            RecyclingRate = Math.Round(byPeriod.Sum(b => b.RecyclingRate), 2, MidpointRounding.AwayFromZero),
                                            NewMaintenanceRate = Math.Round(byPeriod.Sum(b => b.NewMaintenanceRate), 2, MidpointRounding.AwayFromZero),
                                            ElectrRate = Math.Round(byPeriod.Sum(b => b.ElectrRate), 2, MidpointRounding.AwayFromZero),
                                            HotWaterRate = Math.Round(byPeriod.Sum(b => b.HotWaterRate), 2, MidpointRounding.AwayFromZero),
                                            WaterRate = Math.Round(byPeriod.Sum(b => b.WaterRate), 2, MidpointRounding.AwayFromZero),
                                            Debt = Math.Round(byPeriod.Sum(b => b.Debt), 2, MidpointRounding.AwayFromZero),
                                            Days = byPeriod.Sum(b => b.Days),
                                            Days300 = byPeriod.Sum(b => b.Days300),
                                            Days130 = byPeriod.Sum(b => b.Days130),
                                            Fine = Math.Round(byPeriod.Sum(b => b.Fine), 2, MidpointRounding.AwayFromZero)
                                        })
                                })
                            .ToList();

                        foreach (var _item in _raw)
                        {
                            _dtMain.Rows.Add(_item.Year, _item.DebtSum, _item.FineSum);

                            foreach (var _subItem in _item.ByPeriod)
                            {
                                if (_item.Year < YEAR)
                                {
                                    _dtLevel1.Rows.Add(
                                        _item.Year,
                                        _subItem.Period,
                                        _subItem.OldMaintenanceRate,
                                        _subItem.RepairRate,
                                        _subItem.RecyclingRate,
                                        _area,
                                        _subItem.Debt,
                                        _subItem.Days,
                                        _fineRate,
                                        _subItem.Fine);
                                }
                                else if (_item.Year == YEAR)
                                {
                                    _dtLevel2.Rows.Add(
                                        _item.Year,
                                        _subItem.Period,
                                        _subItem.OldMaintenanceRate,
                                        _subItem.RepairRate,
                                        _subItem.RecyclingRate,
                                        _area,
                                        _subItem.Debt,
                                        _subItem.Days300,
                                        _subItem.Days130,
                                        _fineRate,
                                        _subItem.Fine);
                                }
                                else
                                {
                                    _dtLevel3.Rows.Add(
                                        _item.Year,
                                        _subItem.Period,
                                        _subItem.NewMaintenanceRate,
                                        _subItem.ElectrRate,
                                        _subItem.HotWaterRate,
                                        _subItem.WaterRate,
                                        _area,
                                        _subItem.Debt,
                                        _subItem.Days300,
                                        _subItem.Days130,
                                        _fineRate,
                                        _subItem.Fine);
                                }
                            }
                        }
                    }
                    else
                    {
                        View.ShowMessage(_msg, "Ошибка");
                    }
                }
                catch (ApplicationException _ex)
                {
                    View.ShowMessage(_ex.Message, "Ошибка формирования отчета");
                }
                catch (Exception _ex)
                {
                    View.ShowMessage(
                        $"Не удалось сформировать отчет. Произошла внутренняя ошибка : {_ex.Message}", 
                        "Ошибка");
                    Logger.SimpleWrite($"Долги и пеня. GetGridData. Exception: {_ex}");
                }
            }
            return _dataSet.Tables[0];
        }

        private List<int> ConvertServices(DataTable dt)
        {
            List<int> _list = new List<int>(dt.Rows.Count);
            foreach (DataRow _row in dt.Rows)
            {
                _list.Add(int.Parse(_row["ID"].ToString()));
            }

            return _list;
        }

        private bool CheckParameters(Entities db, out string message)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (View.SearchType == CustomerSearchType.Address)
            {
                if (string.IsNullOrEmpty(View.BuildingId))
                {
                    stringBuilder.AppendLine("- Выберите дом");
                }
                if (string.IsNullOrEmpty(View.Apartment))
                {
                    stringBuilder.AppendLine("- Выберите квартиру");
                }
            }
            else
            {
                if(!db.Customers.Any(c => c.Account == View.Account))
                {
                    stringBuilder.AppendLine("- Указанный лицевой счет не существует");
                }
            }

            message = stringBuilder.ToString();

            return message.Length == 0;
        }

        private Tuple<int, decimal> GetCustomerInfo(Entities db)
        {
            var _query = db.Customers.Select(c => c);

            if(View.SearchType == CustomerSearchType.Address)
            {
                
                string _apartment = View.Apartment;
                int _id = int.Parse(View.BuildingId);
                

                _query = _query.Where(c => c.Buildings.ID == _id && c.Apartment == View.Apartment);
            }
            else
            {
                _query = _query.Where(c => c.Account == View.Account);
            }

            var _res = _query.Select(c => new { c.ID, c.Square }).First();

            return new Tuple<int, decimal>(_res.ID, _res.Square);
        }

        private Record GetRecord(
            KeyValuePair<DateTime, PeriodTotal> r, 
            DateTime now, 
            decimal fineRate,
            IEnumerable<int> repairSrvIds,
            IEnumerable<int> oldMaintenanceSrvIds,
            IEnumerable<int> recyclingSrvIds,
            IEnumerable<int> newMaintenanceSrvIds,
            IEnumerable<int> electrSrvIds,
            IEnumerable<int> hotWaterSrvIds,
            IEnumerable<int> waterSrvIds)
        {
            int _days, _days300, _days130;
            decimal _fine;
            decimal _debt = r.Value.Total;
            // срок оплаты - 10 число следующего месяца
            DateTime _payLimitDate = r.Key.AddMonths(1).AddDays(PAY_DAY);

            if (r.Key.Year < YEAR)
            {
                _days = (now - _payLimitDate).Days;
                _days300 = _days130 = 0;
                _fine = _debt * _days * fineRate / 100 / FINE_300;
            }
            else
            {
                // месяц после срока оплаты пеня не начисляется
                _days300 = (now - _payLimitDate.AddDays(FREE_DAYS)).Days;
                if(_days300 > DAYS_300)
                {
                    _days300 = DAYS_300;
                }
                else if(_days300 < 0)
                {
                    _days300 = 0;
                }

                _days130 = (now - _payLimitDate.AddDays(FREE_DAYS + DAYS_300)).Days;
                if(_days130 < 0)
                {
                    _days130 = 0;
                }

                _days = 0;

                _fine = _debt * _days300 * fineRate / 100 / FINE_300 + _debt * _days130 * fineRate / 100 / FINE_130;
            }

            return
                new Record
                {
                    Year = r.Key.Year,
                    Days = _days,
                    Days130 = _days130,
                    Days300 = _days300,
                    Period = r.Key,
                    Debt = _debt,
                    Fine = _fine,
                    RepairRate = r.Value.ServiceBalances.Where(sb => repairSrvIds.Contains(sb.Key)).Sum(sb => sb.Value.Rate),
                    OldMaintenanceRate = r.Value.ServiceBalances.Where(sb => oldMaintenanceSrvIds.Contains(sb.Key)).Sum(sb => sb.Value.Rate),
                    RecyclingRate = r.Value.ServiceBalances.Where(sb => recyclingSrvIds.Contains(sb.Key)).Sum(sb => sb.Value.Rate),
                    NewMaintenanceRate = r.Value.ServiceBalances.Where(sb => newMaintenanceSrvIds.Contains(sb.Key)).Sum(sb => sb.Value.Rate),
                    ElectrRate = r.Value.ServiceBalances.Where(sb => electrSrvIds.Contains(sb.Key)).Sum(sb => sb.Value.Rate),
                    HotWaterRate = r.Value.ServiceBalances.Where(sb => hotWaterSrvIds.Contains(sb.Key)).Sum(sb => sb.Value.Rate),
                    WaterRate = r.Value.ServiceBalances.Where(sb => waterSrvIds.Contains(sb.Key)).Sum(sb => sb.Value.Rate),
                };
        }

        private DataSet GetDataSet()
        {
            DataTable _main = new DataTable();
            _main.Columns.Add(Columns.Main.YEAR, typeof(int));
            _main.Columns.Add(Columns.Main.DEBT, typeof(decimal));
            _main.Columns.Add(Columns.Main.FINE, typeof(decimal));

            DataTable _level1 = new DataTable();
            _level1.Columns.Add(Columns.Level1.YEAR, typeof(int));
            _level1.Columns.Add(Columns.Level1.PERIOD, typeof(DateTime));
            _level1.Columns.Add(Columns.Level1.MAINTENANCE_RATE, typeof(decimal));
            _level1.Columns.Add(Columns.Level1.REPAIR_RATE, typeof(decimal));
            _level1.Columns.Add(Columns.Level1.RECYCLING_RATE, typeof(decimal));
            _level1.Columns.Add(Columns.Level1.AREA, typeof(decimal));
            _level1.Columns.Add(Columns.Level1.DEBT, typeof(decimal));
            _level1.Columns.Add(Columns.Level1.DAYS, typeof(int));
            _level1.Columns.Add(Columns.Level1.FINE_RATE, typeof(decimal));
            _level1.Columns.Add(Columns.Level1.FINE, typeof(decimal));

            DataTable _level2 = new DataTable();
            _level2.Columns.Add(Columns.Level2.YEAR, typeof(int));
            _level2.Columns.Add(Columns.Level2.PERIOD, typeof(DateTime));
            _level2.Columns.Add(Columns.Level2.MAINTENANCE_RATE, typeof(decimal));
            _level2.Columns.Add(Columns.Level2.REPAIR_RATE, typeof(decimal));
            _level2.Columns.Add(Columns.Level2.RECYCLING_RATE, typeof(decimal));
            _level2.Columns.Add(Columns.Level2.AREA, typeof(decimal));
            _level2.Columns.Add(Columns.Level2.DEBT, typeof(decimal));
            _level2.Columns.Add(Columns.Level2.DAYS_300, typeof(int));
            _level2.Columns.Add(Columns.Level2.DAYS_130, typeof(int));
            _level2.Columns.Add(Columns.Level2.FINE_RATE, typeof(decimal));
            _level2.Columns.Add(Columns.Level2.FINE, typeof(decimal));

            DataTable _level3 = new DataTable();
            _level3.Columns.Add(Columns.Level3.YEAR, typeof(int));
            _level3.Columns.Add(Columns.Level3.PERIOD, typeof(DateTime));
            _level3.Columns.Add(Columns.Level3.MAINTENANCE_RATE, typeof(decimal));
            _level3.Columns.Add(Columns.Level3.ELECTR_RATE, typeof(decimal));
            _level3.Columns.Add(Columns.Level3.HOT_WATER_RATE, typeof(decimal));
            _level3.Columns.Add(Columns.Level3.WATER_RATE, typeof(decimal));
            _level3.Columns.Add(Columns.Level3.AREA, typeof(decimal));
            _level3.Columns.Add(Columns.Level3.DEBT, typeof(decimal));
            _level3.Columns.Add(Columns.Level3.DAYS_300, typeof(int));
            _level3.Columns.Add(Columns.Level3.DAYS_130, typeof(int));
            _level3.Columns.Add(Columns.Level3.FINE_RATE, typeof(decimal));
            _level3.Columns.Add(Columns.Level3.FINE, typeof(decimal));

            DataSet _dataSet = new DataSet();
            _dataSet.Tables.Add(_main);
            _dataSet.Tables.Add(_level1);
            _dataSet.Tables.Add(_level2);
            _dataSet.Tables.Add(_level3);

            DataColumn[] _key = new DataColumn[] { _dataSet.Tables[0].Columns[Columns.Main.YEAR] };

            _dataSet.Relations.Add("Level1", _key, new DataColumn[] { _dataSet.Tables[1].Columns[Columns.Level1.YEAR] });
            _dataSet.Relations.Add("Level2", _key, new DataColumn[] { _dataSet.Tables[2].Columns[Columns.Level1.YEAR] });
            _dataSet.Relations.Add("Level3", _key, new DataColumn[] { _dataSet.Tables[3].Columns[Columns.Level1.YEAR] });

            return _dataSet;
        }

        private Dictionary<DateTime, PeriodTotal> GetBalance(int customerID, DateTime since, DateTime till, IEnumerable<int> serviceIds, Entities db)
        {
            return db.ChargeOperPoses
                    .Select(p =>
                        new
                        {
                            CustomerID = p.ChargeOpers.Customers.ID,
                            p.ChargeOpers.ChargeSets.Period,
                            ServiceID = p.Services.ID,
                            Rate = p.ChargeOpers.ChargeCorrectionOpers != null ? 0m : p.Value / p.ChargeOpers.Customers.Square,
                            RechargeRate = (decimal)0,
                            p.Value
                        })
                    .Concat(db.RechargeOperPoses
                        .Select(p =>
                            new
                            {
                                CustomerID = p.RechargeOpers.Customers.ID,
                                p.RechargeOpers.RechargeSets.Period,
                                ServiceID = p.Services.ID,
                                Rate = (decimal)0,
                                RechargeRate = (decimal)0,
                                p.Value,
                            }))
                    .Concat(db.RechargeOperPoses
                        .Where(p => p.RechargeOpers.ChildChargeCorrectionOpers == null)
                        .Select(p =>
                            new
                            {
                                CustomerID = p.RechargeOpers.Customers.ID,
                                p.RechargeOpers.ChargeOpers.ChargeSets.Period,
                                ServiceID = p.Services.ID,
                                Rate = (decimal)0,
                                RechargeRate = p.Value / p.RechargeOpers.Customers.Square,
                                Value = (decimal)0,
                            }))
                    .Concat(db.ChargeOperPoses
                        .Where(p => p.ChargeOpers.ChargeCorrectionOpers != null)
                        .Select(p =>
                            new
                            {
                                CustomerID = p.ChargeOpers.Customers.ID,
                                p.ChargeOpers.ChargeCorrectionOpers.Period,
                                ServiceID = p.Services.ID,
                                Rate = (decimal)0,
                                RechargeRate = (decimal)0,
                                Value = -p.Value,
                            }))
                    .Concat(db.RechargeOperPoses
                        .Where(p => p.RechargeOpers.ChildChargeCorrectionOpers != null)
                        .Select(p =>
                            new
                            {
                                CustomerID = p.RechargeOpers.Customers.ID,
                                p.RechargeOpers.ChildChargeCorrectionOpers.Period,
                                ServiceID = p.Services.ID,
                                Rate = (decimal)0,
                                RechargeRate = (decimal)0,
                                Value = -p.Value,
                            }))
                    .Concat(db.BenefitOperPoses
                        .Select(p =>
                            new
                            {
                                CustomerID = p.BenefitOpers.ChargeOpers.Customers.ID,
                                p.BenefitOpers.ChargeOpers.ChargeSets.Period,
                                ServiceID = p.Services.ID,
                                Rate = (decimal)0,
                                RechargeRate = (decimal)0,
                                p.Value,
                            }))
                    .Concat(db.BenefitOperPoses
                        .Where(p => p.BenefitOpers.BenefitCorrectionOpers != null)
                        .Select(p =>
                            new
                            {
                                CustomerID = p.BenefitOpers.ChargeOpers.Customers.ID,
                                p.BenefitOpers.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                ServiceID = p.Services.ID,
                                Rate = (decimal)0,
                                RechargeRate = (decimal)0,
                                Value = -p.Value,
                            }))
                    .Concat(db.RebenefitOperPoses
                        .Select(p =>
                            new
                            {
                                CustomerID = p.RebenefitOpers.RechargeOpers.Customers.ID,
                                p.RebenefitOpers.RechargeOpers.RechargeSets.Period,
                                ServiceID = p.Services.ID,
                                Rate = (decimal)0,
                                RechargeRate = (decimal)0,
                                p.Value,
                            }))
                    .Concat(db.RebenefitOperPoses
                        .Where(p => p.RebenefitOpers.BenefitCorrectionOpers != null)
                        .Select(p =>
                            new
                            {
                                CustomerID = p.RebenefitOpers.RechargeOpers.Customers.ID,
                                p.RebenefitOpers.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                ServiceID = p.Services.ID,
                                Rate = (decimal)0,
                                RechargeRate = (decimal)0,
                                Value = -p.Value,
                            }))
                    .Concat(db.PaymentOperPoses
                        .Select(p =>
                            new
                            {
                                CustomerID = p.PaymentOpers.Customers.ID,
                                p.Period,
                                ServiceID = p.Services.ID,
                                Rate = (decimal)0,
                                RechargeRate = (decimal)0,
                                p.Value
                            }))
                    .Concat(db.PaymentCorrectionOperPoses
                        .Select(p =>
                            new
                            {
                                CustomerID = p.PaymentCorrectionOpers.PaymentOpers.Customers.ID,
                                p.PaymentCorrectionOpers.Period,
                                ServiceID = p.Services.ID,
                                Rate = (decimal)0,
                                RechargeRate = (decimal)0,
                                p.Value
                            }))
                    .Concat(db.OverpaymentOperPoses
                        .Select(p =>
                            new
                            {
                                CustomerID = p.OverpaymentOpers.Customers.ID,
                                p.Period,
                                ServiceID = p.Services.ID,
                                Rate = (decimal)0,
                                RechargeRate = (decimal)0,
                                p.Value,
                            }))
                    .Concat(db.OverpaymentCorrectionOperPoses
                        .Select(p =>
                            new
                            {
                                CustomerID = p.OverpaymentCorrectionOpers.ChargeOpers.Customers.ID,
                                p.OverpaymentCorrectionOpers.Period,
                                ServiceID = p.Services.ID,
                                Rate = (decimal)0,
                                RechargeRate = (decimal)0,
                                p.Value,
                            }))
                    .Where(p => p.CustomerID == customerID && serviceIds.Contains(p.ServiceID) && p.Period >= since && p.Period <= till)
                    .GroupBy(p => p.Period)
                    .Select(byPeriod =>
                        new
                        {
                            Period = byPeriod.Key,
                            Total = byPeriod.Sum(b => b.Value),
                            Balance = byPeriod
                                .GroupBy(b => b.ServiceID)
                                .Select(byService => 
                                    new
                                    {
                                        ServiceID = byService.Key,
                                        Rate = byService.Sum(b => b.Rate),
                                        RechargeRate = byService.Sum(b => b.RechargeRate),
                                        Value = byService.Sum(b => b.Value),
                                    }),
                        })
                    .Where(x => x.Total > 0)
                    .ToDictionary(
                        x => x.Period,
                        x => 
                        new PeriodTotal
                        {
                            Total = x.Total,
                            ServiceBalances = x.Balance.ToDictionary(
                                sb => sb.ServiceID,
                                sb =>
                                new ServiceTotal
                                {
                                    ID = sb.ServiceID,
                                    Rate = sb.RechargeRate > 0.00m ? sb.RechargeRate : sb.Rate,
                                    Total = sb.Value
                                })
                        });
        }
    }
}