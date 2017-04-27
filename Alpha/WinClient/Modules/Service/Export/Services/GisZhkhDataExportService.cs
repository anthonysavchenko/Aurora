using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Library.Services;
using Taumis.EnterpriseLibrary.Win;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Services
{
    public class GisZhkhDataExportService : IGisZhkhDataExportService
    {
        private const string OUTPUT_FILENAME_FORMAT = "GisZhkh_Customers_{0:yyyyMMdd}_{1:000}";
        private const int ROWS_PER_FILE = 1000;
        private const int FIRST_ROW_INDEX = 3;

        #region Sheets & Columns

        private const string BASIC_SHEET = "Основные сведения";
        private const string ROOM_SHEET = "Помещения";

        // BASIC_SHEET

        private const string BASIC_FIRST_COLUMN = "A";
        private const string BASIC_LAST_COLUMN = "Q";

        private const int BASIC_REC_NUM_INDEX = 0;

        /// <summary>
        /// Номер ЛС
        /// </summary>
        private const int ACCOUNT_INDEX = 1;

        /// <summary>
        /// Идентификатор ЖКУ
        /// </summary>
        private const int GIS_ZHKH_ID_INDEX = 2;

        /// <summary>
        /// Тип лицевого счета
        /// </summary>
        private const int ACCOUNT_TYPE_INDEX = 3;

        /// <summary>
        /// Является нанимателем
        /// </summary>
        private const int IS_TENANT_INDEX = 4;

        /// <summary>
        /// Фамилия
        /// </summary>
        private const int SURNAME_INDEX = 5;

        /// <summary>
        /// Имя
        /// </summary>
        private const int FIRST_NAME_INDEX = 6;

        /// <summary>
        /// Отчество
        /// </summary>
        private const int PATRONYMIC_INDEX = 7;

        /// <summary>
        /// Общая площадь
        /// </summary>
        private const int SQUARE_INDEX = 16;

        // ROOM_SHEET

        private const string ROOM_FIRST_COLUMN = "A";
        private const string ROOM_LAST_COLUMN = "E";

        private const int ROOM_REC_NUM_INDEX = 0;

        /// <summary>
        /// Код дома по ФИАС
        /// </summary>
        private const int FIAS_ID_INDEX = 2;

        /// <summary>
        /// Номер помещения
        /// </summary>
        private const int APARTMENT_INDEX = 4;

        #endregion

        private const string ACCOUNT_TYPE = "ЛС УО";
        private const string IS_TENANT = "Нет";

        private class CustomerInfo
        {
            public string Account { get; set; }
            public string GisZhkhID { get; set; }
            public string AccountType { get; set; }
            public string IsTenant { get; set; }
            public string FullName { get; set; }
            public decimal Square { get; set; }
            public string FiasID { get; set; }
            public string Apartment { get; set; }
        }

        #region Help Methods

        private List<CustomerInfo> GetCustomerInfoList(bool onlyNew)
        {
            using (Entities _db = new Entities())
            {
                return _db.Customers
                    .Where(c => !onlyNew || string.IsNullOrEmpty(c.GisZhkhID))
                    .Where(c => c.OwnerType == (int)Customer.OwnerTypes.PhysicalPerson)
                    .Select(c =>
                        new CustomerInfo
                        {
                            Account = c.Account,
                            GisZhkhID = c.GisZhkhID,
                            AccountType = ACCOUNT_TYPE,
                            IsTenant = IS_TENANT,
                            FullName = c.PhysicalPersonFullName,
                            Square = c.Square,
                            FiasID = c.Buildings.FiasID,
                            Apartment = c.Apartment
                        })
                    .ToList();
            }
        }

        private void FillSheets(List<CustomerInfo> customers, ExcelSheet sheet, string outputPath)
        {
            DateTime _now = DateTime.Now;

            for (int i = 0; i < customers.Count; i += ROWS_PER_FILE)
            {
                int _rowsPerFile = Math.Min(ROWS_PER_FILE, customers.Count - i);

                object[,] _basicInfo = new object[_rowsPerFile, SQUARE_INDEX + 1];
                object[,] _roomInfo = new object[_rowsPerFile, APARTMENT_INDEX + 1];

                for (int j = 0; j < _rowsPerFile; j++)
                {
                    _basicInfo[j, BASIC_REC_NUM_INDEX] = (j + 1);
                    _basicInfo[j, ACCOUNT_INDEX] = customers[i + j].Account;
                    _basicInfo[j, GIS_ZHKH_ID_INDEX] = customers[i + j].GisZhkhID;
                    _basicInfo[j, ACCOUNT_TYPE_INDEX] = customers[i + j].AccountType;
                    _basicInfo[j, IS_TENANT_INDEX] = customers[i + j].IsTenant;

                    string[] _name = customers[i + j].FullName.Split(new char[] { }, 3, StringSplitOptions.RemoveEmptyEntries);
                    _basicInfo[j, SURNAME_INDEX] = _name.Length > 0 ? _name[0] : string.Empty;
                    _basicInfo[j, FIRST_NAME_INDEX] = _name.Length > 1 ? _name[1] : string.Empty;
                    _basicInfo[j, PATRONYMIC_INDEX] = _name.Length > 2 ? _name[2] : string.Empty;

                    _basicInfo[j, SQUARE_INDEX] = customers[i + j].Square;

                    _roomInfo[j, ROOM_REC_NUM_INDEX] = (j + 1);
                    _roomInfo[j, FIAS_ID_INDEX] = customers[i + j].FiasID;
                    _roomInfo[j, APARTMENT_INDEX] = customers[i + j].Apartment;
                }

                int _rowTo = FIRST_ROW_INDEX + _rowsPerFile - 1;

                sheet.SetRange(BASIC_SHEET, BASIC_FIRST_COLUMN, FIRST_ROW_INDEX, BASIC_LAST_COLUMN, _rowTo, _basicInfo);
                sheet.SetRange(ROOM_SHEET, ROOM_FIRST_COLUMN, FIRST_ROW_INDEX, ROOM_LAST_COLUMN, _rowTo, _roomInfo);

                sheet.SaveAs($"{outputPath}\\{string.Format(OUTPUT_FILENAME_FORMAT, _now, i / ROWS_PER_FILE + 1)}");

                sheet.SetRange(BASIC_SHEET, BASIC_FIRST_COLUMN, FIRST_ROW_INDEX, BASIC_LAST_COLUMN, _rowTo, string.Empty);
                sheet.SetRange(ROOM_SHEET, ROOM_FIRST_COLUMN, FIRST_ROW_INDEX, ROOM_LAST_COLUMN, _rowTo, string.Empty);
            }
        }

        #endregion


        #region Implementation of IGisZhkhDataExportService

        public string ProcessFile(string inputFileName, bool onlyNew)
        {
            string _result;

            try
            {
                using (ExcelSheet _sheet = new ExcelSheet(inputFileName))
                {
                    string _outputPath = Path.GetDirectoryName(inputFileName);
                    var _customers = GetCustomerInfoList(onlyNew);

                    FillSheets(_customers, _sheet, _outputPath);

                    _result = "Операция выполнена успешно";
                }
            }
            catch (Exception _ex)
            {
                Logger.SimpleWrite($"GisZhkhDataExportService.ProcessFile() error: {_ex} {(_ex.InnerException?.ToString() ?? string.Empty)}");
                _result = "Произошла ошибка. Операция не выполнена";
            }

            return _result;
        }

        #endregion
    }
}
