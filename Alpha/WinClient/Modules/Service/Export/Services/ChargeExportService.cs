using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Enums;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Services
{
    public class ChargeExportService : IChargeExportService
    {
        private class CustomerInfo
        {
            public string Account { get; set; }
            public string Owner { get; set; }
            public string Street { get; set; }
            public string Building { get; set; }
            public string Apartment { get; set; }
            public decimal Value { get; set; }
        }

        private class BankDetailInfo
        {
            public int ID { get; set; }
            public string INN { get; set; }
            public string Account { get; set; }
        }

        private Dictionary<int, List<CustomerInfo>> GetData(DateTime period)
        {
            Dictionary<int, List<CustomerInfo>> _dataByBankAccount = null;

            using (Entities _db = new Entities())
            {
                _db.CommandTimeout = 3600;

                var _customers =
                    _db.Customers
                        .Select(c =>
                            new
                            {
                                c.ID,
                                c.OwnerType,
                                c.Account,
                                c.PhysicalPersonFullName,
                                c.JuridicalPersonFullName,
                                c.Apartment,
                            })
                        .ToList();

                var _buildings =
                    _db.Buildings
                        .Select(b =>
                            new
                            {
                                b.ID,
                                StreetName = b.Streets.Name,
                                b.Number,
                            })
                        .ToList();

                _dataByBankAccount = _db.ChargeOpers
                    .Select(
                        c =>
                        new
                        {
                            CustomerID = c.Customers.ID,
                            BuildingID = c.Customers.Buildings.ID,
                            c.Customers.Buildings.BankDetailID,
                            c.ChargeSets.Period,
                            c.Value
                        })
                    .Concat(
                        _db.ChargeOpers
                            .Where(c => c.ChargeCorrectionOpers != null)
                            .Select(
                                c =>
                                new
                                {
                                    CustomerID = c.Customers.ID,
                                    BuildingID = c.Customers.Buildings.ID,
                                    c.Customers.Buildings.BankDetailID,
                                    c.ChargeCorrectionOpers.Period,
                                    Value = -1 * c.Value
                                }))
                    .Concat(
                        _db.RechargeOpers
                        .Select(
                            c =>
                            new
                            {
                                CustomerID = c.Customers.ID,
                                BuildingID = c.Customers.Buildings.ID,
                                c.Customers.Buildings.BankDetailID,
                                c.RechargeSets.Period,
                                c.Value
                            }))
                    .Concat(
                        _db.RechargeOpers
                            .Where(c => c.ChildChargeCorrectionOpers != null)
                            .Select(
                                c =>
                                new
                                {
                                    CustomerID = c.Customers.ID,
                                    BuildingID = c.Customers.Buildings.ID,
                                    c.Customers.Buildings.BankDetailID,
                                    c.ChildChargeCorrectionOpers.Period,
                                    Value = -1 * c.Value
                                }))
                    .Concat(
                        _db.BenefitOpers
                            .Select(
                                b =>
                                new
                                {
                                    CustomerID = b.ChargeOpers.Customers.ID,
                                    BuildingID = b.ChargeOpers.Customers.Buildings.ID,
                                    b.ChargeOpers.Customers.Buildings.BankDetailID,
                                    b.ChargeOpers.ChargeSets.Period,
                                    b.Value
                                }))
                    .Concat(
                        _db.BenefitOpers
                            .Where(b => b.BenefitCorrectionOpers != null)
                            .Select(
                                b =>
                                new
                                {
                                    CustomerID = b.ChargeOpers.Customers.ID,
                                    BuildingID = b.ChargeOpers.Customers.Buildings.ID,
                                    b.ChargeOpers.Customers.Buildings.BankDetailID,
                                    b.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                    Value = -1 * b.Value
                                }))
                    .Concat(
                        _db.RebenefitOpers
                            .Select(
                                b =>
                                new
                                {
                                    CustomerID = b.RechargeOpers.Customers.ID,
                                    BuildingID = b.RechargeOpers.Customers.Buildings.ID,
                                    b.RechargeOpers.Customers.Buildings.BankDetailID,
                                    b.RechargeOpers.RechargeSets.Period,
                                    b.Value
                                }))
                    .Concat(
                        _db.RebenefitOpers
                            .Where(b => b.BenefitCorrectionOpers != null)
                            .Select(
                                b =>
                                new
                                {
                                    CustomerID = b.RechargeOpers.Customers.ID,
                                    BuildingID = b.RechargeOpers.Customers.Buildings.ID,
                                    b.RechargeOpers.Customers.Buildings.BankDetailID,
                                    b.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                    Value = -1 * b.Value
                                }))
                    .Concat(
                        _db.PaymentOperPoses
                            .Select(p =>
                                new
                                {
                                    CustomerID = p.PaymentOpers.Customers.ID,
                                    BuildingID = p.PaymentOpers.Customers.Buildings.ID,
                                    p.PaymentOpers.Customers.Buildings.BankDetailID,
                                    p.Period,
                                    p.Value
                                }))
                    .Concat(
                        _db.PaymentCorrectionOpers
                            .Select(p =>
                                new
                                {
                                    CustomerID = p.PaymentOpers.Customers.ID,
                                    BuildingID = p.PaymentOpers.Customers.Buildings.ID,
                                    p.PaymentOpers.Customers.Buildings.BankDetailID,
                                    p.Period,
                                    p.Value
                                }))
                    .Concat(
                        _db.OverpaymentOperPoses
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.OverpaymentOpers.Customers.ID,
                                    BuildingID = o.OverpaymentOpers.Customers.Buildings.ID,
                                    o.OverpaymentOpers.Customers.Buildings.BankDetailID,
                                    o.Period,
                                    o.Value
                                }))
                    .Concat(
                        _db.OverpaymentCorrectionOpers
                            .Select(o =>
                                new
                                {
                                    CustomerID = o.ChargeOpers.Customers.ID,
                                    BuildingID = o.ChargeOpers.Customers.Buildings.ID,
                                    o.ChargeOpers.Customers.Buildings.BankDetailID,
                                    o.Period,
                                    o.Value
                                }))
                    .Where(c => c.Period <= period)
                    .GroupBy(c =>
                        new
                        {
                            c.CustomerID,
                            c.BuildingID,
                            c.BankDetailID
                        })
                    .Select(g =>
                        new
                        {
                            g.Key.CustomerID,
                            g.Key.BuildingID,
                            g.Key.BankDetailID,
                            Value = g.Sum(c => c.Value)
                        })
                    .ToList()
                    .Join(
                        _customers,
                        x => x.CustomerID,
                        c => c.ID,
                        (x, c) =>
                        new
                        {
                            c.Account,
                            c.Apartment,
                            Owner = c.OwnerType == (int)OwnerType.PhysicalPerson ? c.PhysicalPersonFullName : c.JuridicalPersonFullName,
                            x.BuildingID,
                            x.Value,
                            x.BankDetailID
                        })
                    .Join(
                        _buildings,
                        x => x.BuildingID,
                        b => b.ID,
                        (x, b) =>
                        new
                        {
                            x.BankDetailID,
                            Account = x.Account,
                            Apartment = x.Apartment,
                            Owner = x.Owner,
                            Value = x.Value,
                            Street = b.StreetName,
                            Building = b.Number
                        })
                    .GroupBy(x => x.BankDetailID)
                    .Select(g =>
                        new
                        {
                            BankDetailID = g.Key.Value,
                            Data = g
                                .Select(x =>
                                    new CustomerInfo
                                    {
                                        Account = x.Account,
                                        Apartment = x.Apartment,
                                        Building = x.Building,
                                        Owner = x.Owner,
                                        Street = x.Street,
                                        Value = x.Value
                                    })
                                .ToList()
                        })
                    .ToDictionary(x => x.BankDetailID, x => x.Data);
            }
            return _dataByBankAccount;
        }

        private Dictionary<int, BankDetailInfo> GetBankDetailData()
        {
            Dictionary<int, BankDetailInfo> _data = null;
            using (Entities _db = new Entities())
            {
                _data = _db.BankDetails
                    .Select(b =>
                        new BankDetailInfo
                        {
                            ID = b.ID,
                            INN = b.INN,
                            Account = b.Account
                        })
                    .ToDictionary(b => b.ID);
            }

            return _data;
        }

        public ExportResult Export(string outputPath, DateTime period, IEnumerable<ChargeExportFormatType> formats, Action<int> progressAction)
        {
            ExportResult _result = new ExportResult();

            try
            {
                Dictionary<int, List<CustomerInfo>> _data = GetData(period);
                Dictionary<int, BankDetailInfo> _bankDetailData = GetBankDetailData();

                int _count = _data.Values.Sum(v => v.Count);
                int _processed = 1;

                foreach (ChargeExportFormatType _format in formats)
                {
                    Encoding _encoding;
                    string _postfix;
                    if(_format == ChargeExportFormatType.Sberbank)
                    {
                        _postfix = DateTime.Now.ToString("_ddMMyyyy");
                        _encoding = Encoding.UTF8;
                    }
                    else
                    {
                        _postfix = string.Empty;
                        _encoding = Encoding.GetEncoding(1251);
                    }

                    foreach (KeyValuePair<int, List<CustomerInfo>> _pair in _data)
                    {
                        BankDetailInfo _bInfo = _bankDetailData[_pair.Key];
                        string _filePath = $"{outputPath}\\{_bInfo.INN}_{_bInfo.Account}{_postfix}.txt";

                        using (StreamWriter _file = new StreamWriter(_filePath, false, _encoding))
                        {
                            _file.AutoFlush = true;
                            DateTime _period = new DateTime(period.Year, period.Month, 1);

                            if (_format == ChargeExportFormatType.Primsocbank)
                            {
                                _file.WriteLine($"#FILESUM {_pair.Value.Where(x => x.Value > 0).Sum(x => x.Value).ToString().Replace(',', '.')}");
                                _file.WriteLine("#TYPE 7");
                                _file.WriteLine("#SERVICE 63350");
                            }

                            foreach (var _record in _pair.Value)
                            {
                                if (_format == ChargeExportFormatType.Sberbank)
                                {
                                    _file.WriteLine("{0}|{1}|{2}",
                                        _record.Account,
                                        _record.Owner,
                                        _record.Value < 0 ? "0" : _record.Value.ToString().Replace(',', '.'));
                                }
                                else
                                {
                                    _file.WriteLine(
                                        "{0};Владивосток,{1},{2},{3};{4};{5}",
                                        _record.Owner,
                                        _record.Street,
                                        _record.Building,
                                        _record.Apartment,
                                        _record.Account,
                                        _record.Value < 0 ? "0" : _record.Value.ToString().Replace(',', '.'));
                                }
                            }
                        }
                    }

                    progressAction(_processed++ * 100 / _count);
                }

                _result.Info = "Операция выполнена успешно";
            }
            catch (Exception _ex)
            {
                Logger.SimpleWrite($"Export error: {_ex}");
                _result.Info = "Произошла ошибка. Операция не выполнена";
                progressAction(100);
            }

            return _result;
        }
    }
}