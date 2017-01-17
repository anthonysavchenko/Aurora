using System;
using System.Collections.Generic;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;

namespace SQLServerAccessProviderTest
{
    public class InMemoryDatabase
    {
        public readonly Dictionary<string, Customer> Customers = new Dictionary<string, Customer>();
        public readonly Dictionary<string, CustomerPos> CustomerPoses = new Dictionary<string, CustomerPos>();
        public readonly Dictionary<string, PaymentSet> PaymentSets = new Dictionary<string, PaymentSet>();

        public readonly Dictionary<string, PaymentCorrection> PaymentCorrections = new Dictionary<string, PaymentCorrection>();
        public readonly Dictionary<string, PaymentOper> PaymentOpers = new Dictionary<string, PaymentOper>();
        public readonly Dictionary<string, PaymentOperPos> PaymentOperPoses = new Dictionary<string, PaymentOperPos>();

        public readonly Dictionary<string, Building> Buildings = new Dictionary<string, Building>();
        public readonly Dictionary<string, Contractor> Contractors = new Dictionary<string, Contractor>();
        public readonly Dictionary<string, Intermediary> Intermediaries = new Dictionary<string, Intermediary>();
        public readonly Dictionary<string, Service> Services = new Dictionary<string, Service>();
        public readonly Dictionary<string, ServiceType> ServiceTypes = new Dictionary<string, ServiceType>();
        public readonly Dictionary<string, Street> Streets = new Dictionary<string, Street>();

        public InMemoryDatabase()
        {
            #region RefBooks

            Streets.Add(
                "1",
                new Street
                {
                    ID = "1",
                    Name = "Улица"
                });

            Buildings.Add(
                "1",
                new Building
                {
                    ID = "1",
                    Number = "1",
                    Street = Streets["1"],
                    ZipCode = "690091",
                });

            Contractors.Add(
                "1",
                new Contractor
                {
                    ID = "1",
                    Name = "Подрядчик 1",
                    Code = "П1"
                });

            Intermediaries.Add(
                "1",
                new Intermediary
                {
                    ID = "1",
                    Code = "Intermediary1",
                    Name = "Intermediary One",
                    Rate = 10
                });
            Intermediaries.Add(
                "2",
                new Intermediary
                {
                    ID = "2",
                    Code = "Intermediary2",
                    Name = "Intermediary Two",
                    Rate = 10
                });

            ServiceTypes.Add(
                "1",
                new ServiceType
                {
                    ID = "1",
                    Name = "Тип услуги 1",
                    Code = "Т1"
                });

            Services.Add(
                "1",
                new Service
                {
                    ID = "1",
                    Name = "Услуга 1",
                    Code = "У1",
                    ChargeRule = Service.ChargeRuleType.FixedRate,
                    ServiceType = ServiceTypes["1"]
                });
            Services.Add(
                "2",
                new Service
                {
                    ID = "2",
                    Name = "Услуга 2",
                    Code = "У2",
                    ChargeRule = Service.ChargeRuleType.FixedRate,
                    ServiceType = ServiceTypes["1"]
                });

            #endregion

            #region Docs

            Customers.Add(
                "1",
                new Customer
                {
                    ID = "1",
                    Account = "EG-1111-111-1",
                    Apartment = "1",
                    Building = Buildings["1"],
                    NonbenefitResidentsCount = 2,
                    OwnerType = Customer.OwnerTypes.PhysicalPerson,
                    PhysicalPersonFullName = "Пользователь По По",
                    PhysicalPersonShortName = "Пользователь П.П.",
                    RoomsCount = 2,
                    Square = 90,
                });
            CustomerPoses.Add(
                "1",
                new CustomerPos
                {
                    ID = "1",
                    Doc = Customers["1"],
                    Contractor = Contractors["1"],
                    Service = Services["1"],
                    Rate = 10
                });
            CustomerPoses.Add(
                "2",
                new CustomerPos
                {
                    ID = "2",
                    Doc = Customers["1"],
                    Contractor = Contractors["1"],
                    Service = Services["2"],
                    Rate = 10
                });
            Customers["1"].CustomerPoses.Add("1", CustomerPoses["1"]);
            Customers["1"].CustomerPoses.Add("2", CustomerPoses["2"]);

            PaymentSets.Add(
                "1",
                new PaymentSet
                {
                    ID = "1",
                    Comment = "1234test",
                    CreationDateTime = new DateTime(2013, 02, 10),
                    IsFile = false,
                    Number = 1,
                    Quantity = 2,
                    ValueSum = 100,
                    Intermediary = Intermediaries["1"]
                });
            PaymentSets.Add(
                "2",
                new PaymentSet
                {
                    ID = "2",
                    Comment = null,
                    CreationDateTime = new DateTime(2013, 02, 10),
                    IsFile = false,
                    Number = 2,
                    Quantity = 2,
                    ValueSum = 120,
                    Intermediary = Intermediaries["2"]
                });

            #endregion

            PaymentCorrections.Add(
                "1",
                new PaymentCorrection
                {
                    ID = "1",
                    Value = -50,
                    Customer = Customers["1"],
                    Intermediary = Intermediaries["1"]
                });

            #region PaymentOpers

            PaymentOpers.Add(
                "1",
                new PaymentOper
                {
                    ID = "1",
                    CreationDateTime = new DateTime(2013, 02, 10),
                    PaymentPeriod = new DateTime(2013, 01, 01),
                    Value = 50,
                    PaymentSet = PaymentSets["1"],
                    Customer = Customers["1"],
                    PaymentCorrection = null
                });
            PaymentOpers.Add(
                "2",
                new PaymentOper
                {
                    ID = "2",
                    CreationDateTime = new DateTime(2013, 02, 11),
                    PaymentPeriod = new DateTime(2012, 12, 01),
                    Value = 50,
                    PaymentSet = PaymentSets["1"],
                    Customer = Customers["1"],
                    PaymentCorrection = PaymentCorrections["1"]
                });
            PaymentOpers.Add(
                "3",
                new PaymentOper
                {
                    ID = "3",
                    CreationDateTime = new DateTime(2013, 02, 10),
                    PaymentPeriod = new DateTime(2013, 01, 01),
                    Value = 70,
                    PaymentSet = PaymentSets["2"],
                    Customer = Customers["1"],
                    PaymentCorrection = null
                });
            PaymentOpers.Add(
                "4",
                new PaymentOper
                {
                    ID = "4",
                    CreationDateTime = new DateTime(2013, 02, 11),
                    PaymentPeriod = new DateTime(2012, 12, 01),
                    Value = 50,
                    PaymentSet = PaymentSets["2"],
                    Customer = Customers["1"],
                    PaymentCorrection = PaymentCorrections["1"]
                });

            #endregion

            PaymentOperPoses.Add(
                "1",
                new PaymentOperPos
                {
                    ID = "1",
                    Period = new DateTime(2013, 01, 01),
                    Value = 25,
                    Service = Services["1"],
                    PaymentOper = PaymentOpers["1"]
                });
            PaymentOperPoses.Add(
                "2",
                    new PaymentOperPos
                    {
                        ID = "2",
                        Period = new DateTime(2013, 01, 01),
                        Value = 25,
                        Service = Services["2"],
                        PaymentOper = PaymentOpers["1"]
                    });
            PaymentOperPoses.Add(
                "3",
                    new PaymentOperPos
                    {
                        ID = "3",
                        Period = new DateTime(2012, 12, 01),
                        Value = 25,
                        Service = Services["1"],
                        PaymentOper = PaymentOpers["2"]
                    });
            PaymentOperPoses.Add(
                "4",
                new PaymentOperPos
                {
                    ID = "4",
                    Period = new DateTime(2012, 12, 01),
                    Value = 25,
                    Service = Services["2"],
                    PaymentOper = PaymentOpers["2"]
                });
            PaymentOperPoses.Add(
                "5",
                new PaymentOperPos
                {
                    ID = "5",
                    Period = new DateTime(2013, 01, 01),
                    Value = 40,
                    Service = Services["1"],
                    PaymentOper = PaymentOpers["3"]
                });
            PaymentOperPoses.Add(
                "6",
                    new PaymentOperPos
                    {
                        ID = "6",
                        Period = new DateTime(2013, 01, 01),
                        Value = 30,
                        Service = Services["2"],
                        PaymentOper = PaymentOpers["3"]
                    });
            PaymentOperPoses.Add(
                "7",
                    new PaymentOperPos
                    {
                        ID = "7",
                        Period = new DateTime(2012, 12, 01),
                        Value = 30,
                        Service = Services["1"],
                        PaymentOper = PaymentOpers["4"]
                    });
            PaymentOperPoses.Add(
                "8",
                new PaymentOperPos
                {
                    ID = "8",
                    Period = new DateTime(2012, 12, 01),
                    Value = 20,
                    Service = Services["2"],
                    PaymentOper = PaymentOpers["4"]
                });
        }
    }
}