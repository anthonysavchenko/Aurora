using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.WinClient.Aurora.Interface.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.RegularBill.Constants;
using Taumis.EnterpriseLibrary.Win;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.EnterpriseLibrary.Win.Services;
using Taumis.Infrastructure.Interface.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.RegularBill.Views.Report
{
    /// <summary>
    /// Презентер вида с отчетом
    /// </summary>
    public class ReportViewPresenter : BaseReportForReportObjectPresenter<IReportView, EmptyReportParams>
    {
        private class CustomerInfo
        {
            public string Email { get; set; }
            public string SenderName { get; set; }
            public DateTime Period { get; set; }
        }

        /// <summary>
        /// Данные
        /// </summary>
        private DataSets.DataSet _data;
        private readonly Dictionary<int, CustomerInfo> _subscriptedCustomers = new Dictionary<int, CustomerInfo>();

        [ServiceDependency]
        public IEmailService EmailService { get; set; }

        [ServiceDependency]
        public IBillService BillService { get; set; }

        /// <summary>
        /// Выполняет действия при загрузке вида
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();

            DataTable _table = new DataTable();
            _table.Columns.Add("Name", typeof(string));

            PrinterSettings.StringCollection _printerNames = PrinterSettings.InstalledPrinters;

            foreach (string _printerName in _printerNames)
            {
                _table.Rows.Add(_printerName);
            }

            View.Printers = _table;

            if (_printerNames.Count > 0)
            {
                View.SelectedPrinter = new PrinterSettings().PrinterName;
            }
        }

        /// <summary>
        /// Обрабатывает данные табличной части отчета 
        /// </summary>
        protected override void ProcessGridData()
        {
            bool _showReport = _data.Tables["Customers"].Rows.Count > 0;

            View.ReceiptType = _data.Tables["CounterData"].Rows.Count == 0 && _data.Tables["SharedCounterData"].Rows.Count == 0
                                   ? ReceiptTypes.Standart
                                   : ReceiptTypes.WithCountsData;

            View.DataSource = _data;
            View.ReportVisible = _showReport;
            View.PageBreakAfterBill = View.OneBillOnSheet;
            View.ShowLineBetweenBills = _showReport && !View.OneBillOnSheet;
        }

        /// <summary>
        /// Возвращает данные для табличной части отчета
        /// </summary>
        /// <param name="_params">Параметры отчета</param>
        /// <returns>Данные табличной части отчета</returns>
        protected override DataTable GetGridData(EmptyReportParams _params)
        {
            try
            {
                _data = new DataSets.DataSet();
                string[] _billIDStrings = ((string[])WorkItem.State[ModuleStateNames.START_UP_PARAMS_BILL_IDS]);

                if (_billIDStrings.Length > 0)
                {
                    _subscriptedCustomers.Clear();

                    DataTable _customersTable = _data.Tables["Customers"];
                    DataTable _chargeDataTable = _data.Tables["ChargeData"];
                    DataTable _counterDataTable = _data.Tables["CounterData"];
                    DataTable _sharedCounterDataTable = _data.Tables["SharedCounterData"];
                    DataTable _publicPlaceDataTable = _data.Tables["PublicPlaceData"];
                    DateTime _now = ServerTime.GetDateTimeInfo().Now;

                    using (Entities _entities = new Entities())
                    {
                        _entities.CommandTimeout = 3600;

                        List<Services> _ppServices =
                            _entities.Services
                                .Where(s => s.ChargeRule == (byte)Service.ChargeRuleType.PublicPlaceAreaRate)
                                .OrderBy(s => s.Name)
                                .ToList();

                        int[] _billIDs = Array.ConvertAll<string, int>(_billIDStrings, int.Parse);

                        var _bills =
                            _entities.RegularBillDocs
                                .Where(b => _billIDs.Contains(b.ID) && (!View.RemoveEmptyBills || b.MonthChargeValue > 0))
                                .Select(b =>
                                    new
                                    {
                                        b.ID,
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
                                        CustomerID = b.Customers.ID,
                                        Street = b.Customers.Buildings.Streets.Name,
                                        Building = b.Customers.Buildings.Number,
                                        b.Customers.Apartment,
                                        b.Customers.BillSendingSubscription,
                                        Email = b.Customers.User.Login,
                                        b.BuildingArea,
                                        b.Customers.OwnerType,
                                        b.Customers.Buildings.BankDetails,
                                        FullName = 
                                            b.Customers.OwnerType == (int)Customer.OwnerTypes.PhysicalPerson 
                                                ? b.Customers.PhysicalPersonFullName 
                                                : b.Customers.JuridicalPersonFullName,
                                        b.RegularBillDocSeviceTypePoses,
                                        b.RegularBillDocCounterPoses,
                                        b.RegularBillDocSharedCounterPoses,
                                        b.RegularBillDocPublicPlacePoses
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

                            if (_bill.RegularBillDocPublicPlacePoses.Count > 0)
                            {
                                foreach (var _pos in _bill.RegularBillDocPublicPlacePoses.OrderBy(p => p.Service))
                                {
                                    _publicPlaceDataTable.Rows.Add(
                                        _pos.Service,
                                        _pos.Area,
                                        $"{_pos.Norm:0.000} {_pos.NormMeasure}",
                                        _pos.Rate,
                                        $"{_pos.ServiceVolume:0.000} {_pos.NormMeasure}",
                                        _pos.Total,
                                        _bill.CustomerID);
                                }
                            }
                            else
                            {
                                foreach (Services _service in _ppServices)
                                {
                                    _publicPlaceDataTable.Rows.Add(
                                        _service.Name,
                                        0,
                                        $"0 {_service.NormMeasure}",
                                        0,
                                        $"0 {_service.NormMeasure}",
                                        0,
                                        _bill.CustomerID);
                                }
                            }

                            DataRow _odn = null;

                            foreach (var _chargeData in _bill.RegularBillDocSeviceTypePoses.OrderBy(p => p.ServiceTypeName))
                            {
                                DataRow _row = _chargeDataTable.NewRow();
                                _row["Service"] = _chargeData.ServiceTypeName;
                                _row["PayRate"] = _chargeData.PayRate;
                                _row["Charge"] = _chargeData.Charge;
                                _row["Benefit"] = _chargeData.Benefit;
                                _row["Recalculation"] = _chargeData.Recalculation;
                                _row["Payable"] = _chargeData.Payable;
                                _row["CustomerId"] = _bill.CustomerID;

                                if (_chargeData.ServiceTypeName == "Содержание общедомового имущества")
                                {
                                    _odn = _row;
                                }
                                else
                                {
                                    _chargeDataTable.Rows.Add(_row);
                                }
                            }

                            if (_odn != null)
                            {
                                _chargeDataTable.Rows.Add(_odn);
                            }

                            string _barcode = BillService.GenerateBarCodeString(_bill.Account, _bill.Period);
                            string _qrCode = GenerateQrCodeString(
                                _bill.Account, 
                                _bill.OwnerType == (int)Customer.OwnerTypes.PhysicalPerson 
                                    ? _bill.FullName
                                    : string.Empty,
                                _bill.Address,
                                _bill.Period,
                                _bill.Value);

                            _customersTable.Rows.Add(
                                _bill.CustomerID,
                                _now.ToString("dd.MM.yyyy"),
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
                                $"Переплата(-)/Недоплата(+) на {_now:dd.MM.yyyy}",
                                BillService.OrganizationDetails(_bill.BankDetails, _bill.ContractorContactInfo, _bill.EmergencyPhoneNumber),
                                $"{_bill.BuildingArea:0.00} кв.м.",
                                _qrCode);

                            if (_bill.BillSendingSubscription)
                            {
                                _subscriptedCustomers.Add(
                                    _bill.CustomerID, 
                                    new CustomerInfo
                                    {
                                        Email = _bill.Email,
                                        SenderName = _bill.FullName,
                                        Period = _bill.Period
                                    });
                            }
                        }
                    }
                }
            }
            catch (Exception _ex)
            {
                Logger.SimpleWrite($"Не удалось загрузить данные для печати квитанции.\r\n{_ex}");
            }

            return null;
        }

        private string GenerateQrCodeString(string account, string fullName, string address, DateTime period, decimal sum)
        {
            string _accountNum = account.Substring(3);
            // Сумма в копейках
            int _sum = Convert.ToInt32(sum*100);

            string _qrStr = 
                $"ST00011|Name=ООО \"УК Фрунзенского района\"|PersonalAcc=40702810900100001650|BankName=ОАО \"Дальневосточный банк\" г.Владивосток|BIC=040507705|CorrespAcc=30101810900000000705|PayeeINN=2540165515|Category=Квартплата|PersAcc={_accountNum}|PayerAddress={address}|Sum={_sum}|PaymPeriod={period:MM.yyyy}";

            if (!string.IsNullOrEmpty(fullName))
            {
                string[] _parsedFullName = fullName.Split(' ');
                if (_parsedFullName.Length >= 1)
                {
                    _qrStr = $"{_qrStr}|LastName={_parsedFullName[0]}";

                    if (_parsedFullName.Length >= 2)
                    {
                        _qrStr = $"{_qrStr}|FirstName={_parsedFullName[1]}";
                        string _patronymic = string.Empty;
                        for (int i = 2; i < _parsedFullName.Length; i++)
                        {
                            _patronymic += _parsedFullName[i];
                        }

                        if (!string.IsNullOrEmpty(_patronymic))
                        {
                            _qrStr = $"{_qrStr}|MiddleName={_patronymic}";
                        }
                    }
                }
            }

            return ConvertUtf16ToWin1251(_qrStr);
        }

        private string ConvertUtf16ToWin1251(string str)
        {
            Encoding _win1251 = Encoding.GetEncoding(1251);
            Encoding _unicode = Encoding.Unicode;

            byte[] _unicodeBytes = _unicode.GetBytes(str);
            byte[] _win1251Bytes = Encoding.Convert(_unicode, _win1251, _unicodeBytes);

            char[] _win1251Chars = new char[_win1251.GetCharCount(_win1251Bytes, 0, _win1251Bytes.Length)];
            _win1251.GetChars(_win1251Bytes, 0, _win1251Bytes.Length, _win1251Chars, 0);

            return new string(_win1251Chars);
        }

        public void SendBills()
        {
            if (_subscriptedCustomers.Count > 0)
            {
                int _errorCount = 0;

                if (_data.Tables["Customers"].Rows.Count > 1)
                {
                    var _rows =
                        _data.Tables["Customers"].AsEnumerable()
                            .Where(r => _subscriptedCustomers.Keys.Contains((int) r["CustomerID"]));

                    foreach (DataRow _row in _rows)
                    {
                        DataSets.DataSet _dataSet = CreateDataSet(_row);

                        int _customerID = (int) _row["CustomerID"];

                        CustomerInfo _customerInfo = _subscriptedCustomers[_customerID];

                        MemoryStream _pdf = View.GeneratePdf(_dataSet);

                        try
                        {
                            EmailService.SendRegularBills(
                                _pdf,
                                _customerInfo.Email,
                                _customerInfo.SenderName,
                                _customerInfo.Period);
                        }
                        catch (Exception _ex)
                        {
                            Logger.SimpleWrite(_ex.ToString());
                            _errorCount++;
                        }
                        finally
                        {
                            _pdf.Dispose();
                        }
                    }
                }
                else if (_data.Tables["Customers"].Rows.Count != 0)
                {
                    int _customerID = (int) _data.Tables["Customers"].Rows[0]["CustomerID"];
                    CustomerInfo _customerInfo = _subscriptedCustomers[_customerID];

                    MemoryStream _pdf = View.GeneratePdf();

                    try
                    {
                        EmailService.SendRegularBills(
                            _pdf,
                            _customerInfo.Email,
                            _customerInfo.SenderName,
                            _customerInfo.Period);
                    }
                    catch (Exception _ex)
                    {
                        Logger.SimpleWrite(_ex.ToString());
                        _errorCount++;
                    }
                    finally
                    {
                        _pdf.Dispose();
                    }
                }

                View.ShowMessage(
                    $"Всего квитанций: \t{_subscriptedCustomers.Count}\nОтправлено: \t{_subscriptedCustomers.Count - _errorCount}\nНе отправлено: \t{_errorCount}", 
                    "Результаты отправки");
            }
        }

        private DataSets.DataSet CreateDataSet(DataRow _row)
        {
            DataSets.DataSet _dataSet = new DataSets.DataSet();
            DataTable _customersTable = _dataSet.Tables["Customers"];
            DataTable _chargeDataTable = _dataSet.Tables["ChargeData"];
            DataTable _counterDataTable = _dataSet.Tables["CounterData"];
            DataTable _sharedCounterDataTable = _dataSet.Tables["SharedCounterData"];

            DataRow _newCustomerRow = _customersTable.NewRow();
            _newCustomerRow.ItemArray = _row.ItemArray;
            _customersTable.Rows.Add(_newCustomerRow);

            int _customerID = (int)_row["CustomerID"];

            var _chargeRows =
                _data.Tables["ChargeData"].AsEnumerable().Where(r => (int)r["CustomerID"] == _customerID);

            foreach (DataRow _chargeRow in _chargeRows)
            {
                DataRow _newChargeRow = _chargeDataTable.NewRow();
                _newChargeRow.ItemArray = _chargeRow.ItemArray;
                _chargeDataTable.Rows.Add(_newChargeRow);
            }

            var _counterRows =
                _data.Tables["CounterData"].AsEnumerable().Where(r => (int)r["CustomerID"] == _customerID);

            foreach (DataRow _counterRow in _counterRows)
            {
                DataRow _newCounterRow = _counterDataTable.NewRow();
                _newCounterRow.ItemArray = _counterRow.ItemArray;
                _counterDataTable.Rows.Add(_newCounterRow);
            }

            var _sharedCounterRows =
                _data.Tables["SharedCounterData"].AsEnumerable().Where(r => (int)r["CustomerID"] == _customerID);

            foreach (DataRow _sharedCounterRow in _sharedCounterRows)
            {
                DataRow _newSharedCounterRow = _sharedCounterDataTable.NewRow();
                _newSharedCounterRow.ItemArray = _sharedCounterRow.ItemArray;
                _sharedCounterDataTable.Rows.Add(_newSharedCounterRow);
            }

            return _dataSet;
        }

        /// <summary>
        /// Открывает вкладку "Начисление" после запуска модуля "Начисления" из другого модуля
        /// </summary>
        [EventSubscription(CommonEventNames.ON_MAIN_VIEW_SHOWN, ThreadOption.UserInterface)]
        public void ShowRegularBill(object sender, EventArgs<AnyStartUpParams> eventArgsStartUpParams)
        {
            string[] _billIDStrings = ((string[])WorkItem.State[ModuleStateNames.START_UP_PARAMS_BILL_IDS]);

            if (_billIDStrings.Count() > 1)
            {
                View.RemoveEmptyBills = true;
                View.OneBillOnSheet = false;
                View.RemoveEmptyBillsEnabled = true;
                View.OneBillOnSheetEnabled = true;
            }
            else
            {
                View.RemoveEmptyBills = false;
                View.OneBillOnSheet = true;
                View.RemoveEmptyBillsEnabled = false;
                View.OneBillOnSheetEnabled = false;
            }

            View.UpdateReport();
        }
    }
}