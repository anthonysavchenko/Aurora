using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Services.Excel;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Services
{
    public class CounterValuesExportService : ICounterValuesExportService
    {
        private const int COLLECT_FORM_FIRST_ROW = 14;
        private const int FORM2_FIRST_ROW = 1;

        private class CollectFormColumns
        {
            public const string COUNTER_NUM = "D";
            public const string VALUE = "L";
        }

        private class Form2Columns
        {
            public const string COUNTER_NUM = "G";
            public const string VALUE = "K";
        }

        private class Row
        {
            public int Num { get; set; }
            public string CounterNum { get; set; }
        }

        private class CounterValue
        {
            public decimal Value { get; set; }
            public bool IsNight { get; set; }
        }

        private IExcelService _excelService;

        [InjectionConstructor]
        public CounterValuesExportService([ServiceDependency]IExcelService excelService)
        {
            _excelService = excelService;
        }

        public ExportResult Export(string collectFormFilePath, string form2FilePath, DateTime period, Action<int> progressAction)
        {
            ExportResult _result = new ExportResult();

            try
            {
                List<Row> _collectFormRows = GetRows(collectFormFilePath, COLLECT_FORM_FIRST_ROW, CollectFormColumns.COUNTER_NUM);
                progressAction(25);

                List<Row> _form2Rows = GetRows(form2FilePath, FORM2_FIRST_ROW, Form2Columns.COUNTER_NUM);
                progressAction(50);

                Dictionary<string, CounterValue[]> _data = GetData(period);

                WriteData(
                    collectFormFilePath,
                    _data,
                    _collectFormRows,
                    CollectFormColumns.VALUE,
                    (prev, cur, values) => values.Length > 0
                        ? values.Length > 1
                            ? $"{values[0].Value:0}\r\n{values[1].Value:0}"
                            : values[0].Value.ToString("0")
                        : string.Empty);

                progressAction(75);

                WriteData(
                    form2FilePath,
                    _data,
                    _form2Rows,
                    Form2Columns.VALUE,
                    (prevRow, curRow, values) => values.Length > 0
                        ? values.Length > 1
                            ? prevRow != null && prevRow.CounterNum == curRow.CounterNum
                                ? values[0].Value.ToString("0")
                                : values[1].Value.ToString("0")
                            : values[0].Value.ToString("0")
                        : string.Empty);

                progressAction(100);

                _result.Info = "Операция выполнена успешно";
            }
            catch (Exception _ex)
            {
                Logger.SimpleWrite($"Export error: {_ex}");
                _result.Info = $"Произошла ошибка. Операция не выполнена. Подробности: {_ex}";
                progressAction(100);
            }

            return _result;
        }

        private List<Row> GetRows(string path, int firstRowNum, string counterNumCol)
        {
            List<Row> _rows = new List<Row>();

            using (IExcelWorkbook _wb = _excelService.OpenWorkbook(path))
            {
                IExcelWorksheet _ws = _wb.Worksheet(1);
                int _rowCount = _ws.GetRowCount();

                for (int r = firstRowNum; r <= _rowCount; r++)
                {
                    string _counterNum = _ws.Cell(r, counterNumCol).Value;
                    if (Regex.IsMatch(_counterNum, @"\d"))
                    {
                        int _whitespaceIndex = _counterNum.IndexOf(' ');
                        if (_whitespaceIndex >= 0)
                        {
                            _counterNum = _counterNum.Substring(0, _whitespaceIndex);
                        }

                        _rows.Add(
                            new Row
                            {
                                CounterNum = _counterNum,
                                Num = r
                            });
                    }
                }
            }

            return _rows;
        }

        private void WriteData(
            string path,
            Dictionary<string, CounterValue[]> data, 
            List<Row> rows, 
            string valueCol,
            Func<Row, Row, CounterValue[], string> getValueFunc)
        {
            using (IExcelWorkbook _wb = _excelService.OpenWorkbook(path))
            {
                IExcelWorksheet _ws = _wb.Worksheet(1);
                Row _prevRow = null;

                foreach (var _row in rows)
                {
                    if (!data.ContainsKey(_row.CounterNum))
                    {
                        continue;
                    }

                    CounterValue[] _values = data[_row.CounterNum];
                    string value = getValueFunc(_prevRow, _row, _values);

                    _ws.Cell(_row.Num, valueCol).SetValue(value);
                    _prevRow = _row;
                }

                _wb.Save();
            }
        }

        private Dictionary<string, CounterValue[]> GetData(DateTime period)
        {
            const string DAY_POSTFIX = " - Д";
            const string NIGHT_POSTFIX = " - Н";

            Dictionary<string, CounterValue[]> _result;

            using (var _db = new Entities())
            {
                _result = _db.PrivateCounterValues
                    .Where(x => x.Period == period)
                    .Select(x =>
                        new
                        {
                            x.PrivateCounters.Customers.Account,
                            Number = x.PrivateCounters.Number.EndsWith(DAY_POSTFIX)
                                ? x.PrivateCounters.Number.Replace(DAY_POSTFIX, "")
                                : x.PrivateCounters.Number.EndsWith(NIGHT_POSTFIX)
                                    ? x.PrivateCounters.Number.Replace(NIGHT_POSTFIX, "")
                                    : x.PrivateCounters.Number,
                            IsNight = x.PrivateCounters.Number.EndsWith(NIGHT_POSTFIX),
                            x.Value
                        })
                    .GroupBy(x => x.Number)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(x => 
                            new CounterValue
                            {
                                Value = x.Value,
                                IsNight = x.IsNight
                            })
                        .OrderBy(x => x.IsNight)
                        .ToArray());
            }

            return _result;
        }
    }
}
