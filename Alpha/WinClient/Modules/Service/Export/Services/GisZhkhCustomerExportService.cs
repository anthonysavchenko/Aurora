using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Services
{
    public class GisZhkhCustomerExportService : IGisZhkhCustomerExportService
    {
        private const string TEMPLATE_PATH = "Templates\\GisZhkh_Customer_Export.xlsx";

        private const string BASIC_SHEET = "Основные сведения";
        private const string ROOM_SHEET = "Помещения";
        private const string ACCOUNT_TYPE = "ЛС УО";
        private const string IS_TENANT = "Нет";
        private const int FIRST_ROW_NUM = 3;

        private class Columns
        {
            public class BasicSheet
            {
                public const int REC_NUM = 1;
                public const int ACCOUNT = 2;
                public const int GIS_ZHKH_ID = 3;
                public const int ACCOUNT_TYPE = 4;
                public const int IS_TENANT = 5;
                public const int SURNAME = 6;
                public const int NAME = 7;
                public const int PATRONYMIC = 8;
                public const int AREA = 17;
                public const int FIRST = 1;
                public const int LAST = 22;
            }

            public class RoomSheet
            {
                public const int REC_NUM = 1;
                public const int FIAS_ID = 3;
                public const int APARTMENT = 5;
                public const int FIRST = 1;
                public const int LAST = 9;
            }
        }

        private class BuildingInfo
        {
            public int ID { get; set; }
            public string Number { get; set; }
            public string Street { get; set; }
        }

        private class CustomerInfo
        {
            public string Account { get; set; }
            public string GisZhkhID { get; set; }
            public string FullName { get; set; }
            public decimal Square { get; set; }
            public string Apartment { get; set; }
            public string FiasID { get; set; }
        }

        #region Help Methods

        private Dictionary<int, List<CustomerInfo>> GetData(bool onlyNew)
        {
            using (Entities _db = new Entities())
            {
                return _db.Customers
                    .Where(c => !onlyNew || string.IsNullOrEmpty(c.GisZhkhID))
                    .GroupBy(c => c.Buildings.ID)
                    .Select(g =>
                        new
                        {
                            BuildingID = g.Key,
                            Customers = g
                                .Select(c =>
                                    new CustomerInfo
                                    {
                                        Account = c.Account,
                                        GisZhkhID = c.GisZhkhID,
                                        FullName = c.OwnerType == (int)Customer.OwnerTypes.PhysicalPerson ? c.PhysicalPersonFullName : c.JuridicalPersonFullName,
                                        Square = c.Square,
                                        Apartment = c.Apartment,
                                        FiasID = c.Buildings.FiasID
                                    })
                                .OrderBy(c => c.Account)
                                .ToList()
                        })
                    .ToDictionary(x => x.BuildingID, x => x.Customers);
            }
        }

        private void FillSheets(Dictionary<int, List<CustomerInfo>> customersByBuilding, string outputPath, string templatePath, Action<int> progressAction)
        {
            int _total = customersByBuilding.Values.Sum(v => v.Count);
            DateTime _now = DateTime.Now;
            Dictionary<int, BuildingInfo> _buildings;

            using (Entities _db = new Entities())
            {
                _buildings = _db.Buildings
                    .Where(b => customersByBuilding.Keys.Contains(b.ID))
                    .Select(b =>
                        new BuildingInfo
                        {
                            ID = b.ID,
                            Number = b.Number,
                            Street = b.Streets.Name
                        })
                    .ToDictionary(s => s.ID);
            }

            int _processed = 1;

            foreach (KeyValuePair<int, List<CustomerInfo>> _pair in customersByBuilding)
            {
                BuildingInfo _building = _buildings[_pair.Key];
                int _recNum = 1;
                int _row = 3;

                using (XLWorkbook _xwb = new XLWorkbook(templatePath))
                {
                    IXLWorksheet _basicSheet = _xwb.Worksheet(BASIC_SHEET);
                    IXLWorksheet _roomSheet = _xwb.Worksheet(ROOM_SHEET);
                    
                    foreach(CustomerInfo _ci in _pair.Value)
                    {
                        _basicSheet.Cell(_row, Columns.BasicSheet.REC_NUM).SetValue(_recNum);
                        _basicSheet.Cell(_row, Columns.BasicSheet.ACCOUNT).SetValue(_ci.Account);
                        _basicSheet.Cell(_row, Columns.BasicSheet.GIS_ZHKH_ID).SetValue(_ci.GisZhkhID);
                        _basicSheet.Cell(_row, Columns.BasicSheet.ACCOUNT_TYPE).SetValue(ACCOUNT_TYPE);
                        _basicSheet.Cell(_row, Columns.BasicSheet.IS_TENANT).SetValue(IS_TENANT);
                        _basicSheet.Cell(_row, Columns.BasicSheet.AREA).SetValue(_ci.Square);

                        string[] _name = _ci.FullName.Split(new char[] { }, 3, StringSplitOptions.RemoveEmptyEntries);
                        if (_name.Length == 3)
                        {
                            _basicSheet.Cell(_row, Columns.BasicSheet.SURNAME).SetValue(_name[0]);
                            _basicSheet.Cell(_row, Columns.BasicSheet.NAME).SetValue(_name[1]);
                            _basicSheet.Cell(_row, Columns.BasicSheet.PATRONYMIC).SetValue(_name[2]);
                        }
                        else
                        {
                            _basicSheet.Cell(_row, Columns.BasicSheet.SURNAME).SetValue(_ci.FullName);
                        }

                        _roomSheet.Cell(_row, Columns.RoomSheet.REC_NUM).SetValue(_recNum);
                        _roomSheet.Cell(_row, Columns.RoomSheet.FIAS_ID).SetValue(_ci.FiasID);
                        _roomSheet.Cell(_row, Columns.RoomSheet.APARTMENT).SetValue(_ci.Apartment);

                        progressAction(_processed++ * 100 / _total);
                        _recNum++;
                        _row++;
                    }

                    string _buildingNumber = _building.Number.Replace(' ', '_').Replace('\\', '-').Replace('/', '-');
                    _xwb.SaveAs($"{outputPath}\\{_building.Street.Replace(' ','_')}_{_buildingNumber}_абоненты_{_now:yyyyMMddHHmm}.xlsx");
                }
            }
        }

        #endregion

        #region Implementation of IGisZhkhDataExportService

        public ExportResult Export(string outputPath, bool exportOnlyNew, Action<int> progressAction)
        {
            ExportResult _result = new ExportResult();

            try
            {
                Dictionary<int, List<CustomerInfo>> _customersByBuilding = GetData(exportOnlyNew);
                FillSheets(_customersByBuilding, outputPath, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, TEMPLATE_PATH), progressAction);
                _result.Info = "Операция выполнена успешно";
            }
            catch (Exception _ex)
            {
                Logger.SimpleWrite($"GisZhkhDataExportService.ProcessFile() error: {_ex}");
                _result.Info = "Произошла ошибка. Операция не выполнена";
            }

            return _result;
        }

        #endregion
    }
}