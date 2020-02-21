using System;
using System.Collections.Generic;
using System.Data;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Models;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.FillFormAndPrintFormDiff.DataSets;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.FillFormAndPrintFormDiff.Views.Report.Queries
{
    public static class GetReportDataQuery
    {
        public static PrintFormAndFillFormDiffsDataSet GetReportData(this Entities db, List<Diff> diffs)
        {
            var _data = new PrintFormAndFillFormDiffsDataSet();

            DataTable _headerTable = _data.Tables["HeaderTable"];
            DataTable _detailTable = _data.Tables["DetailTable"];

            _headerTable.Rows.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm"));

            string building = string.Empty;
            string apartment = string.Empty;
            string description = string.Empty;
            string printFormValue = string.Empty;
            string fillFormValue = string.Empty;

            foreach (Diff diff in diffs)
            {
                if (diff is CustomerDiff customerDiff)
                {
                    building =
                        customerDiff.diffType == CustomerDiffType.NoPrintFormCustomer
                            ? customerDiff.FillFormCustomer.Address.Building
                            : customerDiff.PrintFormCustomer.Address.Building;

                    apartment =
                        string.Format(
                            "Кв. {0}",
                            customerDiff.diffType == CustomerDiffType.NoPrintFormCustomer
                                ? customerDiff.FillFormCustomer.Address.Apartment
                                : customerDiff.PrintFormCustomer.Address.Apartment);

                    switch (customerDiff.diffType)
                    {
                        case CustomerDiffType.NoFillFormCustomer:
                            description = "Нет соответствующей квартиры";
                            printFormValue = "Есть данные";
                            fillFormValue = "Нет данных";
                            break;
                        case CustomerDiffType.NoPrintFormCustomer:
                            description = "Нет соответствующей квартиры";
                            printFormValue = "Нет данных";
                            fillFormValue = "Есть данные";
                            break;
                        case CustomerDiffType.NormAndCounter:
                            description = "Различаются способы начисления";
                            printFormValue = "По нормативу";
                            fillFormValue = "По счетчику";
                            break;
                        case CustomerDiffType.SingleAndDoubleCounter:
                            description = "Различаются типы счетчиков";
                            printFormValue = "Однотарифный";
                            fillFormValue = "Духтарифный";
                            break;
                        case CustomerDiffType.DoubleAndSingleCounter:
                            description = "Различаются типы счетчиков";
                            printFormValue = "Духтарифный";
                            fillFormValue = "Однотарифный";
                            break;
                        case CustomerDiffType.PrevDateTime:
                            description = "Различаются даты предыдущих показаний";
                            printFormValue = ((DateTime)customerDiff.PrintFormCustomer.PrevDate)
                                .ToString("dd.MM.yyyy");
                            fillFormValue = ((DateTime)customerDiff.FillFormCustomer.PrevDate)
                                .ToString("dd.MM.yyyy");
                            break;
                        case CustomerDiffType.SingleCounterValue:
                            description = "Различаются предыдущие показания однотарифного счетчика";
                            printFormValue =
                                (customerDiff.PrintFormCustomer.Counter as SingleCounter).prevValue.ToString();
                            fillFormValue =
                                (customerDiff.FillFormCustomer.Counter as SingleCounter).prevValue.ToString();
                            break;
                        case CustomerDiffType.DoubleCounterDayValue:
                            description = "Различаются предыдущие дневные показания двухтарифного счетчика";
                            printFormValue =
                                (customerDiff.PrintFormCustomer.Counter as DoubleCounter).PrevDayValue.ToString();
                            fillFormValue =
                                (customerDiff.FillFormCustomer.Counter as DoubleCounter).PrevDayValue.ToString();
                            break;
                        case CustomerDiffType.DoubleCounterNightValue:
                            description = "Различаются предыдущие ночные показания двухтарифного счетчика";
                            printFormValue =
                                (customerDiff.PrintFormCustomer.Counter as DoubleCounter).PrevNightValue.ToString();
                            fillFormValue =
                                (customerDiff.FillFormCustomer.Counter as DoubleCounter).PrevNightValue.ToString();
                            break;
                    }
                }
                else if (diff is BuildingDiff buildingDiff)
                {
                    apartment = string.Empty;

                    switch (buildingDiff.diffType)
                    {
                        case BuildingDiffType.NoFillForm:
                            building = buildingDiff.PrintFormBuilding.Number;
                            description = "Нет соответствующего файла";
                            printFormValue = "Есть данные";
                            fillFormValue = "Нет данных";
                            break;
                        case BuildingDiffType.NoPrintForm:
                            building = buildingDiff.FillFormBuilding.Number;
                            description = "Нет соответствующего файла";
                            printFormValue = "Нет данных";
                            fillFormValue = "Есть данные";
                            break;
                    }
                }

                _detailTable.Rows.Add(building, apartment, description, printFormValue, fillFormValue);
            }

            return _data;
        }
    }
}
