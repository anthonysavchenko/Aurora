using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.CalculationUploads.Constants;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.CalculationUploads.Views.CalculationForm
{
    public class CalculationFormViewPresenter : BaseDomainPresenter<ICalculationFormView>
    {
        /// <summary>
        /// Отображает домен на view
        /// </summary>
        public void ShowDomainOnView()
        {
            string itemID = (string)WorkItem.State[ModuleStateNames.SELECTED_FILE_ID];

            if (int.TryParse(itemID, out int id))
            {
                using (var db = new Entities())
                {
                    var rows =
                        db.CalculationRows
                            .Where(x => x.CalculationForms.CalculationFiles.ID == id)
                            .Select(x =>
                                new
                                {
                                    x.ID,

                                    x.RowType,
                                    BuildingInfoRowType =
                                        x.RowType == (byte)CalculationRowType.BuildingInfo
                                            ? x.BuildingInfo.RowType
                                            : (byte)BuildingInfoRowType.Unknown,

                                    Street =
                                        x.RowType == (byte)CalculationRowType.BuildingInfo
                                            && x.BuildingInfo.RowType == (byte)BuildingInfoRowType.Address
                                                ? x.BuildingInfo.Street
                                                : string.Empty,

                                    Building =
                                        x.RowType == (byte)CalculationRowType.BuildingInfo
                                            && x.BuildingInfo.RowType == (byte)BuildingInfoRowType.Address
                                                ? x.BuildingInfo.Building
                                                : string.Empty,

                                    CalculationMethod =
                                        x.RowType == (byte)CalculationRowType.BuildingInfo
                                            && x.BuildingInfo.RowType == (byte)BuildingInfoRowType.CalculationMethod
                                                ? x.BuildingInfo.CalculationMethod == (byte)CalculationMethod.BuildingCounters
                                                    ? "ОДПУ"
                                                    : x.BuildingInfo.CalculationMethod == (byte)CalculationMethod.Norm
                                                        ? "Норматив"
                                                        : x.BuildingInfo.CalculationMethod == (byte)CalculationMethod.Avarage
                                                            ? "Среднее"
                                                            : string.Empty
                                                : string.Empty,


                                    CounterNumber =
                                        x.RowType == (byte)CalculationRowType.BuildingCounter
                                            ? x.BuildingCounter.CounterNumber
                                            : string.Empty,

                                    Model =
                                        x.RowType == (byte)CalculationRowType.BuildingCounter
                                            ? x.BuildingCounter.Model
                                            : string.Empty,

                                    Coefficient =
                                        x.RowType == (byte)CalculationRowType.BuildingCounter
                                            ? x.BuildingCounter.Coefficient
                                            : (byte?)null,

                                    CurrentValue =
                                        x.RowType == (byte)CalculationRowType.BuildingCounter
                                            ? x.BuildingCounter.CurrentValue
                                            : (decimal?)null,

                                    PrevValue =
                                        x.RowType == (byte)CalculationRowType.BuildingCounter
                                            ? x.BuildingCounter.PrevValue
                                            : (decimal?)null,


                                    Account =
                                        x.RowType == (byte)CalculationRowType.Customer
                                            ? x.Customer.Account
                                            : string.Empty,

                                    Apartment =
                                        x.RowType == (byte)CalculationRowType.Customer
                                            ? x.Customer.Apartment
                                            : string.Empty,

                                    Owner =
                                        x.RowType == (byte)CalculationRowType.Customer
                                            ? x.Customer.Owner
                                            : string.Empty,

                                    CounterType =
                                        x.RowType == (byte)CalculationRowType.Customer
                                            ? x.Customer.CounterType == (byte)CalculationCounterType.Common
                                                ? "Двухтарифный"
                                                : x.Customer.CounterType == (byte)CalculationCounterType.Day
                                                    ? "Однотарифный (день)"
                                                    : x.Customer.CounterType == (byte)CalculationCounterType.Night
                                                        ? "Однотарифный (ночь)"
                                                        : "Норматив"
                                            : string.Empty,

                                    Volume =
                                        x.RowType == (byte)CalculationRowType.Customer
                                            ? x.Customer.Volume
                                            : (decimal?)null,

                                    Recalculation =
                                        x.RowType == (byte)CalculationRowType.Customer
                                            ? x.Customer.Recalculation
                                            : (decimal?)null,

                                    Square =
                                        x.RowType == (byte)CalculationRowType.Customer
                                            ? x.Customer.Square
                                            : null,

                                    Residents =
                                        x.RowType == (byte)CalculationRowType.Customer
                                            ? x.Customer.Residents
                                            : (byte?)null,


                                    Contract =
                                        x.RowType == (byte)CalculationRowType.LegalEntity
                                            ? x.LegalEntity.Contract
                                            : string.Empty,

                                    EntityName =
                                        x.RowType == (byte)CalculationRowType.LegalEntity
                                            ? x.LegalEntity.EntityName
                                            : string.Empty,

                                    ChargedVolume =
                                        x.RowType == (byte)CalculationRowType.LegalEntity
                                            ? x.LegalEntity.ChargedVolume
                                            : (decimal?)null,

                                    EntitySquare =
                                        x.RowType == (byte)CalculationRowType.LegalEntity
                                            ? x.LegalEntity.Square
                                            : null,

                                    x.ProcessingResult,

                                    x.ErrorDescription,

                                    x.ExceptionMessage,
                                })
                            .OrderBy(x => x.ID)
                            .ToList();

                    DataTable table = new DataTable();

                    table.Columns.Add("ID");
                    table.Columns.Add("A");
                    table.Columns.Add("B");
                    table.Columns.Add("C");
                    table.Columns.Add("D");
                    table.Columns.Add("E");
                    table.Columns.Add("F");
                    table.Columns.Add("G");
                    table.Columns.Add("H");
                    table.Columns.Add("I");
                    table.Columns.Add("J");
                    table.Columns.Add("K");
                    table.Columns.Add("L");
                    table.Columns.Add("M");
                    table.Columns.Add("N");
                    table.Columns.Add("O");
                    table.Columns.Add("P");
                    table.Columns.Add("Result");
                    table.Columns.Add("Description");

                    foreach (var row in rows)
                    {
                        table.Rows.Add(
                            row.ID.ToString(),

                            //A
                            row.RowType == (byte)CalculationRowType.Customer
                                ? row.Account
                                : row.RowType == (byte)CalculationRowType.LegalEntity
                                    ? row.Contract
                                    : string.Empty,

                            //B
                            row.BuildingInfoRowType == (byte)BuildingInfoRowType.Address
                                ? $"{row.Street}, д. {row.Building}"
                                : string.Empty,

                            //C
                            row.RowType == (byte)CalculationRowType.BuildingCounter
                                ? row.Model
                                : row.RowType == (byte)CalculationRowType.Customer
                                    ? row.Apartment
                                    : string.Empty,

                            //D
                            row.RowType == (byte)CalculationRowType.BuildingCounter
                                ? row.CounterNumber
                                : row.RowType == (byte)CalculationRowType.Customer
                                    ? row.Owner
                                    : row.RowType == (byte)CalculationRowType.LegalEntity
                                        ? row.EntityName
                                        : string.Empty,

                            //E
                            row.RowType == (byte)CalculationRowType.BuildingCounter
                                && row.Coefficient.HasValue
                                    ? $"{row.Coefficient:n0}"
                                    : string.Empty,

                            //F
                            row.RowType == (byte)CalculationRowType.Customer
                                ? row.CounterType
                                : string.Empty,

                            //G
                            row.RowType == (byte)CalculationRowType.Customer
                                && row.Volume.HasValue
                                    ? $"{row.Volume:n2}"
                                    : string.Empty,

                            //H
                            row.RowType == (byte)CalculationRowType.BuildingCounter
                                ? $"{row.CurrentValue:n2}"
                                : row.RowType == (byte)CalculationRowType.Customer
                                    && row.Recalculation.HasValue
                                        ? $"{row.Recalculation:n2}"
                                        : string.Empty,

                            //I
                            string.Empty,

                            //J
                            row.RowType == (byte)CalculationRowType.BuildingCounter
                                ? $"{row.CurrentValue:n2}"
                                : string.Empty,

                            //K
                            string.Empty,

                            //L
                            string.Empty,

                            //M
                            row.RowType == (byte)CalculationRowType.LegalEntity
                                && row.ChargedVolume.HasValue
                                    ? $"{row.ChargedVolume:n2}"
                                    : string.Empty,

                            //N
                            string.Empty,

                            //O
                            row.RowType == (byte)CalculationRowType.Customer
                                && row.Square.HasValue
                                    ? $"{row.Square:n2}"
                                    : row.RowType == (byte)CalculationRowType.LegalEntity
                                        && row.EntitySquare.HasValue
                                            ? $"{row.EntitySquare:n2}"
                                            : string.Empty,

                            //P
                            row.RowType == (byte)CalculationRowType.Customer
                                && row.Residents.HasValue
                                    ? $"{row.Residents:n0}"
                                    : string.Empty,

                            row.ProcessingResult == (byte)RowProcessingResult.Exception
                                ? "Программная ошибка"
                                : row.ProcessingResult == (byte)RowProcessingResult.Error
                                    ? "Ошибка"
                                    : row.ProcessingResult == (byte)RowProcessingResult.Skipped
                                        ? "Пропущена"
                                        : row.ProcessingResult == (byte)RowProcessingResult.OK
                                            ? "ОК"
                                            : string.Empty,

                            row.ProcessingResult == (byte)RowProcessingResult.Exception
                                ? $"Программная ошибка.\r\n\r\nПодробная техническая информация: {row.ExceptionMessage}"
                                : row.ProcessingResult == (byte)RowProcessingResult.Error
                                    ? row.ErrorDescription
                                    : row.ProcessingResult == (byte)RowProcessingResult.Skipped
                                        ? "Строка пропущена по правилам формата."
                                        : row.ProcessingResult == (byte)RowProcessingResult.OK
                                            ? "ОК"
                                            : string.Empty);
                    }

                    View.Rows = table;
                }
            }
            else
            {
                View.Rows = new DataTable();
            }
        }
    }
}