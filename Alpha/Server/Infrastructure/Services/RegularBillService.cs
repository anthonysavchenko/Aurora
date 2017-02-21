using System;
using System.Data;
using System.Linq;
using System.Text;
using Taumis.Alpha.Server.Core.Models.Enums;
using Taumis.Alpha.Server.Core.Services;
using Taumis.Alpha.Server.Infrastructure.Data;
using Taumis.Alpha.Server.PrintForms.DataSets;

namespace Taumis.Alpha.Server.Infrastructure.Services
{
    public class RegularBillService : IRegularBillService
    {
        private readonly IAlphaDbContext _db;
        private IBillService BillService { get; }

        public RegularBillService(IAlphaDbContext db, IBillService billService)
        {
            _db = db;
            BillService = billService;
        }

        public DataSet GetDataForReport(int billID)
        {
            return GetDataForReport(new[] { billID }, false);
        }

        public DataSet GetDataForReport(int[] billIDs, bool removeEmptyBills)
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
                                b.ID,
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
                                b.ContractorContactInfo,
                                b.CustomerID,
                                Street = b.Customer.Building.Street.Name,
                                Building = b.Customer.Building.Number,
                                b.Customer.Apartment,
                                b.Customer.BillSendingSubscription,
                                Email = b.Customer.User.Login,
                                b.Customer.OwnerType,
                                b.Customer.Building.BankDetail,
                                FullName =
                                    b.Customer.OwnerType == OwnerTypes.PhysicalPerson
                                        ? b.Customer.PhysicalPersonFullName
                                        : b.Customer.JuridicalPersonFullName,
                                b.RegularBillDocSeviceTypePoses,
                                b.RegularBillDocCounterPoses,
                                b.RegularBillDocSharedCounterPoses
                            })
                        .ToList()
                        .OrderBy(b => b.Street)
                        .ThenBy(b => b.Building, new StringWithNumbersComparer())
                        .ThenBy(b => b.Apartment, new StringWithNumbersComparer());

                foreach (var _bill in _bills)
                {
                    foreach (var _pos in _bill.RegularBillDocCounterPoses)
                    {
                        _counterDataTable.Rows.Add(
                            _pos.Number,
                            _pos.PrevValue,
                            _pos.CurValue,
                            _pos.Consumption,
                            _pos.Rate,
                            _bill.CustomerID);
                    }

                    foreach (var _pos in _bill.RegularBillDocSharedCounterPoses)
                    {
                        _sharedCounterDataTable.Rows.Add(
                            _pos.SharedCounterValue,
                            _pos.SharedCharge,
                            _bill.CustomerID);
                    }

                    foreach (var _chargeData in _bill.RegularBillDocSeviceTypePoses)
                    {
                        DataRow _row = _chargeDataTable.NewRow();
                        _row["Service"] = _chargeData.ServiceTypeName;
                        _row["PayRate"] = _chargeData.PayRate;
                        _row["Charge"] = _chargeData.Charge;
                        _row["Benefit"] = _chargeData.Benefit;
                        _row["Recalculation"] = _chargeData.Recalculation;
                        _row["Payable"] = _chargeData.Payable;
                        _row["CustomerId"] = _bill.CustomerID;

                        _chargeDataTable.Rows.Add(_row);
                    }

                    string _barcode = BillService.GenerateBarCodeString(_bill.Account, _bill.Period);
                    string _qrCode = BillService.GenerateQrCodeString(
                        _bill.Account,
                        _bill.OwnerType == OwnerTypes.PhysicalPerson
                            ? _bill.FullName
                            : string.Empty,
                        _bill.Address,
                        _bill.Period,
                        _bill.Value);

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
                        BillService.FormatBarcodeString(_barcode),
                        $"Переплата(-)/Недоплата(+) на {_bill.CreationDateTime:dd.MM.yyyy}",
                        BillService.OrganizationDetails(
                            _bill.BankDetail.Account,
                            _bill.BankDetail.CorrAccount,
                            _bill.BankDetail.INN,
                            _bill.BankDetail.KPP,
                            _bill.BankDetail.BIK,
                            _bill.BankDetail.Name, 
                            _bill.ContractorContactInfo, 
                            _bill.EmergencyPhoneNumber),
                        _qrCode);
                }
            }
            catch (Exception _ex)
            {
                //Logger.SimpleWrite(string.Format("Не удалось загрузить данные для печати квитанции.\r\n{0}", _ex));
            }

            return _data;
        }
    }
}