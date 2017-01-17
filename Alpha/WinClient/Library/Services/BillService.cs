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
        public string OrganizationDetails(BankDetails bankDetail, string contractorInfo, string emergencyPhoneNumber)
        {
            StringBuilder _builder = new StringBuilder();

            _builder.AppendLine("Директор Слаутенко А.В. г. Владивосток, ул. Рылеева, 8.");

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
