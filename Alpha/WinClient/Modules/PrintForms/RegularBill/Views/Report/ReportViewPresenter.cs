using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.Server.PrintForms.Constants;
using Taumis.Alpha.Server.PrintForms.DataSets;
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
        private RegularBillDataSet _data;
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
                _data = new RegularBillDataSet();
                string[] _billIDStrings = ((string[])WorkItem.State[ModuleStateNames.START_UP_PARAMS_BILL_IDS]);

                if (_billIDStrings.Length > 0)
                {
                    _subscriptedCustomers.Clear();

                    DataTable _customersTable = _data.Tables["Customers"];
                    DataTable _chargeDataTable = _data.Tables["ChargeData"];
                    DataTable _counterDataTable = _data.Tables["CounterData"];
                    DataTable _sharedCounterDataTable = _data.Tables["SharedCounterData"];
                    DataTable _buildingConsumptionDataTable = _data.Tables["BuildingConsumptionData"];
                    DateTime _now = ServerTime.GetDateTimeInfo().Now;

                    using (Entities _entities = new Entities())
                    {
                        _entities.CommandTimeout = 3600;

                        int[] _billIDs = Array.ConvertAll<string, int>(_billIDStrings, int.Parse);

                        var _bills =
                            _entities.RegularBillDocs
                                .Where(b => _billIDs.Contains(b.ID) && (!View.RemoveEmptyBills || b.MonthChargeValue != 0) && (!View.RemoveMunicipalBills || b.Customers.IsPrivate))
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
                                        b.Customers.OwnerType,
                                        b.Customers.Buildings.BankDetails,
                                        FullName =
                                            b.Customers.OwnerType == (int)OwnerType.PhysicalPerson
                                                ? b.Customers.PhysicalPersonFullName
                                                : b.Customers.JuridicalPersonFullName,
                                        b.RegularBillDocSeviceTypePoses,
                                        b.RegularBillDocCounterPoses,
                                        b.RegularBillDocSharedCounterPoses,
                                        BuildingConsumptionPoses = _entities.BuildingConsumptions
                                            .Where(x => x.BuildingID == b.Customers.Buildings.ID && x.Period == b.Period)
                                            .ToList()
                                    })
                                .ToList()
                                .OrderBy(b => b.Street)
                                .ThenBy(b => b.Building, new StringWithNumbersComparer())
                                .ThenBy(b => b.Apartment, new StringWithNumbersComparer())
                                .ToList();

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

                            foreach (var _pos in _bill.BuildingConsumptionPoses)
                            {
                                _buildingConsumptionDataTable.Rows.Add(
                                    _pos.ElectrVol,
                                    _pos.ElectrOdnVol,
                                    _pos.ElectrCounterValue,
                                    _pos.HotWaterVol,
                                    _pos.HotWaterOdnVol,
                                    _pos.HotWaterCounterValue,
                                    _pos.ColdWaterVol,
                                    _pos.ColdWaterOdnVol,
                                    _pos.ColdWaterCounterValue,
                                    _pos.WasteWaterVol,
                                    _pos.WasteWaterOdnVol,
                                    _pos.HeatingVol,
                                    _pos.HeatingOdnVol,
                                    _pos.HeatingCounterValue,
                                    _bill.CustomerID);
                            }

                            string _barcode = BillService.GenerateBarCodeString(_bill.Account, _bill.BankDetails.INN, _bill.Period, _bill.Value);
                            string _qrCode = BillService.GenerateQrCodeString(
                                "ООО \"Жилищные услуги\"",
                                _bill.BankDetails.Account,
                                _bill.BankDetails.Name,
                                _bill.BankDetails.BIK,
                                _bill.BankDetails.CorrAccount,
                                _bill.BankDetails.INN,
                                "Квартплата",
                                _bill.Account,
                                _bill.OwnerType == (int)OwnerType.PhysicalPerson
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
                                BillService.OrganizationDetails(_bill.BankDetails, _bill.ContractorContactInfo, GetEmergencyPhoneNumber(_bill.Street, _bill.Building)),
                                _qrCode,
                                "Все вопросы, связанные с начислением взносов и предоставлением платежных документов необходимо направлять в ООО \"Жилищные услуги\" по адресу: г. Владивосток, ул. Пушкинская, д. 83, тел. 2-469-240");

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

        private string GetBottomInfoString(string street, string building)
        {
            street = street.ToLower();

            if (street == "остров русский, ул. зеленая"
                || street == "остров русский, п. поспелово"
                || street == "остров русский, п. подножье"
                || street == "остров русский, ул. экипажная"
                || street == "остров русский, поселок воевода")
            {
                return "Управляющий по дому - Елена Сергеевна, тел. +7-924-428-48-84";
            }

            if (street == "уткинская"
                || street == "прапорщика комарова")
            {
                return "Управляющий по дому - Адрианова Варвара Георгиевна, тел. +7-914-703-97-87";
            }

            if (street == "космонавтов" || street == "борисенко")
            {
                return "Управляющий по дому - Милованова Оксана Васильевна, тел 271-80-55";
            }

            if (street == "гульбиновича"
                || street == "тунгусская"
                || street == "тобольская"
                || street == "терешковой"
                || street == "шилкинская"
                || (street == "красного знамени проспект" 
                    && (building == "88" || building == "162" || building == "114")))
            {
                return "Управляющий по дому - Милованова Оксана Васильевна, тел. +7-924-736-61-45";
            }

            if (street == "магнитогорская"
                || street == "енисейская"
                || street == "окатовая" 
                || street == "океанский проспект"
                || street == "острякова проспект"
                || street == "некрасовская"
                || (street == "красного знамени проспект" && (building == "93" || building == "107")))
            {
                return "Управляющий по дому - Скляр Алексей Олегович, тел. +7-914-662-35-92";
            }

            if (street == "адмирала спиридонова"
                || street == "адмирала кузнецова"
                || street == "луговая"
                || street == "баляева")
            {
                return "Управляющий по дому - Горбачева Вера Александровна, тел 255-82-23";
            }

            if (street == "ульяновская"
                || street == "полетаева")
            {
                return "Управляющий по дому - Шевелева Надежда Дмитриевна, тел. 277-02-99";
            }

            return string.Empty;
        }

        private string GetEmergencyPhoneNumber(string street, string building)
        {
            street = street.ToLower();
            building = building.ToLower();

            if (street == "борисенко" && building == "100б"
                || street == "космонавтов"
                || street == "луговая"
                || street == "адмирала кузнецова"
                || street == "адмирала спиридонова"
                || street == "терешковой"
                || street == "окатовая")
            {
                return "206-03-20";
            }

            if (street == "борисенко"
                || street == "гульбиновича"
                || street == "баляева")
            {
                return "2-614-714";
            }

            if (street == "прапорщика комарова"
                || street == "уткинская"
                || street == "западная"
                || street == "острякова проспект"
                || street == "океанский проспект"
                || street == "некрасовская"
                || street == "станюковича"
                || street == "красного знамени проспект"
                || street == "шилкинская"
                || street == "тунгусская"
                || street == "тобольская")
            {
                return "2-980-981";
            }

            if (street == "магнитогорская"
                || street == "енисейская"
                || street == "полетаева")
            {
                return "2-667-206, 2-666-964";
            }

            if (street == "ульяновская")
            {
                return "2-340-141";
            }

            return "2-614-714, 2-980-981";
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
                            .Where(r => _subscriptedCustomers.Keys.Contains((int)r["CustomerID"]));

                    foreach (DataRow _row in _rows)
                    {
                        RegularBillDataSet _dataSet = CreateDataSet(_row);

                        int _customerID = (int)_row["CustomerID"];

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
                    int _customerID = (int)_data.Tables["Customers"].Rows[0]["CustomerID"];
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
            else
            {
                View.ShowMessage(
                    $"Нет абонентов, подписанных на получение квитанций",
                    "Результаты отправки");
            }
        }

        private RegularBillDataSet CreateDataSet(DataRow _row)
        {
            RegularBillDataSet _dataSet = new RegularBillDataSet();
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
                View.RemoveMunicipalBills = true;
                View.OneBillOnSheet = false;
                View.RemoveEmptyBillsEnabled = true;
                View.RemoveMunicipalBillsEnabled = true;
                View.OneBillOnSheetEnabled = true;
            }
            else
            {
                View.RemoveEmptyBills = false;
                View.RemoveMunicipalBills = false;
                View.OneBillOnSheet = true;
                View.RemoveEmptyBillsEnabled = false;
                View.RemoveMunicipalBillsEnabled = false;
                View.OneBillOnSheetEnabled = false;
            }

            View.UpdateReport();
        }
    }
}