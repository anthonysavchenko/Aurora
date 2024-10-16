﻿using Microsoft.Practices.CompositeUI;
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
        // TODO перенести в настройки
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
                public const int REQUISITE = 15;
            }
        }

        private class Section3_6Sheet
        {
            public const int INDEX = 2;
            public const int FIRST_ROW_NUM = 4;

            public const string PP_CALC_TYPE = "Норматив";

            public class Columns
            {
                public const int NUMBER = 1;
                public const int SERVICE = 2;
                public const int PP_VOLUME_TYPE = 6;
                public const int PP_VOLUME = 7;
                public const int RATE = 8;
                public const int RECALCULATION = 12;
                public const int BENEFIT = 13;
                public const int TOTAL = 15;
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

        private class Section9Sheet
        {
            public const int INDEX = 9;
            public const int FIRST_ROW_NUM = 2;

            public class Columns
            {
                public const int NUMBER = 1;
                public const int REQUISITE = 2;
                public const int BIK = 3;
                public const int BANK_ACCOUNT = 4;
                public const int SUBTOTAL = 6;
                public const int OVERPAYMENT = 7;
                public const int TOTAL = 8;
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
            public int BankDetailsID { get; set; }
            public int BillID { get; set; }
            public decimal Subtotal { get; set; }
            public decimal Overpayment { get; set; }
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
                        IExcelWorksheet _section9 = _wb.Worksheet(Section9Sheet.INDEX);
                        
                        //Ошибка в шаблоне - удаляем проверку на 7 листе
                        IExcelWorksheet _temp = _wb.Worksheet(7);
                        _temp.ClearDataValidations();

                        int _section1_2Row = Section1_2Sheet.FIRST_ROW_NUM;
                        int _section3_6Row = Section3_6Sheet.FIRST_ROW_NUM;
                        int _section9Row = Section9Sheet.FIRST_ROW_NUM;

                        foreach(CustomerInfo _ci in _byBuilding.Value)
                        {
                            _section1_2.Cell(_section1_2Row, Section1_2Sheet.Columns.GIS_ZHKH_ID).SetValue(_ci.CustomerGisZhkhID);
                            _section1_2.Cell(_section1_2Row, Section1_2Sheet.Columns.DOC_TYPE).SetValue(Section1_2Sheet.DOC_TYPE);
                            _section1_2.Cell(_section1_2Row, Section1_2Sheet.Columns.NUMBER).SetValue(_ci.BillID);
                            _section1_2.Cell(_section1_2Row, Section1_2Sheet.Columns.PERIOD).SetValue(period.ToString("MM.yyyy"));
                            _section1_2.Cell(_section1_2Row, Section1_2Sheet.Columns.AREA).SetValue(_ci.Area);
                            _section1_2.Cell(_section1_2Row, Section1_2Sheet.Columns.REQUISITE).SetValue($"{_ci.BillID}-{_ci.BankDetailsID}");

                            _section9.Cell(_section9Row, Section9Sheet.Columns.NUMBER).SetValue(_ci.BillID);
                            _section9.Cell(_section9Row, Section9Sheet.Columns.REQUISITE).SetValue($"{_ci.BillID}-{_ci.BankDetailsID}");
                            _section9.Cell(_section9Row, Section9Sheet.Columns.BIK).SetValue(_ci.Bik);
                            _section9.Cell(_section9Row, Section9Sheet.Columns.BANK_ACCOUNT).SetValue(_ci.BankAccount);
                            _section9.Cell(_section9Row, Section9Sheet.Columns.SUBTOTAL).SetValue(_ci.Subtotal);
                            _section9.Cell(_section9Row, Section9Sheet.Columns.OVERPAYMENT).SetValue(_ci.Overpayment);
                            _section9.Cell(_section9Row, Section9Sheet.Columns.TOTAL).SetValue(_ci.Total);

                            foreach (BillInfo _bi in _ci.Bills)
                            {
                                _section3_6.Cell(_section3_6Row, Section3_6Sheet.Columns.NUMBER).SetValue(_ci.BillID);
                                _section3_6.Cell(_section3_6Row, Section3_6Sheet.Columns.SERVICE).SetValue(serviceMatchingDict[_bi.ServiceTypeID]);

                                decimal _total = _bi.Total;
                                decimal _rate = _bi.Rate;
                                if (_bi.ServiceTypeID == _maintenanceServiceTypeID)
                                {
                                    _total += _ci.Bills.Where(b => b.IsPublicPlaceService).Sum(b => b.Total);
                                    _rate += _ci.Bills.Where(b => b.IsPublicPlaceService).Sum(b => b.Rate);
                                }
                                _section3_6.Cell(_section3_6Row, Section3_6Sheet.Columns.TOTAL).SetValue(_total);
                                _section3_6.Cell(_section3_6Row, Section3_6Sheet.Columns.RATE).SetValue(_rate);

                                if (_bi.IsPublicPlaceService)
                                {
                                    _section3_6.Cell(_section3_6Row, Section3_6Sheet.Columns.PP_VOLUME_TYPE).SetValue(Section3_6Sheet.PP_CALC_TYPE);
                                    _section3_6.Cell(_section3_6Row, Section3_6Sheet.Columns.PP_VOLUME).SetValue(_ci.Area);
                                }

                                /*
                                if (_bi.Recalculation != 0)
                                {
                                    _section3_6.Cell(_section3_6Row, Section3_6Sheet.Columns.RECALCULATION).SetValue(_bi.Recalculation);
                                }

                                if (_bi.Benefit != 0)
                                {
                                    _section3_6.Cell(_section3_6Row, Section3_6Sheet.Columns.BENEFIT).SetValue(Math.Abs(_bi.Benefit));
                                }
                                */

                                _section3_6Row++;
                            }

                            _section1_2Row++;
                            _section9Row++;
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
                                BankDetaildID = p.RegularBillDocs.Customers.Buildings.BankDetails.ID,
                                p.ServiceTypeID,
                                p.ServiceTypeName,
                                Rate = p.PayRate,
                                p.Recalculation,
                                p.Benefit,
                                Total = p.Payable,
                                p.RegularBillDocs.MonthChargeValue,
                                p.RegularBillDocs.OverpaymentValue,
                                p.RegularBillDocs.Value,
                            })
                        .Where(bi => serviceMatchingDict.Keys.Contains((int)bi.ServiceTypeID))
                        .ToList()
                        .GroupBy(bi => bi.BuildingID)
                        .Select(g =>
                            new
                            {
                                BuildingID = g.Key,
                                Data = g.GroupBy(bi => 
                                    new
                                    {
                                        bi.CustomerGisZhkhID,
                                        bi.Area,
                                        bi.BIK,
                                        bi.BankAccount,
                                        bi.BankDetaildID,
                                        bi.BillID,
                                        bi.MonthChargeValue,
                                        bi.OverpaymentValue,
                                        bi.Value,
                                    })
                                .Select(gByGisZhkhID =>
                                    new CustomerInfo
                                    {
                                        CustomerGisZhkhID = gByGisZhkhID.Key.CustomerGisZhkhID,
                                        Area = gByGisZhkhID.Key.Area,
                                        Bik = gByGisZhkhID.Key.BIK,
                                        BankAccount = gByGisZhkhID.Key.BankAccount,
                                        BankDetailsID = gByGisZhkhID.Key.BankDetaildID,
                                        BillID = gByGisZhkhID.Key.BillID,
                                        Subtotal = gByGisZhkhID.Key.MonthChargeValue,
                                        Overpayment = gByGisZhkhID.Key.OverpaymentValue,
                                        Total = gByGisZhkhID.Key.Value,
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
