using System.Text.RegularExpressions;

namespace Taumis.Alpha.Infrastructure.Library.Services.CommonParsers
{
    public static class CommonCellParser
    {
        const int ADDRESS_WITHOUT_BUILDING_PART_ITEMS_COUNT = 3;

        const int BUILDING_DB_LENGTH = 25;
        const int STREET_DB_LENGTH = 50;

        const byte MAX_BYTE_VALUE = 100;

        public static bool TryParseBuildingAddress(
            string source,
            out string street,
            out string building,
            out string errorDescription)
        {
            street = null;
            building = null;
            errorDescription = null;

            string sourceNoCR = source != null ? source.Replace("\n", " ").Trim() : source;

            if (string.IsNullOrWhiteSpace(sourceNoCR))
            {
                errorDescription = $"Ячейка обязательно должна быть заполнена. В данном случае она пустая.";
                return false;
            }

            string[] buildingItems = sourceNoCR.Split(new char[] { ',' });

            bool noBuildingPartFormat =
                buildingItems.Length == ADDRESS_WITHOUT_BUILDING_PART_ITEMS_COUNT
                    && buildingItems[0].Trim() == "г. Владивосток"
                    && (buildingItems[1].Trim().StartsWith("пер")
                        || buildingItems[1].Trim().StartsWith("пр-кт")
                        || buildingItems[1].Trim().StartsWith("ул."))
                    && buildingItems[2].Trim().StartsWith("д.");

            bool withBuildingPartFormat =
                buildingItems.Length == ADDRESS_WITHOUT_BUILDING_PART_ITEMS_COUNT + 1
                    && buildingItems[0].Trim() == "г. Владивосток"
                    && (buildingItems[1].Trim().StartsWith("пер")
                        || buildingItems[1].Trim().StartsWith("пр-кт")
                        || buildingItems[1].Trim().StartsWith("ул."))
                    && buildingItems[2].Trim().StartsWith("д.")
                    && buildingItems[3].Trim().StartsWith("корп.");

            if (!noBuildingPartFormat
                && !withBuildingPartFormat)
            {
                errorDescription = $"Прочитано значение: \"{source.Replace("\n", "<Перенос строки>")}\". " +
                    "Предусмотрено распознавание адресов в формате: \"г. Владивосток, пер|пр-кт|ул. " +
                    "<Название улицы>, д. <Номер дома>[, корп. <Номер корпуса>]\". В данном случае данные " +
                    "не соответствуют этому формату, поэтому не могут быть распознаны.";
                return false;
            }

            street = buildingItems[1].Trim();

            if (street.Length > STREET_DB_LENGTH)
            {
                errorDescription = $"Распознано значение: \"{street}\". " +
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
                errorDescription = $"Распознано значение: \"{building}\". " +
                    $"Предусмотрено сохранение номера дома длиной не более {BUILDING_DB_LENGTH} " +
                    "символов. В данном случае это ограничение превышено, поэтому данные не могут быть сохранены.";
                return false;
            }

            return true;
        }

        public static bool TryParseDecimal(
            string source,
            string sourceNoCR,
            string cellDataName,
            out decimal? value,
            out string errorDescription,
            bool negativeAllowed = true,
            bool zeroAllowed = true)
        {
            value = null;
            errorDescription = null;

            if (negativeAllowed)
            {
                zeroAllowed = true;
            }

            if (!string.IsNullOrWhiteSpace(sourceNoCR))
            {
                bool isMatchRegex =
                    negativeAllowed
                        ? Regex.IsMatch(sourceNoCR, @"^-?\d{1,8}(,\d{1,3})?$")
                        : Regex.IsMatch(sourceNoCR, @"^\d{1,8}(,\d{1,3})?$");

                if (!isMatchRegex
                    || !decimal.TryParse(sourceNoCR, out decimal parsedValue)
                    || (!zeroAllowed
                        && parsedValue == 0))
                {
                    string formatAllowed =
                        negativeAllowed
                            ? "положительного или отрицательного десятичного числа (0 также допускается)"
                            : zeroAllowed
                                ? "положительного десятичного числа (0 также допускается)"
                                : "положительного десятичного числа (0 не допускается)";

                    errorDescription = $"Прочитано значение: \"{source.Replace("\n", "<Перенос строки>")}\". " +
                        $"Для ячейки, содержащей {cellDataName}, предусмотрено распознавание данных в формате " +
                        $"одного {formatAllowed}, которое содержит не более 8 цифр до запятой " +
                        "и не более 3 цифр после запятой. В данном случае данные не соответствуют " +
                        "этому формату, поэтому не могут быть распознаны.";
                    return false;
                }

                value = parsedValue;
            }

            return true;
        }

        public static bool TryParseByte(
            string source,
            string sourceNoCR,
            string cellDataName,
            out byte? value,
            out string errorDescription,
            bool zeroAllowed = true)
        {
            value = null;
            errorDescription = null;

            if (!string.IsNullOrWhiteSpace(sourceNoCR))
            {
                byte minValue = (byte)(zeroAllowed ? 0 : 1);

                if (!byte.TryParse(sourceNoCR, out byte parsedValue)
                    || parsedValue < minValue
                    || parsedValue > MAX_BYTE_VALUE)
                {
                    errorDescription = $"Прочитано значение: \"{source.Replace("\n", "<Перенос строки>")}\". " +
                        $"Для ячейки, содержащей {cellDataName}, предусмотрено распознавание данных " +
                        $"в формате целого числа от {minValue} до {MAX_BYTE_VALUE}. В данном случае " +
                        "данные не соответствуют этому формату, поэтому не могут быть распознаны.";
                    return false;
                }

                value = parsedValue;
            }

            return true;
        }

        public static bool HasValue(
            string sourceNoCR,
            string cellDataName,
            out string errorDescription)
        {
            errorDescription = null;

            if (string.IsNullOrWhiteSpace(sourceNoCR))
            {
                errorDescription = $"Ячейка, содержащая {cellDataName}, обязательно должна быть заполнена. " +
                    "В данном случае ячейка пуста.";
                return false;
            }

            return true;
        }

        public static bool HasAppropriateLength(
            string value,
            int maxStringLength,
            string cellDataName,
            out string errorDescription)
        {
            errorDescription = null;

            if (!string.IsNullOrEmpty(value) && value.Length > maxStringLength)
            {
                errorDescription = $"Распознано значение: \"{value}\". Для ячейки, содержащей {cellDataName}, " +
                    "предусмотрено сохранение данных в формате строки, которая содержит не более " +
                    $"{maxStringLength} символов. В данном случае это ограничение превышено, " +
                    "поэтому данные не могут быть сохранены.";
                return false;
            }

            return true;
        }

        public static string GetEnumParsingErrorDescription(
            string source,
            string cellDataName,
            string values)
        {
            return
                $"Прочитано значение: \"{source.Replace("\n", "<Перенос строки>")}\". " +
                $"Для ячейки, содержащей {cellDataName}, предусмотрено распознавание данных в формате " +
                $"одного из значений: {values}. В данном случае данные не соответствуют этому формату, " +
                "поэтому не могут быть распознаны.";
        }

        public static string ReplaceCaretReturn(string source)
        {
            string sourceNoCR =
                source != null
                    ? source.Replace("\n", " ").Trim()
                    : source;

            return
                !string.IsNullOrEmpty(sourceNoCR)
                    ? sourceNoCR
                    : null;
        }
    }
}
