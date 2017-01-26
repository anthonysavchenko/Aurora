using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Services
{
    public class GisZhkhDataImportService : IGisZhkhDataImportService
    {
        private const int FIRST_ROW_INDEX = 3;

        #region Sheets & Columns

        private const string ACCOUNT_SHEET = "ЕЛС";

        private const string ACCOUNT_FIRST_COLUMN = "A";
        private const string ACCOUNT_LAST_COLUMN = "D";

        /// <summary>
        /// Номер ЛС
        /// </summary>
        private const int ACCOUNT_INDEX = 1;

        /// <summary>
        /// Идентификатор ЖКУ
        /// </summary>
        private const int GIS_ZHKH_ID_INDEX = 3;

        #endregion

        private class CustomerInfo
        {
            public string Account { get; set; }
            public string GisZhkhID { get; set; }
        }

        #region Help Methods

        private List<CustomerInfo> GetCustomerInfoList(ExcelSheet sheet)
        {
            object[,] _range = sheet.GetRange(ACCOUNT_FIRST_COLUMN, FIRST_ROW_INDEX, ACCOUNT_LAST_COLUMN, sheet.RowsCount);
            List<CustomerInfo> _result = new List<CustomerInfo>();

            for (int i = 0; i < _range.GetLength(0); i++)
            {
                _result.Add(new CustomerInfo
                {
                    Account = (string)_range[i, ACCOUNT_INDEX],
                    GisZhkhID = (string)_range[i, GIS_ZHKH_ID_INDEX]
                });
            }

            return _result;
        }

        private void UpdateCustomerZhkhIDs(List<CustomerInfo> customerInfos)
        {
            using (Entities _db = new Entities())
            {
                List<string> _accounts = customerInfos.Select(ci => ci.Account).ToList();
                List<Customers> _customers = _db.Customers.Where(c => _accounts.Contains(c.Account)).ToList();

                foreach (var _customer in _customers)
                {
                    CustomerInfo _customerInfo = customerInfos.FirstOrDefault(ci => ci.Account == _customer.Account);

                    if (_customerInfo != null && _customer.GisZhkhID != _customerInfo.GisZhkhID)
                    {
                        _customer.GisZhkhID = _customerInfo.GisZhkhID;
                    }
                }

                _db.SaveChanges();
            }
        } 

        #endregion

        #region Implementation of IGisZhkhDataImportService

        public string ProcessFile(string inputFileName)
        {
            string _result;

            try
            {
                using (ExcelSheet _sheet = new ExcelSheet(inputFileName, ACCOUNT_SHEET))
                {
                    var _customerInfos = GetCustomerInfoList(_sheet);
                    UpdateCustomerZhkhIDs(_customerInfos);
                }
                _result = "Операция выполнена успешно";
            }
            catch (Exception _ex)
            {
                Logger.SimpleWrite($"GisZhkhDataImportService.ProcessFile() error: {_ex} {(_ex.InnerException?.ToString() ?? string.Empty)}");
                _result = "Произошла ошибка. Операция не выполнена";
            }

            return _result;
        }

        #endregion
    }
}
