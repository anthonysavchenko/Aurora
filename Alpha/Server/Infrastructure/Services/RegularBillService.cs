using System;
using System.Data;
using System.Linq;
using System.Text;
using Taumis.Alpha.Server.Core.Services;
using Taumis.Alpha.Server.Core.Services.RegularBill;
using Taumis.Alpha.Server.Core.Services.RegularBill.DataSets;
using Taumis.Alpha.Server.Core.Services.ServerTime;
using Taumis.Alpha.Server.Infrastructure.Data;

namespace Taumis.Alpha.Server.Infrastructure.Services
{
    public class RegularBillService : IRegularBillService
    {
        private const string BARCODE_COMPANY_CODE = "133";
        private const string BARCODE_SERVICE_CODE = "21";

        private readonly IAlphaDbContext _db;

        public RegularBillService(IAlphaDbContext db)
        {
            _db = db;
        }

        public RegularBillDataSet GetDataForReport(int billID)
        {
            return GetDataForReport(new[] { billID }, false);
        }

        public RegularBillDataSet GetDataForReport(int[] billIDs, bool removeEmptyBills)
        {
            RegularBillDataSet _data = new RegularBillDataSet();
            DataTable _customersTable = _data.Tables["Customers"];
            DataTable _chargeDataTable = _data.Tables["ChargeData"];
            DataTable _counterDataTable = _data.Tables["CounterData"];
            DataTable _sharedCounterDataTable = _data.Tables["SharedCounterData"];

            try
            {
                var _bills =
                    _db.RegularBillDocs
                        .Where(b => billIDs.Contains(b.ID) && (!removeEmptyBills || b.MonthChargeValue > 0))
                        .Select(b =>
                            new
                            {
                                b.CreationDateTime,
                                b.Period,
                                b.Account,
                                b.Owner,
                                b.Address,
                                b.Square,
                                b.ResidentsCount,
                                b.OverpaymentValue,
                                b.MonthChargeValue,
                                b.Value,
                                b.EmergencyPhoneNumber,
                                b.CustomerID,
                                Street = b.Customer.Building.Street.Name,
                                Building = b.Customer.Building.Number,
                                b.Customer.Apartment,
                                RegularBillDocSeviceTypePoses =
                                    b.RegularBillDocSeviceTypePoses
                                        .Select(p =>
                                            new
                                            {
                                                p.ServiceTypeName,
                                                p.PayRate,
                                                p.Charge,
                                                p.Benefit,
                                                p.Recalculation,
                                                p.Payable
                                            }),
                                RegularBillDocCounterPoses =
                                    b.RegularBillDocCounterPoses
                                        .Select(p =>
                                            new
                                            {
                                                p.Number,
                                                p.PrevValue,
                                                p.CurValue,
                                                p.Consumption,
                                                p.Rate
                                            }),
                                RegularBillDocSharedCounterPoses =
                                    b.RegularBillDocSharedCounterPoses
                                        .Select(p =>
                                            new
                                            {
                                                p.SharedCounterValue,
                                                p.SharedCharge
                                            })
                            })
                        .ToList()
                        .OrderBy(b => b.Street)
                        .ThenBy(b => b.Building, new StringWithNumbersComparer())
                        .ThenBy(b => b.Apartment, new StringWithNumbersComparer());

                foreach (var _bill in _bills)
                {
                    foreach (var _counterData in _bill.RegularBillDocCounterPoses)
                    {
                        _counterDataTable.Rows.Add(
                            _counterData.Number,
                            _counterData.PrevValue,
                            _counterData.CurValue,
                            _counterData.Consumption,
                            _counterData.Rate,
                            _bill.CustomerID);
                    }

                    foreach (var _sharedCounterData in _bill.RegularBillDocSharedCounterPoses)
                    {
                        _sharedCounterDataTable.Rows.Add(
                            _sharedCounterData.SharedCounterValue,
                            _sharedCounterData.SharedCharge,
                            _bill.CustomerID);
                    }

                    foreach (var _chargeData in _bill.RegularBillDocSeviceTypePoses)
                    {
                        _chargeDataTable.Rows.Add(
                            _chargeData.ServiceTypeName,
                            _chargeData.PayRate > 0 ? _chargeData.PayRate.ToString("0.00") : string.Empty,
                            _chargeData.Charge,
                            _chargeData.Benefit,
                            _chargeData.Recalculation,
                            _chargeData.Payable,
                            _bill.CustomerID);
                    }

                    string _barcode = GenerateBarCodeString(_bill.Account, _bill.Period);

                    _customersTable.Rows.Add(
                        _bill.CustomerID,
                        _bill.CreationDateTime.ToString("dd.MM.yyyy"),
                        _bill.Period.ToString("MMMM yyyy (MM.yy)"),
                        new DateTime(_bill.Period.Year, _bill.Period.Month, 10).AddMonths(1).ToString("dd.MM.yyyy"),
                        _bill.Account,
                        _bill.Owner,
                        _bill.Address,
                        _bill.Square,
                        _bill.ResidentsCount,
                        _bill.OverpaymentValue,
                        _bill.MonthChargeValue,
                        _bill.Value,
                        _barcode,
                        FormatBarcodeString(_barcode),
                        string.Format("Переплата(-)/Недоплата(+) на {0:dd.MM.yyyy}", _bill.CreationDateTime),
                        _bill.EmergencyPhoneNumber);
                }
            }
            catch (Exception _ex)
            {
                //Logger.SimpleWrite(string.Format("Не удалось загрузить данные для печати квитанции.\r\n{0}", _ex));
            }

            return _data;
        }

        /// <summary>
        /// Генерирует строку для штрих кода
        /// </summary>
        /// <param name="account">Лицевой счет абонента</param>
        /// <param name="period">Дата квитанции</param>
        /// <returns>Строка для штрих кода</returns>
        private string GenerateBarCodeString(string account, DateTime period)
        {
            string _accountNum = string.Format("{0}{1}{2}",
                                               account.Substring(3, 4),
                                               account.Substring(8, 3),
                                               account.Substring(12, 1));
            string _barcode =
                        string.Format(
                            "{0}{1}{2:yyyyMM}{3}",
                            BARCODE_COMPANY_CODE,
                            _accountNum,
                            period,
                            BARCODE_SERVICE_CODE);

            int _barcodeSum = 0;

            foreach (char _c in _barcode)
            {
                _barcodeSum += int.Parse(_c.ToString());
            }

            return string.Format("{0}{1}", _barcode, _barcodeSum % 10);
        }

        private string FormatBarcodeString(string barcode)
        {
            StringBuilder _builder = new StringBuilder();
            _builder.Append("*   ");

            foreach (char _c in barcode)
            {
                _builder.AppendFormat("{0}   ", _c);
            }

            _builder.Append("*");

            return _builder.ToString();
        }
    }
}