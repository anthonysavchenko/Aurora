using System;
using System.Globalization;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsUploader.DecFormsParser
{
    static public class ColumnParser
    {
        const int ADDRESS_WITHOUT_BUILDING_PART_ITEMS_COUNT = 4;

        const int APARTMENT_DB_LENGTH = 10;
        const int BUILDING_DB_LENGTH = 10;
        const int STREET_DB_LENGTH = 50;

        static public bool ParseApartmentColumn(
            string source,
            out string street,
            out string building,
            out string apartment,
            out string message)
        {
            street = null;
            building = null;
            apartment = null;
            message = null;

            string sourceNoCR = source != null ? source.Replace("\n", " ").Trim() : source;

            if (string.IsNullOrWhiteSpace(sourceNoCR))
            {
                message = $"Ячейка обязательно должна быть заполнена. В данном случае она пустая.";
                return false;
            }

            string[] apartmentItems = sourceNoCR.Split(new char[] { ',' });

            bool wrong_items_number =
                apartmentItems.Length != ADDRESS_WITHOUT_BUILDING_PART_ITEMS_COUNT
                && apartmentItems.Length != ADDRESS_WITHOUT_BUILDING_PART_ITEMS_COUNT + 1;

            bool wrong_format_for_items_min_number =
                apartmentItems.Length == ADDRESS_WITHOUT_BUILDING_PART_ITEMS_COUNT
                    && (apartmentItems[0].Trim() != "г. Владивосток"
                        || string.IsNullOrWhiteSpace(apartmentItems[1])
                        || !apartmentItems[2].Trim().StartsWith("д.")
                        || !apartmentItems[3].Trim().StartsWith("кв."));

            bool wrong_format_for_items_max_number =
                apartmentItems.Length == ADDRESS_WITHOUT_BUILDING_PART_ITEMS_COUNT + 1
                    && (apartmentItems[0].Trim() != "г. Владивосток"
                        || string.IsNullOrWhiteSpace(apartmentItems[1])
                        || !apartmentItems[2].Trim().StartsWith("д.")
                        || !apartmentItems[3].Trim().StartsWith("корп.")
                        || !apartmentItems[4].Trim().StartsWith("кв."));

            if (wrong_items_number || wrong_format_for_items_min_number || wrong_format_for_items_max_number)
            {
                message = $"Прочитано значение: \"{source.Replace("\n", "<Перенос строки>")}\". " +
                    "Предусмотрено распознавание адресов в формате: \"г. Владивосток, <Название улицы>, д. " +
                    "<Номер дома>, [корп. <Номер корпуса>,] кв. <Номер квартиры> [(одпу)]\". В данном случае данные " +
                    "не соответствуют этому формату, поэтому не могут быть распознаны.";
                return false;
            }

            street = apartmentItems[1].Trim();

            if (street.Length > STREET_DB_LENGTH)
            {
                message = $"Распознано значение: \"{street}\". " +
                    $"Предусмотрено сохранение улицы длиной не более {STREET_DB_LENGTH} " +
                    "символов. В данном случае это ограничение превышено, поэтому данные не могут быть сохранены.";
                return false;
            }

            string buildingFirstPart =
                apartmentItems[2]
                    .Replace("д.", string.Empty)
                    .Trim();
            building = apartmentItems.Length == ADDRESS_WITHOUT_BUILDING_PART_ITEMS_COUNT + 1
                ? buildingFirstPart + ", корп. " +
                    apartmentItems[3]
                        .Replace("корп.", string.Empty)
                        .Trim()
                : buildingFirstPart;

            if (building.Length > BUILDING_DB_LENGTH)
            {
                message = $"Распознано значение: \"{building}\". " +
                    $"Предусмотрено сохранение номера дома длиной не более {BUILDING_DB_LENGTH} " +
                    "символов. В данном случае это ограничение превышено, поэтому данные не могут быть сохранены.";
                return false;
            }

            string apartmentDirty = apartmentItems.Length == ADDRESS_WITHOUT_BUILDING_PART_ITEMS_COUNT + 1
                ? apartmentItems[4]
                : apartmentItems[3];
            apartment = apartmentDirty
                .Replace("кв.", string.Empty)
                .Replace("(одпу)", string.Empty)
                .Trim();

            if (apartment.Length > APARTMENT_DB_LENGTH)
            {
                message = $"Распознано значение: \"{apartment}\". " +
                    $"Предусмотрено сохранение номера квартиры длиной не более {APARTMENT_DB_LENGTH} " +
                    "символов. В данном случае это ограничение превышено, поэтому данные не могут быть сохранены.";
                return false;
            }

            return true;
        }

        static public bool ParsePrevDateColumn(
            string source,
            bool isNorm,
            out DateTime? date,
            out string message)
        {
            date = null;
            message = null;

            if (!isNorm)
            {
                string sourceNoCR = source != null ? source.Replace("\n", " ").Trim() : source;

                if (string.IsNullOrWhiteSpace(sourceNoCR))
                {
                    message = "Ячейка обязательно должна быть заполнена, если не указано начисление по нормативу. " +
                        "В данном случае начисление не по нормативу, а ячейка пустая.";
                    return false;
                }

                if (!DateTime.TryParseExact(
                    sourceNoCR,
                    "dd.MM.yyyy",
                    new CultureInfo("ru-RU"),
                    DateTimeStyles.None,
                    out DateTime dateParsed))
                {
                    message = $"Прочитано значение: \"{source.Replace("\n", "<Перенос строки>")}\". " +
                        "Предусмотрено распознавание даты в формате: \"ДД.ММ.ГГГГ\". " +
                        "В данном случае данные не соответствуют этому формату, поэтому не могут быть распознаны.";
                    return false;
                }

                date = dateParsed;
            }

            return true;
        }

        static public bool ParseOptionalColumn(
            string source,
            int maxLength,
            string ofColumn,
            out string value,
            out string message)
        {
            value = null;
            message = null;

            string sourceNoCR = source != null ? source.Replace("\n", " ").Trim() : source;

            if (!string.IsNullOrWhiteSpace(sourceNoCR))
            {
                value = sourceNoCR;

                if (value.Length > maxLength)
                {
                    message = $"Распознано значение: \"{value}\". " +
                        $"Предусмотрено сохранение {ofColumn} длиной не более {maxLength} " +
                        "символов. В данном случае это ограничение превышено, поэтому данные не могут быть сохранены.";
                    return false;
                }
            }

            return true;
        }

        static public bool ParseOptionalColumn(
            string source,
            bool isNorm,
            int maxLength,
            string ofColumn,
            out string value,
            out string message)
        {
            value = null;
            message = null;

            if (!isNorm)
            {
                return ParseOptionalColumn(source, maxLength, ofColumn, out value, out message);
            }

            return true;
        }
    }
}
