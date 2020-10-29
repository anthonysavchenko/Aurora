using System;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.CellParsers;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser.RowParsers
{
    public static class BuildingInfoRowParser
    {
        const string ADDRESS_COLUMN = "B";
        const string DEBT_COLUMN = "J";
        const string CALCULATION_METHOD_COLUMN = "D";
        const string VOLUME_COLUMN = "J";
        const string NORM_COLUMN = "E";
        const string COLLECTIVE_VOLUME_COLUMN = "E";
        const string NOT_DISTRIBUTED_VOLUME_COLUMN = "I";
        const string COLLECTIVE_SQUARE_COLUMN = "E";

        public delegate bool TryParseRowMethod(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            CalculationMethod calculationMethod,
            int rowNumber,
            out CalculationRows row);

        public static bool TryParseAddressRow(
            ExcelSheet source,
            int rowNumber,
            out CalculationRows row)
        {
            row = new CalculationRows()
            {
                RowType = (byte)CalculationRowType.BuildingInfo,
            };

            try
            {
                if (!BuildingInfoCellParser.TryParseAddress(
                    source.GetCellText($"{ADDRESS_COLUMN}{rowNumber}"),
                    out string street,
                    out string building,
                    out string description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        ADDRESS_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                row.BuildingInfo = new CalculationBuildingInfos()
                {
                    RowType = (byte)BuildingInfoRowType.Address,
                    Street = street,
                    Building = building,
                };

                row.ProcessingResult = (byte)RowProcessingResult.OK;
            }
            catch (Exception e)
            {
                CalculationRowHandler.SetParsingError(row, e);
                return false;
            }

            return true;
        }

        public static bool TryParseDebtRow(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            int rowNumber,
            out CalculationRows row)
        {
            row = new CalculationRows()
            {
                RowType = (byte)CalculationRowType.BuildingInfo,
                BuildingAddressRow = buildingAddressRow,
            };

            try
            {
                if (!BuildingInfoCellParser.TryParseDebt(
                    source.GetCellText($"{DEBT_COLUMN}{rowNumber}"),
                    out decimal? debt,
                    out string description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        DEBT_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                row.BuildingInfo = new CalculationBuildingInfos()
                {
                    RowType = (byte)BuildingInfoRowType.Debt,
                    Debt = debt.Value,
                };

                row.ProcessingResult = (byte)RowProcessingResult.OK;
            }
            catch (Exception e)
            {
                CalculationRowHandler.SetParsingError(row, e);
                return false;
            }

            return true;
        }

        public static bool TryParseCalculationMethodRow(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            int rowNumber,
            out CalculationMethod calculationMethod,
            out CalculationRows row)
        {
            row = new CalculationRows()
            {
                RowType = (byte)CalculationRowType.BuildingInfo,
                BuildingAddressRow = buildingAddressRow,
            };

            calculationMethod = CalculationMethod.Unknown;

            try
            {
                calculationMethod = BuildingInfoCellParser.ParseCalculationMethod(
                    source.GetCellText($"{CALCULATION_METHOD_COLUMN}{rowNumber}"));

                if (calculationMethod != CalculationMethod.BuildingCounters)
                {
                    decimal? volume = null;

                    if (calculationMethod == CalculationMethod.Avarage)
                    {
                        if (!BuildingInfoCellParser.TryParseVolume(
                            source.GetCellText($"{VOLUME_COLUMN}{rowNumber}"),
                            out volume,
                            out string description))
                        {
                            CalculationRowHandler.SetParsingError(
                                row,
                                VOLUME_COLUMN,
                                rowNumber,
                                description);
                            return false;
                        }
                    }

                    row.BuildingInfo = new CalculationBuildingInfos()
                    {
                        RowType = (byte)BuildingInfoRowType.CalculationMethod,
                        CalculationMethod = (byte)calculationMethod,
                        Volume = volume,
                    };

                    row.ProcessingResult = (byte)RowProcessingResult.OK;
                }
                else
                {
                    row.BuildingAddressRow = null;
                    row = null;
                }
            }
            catch (Exception e)
            {
                CalculationRowHandler.SetParsingError(row, e);
                return false;
            }

            return true;
        }

        public static bool TryParseNormRow(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            CalculationMethod calculationMethod,
            int rowNumber,
            out CalculationRows row)
        {
            row = new CalculationRows()
            {
                RowType = (byte)CalculationRowType.BuildingInfo,
                BuildingAddressRow = buildingAddressRow,
            };

            try
            {
                if (!BuildingInfoCellParser.TryParseNorm(
                    source.GetCellText($"{NORM_COLUMN}{rowNumber}"),
                    calculationMethod,
                    out decimal? norm,
                    out string description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        NORM_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                row.BuildingInfo = new CalculationBuildingInfos()
                {
                    RowType = (byte)BuildingInfoRowType.Norm,
                    Norm = norm,
                };

                row.ProcessingResult = (byte)RowProcessingResult.OK;
            }
            catch (Exception e)
            {
                CalculationRowHandler.SetParsingError(row, e);
                return false;
            }

            return true;
        }

        public static bool TryParseCollectiveVolumeRow(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            int rowNumber,
            out CalculationRows row)
        {
            row = new CalculationRows()
            {
                RowType = (byte)CalculationRowType.BuildingInfo,
                BuildingAddressRow = buildingAddressRow,
            };

            try
            {
                if (!BuildingInfoCellParser.TryParseCollectiveVolume(
                    source.GetCellText($"{COLLECTIVE_VOLUME_COLUMN}{rowNumber}"),
                    out decimal? collectiveVolume,
                    out string description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        COLLECTIVE_VOLUME_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                if (!BuildingInfoCellParser.TryParseNotDistributedVolume(
                    source.GetCellText($"{NOT_DISTRIBUTED_VOLUME_COLUMN}{rowNumber}"),
                    out decimal? notDistributedVolume,
                    out description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        NOT_DISTRIBUTED_VOLUME_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                row.BuildingInfo = new CalculationBuildingInfos()
                {
                    RowType = (byte)BuildingInfoRowType.CollectiveVolume,
                    CollectiveVolume = collectiveVolume.Value,
                    NotDistributedVolume = notDistributedVolume.Value,
                };

                row.ProcessingResult = (byte)RowProcessingResult.OK;
            }
            catch (Exception e)
            {
                CalculationRowHandler.SetParsingError(row, e);
                return false;
            }

            return true;
        }

        public static bool TryParseCollectiveSquareRow(
            ExcelSheet source,
            CalculationRows buildingAddressRow,
            CalculationMethod calculationMethod,
            int rowNumber,
            out CalculationRows row)
        {
            row = new CalculationRows()
            {
                RowType = (byte)CalculationRowType.BuildingInfo,
                BuildingAddressRow = buildingAddressRow,
            };

            try
            {
                if (!BuildingInfoCellParser.TryParseCollectiveSquare(
                    source.GetCellText($"{COLLECTIVE_SQUARE_COLUMN}{rowNumber}"),
                    calculationMethod,
                    out decimal? collectiveSquare,
                    out string description))
                {
                    CalculationRowHandler.SetParsingError(
                        row,
                        COLLECTIVE_SQUARE_COLUMN,
                        rowNumber,
                        description);
                    return false;
                }

                row.BuildingInfo = new CalculationBuildingInfos()
                {
                    RowType = (byte)BuildingInfoRowType.CollectiveSquare,
                    CollectiveSquare = collectiveSquare,
                };

                row.ProcessingResult = (byte)RowProcessingResult.OK;
            }
            catch (Exception e)
            {
                CalculationRowHandler.SetParsingError(row, e);
                return false;
            }

            return true;
        }

        public static bool IsDebtCellEmpty(
            ExcelSheet source,
            int rowNumber)
        {
            return
                string.IsNullOrEmpty(
                    source.GetCellText($"{DEBT_COLUMN}{rowNumber}"));
        }
    }
}
