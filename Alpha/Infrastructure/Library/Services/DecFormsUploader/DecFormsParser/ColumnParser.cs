using System;
using System.Globalization;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsUploader.DecFormsParser
{
    static public class ColumnParser
    {
        const int ADDRESS_WITHOUT_BUILDING_PART_ITEMS_COUNT = 4;
        const int ACCOUNT_DB_LENGTH = 25;

        const int APARTMENT_DB_LENGTH = 10;
        const int BUILDING_DB_LENGTH = 25;
        const int STREET_DB_LENGTH = 50;

        static public bool ParseAccountColumn(
            string source,
            out string account,
            out string message)
        {
            account = null;
            message = null;

            string sourceNoCR = source != null ? source.Replace("\n", " ").Trim() : source;

            if (string.IsNullOrWhiteSpace(sourceNoCR))
            {
                message = $"Ячейка обязательно должна быть заполнена. В данном случае она пустая.";
                return false;
            }

            account = 
                sourceNoCR
                    .Replace("(о)", string.Empty)
                    .Trim();

            if (account.Length > ACCOUNT_DB_LENGTH)
            {
                message = $"Распознано значение: \"{account}\". " +
                    $"Предусмотрено сохранение лицевого счета длиной не более {ACCOUNT_DB_LENGTH} " +
                    "символов. В данном случае это ограничение превышено, поэтому данные не могут быть сохранены.";
                return false;
            }

            return true;
        }

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

            bool format_no_building_part_no_apartment =
                apartmentItems.Length == ADDRESS_WITHOUT_BUILDING_PART_ITEMS_COUNT - 1
                    && apartmentItems[0].Trim() == "г. Владивосток"
                    && (apartmentItems[1].Trim().StartsWith("пер")
                        || apartmentItems[1].Trim().StartsWith("пр-кт")
                        || apartmentItems[1].Trim().StartsWith("ул."))
                    && apartmentItems[2].Trim().StartsWith("д.")
                    && !apartmentItems[2].Contains("/");

            bool format_no_building_part_with_apartment =
                apartmentItems.Length == ADDRESS_WITHOUT_BUILDING_PART_ITEMS_COUNT
                    && apartmentItems[0].Trim() == "г. Владивосток"
                    && (apartmentItems[1].Trim().StartsWith("пер")
                        || apartmentItems[1].Trim().StartsWith("пр-кт")
                        || apartmentItems[1].Trim().StartsWith("ул."))
                    && apartmentItems[2].Trim().StartsWith("д.")
                    && !apartmentItems[2].Contains("/")
                    && apartmentItems[3].Trim().StartsWith("кв.");

            bool format_with_building_part_no_apartment =
                apartmentItems.Length == ADDRESS_WITHOUT_BUILDING_PART_ITEMS_COUNT + 1 - 1
                    && apartmentItems[0].Trim() == "г. Владивосток"
                    && (apartmentItems[1].Trim().StartsWith("пер")
                        || apartmentItems[1].Trim().StartsWith("пр-кт")
                        || apartmentItems[1].Trim().StartsWith("ул."))
                    && apartmentItems[2].Trim().StartsWith("д.")
                    && !apartmentItems[2].Contains("/")
                    && apartmentItems[3].Trim().StartsWith("корп.");

            bool format_with_building_part_with_apartment =
                apartmentItems.Length == ADDRESS_WITHOUT_BUILDING_PART_ITEMS_COUNT + 1
                    && apartmentItems[0].Trim() == "г. Владивосток"
                    && (apartmentItems[1].Trim().StartsWith("пер")
                        || apartmentItems[1].Trim().StartsWith("пр-кт")
                        || apartmentItems[1].Trim().StartsWith("ул."))
                    && apartmentItems[2].Trim().StartsWith("д.")
                    && !apartmentItems[2].Contains("/")
                    && apartmentItems[3].Trim().StartsWith("корп.")
                    && apartmentItems[4].Trim().StartsWith("кв.");

            bool format_no_building_part_slash_apartment =
                apartmentItems.Length == ADDRESS_WITHOUT_BUILDING_PART_ITEMS_COUNT - 1
                    && apartmentItems[0].Trim() == "г. Владивосток"
                    && (apartmentItems[1].Trim().StartsWith("пер")
                        || apartmentItems[1].Trim().StartsWith("пр-кт")
                        || apartmentItems[1].Trim().StartsWith("ул."))
                    && apartmentItems[2].Trim().StartsWith("д.")
                    && apartmentItems[2].Contains("/");


            if (!format_no_building_part_no_apartment
                && !format_no_building_part_with_apartment
                && !format_with_building_part_no_apartment
                && !format_with_building_part_with_apartment
                && !format_no_building_part_slash_apartment)
            {
                message = $"Прочитано значение: \"{source.Replace("\n", "<Перенос строки>")}\". " +
                    "Предусмотрено распознавание адресов в формате: \"г. Владивосток, пер|пр-кт|ул. " +
                    "<Название улицы>, д. <Номер дома>[, корп. <Номер корпуса>][, кв. <Номер квартиры>][ (одпу)]\" " +
                    "(в номере дома может быть указана дробь только если, через дробь указывается номер " +
                    "квартиры). В данном случае данные не соответствуют этому формату, поэтому не могут быть " +
                    "распознаны.";
                return false;
            }

            if (format_no_building_part_slash_apartment)
            {
                string[] buildingItems =
                    apartmentItems[2]
                        .Replace("д.", string.Empty)
                        .Replace("(одпу)", string.Empty)
                        .Trim()
                        .Split(new char[] { '/' });

                if (buildingItems.Length != 2
                    || string.IsNullOrEmpty(buildingItems[0])
                    || string.IsNullOrEmpty(buildingItems[1]))
                {
                    message = $"Прочитано значение: \"{source.Replace("\n", "<Перенос строки>")}\". " +
                        "При наличие дроби в номере дома предусмотрено распознавание адресов в формате: " +
                        "\"г. Владивосток, пер|пр-кт|ул. <Название улицы>, д. <Номер дома>/<Номер квартиры>" +
                        "[ (одпу)]\". В данном случае данные не соответствуют этому формату, поэтому не могут быть " +
                        "распознаны.";
                    return false;
                }

                building = buildingItems[0].Trim();
                apartment = "/" + buildingItems[1].Trim();
            }

            street = apartmentItems[1].Trim();

            if (street.Length > STREET_DB_LENGTH)
            {
                message = $"Распознано значение: \"{street}\". " +
                    $"Предусмотрено сохранение улицы длиной не более {STREET_DB_LENGTH} " +
                    "символов. В данном случае это ограничение превышено, поэтому данные не могут быть сохранены.";
                return false;
            }

            if (!format_no_building_part_slash_apartment)
            {
                string buildingFirstPart =
                    apartmentItems[2]
                        .Replace("д.", string.Empty)
                        .Replace("(одпу)", string.Empty)
                        .Trim();
                building =
                    format_with_building_part_no_apartment || format_with_building_part_with_apartment
                        ? buildingFirstPart + ", корп. " +
                            apartmentItems[3]
                                .Replace("корп.", string.Empty)
                                .Replace("(одпу)", string.Empty)
                                .Trim()
                        : buildingFirstPart;
            }

            if (building.Length > BUILDING_DB_LENGTH)
            {
                message = $"Распознано значение: \"{building}\". " +
                    $"Предусмотрено сохранение номера дома длиной не более {BUILDING_DB_LENGTH} " +
                    "символов. В данном случае это ограничение превышено, поэтому данные не могут быть сохранены.";
                return false;
            }

            if (!format_no_building_part_slash_apartment)
            {
                string apartmentDirty =
                    format_no_building_part_with_apartment
                        ? apartmentItems[3]
                        : format_with_building_part_with_apartment
                            ? apartmentItems[4]
                            : string.Empty;
                apartment = apartmentDirty
                    .Replace("кв.", string.Empty)
                    .Replace("(одпу)", string.Empty)
                    .Trim();
            }

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
