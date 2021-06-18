using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Interface.Services.Excel;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Services
{
    public class PublicPlaceServiceVolumesImportService : IImportService
    {
        public const int SERVICE_FIRST_COLUMN = 3;
        public const int TITLE_ROW = 1;

        private class BuildingColumns
        {
            public const int ID = 1;
            public const int Address = 2;
        }

        private class ServiceColumns
        {
            public const int NAME = 1;
            public const int ID = 2;
        }

        private class ServiceVolume
        {
            public int Service { get; set; }
            public decimal Volume { get; set; }
        }

        private class Service
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        private class Building
        {
            public int ID { get; set; }
            public string Address { get; set; }
        }

        private readonly IExcelService _excelService;
        private readonly IServerTimeService _serverTimeService;

        public PublicPlaceServiceVolumesImportService(IExcelService excelService, IServerTimeService serverTimeService)
        {
            _excelService = excelService;
            _serverTimeService = serverTimeService;
        }

        public bool GenerateImportTemplate(string path)
        {
            DateTime _now = _serverTimeService.GetDateTimeInfo().Now;
            List<Service> _services;
            List<Building> _buildings;
            bool _result;

            try
            {
                using (Entities _db = new Entities())
                {
                    var _raw = _db.CustomerPoses
                        .Where(p => p.Till >= _now && p.Services.ChargeRule == (byte)ChargeRuleType.PublicPlaceVolumeAreaRate);

                    _services = _raw
                        .Select(p => p.Services)
                        .Distinct()
                        .Select(s =>
                            new Service
                            {
                                ID = s.ID,
                                Name = s.Name
                            })
                        .ToList();

                    _buildings = _raw
                        .Select(p => p.Customers.Buildings)
                        .Distinct()
                        .Select(b =>
                            new Building
                            {
                                ID = b.ID,
                                Address = b.Streets.Name + ", " + b.Number
                            })
                        .OrderBy(b => b.Address)
                        .ToList();
                }

                using (IExcelWorkbook _wb = _excelService.CreateWorkbook())
                {
                    IExcelWorksheet _wsData = _wb.AddWorksheet("Данные");
                    IExcelWorksheet _wsService = _wb.AddWorksheet("Справочник услуг");

                    _wsData.Cell(TITLE_ROW, BuildingColumns.ID).Value = "ID";
                    _wsData.Cell(TITLE_ROW, BuildingColumns.Address).Value = "Дом";

                    int _row = 0;
                    foreach (Service _s in _services)
                    {
                        _row++;
                        _wsService.Cell(_row, ServiceColumns.NAME).Value = _s.Name;
                        _wsService.Cell(_row, ServiceColumns.ID).SetValue(_s.ID);

                        _wsData.Cell(TITLE_ROW, SERVICE_FIRST_COLUMN + _row - 1).Value = _s.Name;
                    }

                    _row = 1;
                    foreach (Building _b in _buildings)
                    {
                        _row++;
                        _wsData.Cell(_row, BuildingColumns.ID).SetValue(_b.ID);
                        _wsData.Cell(_row, BuildingColumns.Address).Value = _b.Address;
                    }

                    _wsData.AdjustColumnsToContents();
                    _wsService.AdjustColumnsToContents();
                    _wb.SaveAs(path);
                }
                _result = true;
            }
            catch (Exception _ex)
            {
                _result = false;
                Logger.SimpleWrite($"Импорт. Шаблон импорта показаний ОДПУ для начислений. Ошибка {_ex}");
            }

            return _result;
        }

        public string ProcessFile(string inputFileName, Action<int> reportProgressAction, DateTime? period)
        {
            StringBuilder _errors = new StringBuilder();
            Dictionary<int, List<ServiceVolume>> _data = GetData(inputFileName, reportProgressAction, _errors);

            if (_errors.Length == 0 && _data != null && _data.Count > 0)
            {
                SaveData(_data, period.Value, reportProgressAction, _errors);
            }

            return _errors.Length > 0
                ? $"Не удалось завершить операцию импорта.\r\n{_errors}"
                : "Импорт успешно выполнен.";
        }

        Dictionary<string, int> GetServices(IExcelWorkbook wb)
        {
            Dictionary<string, int> _result = new Dictionary<string, int>();

            try
            {
                IExcelWorksheet _ws = wb.Worksheet(2);
                int _rowCount = _ws.GetRowCount();

                for (int r = 1; r <= _rowCount; r++)
                {
                    _result.Add(
                        _ws.Cell(r, ServiceColumns.NAME).Value,
                        int.Parse(_ws.Cell(r, ServiceColumns.ID).Value));
                }
            }
            catch (Exception _ex)
            {
                Logger.SimpleWrite("Импорт показаний ОДПУ для начислений. " +
                    $"Не удалось разобрать раздел с услугами: {_ex}");
            }

            return _result;
        }

        private Dictionary<int, List<ServiceVolume>> GetData(string inputFileName, Action<int> reportProgressAction, StringBuilder errors)
        {
            Dictionary<int, List<ServiceVolume>> _data = null;

            try
            {
                using (IExcelWorkbook _wb = _excelService.OpenWorkbook(inputFileName))
                {
                    Dictionary<string, int> _services = GetServices(_wb);

                    if (_services.Count > 0)
                    {
                        IExcelWorksheet _ws = _wb.Worksheet(1);
                        int _lastColumn = _ws.GetLastUsedColumnNumber();
                        int _rowCount = _ws.GetRowCount();

                        if (_rowCount > 1)
                        {
                            Dictionary<int, string> _serviceByColumn = new Dictionary<int, string>();

                            for (int i = SERVICE_FIRST_COLUMN; i <= _lastColumn; i++)
                            {
                                _serviceByColumn.Add(i, _ws.Cell(TITLE_ROW, i).Value);
                            }

                            _data = new Dictionary<int, List<ServiceVolume>>();

                            for (int r = 2; r <= _rowCount; r++)
                            {
                                try
                                {
                                    _ws.Cell(r, BuildingColumns.ID).TryGetValue(out int buildingID);

                                    if (buildingID == default
                                        && string.IsNullOrEmpty(_ws.Cell(r, BuildingColumns.Address).Value))
                                    {
                                        continue;
                                    }

                                    if (buildingID == default)
                                    {
                                        errors.AppendLine(
                                            $"Строка {r}, колонка {BuildingColumns.ID}: " +
                                                "отсутствует идентификатор дома");
                                        break;
                                    }

                                    List<ServiceVolume> _svList = new List<ServiceVolume>();
                                    for (int c = SERVICE_FIRST_COLUMN; c <= _lastColumn; c++)
                                    {
                                        try
                                        {
                                            string _volStr = _ws.Cell(r, c).Value;
                                            if (!string.IsNullOrEmpty(_volStr))
                                            {
                                                decimal _volume = decimal.Parse(_volStr);
                                                _svList.Add(
                                                    new ServiceVolume
                                                    {
                                                        Service = _services[_serviceByColumn[c]],
                                                        Volume = _volume
                                                    });
                                            }
                                        }
                                        catch (Exception _ex)
                                        {
                                            errors.AppendLine($"Строка {r}, колонка {c}: {_ex}");
                                        }
                                    }

                                    _data.Add(buildingID, _svList);
                                }
                                catch (Exception _ex)
                                {
                                    errors.AppendLine($"Строка {r}: {_ex}");
                                }

                                reportProgressAction(r * 50 / (_rowCount - 1));
                            }
                        }
                    }
                    else
                    {
                        errors.AppendLine("Ошибка при разборе листа со списком услуг");
                    }
                }
            }
            catch (IOException _ex)
            {
                errors.AppendLine("Ошибка при открытии файла для чтения. Убедитесь, что файл не открыт в другой программе.");
                Logger.SimpleWrite($"PublicPlaceServiceVolumesImportService. {_ex}");
            }
            catch (Exception _ex)
            {
                errors.AppendLine($"Произошла ошибка при чтении файла.");
                Logger.SimpleWrite($"PublicPlaceServiceVolumesImportService. {_ex}");
            }

            return _data;
        }

        private void SaveData(Dictionary<int, List<ServiceVolume>> data, DateTime period, Action<int> reportProgressAction, StringBuilder errors)
        {
            using (Entities _db = new Entities())
            {
                int _processed = 0;
                foreach (var _b in data)
                {
                    foreach (ServiceVolume _sv in _b.Value)
                    {
                        PublicPlaceServiceVolumes _item = _db.PublicPlaceServiceVolumes
                            .Where(p => p.Period == period && p.ServiceID == _sv.Service && p.BuildingID == _b.Key)
                            .FirstOrDefault();

                        if (_sv.Volume > 0)
                        {
                            if (_item == null)
                            {
                                _item =
                                    new PublicPlaceServiceVolumes
                                    {
                                        BuildingID = _b.Key,
                                        ServiceID = _sv.Service
                                    };
                                _db.PublicPlaceServiceVolumes.AddObject(_item);
                            }

                            _item.Period = period;
                            _item.Volume = _sv.Volume;
                        }
                        else if (_item != null)
                        {
                            _db.DeleteObject(_item);
                        }
                    }

                    reportProgressAction(++_processed * 50 / data.Count + 50);
                }
                _db.SaveChanges();
            }
        }
    }
}
