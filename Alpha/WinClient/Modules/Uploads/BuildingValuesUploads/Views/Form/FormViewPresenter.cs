using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Constants;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Views.Form
{
    public class FormViewPresenter : BaseDomainPresenter<IFormView>
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
                    var rawRows =
                        db.BuildingValuesRows
                            .Where(x => x.BuildingValuesForms.BuildingValuesFiles.ID == id)
                            .Select(x =>
                                new
                                {
                                    x.ID,
                                    x.Street,
                                    x.Building,
                                    x.CounterNumber,
                                    x.CurrentValue,
                                    x.PrevValue,
                                    x.Coefficient,
                                    x.ProcessingResult,
                                    x.ErrorDescription,
                                    x.ExceptionMessage,
                                })
                            .ToList();

                    var rows =
                        rawRows
                            .Select(x =>
                                new
                                {
                                    x.ID,
                                    Building =
                                        !string.IsNullOrEmpty(x.Street) && !string.IsNullOrEmpty(x.Building)
                                            ? $"{x.Street}, д. {x.Building}"
                                            : null,
                                    x.CounterNumber,
                                    x.CurrentValue,
                                    x.PrevValue,
                                    x.Coefficient,
                                    x.ProcessingResult,
                                    x.ErrorDescription,
                                    x.ExceptionMessage,
                                })
                            .OrderBy(x => x.ID)
                            .ToList();

                    DataTable table = new DataTable();

                    table.Columns.Add("ID");
                    table.Columns.Add("Building");
                    table.Columns.Add("Counter");
                    table.Columns.Add("CurrentValue");
                    table.Columns.Add("PrevValue");
                    table.Columns.Add("Coefficient");
                    table.Columns.Add("Result");
                    table.Columns.Add("Description");

                    foreach (var row in rows)
                    {
                        table.Rows.Add(
                            row.ID.ToString(),
                            row.Building,
                            row.CounterNumber,
                            row.CurrentValue,
                            row.PrevValue,
                            row.Coefficient,

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