using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Services
{
    public class GisZhkhChargesExportService : IDataExportService
    {
        private const string OUTPUT_FILENAME_FORMAT = "{0:yyyy-MM}_{1}_{2}_{3:000}";
        private const int ROWS_PER_FILE = 1000;

        private const string DOC_TYPE = "Текущий";
        private const string BIC = "040507705";
        private const string ACCOUNT = "40702810900100001650";
        private const string CALCULATED_VALUE = "@";

        #region Sheets & Columns

        private const string SERVICES_SHEET = "Услуги исполнителя";
        private const string SECTIONS12_SHEET = "Разделы 1-2";
        private const string SECTIONS36_SHEET = "Разделы 3-6";

        // SERVICES_SHEET

        private const int SERVICES_FIRST_ROW = 2;

        private const string SERVICES_FIRST_COLUMN = "C";
        private const string SERVICES_LAST_COLUMN = "D";

        private const int GISZHKH_SERVICE_ID_INDEX = 1;
        private const int GISZHKH_SERVICE_NAME_INDEX = 0;

        //SECTIONS12_SHEET

        private const int SECTIONS12_FIRST_ROW = 4;

        private const string SECTIONS12_FIRST_COLUMN = "A";
        private const string SECTIONS12_LAST_COLUMN = "M";

        private const int CUSTOMER_GISZHKH_ID_INDEX = 0;
        private const int DOC_TYPE_INDEX = 1;
        private const int DOC_NUMBER_INDEX = 2;
        private const int PERIOD_INDEX = 3;
        private const int SQUARE_INDEX = 4;
        private const int LIVING_AREA_INDEX = 5;
        private const int HEATED_AREA_INDEX = 6;
        private const int RESIDENTS_COUNT_INDEX = 7;
        private const int BIC_INDEX = 11;
        private const int ACCOUNT_INDEX = 12;

        // SECTIONS36_SHEET

        private const int SECTIONS36_FIRST_ROW = 5;

        private const string SECTIONS36_FIRST_COLUMN = "A";
        private const string SECTIONS36_LAST_COLUMN = "X";

        private const int SERVICE_DOC_NUMBER_INDEX = 0;
        private const int SERVICE_NAME_INDEX = 1;
        private const int PAY_RATE_INDEX = 6;
        private const int RECALCULATION_INDEX = 7;
        private const int BENEFIT_INDEX = 8;
        private const int PAYABLE_INDEX = 23;

        #endregion

        #region Help Classes

        private class DocInfo
        {
            public int Id { get; set; }
            public string GisZhkhId { get; set; }
            public DateTime Period { get; set; }
            public decimal Square { get; set; }
        }

        private class DocInfoComparer : IEqualityComparer<DocInfo>
        {
            public bool Equals(DocInfo x, DocInfo y) => x.Id == y.Id;

            public int GetHashCode(DocInfo obj) => obj.Id.GetHashCode();
        }

        private class ServiceInfo
        {
            public int DocId { get; set; }
            public string GisZhkhId { get; set; }
            public decimal PayRate { get; set; }
            public decimal Recalculation { get; set; }
            public decimal Benefit { get; set; }
            public decimal Payable { get; set; }
        }

        private class BuildingInfo
        {
            public string StreetName { get; set; }
            public string BuildingNumber { get; set; }
            public ILookup<DocInfo, ServiceInfo> Docs { get; set; }
        }

        #endregion

        #region Help Methods

        private List<BuildingInfo> GetBuildings(Entities db, DateTime period)
        {
            return db.RegularBillDocSeviceTypePoses
                .Where(stp => stp.RegularBillDocs.Period == period)
                .Select(stp => new
                    {
                        StreetName = stp.RegularBillDocs.Customers.Buildings.Streets.Name,
                        BuildingNumber = stp.RegularBillDocs.Customers.Buildings.Number,
                        CustomerGisZhkhId = stp.RegularBillDocs.Customers.GisZhkhID,
                        RegularBillDocId = stp.RegularBillDocs.ID,
                        stp.RegularBillDocs.Period,
                        stp.RegularBillDocs.Customers.Square,
                        GisZhkhId = stp.GisZhkhID,
                        stp.PayRate,
                        stp.Recalculation,
                        stp.Benefit,
                        stp.Payable
                    })
                .ToList()
                .GroupBy(s => new { s.StreetName, s.BuildingNumber },
                    (key, services) => new BuildingInfo
                    {
                        StreetName = key.StreetName,
                        BuildingNumber = key.BuildingNumber,
                        Docs = services
                            .ToLookup(s => new DocInfo
                                {
                                    Id = s.RegularBillDocId,
                                    GisZhkhId = s.CustomerGisZhkhId,
                                    Period = s.Period,
                                    Square = s.Square
                                },
                                s => new ServiceInfo
                                {
                                    DocId = s.RegularBillDocId,
                                    GisZhkhId = s.GisZhkhId,
                                    PayRate = s.PayRate,
                                    Recalculation = s.Recalculation,
                                    Benefit = s.Benefit,
                                    Payable = s.Payable
                                },
                                new DocInfoComparer())
                    })
                .ToList();
        }

        private Dictionary<string, string> GetGisZhkhServices(ExcelSheet sheet)
        {
            sheet.SelectSheet(SERVICES_SHEET);
            object[,] _servicesArray =
                sheet.GetRange(SERVICES_FIRST_COLUMN, SERVICES_FIRST_ROW, SERVICES_LAST_COLUMN, sheet.RowsCount);

            Dictionary<string, string> _services = new Dictionary<string, string>();

            for (int i = 0; i < _servicesArray.GetLength(0); i++)
            {
                _services.Add(
                    (string)_servicesArray[i, GISZHKH_SERVICE_ID_INDEX],
                    (string)_servicesArray[i, GISZHKH_SERVICE_NAME_INDEX]);
            }

            return _services;
        }

        private void FillSheets(ExcelSheet sheet, DateTime period, string outputPath,
            List<BuildingInfo> buildings, Dictionary<string, string> gisZhkhServices)
        {
            foreach (var _building in buildings)
            {
                int _docsCount = _building.Docs.Select(d => d.Key).Count();
                int _filesCount = _docsCount / ROWS_PER_FILE + (_docsCount % ROWS_PER_FILE == 0 ? 0 : 1);

                for (int i = 0; i < _filesCount; i++)
                {
                    Regex _regex = new Regex("[\\s\\.\\,/\\\\]", RegexOptions.Compiled);

                    string _fileName = string.Format(OUTPUT_FILENAME_FORMAT, period, 
                        _regex.Replace(_building.StreetName.Trim(), "_"),
                        _regex.Replace(_building.BuildingNumber.Trim(), "_"), i + 1);

                    var _docs = _building.Docs.Select(d => d.Key).Skip(i * ROWS_PER_FILE).Take(ROWS_PER_FILE).ToList();
                    var _services = _docs.SelectMany(d => _building.Docs[d]).ToList();

                    object[,] _docsArray = GetDocsArray(_docs);
                    object[,] _servicesArray = GetServicesArray(_services, gisZhkhServices);

                    sheet.SetRange(SECTIONS12_SHEET, 
                        SECTIONS12_FIRST_COLUMN, SECTIONS12_FIRST_ROW,
                        SECTIONS12_LAST_COLUMN, SECTIONS12_FIRST_ROW + _docs.Count - 1, 
                        _docsArray);
                    sheet.SetRange(SECTIONS36_SHEET, 
                        SECTIONS36_FIRST_COLUMN, SECTIONS36_FIRST_ROW,
                        SECTIONS36_LAST_COLUMN, SECTIONS36_FIRST_ROW + _services.Count - 1, 
                        _servicesArray);

                    sheet.SaveAs(Path.Combine(outputPath, _fileName));

                    sheet.SetRange(SECTIONS12_SHEET, 
                        SECTIONS12_FIRST_COLUMN, SECTIONS12_FIRST_ROW,
                        SECTIONS12_LAST_COLUMN, SECTIONS12_FIRST_ROW + _docs.Count - 1, 
                        null);
                    sheet.SetRange(SECTIONS36_SHEET, 
                        SECTIONS36_FIRST_COLUMN, SECTIONS36_FIRST_ROW,
                        SECTIONS36_LAST_COLUMN, SECTIONS36_FIRST_ROW + _services.Count - 1, 
                        null);
                }
            }
        }

        private object[,] GetDocsArray(List<DocInfo> docs)
        {
            object[,] _docsArray = new object[docs.Count, ACCOUNT_INDEX + 1];

            for (int i = 0; i < docs.Count; i++)
            {
                _docsArray[i, CUSTOMER_GISZHKH_ID_INDEX] = docs[i].GisZhkhId;
                _docsArray[i, DOC_TYPE_INDEX] = DOC_TYPE;
                _docsArray[i, DOC_NUMBER_INDEX] = docs[i].Id.ToString();
                _docsArray[i, PERIOD_INDEX] = docs[i].Period.ToString("MM.yyyy");
                _docsArray[i, SQUARE_INDEX] = docs[i].Square;
                _docsArray[i, LIVING_AREA_INDEX] = CALCULATED_VALUE;
                _docsArray[i, HEATED_AREA_INDEX] = CALCULATED_VALUE;
                _docsArray[i, RESIDENTS_COUNT_INDEX] = CALCULATED_VALUE;
                _docsArray[i, BIC_INDEX] = BIC;
                _docsArray[i, ACCOUNT_INDEX] = ACCOUNT;
            }

            return _docsArray;
        }

        private object[,] GetServicesArray(List<ServiceInfo> services, Dictionary<string, string> gisZhkhServices)
        {
            object[,] _servicesArray = new object[services.Count, PAYABLE_INDEX + 1];

            for (int i = 0; i < services.Count; i++)
            {
                string _serviceName = string.IsNullOrEmpty(services[i].GisZhkhId)
                    ? string.Empty
                    : gisZhkhServices[services[i].GisZhkhId];

                _servicesArray[i, SERVICE_DOC_NUMBER_INDEX] = services[i].DocId;
                _servicesArray[i, SERVICE_NAME_INDEX] = _serviceName;
                _servicesArray[i, PAY_RATE_INDEX] = services[i].PayRate;
                _servicesArray[i, RECALCULATION_INDEX] = services[i].Recalculation;
                _servicesArray[i, BENEFIT_INDEX] = services[i].Benefit;
                _servicesArray[i, PAYABLE_INDEX] = services[i].Payable;
            }

            return _servicesArray;
        }

        #endregion

        #region Implementation of IDataExportService

        /// <summary>
        /// Производит экспорт данных в файл
        /// </summary>
        /// <param name="inputFile">
        ///     Имя результирующего файла либо шаблона, на основании которого будут созданы файлы с экспортируемыми данными
        /// </param>
        /// <param name="additionalParams">Дополнительные параметры</param>
        /// <returns>Строка с результатом экспорта</returns>
        public bool ProcessFile(string inputFile, params object[] additionalParams)
        {
            if (!File.Exists(inputFile))
            {
                Logger.SimpleWrite($"GisZhkhChargesExportService.ProcessFile(): файл \"{inputFile}\" не найден");
                return false;
            }

            if (additionalParams.Length < 1 || !(additionalParams[0] is DateTime))
            {
                Logger.SimpleWrite("GisZhkhChargesExportService.ProcessFile(): additionalParams[0] не задан или не DateTime");
                return false;
            }

            bool _result;
            DateTime _period = (DateTime)additionalParams[0];

            try
            {
                List<BuildingInfo> _buildings;

                using (Entities _db = new Entities())
                {
                    _buildings = GetBuildings(_db, _period);
                }

                using (ExcelSheet _sheet = new ExcelSheet(inputFile))
                {
                    string _outputPath = Path.GetDirectoryName(inputFile);
                    var _gisZhkhServices = GetGisZhkhServices(_sheet);
                    FillSheets(_sheet, _period, _outputPath, _buildings, _gisZhkhServices);
                }

                _result = true;
            }
            catch (Exception _ex)
            {
                Logger.SimpleWrite($"GisZhkhChargesExportService.ProcessFile(): {_ex}");
                _result = false;
            }

            return _result;
        }

        #endregion
    }
}
