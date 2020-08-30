using System;
using System.Globalization;

namespace Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesParser
{
    static public class ColumnParser
    {
        const int ADDRESS_WITHOUT_BUILDING_PART_ITEMS_COUNT = 3;

        const int BUILDING_DB_LENGTH = 25;
        const int STREET_DB_LENGTH = 50;

        static public bool ParseAddressColumn(
            string source,
            out string street,
            out string building,
            out string message)
        {
            street = null;
            building = null;
            message = null;

            string sourceNoCR = source != null ? source.Replace("\n", " ").Trim() : source;

            if (string.IsNullOrWhiteSpace(sourceNoCR))
            {
                message = $"Ячейка обязательно должна быть заполнена. В данном случае она пустая.";
                return false;
            }

            string[] buildingItems = sourceNoCR.Split(new char[] { ',' });

            bool wrong_items_number =
                buildingItems.Length != ADDRESS_WITHOUT_BUILDING_PART_ITEMS_COUNT
                && buildingItems.Length != ADDRESS_WITHOUT_BUILDING_PART_ITEMS_COUNT + 1;

            bool wrong_format_for_items_min_number =
                buildingItems.Length == ADDRESS_WITHOUT_BUILDING_PART_ITEMS_COUNT
                    && (buildingItems[0].Trim() != "г. Владивосток"
                        || string.IsNullOrWhiteSpace(buildingItems[1])
                        || !buildingItems[2].Trim().StartsWith("д."));

            bool wrong_format_for_items_max_number =
                buildingItems.Length == ADDRESS_WITHOUT_BUILDING_PART_ITEMS_COUNT + 1
                    && (buildingItems[0].Trim() != "г. Владивосток"
                        || string.IsNullOrWhiteSpace(buildingItems[1])
                        || !buildingItems[2].Trim().StartsWith("д.")
                        || !buildingItems[3].Trim().StartsWith("корп."));

            if (wrong_items_number || wrong_format_for_items_min_number || wrong_format_for_items_max_number)
            {
                message = $"Прочитано значение: \"{source.Replace("\n", "<Перенос строки>")}\". " +
                    "Предусмотрено распознавание адресов в формате: \"г. Владивосток, <Название улицы>, д. " +
                    "<Номер дома>[, корп. <Номер корпуса>]\". В данном случае данные " +
                    "не соответствуют этому формату, поэтому не могут быть распознаны.";
                return false;
            }

            street = buildingItems[1].Trim();

            if (street.Length > STREET_DB_LENGTH)
            {
                message = $"Распознано значение: \"{street}\". " +
                    $"Предусмотрено сохранение улицы длиной не более {STREET_DB_LENGTH} " +
                    "символов. В данном случае это ограничение превышено, поэтому данные не могут быть сохранены.";
                return false;
            }

            string buildingFirstPart =
                buildingItems[2]
                    .Replace("д.", string.Empty)
                    .Trim();
            building = buildingItems.Length == ADDRESS_WITHOUT_BUILDING_PART_ITEMS_COUNT + 1
                ? buildingFirstPart + ", корп. " +
                    buildingItems[3]
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

            return true;
        }

        static public bool ParseCurrentDateColumn(
            string source,
            out DateTime? date,
            out string message)
        {
            date = null;
            message = null;

            string sourceNoCR = source != null ? source.Replace("\n", " ").Trim() : source;

            if (!string.IsNullOrWhiteSpace(sourceNoCR))
            {
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
    }
}
