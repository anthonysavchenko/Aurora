using System;
using System.Linq;
using System.Text;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.WinClient.Aurora.Interface.Services;

namespace Taumis.Alpha.WinClient.Aurora.Library.Services
{
    public class BillService : IBillService
    {
        private const string BARCODE_COMPANY_CODE = "133";
        private const string BARCODE_SERVICE_CODE = "21";

        /// <summary>
        /// Создает строку с данными об обслуживающей организации
        /// </summary>
        /// <param name="bankDetail">Банковские реквизиты</param>
        /// <param name="contractorInfo">Данные подрядчика</param>
        /// <param name="emergencyPhoneNumber">Телефон аварийных служб</param>
        public string OrganizationDetails(BankDetails bankDetail, string emergencyPhoneNumber)
        {
            StringBuilder _builder = new StringBuilder();

            _builder.AppendLine("Вас обслуживает ООО \"УК Фрунзенского района\"");
            _builder.AppendLine("адрес: Рылеева, 8");
            _builder.AppendLine("по агент.договору с МУП РКЦ");

            if (bankDetail != null)
            {
                if (!string.IsNullOrEmpty(bankDetail.INN))
                {
                    _builder.Append($"ИНН {bankDetail.INN}, ");
                }

                if (!string.IsNullOrEmpty(bankDetail.KPP))
                {
                    _builder.Append($"КПП {bankDetail.KPP}, ");
                }

                _builder.Append($"БИК {bankDetail.BIK},");
                _builder.AppendLine();

                if (!string.IsNullOrEmpty(bankDetail.CorrAccount))
                {
                    _builder.Append($"к/с {bankDetail.CorrAccount}, ");
                }

                _builder.AppendLine($"р/с {bankDetail.Account},");
                _builder.AppendLine(bankDetail.Name);
            }

            _builder.AppendLine("Юр. от. 279-15-81, отд. по раб. с нас. 279-15-85, ПТО 279-15-84,");
            _builder.Append(
                $"Авар. служба {(string.IsNullOrEmpty(emergencyPhoneNumber) ? "298-09-81" : emergencyPhoneNumber)}, аб. отд. 230-27-72");

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
            string _accountNum = $"{account.Substring(0, 4)}{account.Substring(5, 3)}{account.Substring(9, 1)}";
            string _barcode = $"{BARCODE_COMPANY_CODE}{_accountNum}{period:yyyyMM}{BARCODE_SERVICE_CODE}";

            int _barcodeSum = _barcode.Sum(c => int.Parse(c.ToString()));

            return $"{_barcode}{_barcodeSum % 10}";
        }

        /// <summary>
        /// Генерирует строку для QR-кода
        /// </summary>
        /// <param name="name">Наименование организации</param>
        /// <param name="bankAccount">Расчетный счет</param>
        /// <param name="bankName">Наименование банка</param>
        /// <param name="bic">БИК</param>
        /// <param name="corrAccount">Корр. счет</param>
        /// <param name="inn">ИНН</param>
        /// <param name="category">Категория платежа</param>
        /// <param name="account">Лицевой счет</param>
        /// <param name="fullName">ФИО</param>
        /// <param name="address">Адрес</param>
        /// <param name="period">Учетный период</param>
        /// <param name="sum">Сумма платежа</param>
        /// <returns>Строка для QR-кода</returns>
        public string GenerateQrCodeString(
            string name,
            string bankAccount,
            string bankName,
            string bic,
            string corrAccount,
            string inn,
            string category,
            string account, 
            string fullName, 
            string address, 
            DateTime period, 
            decimal sum)
        {
            // Сумма в копейках
            int _sum = Convert.ToInt32(sum * 100);

            string _qrStr =
                $"ST00012|Name={name}|PersonalAcc={bankAccount}|BankName={bankName}|BIC={bic}|CorrespAcc={corrAccount}|PayeeINN={inn}|Category={category}|PersAcc={account}|PayerAddress={address}|Sum={_sum}|PaymPeriod={period:MM.yyyy}";

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
