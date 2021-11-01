﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Services.Excel;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Services
{
    public class BuildingConsumptionImportService : IImportService
    {
        private const string TEMPLATE_PATH = "Templates\\BuildingConsumptionImport.xlsx";
        private const int FIRST_ROW = 3;

        private class Columns
        {
            public const string BUILDING_ID = "A";
            public const string ADDRESS = "B";
            public const string ELECTR_VOL = "C";
            public const string ELECTR_ODN_VOL = "D";
            public const string ELECTR_COUNTER_VOL = "E";
            public const string HOT_WATER_VOL = "F";
            public const string HOT_WATER_ODN_VOL = "G";
            public const string HOT_WATER_COUNTER_VALUE = "H";
            public const string COLD_WATER_VOL = "I";
            public const string COLD_WATER_ODN_VOL = "J";
            public const string COLD_WATER_COUNTER_VALUE = "K";
            public const string WASTE_WATER_VOL = "L";
            public const string WASTE_WATER_ODN_VOL = "M";
            public const string HEATING_VOL = "N";
            public const string HEATING_ODN_VOL = "O";
            public const string HEATING_COUNTER_VALUE = "P";
        }

        private class ParsedRow
        {
            public int RowNumber { get; set; }
            public int BuildingId { get; set; }
            public string ElectrVol { get; set; }
            public string ElectrOdnVol { get; set; }
            public string ElectrCounterVol { get; set; }
            public string HotWaterVol { get; set; }
            public string HotWaterOdnVol { get; set; }
            public string HotWaterCounterValue { get; set; }
            public string ColdWaterVol { get; set; }
            public string ColdWaterOdnVol { get; set; }
            public string ColdWaterCounterValue { get; set; }
            public string WasteWaterVol { get; set; }
            public string WasteWaterOdnVol { get; set; }
            public string HeatingVol { get; set; }
            public string HeatingOdnVol { get; set; }
            public string HeatingCounterValue { get; set; }
        }

        private class BuildingInfo
        {
            public int ID { get; set; }
            public string Address { get; set; }
        }

        private readonly IExcelService _excelService;

        public BuildingConsumptionImportService(IExcelService excelService)
        {
            _excelService = excelService;
        }

        public string ProcessFile(string inputFileName, Action<int> reportProgressAction, DateTime? period)
        {
            List<ParsedRow> _rows = ParseFile(inputFileName, reportProgressAction, out string message);

            if (string.IsNullOrEmpty(message) && _rows.Count > 0)
            {
                message = Save(_rows, period.Value, reportProgressAction);
            }

            return message;
        }

        private string Save(List<ParsedRow> rows, DateTime period, Action<int> reportProgressAction)
        {
            int saved = 0;
            StringBuilder _erorrs = new StringBuilder();

            foreach (ParsedRow _row in rows)
            {
                if (!ValidateParsedRow(_row))
                {
                    continue;
                }

                using (var _db = new Entities())
                {
                    try
                    {
                        BuildingConsumptions _item = _db.BuildingConsumptions
                            .FirstOrDefault(x => x.BuildingID == _row.BuildingId && x.Period == period);
                        if (_item == null)
                        {
                            _item =
                                new BuildingConsumptions
                                {
                                    BuildingID = _row.BuildingId,
                                    Period = period
                                };
                            _db.BuildingConsumptions.AddObject(_item);
                        }

                        _item.ElectrVol = _row.ElectrVol;
                        _item.ElectrOdnVol = _row.ElectrOdnVol;
                        _item.ElectrCounterValue = _row.ElectrCounterVol;
                        _item.HotWaterVol = _row.HotWaterVol;
                        _item.HotWaterOdnVol = _row.HotWaterOdnVol;
                        _item.HotWaterCounterValue = _row.HotWaterCounterValue;
                        _item.ColdWaterVol = _row.ColdWaterVol;
                        _item.ColdWaterOdnVol = _row.ColdWaterOdnVol;
                        _item.ColdWaterCounterValue = _row.ColdWaterCounterValue;
                        _item.WasteWaterVol = _row.WasteWaterVol;
                        _item.WasteWaterOdnVol = _row.WasteWaterOdnVol;
                        _item.HeatingVol = _row.HeatingVol;
                        _item.HeatingOdnVol = _row.HeatingOdnVol;
                        _item.HeatingCounterValue = _row.HeatingCounterValue;

                        _db.SaveChanges();
                        saved++;
                    }
                    catch (Exception ex)
                    {
                        _erorrs.AppendLine($"Строка {_row.RowNumber}: {ex.Message}");
                    }
                }
            }

            return _erorrs.Length > 0 
                ? _erorrs.ToString()
                : $"Импорт данных выполнен успешно, сохранено строк: {saved}";
        }

        private List<ParsedRow> ParseFile(string inputFileName, Action<int> reportProgressAction, out string message)
        {
            int _row = FIRST_ROW;
            List<ParsedRow> _rows = null;

            try
            {
                using (IExcelWorkbook _xwb = _excelService.OpenWorkbook(inputFileName))
                {
                    IExcelWorksheet _xws = _xwb.Worksheet(1);
                    int _rowCount = _xws.GetRowCount();
                    _rows = new List<ParsedRow>(_rowCount);

                    for (; _row <= _rowCount; _row++)
                    {
                        _xws.Cell(_row, Columns.BUILDING_ID).TryGetValue(out int buildingID);
                        string electrVol = _xws.Cell(_row, Columns.ELECTR_VOL).Value;
                        string electrOdnVol = _xws.Cell(_row, Columns.ELECTR_ODN_VOL).Value;
                        string electrCounterVol = _xws.Cell(_row, Columns.ELECTR_COUNTER_VOL).Value;
                        string hotWaterVol = _xws.Cell(_row, Columns.HOT_WATER_VOL).Value;
                        string hotWaterOdnVol = _xws.Cell(_row, Columns.HOT_WATER_ODN_VOL).Value;
                        string hotWaterCounterValue = _xws.Cell(_row, Columns.HOT_WATER_COUNTER_VALUE).Value;
                        string coldWaterVol = _xws.Cell(_row, Columns.COLD_WATER_VOL).Value;
                        string coldWaterOdnVol = _xws.Cell(_row, Columns.COLD_WATER_ODN_VOL).Value;
                        string coldWaterCounterValue = _xws.Cell(_row, Columns.COLD_WATER_COUNTER_VALUE).Value;
                        string wasteWaterVol = _xws.Cell(_row, Columns.WASTE_WATER_VOL).Value;
                        string wasteWaterOdnVol = _xws.Cell(_row, Columns.WASTE_WATER_ODN_VOL).Value;
                        string heatingVol = _xws.Cell(_row, Columns.HEATING_VOL).Value;
                        string heatingOdnVol = _xws.Cell(_row, Columns.HEATING_ODN_VOL).Value;
                        string heatingCounterValue = _xws.Cell(_row, Columns.HEATING_COUNTER_VALUE).Value;

                        if (buildingID == default
                            && string.IsNullOrEmpty(electrVol)
                            && string.IsNullOrEmpty(electrOdnVol)
                            && string.IsNullOrEmpty(electrCounterVol)
                            && string.IsNullOrEmpty(hotWaterVol)
                            && string.IsNullOrEmpty(hotWaterOdnVol)
                            && string.IsNullOrEmpty(hotWaterCounterValue)
                            && string.IsNullOrEmpty(coldWaterVol)
                            && string.IsNullOrEmpty(coldWaterOdnVol)
                            && string.IsNullOrEmpty(coldWaterCounterValue)
                            && string.IsNullOrEmpty(wasteWaterVol)
                            && string.IsNullOrEmpty(wasteWaterOdnVol)
                            && string.IsNullOrEmpty(heatingVol)
                            && string.IsNullOrEmpty(heatingOdnVol)
                            && string.IsNullOrEmpty(heatingCounterValue))
                        {
                            reportProgressAction(_row * 50 / _rowCount);
                            continue;
                        }

                        if (buildingID == default)
                        {
                            throw new Exception($"Указан неправильный ID (идентификатор дома) в строке {_row}.");
                        }

                        _rows.Add(
                            new ParsedRow
                            {
                                RowNumber = _row,
                                BuildingId = buildingID,
                                ElectrVol = _xws.Cell(_row, Columns.ELECTR_VOL).Value,
                                ElectrOdnVol = _xws.Cell(_row, Columns.ELECTR_ODN_VOL).Value,
                                ElectrCounterVol = _xws.Cell(_row, Columns.ELECTR_COUNTER_VOL).Value,
                                HotWaterVol = _xws.Cell(_row, Columns.HOT_WATER_VOL).Value,
                                HotWaterOdnVol = _xws.Cell(_row, Columns.HOT_WATER_ODN_VOL).Value,
                                HotWaterCounterValue = _xws.Cell(_row, Columns.HOT_WATER_COUNTER_VALUE).Value,
                                ColdWaterVol = _xws.Cell(_row, Columns.COLD_WATER_VOL).Value,
                                ColdWaterOdnVol = _xws.Cell(_row, Columns.COLD_WATER_ODN_VOL).Value,
                                ColdWaterCounterValue = _xws.Cell(_row, Columns.COLD_WATER_COUNTER_VALUE).Value,
                                WasteWaterVol = _xws.Cell(_row, Columns.WASTE_WATER_VOL).Value,
                                WasteWaterOdnVol = _xws.Cell(_row, Columns.WASTE_WATER_ODN_VOL).Value,
                                HeatingVol = _xws.Cell(_row, Columns.HEATING_VOL).Value,
                                HeatingOdnVol = _xws.Cell(_row, Columns.HEATING_ODN_VOL).Value,
                                HeatingCounterValue = _xws.Cell(_row, Columns.HEATING_COUNTER_VALUE).Value,
                            });
                        reportProgressAction(_row * 50 / _rowCount);
                    }

                    message = string.Empty;
                }
            }
            catch (Exception ex)
            {
                message = $"Не удалось прочитать строку {_row}.\r\n\r\n{ex.Message}";
            }

            return _rows;
        }

        public bool GenerateImportTemplate(string path)
        {
            bool _result = true;
            
            try
            {
                List<BuildingInfo> _buildings;
                using (var _db = new Entities())
                {
                    _buildings = _db.Buildings
                        .Select(x =>
                            new BuildingInfo
                            {
                                ID = x.ID,
                                Address = x.Streets.Name + ", " + x.Number
                            })
                        .ToList();
                }

                if (_buildings.Count > 0)
                {
                    int _row = FIRST_ROW;

                    using (IExcelWorkbook _xwb = _excelService.OpenWorkbook(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, TEMPLATE_PATH)))
                    {
                        IExcelWorksheet _xws = _xwb.Worksheet(1);

                        foreach (var _building in _buildings)
                        {
                            _xws.Cell(_row, Columns.BUILDING_ID).SetValue(_building.ID);
                            _xws.Cell(_row, Columns.ADDRESS).SetValue(_building.Address);
                            _row++;
                        }

                        _xwb.SaveAs(path);
                    }
                }
            }
            catch(Exception ex)
            {
                _result = false;
                Logger.SimpleWrite($"Шаблон импорта. Ошибка {ex}");
            }

            return _result;
        }

        private bool ValidateParsedRow(ParsedRow row)
        {
            return row.BuildingId > 0
                && !(string.IsNullOrEmpty(row.ElectrVol) && string.IsNullOrEmpty(row.ElectrOdnVol) && string.IsNullOrEmpty(row.ElectrCounterVol)
                && string.IsNullOrEmpty(row.HotWaterVol) && string.IsNullOrEmpty(row.HotWaterOdnVol) && string.IsNullOrEmpty(row.HotWaterCounterValue)
                && string.IsNullOrEmpty(row.ColdWaterVol) && string.IsNullOrEmpty(row.ColdWaterOdnVol) && string.IsNullOrEmpty(row.ColdWaterCounterValue)
                && string.IsNullOrEmpty(row.HeatingVol) && string.IsNullOrEmpty(row.HeatingOdnVol) && string.IsNullOrEmpty(row.HeatingCounterValue)
                && string.IsNullOrEmpty(row.WasteWaterVol) && string.IsNullOrEmpty(row.WasteWaterOdnVol));
        }
    }
}
