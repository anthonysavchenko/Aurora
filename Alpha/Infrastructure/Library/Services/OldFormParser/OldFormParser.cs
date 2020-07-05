using System;
using System.Globalization;
using Taumis.Alpha.Infrastructure.Interface.Models;

namespace Taumis.Alpha.Infrastructure.Library.Services.OldFormParser
{
    static public class OldFormParser
    {
        const int ADDRESS_ITEMS_MIN_NUMBER = 4;
        const int ADDRESS_ITEMS_MAX_NUMBER = 5;

        /* Settings */
        const int ADDRESS_MAX_LENGTH = 200;

        static public bool ParseAddress(string source, out Address address, out string message)
        {
            address = null;
            message = string.Empty;

            if (source.Length > ADDRESS_MAX_LENGTH)
            {
                message = $"Предусмотрена максимально допустимая длина строки: {ADDRESS_MAX_LENGTH} символов. В " +
                    $"данном случае она превышена.";
                return false;
            }

            while (source.Contains("  "))
            {
                source = source.Replace("  ", " ");
            }

            string[] addressItems = source.Split(new char[] { ',' });

            bool wrong_items_number = addressItems.Length < ADDRESS_ITEMS_MIN_NUMBER 
                || addressItems.Length > ADDRESS_ITEMS_MAX_NUMBER;

            bool wrong_format_for_items_min_number =
                addressItems.Length == ADDRESS_ITEMS_MIN_NUMBER
                    && (addressItems[0].Trim() != "г. Владивосток"
                        || !addressItems[2].Trim().StartsWith("д.")
                        || !addressItems[3].Trim().StartsWith("кв."));

            bool wrong_format_for_items_max_number =
                addressItems.Length == ADDRESS_ITEMS_MAX_NUMBER
                    && (addressItems[0].Trim() != "г. Владивосток"
                        || !addressItems[2].Trim().StartsWith("д.")
                        || !addressItems[3].Trim().StartsWith("корп.")
                        || !addressItems[4].Trim().StartsWith("кв."));

            if (wrong_items_number || wrong_format_for_items_min_number || wrong_format_for_items_max_number)
            {
                message = "Предусмотрено распознавание адресов в формате: \"г. Владивосток, <Название улицы>, д. " +
                    "<Номер дома>, [корп. <Номер корпуса>,] кв. <Номер квартиры> [(одпу)]\". В данном случае данные " +
                    "не соответствуют этому формату, поэтому не могут быть распознаны.";
                return false;
            }

            address =
                new Address()
                {
                    Building = addressItems[1].Trim() + ", " + addressItems[2].Trim(),
                    Apartment = addressItems[3]
                        .Replace("кв.", string.Empty)
                        .Replace("(одпу)", string.Empty)
                        .Trim(),
                };

            return true;
        }

        static public bool ParsePrevDate(string source, out DateTime date, out string message)
        {
            message = string.Empty;

            if (!DateTime.TryParseExact(source, "dd.MM.yyyy", new CultureInfo("ru-RU"), DateTimeStyles.None, out date))
            {
                message = "Предусмотрено распознавание даты в формате: \"ДД.ММ.ГГГГ\". " +
                    "В данном случае данные не соответствуют этому формату, поэтому не могут быть распознаны.";
                return false;
            }

            return true;
        }
    }
}
