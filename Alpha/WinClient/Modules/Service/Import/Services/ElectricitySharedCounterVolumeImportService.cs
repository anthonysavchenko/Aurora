using System;
using System.Linq;
using System.Collections.Generic;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Services.Excel;
using System.Text;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Services
{
    public class ElectricitySharedCounterVolumeImportService : IImportService
    {
        private static class Columns
        {
            public const int STREET = 1;
            public const int BUILDING = 2;
            public const int VOLUME = 3;
        }

        private class ParsedRow
        {
            public int RowNumber { get; set; }
            public string Street { get; set; }
            public string Building { get; set; }
            public decimal Volume { get; set; }
        }

        private IExcelService _excelService;

        public ElectricitySharedCounterVolumeImportService(IExcelService excelService)
        {
            _excelService = excelService;
        }

        public string ProcessFile(string inputFileName, Action<int> reportProgressAction, DateTime? period = null)
        {
            List<ParsedRow> _parsedRows = ParseFile(inputFileName, reportProgressAction, out string message);

            if (string.IsNullOrEmpty(message) && _parsedRows.Count > 0)
            {
                message = Save(_parsedRows, period.Value, reportProgressAction);
            }

            return message;
        }

        private List<ParsedRow> ParseFile(string fileName, Action<int> reportProgressAction, out string message)
        {
            int _row = 1;
            List<ParsedRow> _rows = null;

            try
            {
                using (IExcelWorkbook _xwb = _excelService.OpenWorkbook(fileName))
                {
                    IExcelWorksheet _xws = _xwb.Worksheet(1);
                    int _rowCount = _xws.GetRowCount();
                    _rows = new List<ParsedRow>(_rowCount);

                    while (_row < _rowCount)
                    {
                        _rows.Add(
                            new ParsedRow
                            {
                                RowNumber = ++_row,
                                Building = _xws.Cell(_row, Columns.BUILDING).Value,
                                Street = _xws.Cell(_row, Columns.STREET).Value,
                                Volume = Convert.ToDecimal(_xws.Cell(_row, Columns.VOLUME).Value)
                            });
                        reportProgressAction(_row * 50 / _rowCount);
                    }
                }

                message = string.Empty;
            }
            catch (Exception _ex)
            {
                message = $"Не удалось прочитать строку {_row}.\r\n\r\n{_ex.Message}";
            }

            return _rows;
        }

        private string Save(List<ParsedRow> parsedRows, DateTime period, Action<int> reportProgressAction)
        {
            StringBuilder _erorrs = new StringBuilder();

            foreach (var _row in parsedRows)
            {
                using (var _db = new Entities())
                {
                    try
                    {
                        int _buildingId = _db.Buildings
                            .Where(x => x.Streets.Name.ToLower() == _row.Street.ToLower() && x.Number.ToLower() == _row.Building.ToLower())
                            .Select(x => x.ID)
                            .FirstOrDefault();

                        if (_buildingId <= 0)
                        {
                            throw new ApplicationException("Не найден дом в БД");
                        }

                        ElectricitySharedCounterVolumes _item = _db.ElectricitySharedCounterVolumes.FirstOrDefault(x => x.BuildingID == _buildingId && x.Period == period);

                        if (_item == null)
                        {
                            _item = new ElectricitySharedCounterVolumes
                            {
                                BuildingID = _buildingId,
                                Period = period
                            };
                            _db.ElectricitySharedCounterVolumes.AddObject(_item);
                        }

                        _item.Volume = Math.Round(_row.Volume, 3, MidpointRounding.AwayFromZero);

                        _db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        _erorrs.AppendLine($"Строка {_row.RowNumber}: {ex.Message} [ {_row.Street} {_row.Building} {_row.Volume} ]");
                    }
                }
            }

            return _erorrs.Length > 0 ? _erorrs.ToString() : "Импорт данных выполнен успешно";
        }
    }
}
