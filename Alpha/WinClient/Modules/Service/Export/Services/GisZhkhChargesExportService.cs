using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Constants;
using Taumis.Alpha.Infrastructure.Interface.Services.Excel;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Services
{
    public class GisZhkhChargesExportService : IGisZhkhChargesExportService
    {
        private const int ROWS_PER_FILE = 50000;
        private const string CALCULATED_VALUE = "@";

        private class Section1_2Sheet
        {
            public const int INDEX = 1;
            public const int FIRST_ROW_NUM = 4;

            public const string DOC_TYPE = "Текущий";

            public class Columns
            {
                public const int GIS_ZHKH_ID = 1;
                public const int DOC_TYPE = 2;
                public const int NUMBER = 3;
                public const int PERIOD = 4;
                public const int AREA = 5;
                public const int REQ_DOC_NUMBER = 15;

                public const int REPAIR_RATE = 18;
                public const int REPAIR_CHARGE = 19;
                public const int REPAIR_TOTAL = 23;
            }
        }

        private static class Section3_6Sheet
        {
            public const int INDEX = 2;
            public const int FIRST_ROW_NUM = 4;

            public class Columns
            {
                public const int NUMBER = 1;
                public const int SERVICE = 2;
                public const int RATE = 8;
                public const int RECALCULATION = 12;
                public const int BENEFIT = 13;
                public const int TOTAL = 15;
            }

            public static void WriteLine(
                IExcelWorksheet sheet,
                int row,
                int docNumber,
                string service,
                decimal rate,
                decimal recalculation,
                decimal benefit,
                decimal total)
            {
                sheet.Cell(row, Columns.NUMBER).SetValue(docNumber);
                sheet.Cell(row, Columns.SERVICE).SetValue(service);
                sheet.Cell(row, Columns.RATE).SetValue(rate);

                if (recalculation != 0)
                {
                    sheet.Cell(row, Columns.RECALCULATION).SetValue(recalculation);
                }

                if (benefit != 0)
                {
                    sheet.Cell(row, Columns.BENEFIT).SetValue(Math.Abs(benefit));
                }

                sheet.Cell(row, Columns.TOTAL).SetValue(total);
            }
        }

        private static class PaymentRequisiteSheet
        {
            public const int INDEX = 9;
            public const int FIRST_ROW_NUM = 2;

            public class Columns
            {
                public const int DOC_NUMBER = 1;
                public const int REQ_NUMBER = 2;
                public const int BIK = 3;
                public const int BANK_ACCOUNT = 4;
                public const int MONTH_TOTAL = 6;
                public const int TOTAL = 8;
            }

            public static void WriteLine(
                IExcelWorksheet sheet,
                int row,
                int docNumber,
                string bik,
                string bankAccount,
                decimal monthTotal,
                decimal total)
            {
                sheet.Cell(row, Columns.DOC_NUMBER).SetValue(docNumber);
                sheet.Cell(row, Columns.REQ_NUMBER).SetValue($"{docNumber}-10");
                sheet.Cell(row, Columns.BIK).SetValue(bik);
                sheet.Cell(row, Columns.BANK_ACCOUNT).SetValue(bankAccount);
                sheet.Cell(row, Columns.MONTH_TOTAL).SetValue(monthTotal);
                sheet.Cell(row, Columns.TOTAL).SetValue(total);
            }
        }

        private class ServiceSheet
        {
            public const int INDEX = 10;
            public const int FIRST_ROW_NUM = 2;

            public class Columns
            {
                public const int SERVICE_NAME = 3;
            }
        }

        private class BuildingInfo
        {
            public int ID { get; set; }
            public string Street { get; set; }
            public string Number { get; set; }
        }

        private class CustomerInfo
        {
            public string CustomerGisZhkhID { get; set; }
            public decimal Area { get; set; }
            public string Bik { get; set; }
            public string BankAccount { get; set; }
            public int BillID { get; set; }
            public decimal Total { get; set; }
            public List<BillInfo> Bills { get; set; }
        }

        private class BillInfo
        {
            public int ServiceTypeID { get; set; }
            public decimal ServiceTypeRate { get; set; }
            public decimal Rate { get; set; }
            public decimal Recalculation { get; set; }
            public decimal Benefit { get; set; }
            public decimal Total { get; set; }
            public bool IsPublicPlaceService { get; set; }
        }

        [ServiceDependency]
        public IExcelService ExcelService { get; set; }

        public ExportResult Export(string outputPath, string templatePath, DateTime period, Dictionary<int, string> serviceMatchingDict, Action<int> progressAction)
        {
            ExportResult _result = new ExportResult();

            Dictionary<int, List<CustomerInfo>> _data = GetData(period, serviceMatchingDict);
            FillOutput(outputPath, templatePath, _data, serviceMatchingDict, period, _result, progressAction);

            return _result;
        }

        private void FillOutput(
            string outputPath,
            string templatePath,
            Dictionary<int, List<CustomerInfo>> data,
            Dictionary<int, string> serviceMatchingDict,
            DateTime period,
            ExportResult result,
            Action<int> progressAction)
        {
            int _count = 0;

            try
            {
                Dictionary<int, BuildingInfo> _buildings;
                int _maintenanceServiceTypeID;

                using (Entities _db = new Entities())
                {
                    _buildings = _db.Buildings
                        .Select(b =>
                            new BuildingInfo
                            {
                                ID = b.ID,
                                Street = b.Streets.Name,
                                Number = b.Number
                            })
                        .ToDictionary(b => b.ID);

                    _maintenanceServiceTypeID = _db.ServiceTypes.Where(st => st.Code == ServiceTypeConstants.MAINTENANCE).Select(st => st.ID).First();
                }

                foreach (KeyValuePair<int, List<CustomerInfo>> _byBuilding in data)
                {
                    BuildingInfo _building = _buildings[_byBuilding.Key];

                    string _fileName = $"{_building.Street.Replace(' ', '_')}_{_building.Number.Replace(' ', '_').Replace('\\', '_').Replace('/', '_')}_ПД_{period:yyyy-MM}.xlsx";

                    using (IExcelWorkbook _wb = ExcelService.OpenWorkbook(templatePath))
                    {
                        IExcelWorksheet _section1_2 = _wb.Worksheet(Section1_2Sheet.INDEX);
                        IExcelWorksheet _section3_6 = _wb.Worksheet(Section3_6Sheet.INDEX);
                        IExcelWorksheet paymentRequisitesSheet = _wb.Worksheet(PaymentRequisiteSheet.INDEX);

                        //Ошибка в шаблоне - удаляем проверку на листе "Составляющие стоимости ЭЭ"
                        IExcelWorksheet _temp = _wb.Worksheet(7);
                        _temp.ClearDataValidations();

                        int _section1_2Row = Section1_2Sheet.FIRST_ROW_NUM;
                        int _section3_6Row = Section3_6Sheet.FIRST_ROW_NUM;
                        int paymentRequisitesRow = PaymentRequisiteSheet.FIRST_ROW_NUM;

                        foreach(CustomerInfo _ci in _byBuilding.Value)
                        {
                            _section1_2.Cell(_section1_2Row, Section1_2Sheet.Columns.GIS_ZHKH_ID).SetValue(_ci.CustomerGisZhkhID);
                            _section1_2.Cell(_section1_2Row, Section1_2Sheet.Columns.DOC_TYPE).SetValue(Section1_2Sheet.DOC_TYPE);
                            _section1_2.Cell(_section1_2Row, Section1_2Sheet.Columns.NUMBER).SetValue(_ci.BillID);
                            _section1_2.Cell(_section1_2Row, Section1_2Sheet.Columns.PERIOD).SetValue(period.ToString("MM.yyyy"));
                            _section1_2.Cell(_section1_2Row, Section1_2Sheet.Columns.AREA).SetValue(_ci.Area);
                            _section1_2.Cell(_section1_2Row, Section1_2Sheet.Columns.REQ_DOC_NUMBER).SetValue($"{_ci.BillID}-10");

                            // TODO: Переделать под новый шаблон
                            //_section1_2.Cell(_section1_2Row, Section1_2Sheet.Columns.REPAIR_RATE).SetValue(0);
                            //_section1_2.Cell(_section1_2Row, Section1_2Sheet.Columns.REPAIR_CHARGE).SetValue(0);
                            //_section1_2.Cell(_section1_2Row, Section1_2Sheet.Columns.REPAIR_TOTAL).SetValue(0);

                            var mainPos = _ci.Bills.FirstOrDefault(b => b.ServiceTypeID == _maintenanceServiceTypeID);
                            decimal rate = _ci.Bills.Where(b => b.IsPublicPlaceService).Sum(b => b.Rate);
                            decimal total = _ci.Bills.Where(b => b.IsPublicPlaceService).Sum(b => b.Total);

                            Section3_6Sheet.WriteLine(
                                _section3_6,
                                _section3_6Row,
                                _ci.BillID,
                                serviceMatchingDict[_maintenanceServiceTypeID],
                                (mainPos != null ? mainPos.Rate : 0) + rate,
                                mainPos != null ? mainPos.Recalculation : 0,
                                mainPos != null ? mainPos.Benefit : 0,
                                (mainPos != null ? mainPos.Total : 0) + total);

                            _section3_6Row++;

                            foreach (BillInfo _bi in _ci.Bills.Where(b => b.ServiceTypeID != _maintenanceServiceTypeID))
                            {
                                Section3_6Sheet.WriteLine(
                                    _section3_6,
                                    _section3_6Row,
                                    _ci.BillID,
                                    serviceMatchingDict[_bi.ServiceTypeID],
                                    _bi.Rate,
                                    _bi.Recalculation,
                                    _bi.Benefit,
                                    _bi.Total);

                                _section3_6Row++;
                            }

                            PaymentRequisiteSheet.WriteLine(
                                paymentRequisitesSheet,
                                paymentRequisitesRow,
                                _ci.BillID,
                                _ci.Bik,
                                _ci.BankAccount,
                                _ci.Bills.Sum(b => b.Total),
                                _ci.Total);

                            paymentRequisitesRow++;
                            _section1_2Row++;
                        }

                        _wb.SaveAs($"{outputPath}\\{_fileName}");
                    }

                    progressAction(++_count * 100 / data.Count);
                }
                result.Info = "Операция выполнена успешно";
            }
            catch (Exception _ex)
            {
                Logger.SimpleWrite($"ГИС ЖКХ. Ошибка экспорт начилений. {_ex}");
                result.Info = "Операция не выполнена, возникли ошибки.";
            }

            result.Total = data.Count;
            result.Succeded = _count;
            result.Failed = data.Count - _count;
        }

        private Dictionary<int, List<CustomerInfo>> GetData(DateTime period, Dictionary<int, string> serviceMatchingDict)
        {
            Dictionary<int, List<CustomerInfo>> _result;

            try
            {
                using (Entities _db = new Entities())
                {
                    _db.CommandTimeout = 3600;

                    List<int> _publicPlaceServiceTypes =
                        _db.ServiceTypes
                            .Where(s => 
                                s.Code == ServiceTypeConstants.PP_ELECTRICITY ||
                                s.Code == ServiceTypeConstants.PP_HOT_WATER ||
                                s.Code == ServiceTypeConstants.PP_WATER ||
                                s.Code == ServiceTypeConstants.PP_WASTEWATER)
                            .Select(s => s.ID)
                            .ToList();

                    _result = _db.RegularBillDocSeviceTypePoses
                        .Where(p =>
                            p.RegularBillDocs.Period == period
                            && !string.IsNullOrEmpty(p.RegularBillDocs.Customers.GisZhkhID))
                        .Select(p =>
                            new
                            {
                                BillID = p.RegularBillDocs.ID,
                                BuildingID = p.RegularBillDocs.Customers.Buildings.ID,
                                CustomerGisZhkhID = p.RegularBillDocs.Customers.GisZhkhID,
                                Area = p.RegularBillDocs.Customers.Square,
                                p.RegularBillDocs.Customers.Buildings.BankDetails.BIK,
                                BankAccount = p.RegularBillDocs.Customers.Buildings.BankDetails.Account,
                                p.ServiceTypeID,
                                p.ServiceTypeName,
                                Rate = p.PayRate,
                                p.Recalculation,
                                p.Benefit,
                                Total = p.Payable,
                                BillTotal = p.RegularBillDocs.Value
                            })
                        .Where(bi => serviceMatchingDict.Keys.Contains((int)bi.ServiceTypeID))
                        .ToList()
                        .GroupBy(bi => bi.BuildingID)
                        .Select(g =>
                            new
                            {
                                BuildingID = g.Key,
                                Data = g.GroupBy(bi => new { bi.CustomerGisZhkhID, bi.Area, bi.BIK, bi.BankAccount, bi.BillID, bi.BillTotal })
                                    .Select(gByGisZhkhID =>
                                        new CustomerInfo
                                        {
                                            CustomerGisZhkhID = gByGisZhkhID.Key.CustomerGisZhkhID,
                                            Area = gByGisZhkhID.Key.Area,
                                            Bik = gByGisZhkhID.Key.BIK,
                                            BankAccount = gByGisZhkhID.Key.BankAccount,
                                            BillID = gByGisZhkhID.Key.BillID,
                                            Total = gByGisZhkhID.Key.BillTotal,
                                            Bills = gByGisZhkhID.Select(bi =>
                                                new BillInfo
                                                {
                                                    Rate = bi.Rate,
                                                    Recalculation = bi.Recalculation,
                                                    Benefit = bi.Benefit,
                                                    Total = bi.Total,
                                                    ServiceTypeID = bi.ServiceTypeID.Value,
                                                    IsPublicPlaceService = _publicPlaceServiceTypes.Any(id => id == bi.ServiceTypeID)
                                                }).ToList()
                                        }).ToList()
                            })
                        .ToDictionary(x => x.BuildingID, x => x.Data);
                }
            }
            catch (Exception _ex)
            {
                Logger.SimpleWrite(_ex.ToString());
                _result = new Dictionary<int, List<CustomerInfo>>();
            }

            return _result;
        }

        public List<string> GetGisZhkhServices(string templatePath)
        {
            List<string> _result = new List<string>();

            try
            {
                using (IExcelWorkbook _wb = ExcelService.OpenWorkbook(templatePath))
                {
                    IExcelWorksheet _ws = _wb.Worksheet(ServiceSheet.INDEX);

                    int _rowCount = _ws.GetRowCount() - ServiceSheet.FIRST_ROW_NUM + 1;

                    for (int i = 0; i < _rowCount; i++)
                    {
                        _result.Add(_ws.Cell(i + ServiceSheet.FIRST_ROW_NUM, ServiceSheet.Columns.SERVICE_NAME).Value);
                    }
                    _result.Sort();
                }
            }
            catch (Exception _ex)
            {
                Logger.SimpleWrite($"ГИС ЖКХ. Экспорт начислений. Запрос услуг из шаблона. Ошибка: {_ex}");
            }

            return _result;
        }

        public Dictionary<int, string> GetServices(DateTime period)
        {
            Dictionary<int, string> _result;

            try
            {
                using (Entities _db = new Entities())
                {
                    _result = _db.CustomerPoses
                        .Where(p => p.Since <= period && p.Till >= period)
                        .Select(p =>
                            new
                            {
                                p.Services.ServiceTypes.ID,
                                p.Services.ServiceTypes.Name
                            })
                        .Distinct()
                        .OrderBy(st => st.Name)
                        .ToDictionary(st => st.ID, st => st.Name);
                }
            }
            catch (Exception _ex)
            {
                Logger.SimpleWrite($"ГИС ЖКХ. Экспорт начислений. Запрос видов услуг из БД. Ошибка: {_ex}");
                _result = new Dictionary<int, string>(1);
            }

            return _result;
        }
    }
}
