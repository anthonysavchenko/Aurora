﻿using System;
using System.Linq;
using System.Text;
using Taumis.Alpha.Server.Core.Services;

namespace Taumis.Alpha.Server.Infrastructure.Services
{
    public class BillService : IBillService
    {
        private const string BARCODE_COMPANY_CODE = "133";
        private const string BARCODE_SERVICE_CODE = "21";

        /// <summary>
        /// Создает строку с данными об обслуживающей организации
        /// </summary>
        /// <param name="contractorInfo">Данные подрядчика</param>
        /// <param name="emergencyPhoneNumber">Телефон аварийных служб</param>
        public string OrganizationDetails(
            string account,
            string corrAccount,
            string inn, 
            string kpp, 
            string bik, 
            string bankName, 
            string contractorInfo, 
            string emergencyPhoneNumber)
        {
            StringBuilder _builder = new StringBuilder();

            _builder.AppendLine("Директор Слаутенко А.В. г. Владивосток, ул. Рылеева, 8.");

            if (!string.IsNullOrEmpty(inn))
            {
                _builder.Append($"ИНН {inn}, ");
            }

            if (!string.IsNullOrEmpty(kpp))
            {
                _builder.Append($"КПП {kpp}, ");
            }
                
            _builder.Append($"БИК {bik},");
            _builder.AppendLine();

            if (!string.IsNullOrEmpty(corrAccount))
            {
                _builder.Append($"к/с {corrAccount}, ");
            }

            _builder.AppendLine($"р/с {account},");
            _builder.AppendLine(bankName);

            _builder.AppendLine("Юр. от. 279-15-81, отд. по раб. с нас. 279-15-85, ПТО 279-15-84,");
            _builder.Append(
                $"Авар. служба {(string.IsNullOrEmpty(emergencyPhoneNumber) ? "298-09-81" : emergencyPhoneNumber)}, аб. отд. 230-27-72, адрес: Рылеева, 8");

            if (!string.IsNullOrEmpty(contractorInfo))
            {
                _builder.AppendLine();
                _builder.Append(contractorInfo);
            }

            return _builder.ToString();
        }

        /// <summary>
        /// Генерирует строку для штрих кода
        /// </summary>
        /// <param name="account">Лицевой счет абонента</param>
        /// <param name="period">Дата квитанции</param>
        /// <returns>Строка для штрих кода</returns>
        public string GenerateBarCodeString(string account, DateTime period)
        {
            string _accountNum = $"{account.Substring(3, 4)}{account.Substring(8, 3)}{account.Substring(12, 1)}";
            string _barcode = $"{BARCODE_COMPANY_CODE}{_accountNum}{period:yyyyMM}{BARCODE_SERVICE_CODE}";

            int _barcodeSum = _barcode.Sum(c => int.Parse(c.ToString()));

            return $"{_barcode}{_barcodeSum % 10}";
        }

        /// <summary>
        /// Генерирует строку для QR-кода
        /// </summary>
        /// <param name="account">Лицевой счет</param>
        /// <param name="fullName">ФИО</param>
        /// <param name="address">Адрес</param>
        /// <param name="period">Учетный период</param>
        /// <param name="sum">Сумма платежа</param>
        /// <returns>Строка для QR-кода</returns>
        public string GenerateQrCodeString(string account, string fullName, string address, DateTime period, decimal sum)
        {
            string _accountNum = account.Substring(3);
            // Сумма в копейках
            int _sum = Convert.ToInt32(sum * 100);

            string _qrStr =
                $"ST00012|Name=ООО \"УК Фрунзенского района\"|PersonalAcc=40702810900100001650|BankName=ОАО \"Дальневосточный банк\" г.Владивосток|BIC=040507705|CorrespAcc=30101810900000000705|PayeeINN=2540165515|Category=Квартплата|PersAcc={_accountNum}|PayerAddress={address}|Sum={_sum}|PaymPeriod={period:MM.yyyy}";

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

            return _qrStr;
        }

        /// <summary>
        /// Форматирует строку для штрих кода
        /// </summary>
        /// <param name="barcode">Строка штих кода</param>
        public string FormatBarcodeString(string barcode)
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
