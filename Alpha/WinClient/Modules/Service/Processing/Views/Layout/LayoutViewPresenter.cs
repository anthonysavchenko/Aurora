using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using System;
using System.Collections.Generic;
using System.IO;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Processing.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Processing.Views.Layout;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Processing.Layout
{
    public class LayoutViewPresenter : BaseLayoutViewPresenter<ILayoutView>
    {
        public class Month
        {
            public string N;
            public string T;
            public string AN;
            public string BP;
            public string BU;
        }

        public class Building
        {
            public string address;

            public Dictionary<DateTime, Month> months;

            public Building()
            {
                months = new Dictionary<DateTime, Month>();
            }
        }

        /// <summary>
        /// Выполняет действия при загрузке вида.
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();

            RefreshView();
        }

        /// <summary>
        /// Выполняет действия при активации юз-кейса.
        /// </summary>
        public override void ActivateUseCase()
        {
            // Все кнопки панели инструментов всегда не активны.
        }

        /// <summary>
        /// Выполняет действия при глобальной команде "Обновить".
        /// </summary>
        [EventSubscription(CommonEventNames.RefreshItemFired, ThreadOption.UserInterface)]
        public void OnRefreshItemFired(object sender, EventArgs eventArgs)
        {
            // Если текущий юзкейс не активен - 
            // глобальные команды обрабатывать не нужно.
            if (WorkItem.Status == WorkItemStatus.Inactive) return;

            RefreshView();
        }

        /// <summary>
        /// Обновляет вид.
        /// </summary>
        private void RefreshView()
        {
            View.ClearView();
        }

        /// <summary>
        /// Выполнеяет обработку.
        /// </summary>
        public void Process()
        {
            try
            {
                string sourceDirectoryPath = View.DirectoryPath;
                string targetDirectoryPath = string.Empty;
                var buildings = new List<Building>();
                var months = new Dictionary<DateTime, string>
                {
                    { new DateTime(2017, 1, 1), "D" },
                    { new DateTime(2017, 2, 1), "E" },
                    { new DateTime(2017, 3, 1), "F" },
                    { new DateTime(2017, 4, 1), "G" },
                    { new DateTime(2017, 5, 1), "H" },
                    { new DateTime(2017, 6, 1), "I" },
                    { new DateTime(2017, 7, 1), "J" },
                    { new DateTime(2017, 8, 1), "K" },
                    { new DateTime(2017, 9, 1), "L" },
                    { new DateTime(2017, 10, 1), "M" },
                    { new DateTime(2017, 11, 1), "N" },
                    { new DateTime(2017, 12, 1), "O" },
                    { new DateTime(2018, 1, 1), "P" },
                    { new DateTime(2018, 2, 1), "Q" },
                    { new DateTime(2018, 3, 1), "R" },
                    { new DateTime(2018, 4, 1), "S" },
                    { new DateTime(2018, 5, 1), "T" },
                    { new DateTime(2018, 6, 1), "U" },
                    { new DateTime(2018, 7, 1), "V" },
                    { new DateTime(2018, 8, 1), "W" },
                    { new DateTime(2018, 9, 1), "X" },
                    { new DateTime(2018, 10, 1), "Y" },
                    { new DateTime(2018, 11, 1), "Z" },
                    { new DateTime(2018, 12, 1), "AA" }
                };

                if (!Directory.Exists(sourceDirectoryPath))
                {
                    View.Result = $"Некорректно указана папка с файлами. Такой папки не существует.";
                }
                else
                {
                    View.Result = $"Поиск файлов по адресу {sourceDirectoryPath}.";

                    string[] files = Directory.GetFiles(sourceDirectoryPath, "*.xls", SearchOption.TopDirectoryOnly);

                    if (files.Length < 1)
                    {
                        View.Result = $"В указанной папке не найдено ни одного файла в формате Excel 97-2003 с расширением .xls.";
                    }
                    else
                    {
                        View.Result = $"Найдено {files.Length} файлов.";

                        try
                        {
                            targetDirectoryPath = Path.Combine(sourceDirectoryPath, "Результат");

                            if (!Directory.Exists(targetDirectoryPath))
                            {
                                Directory.CreateDirectory(targetDirectoryPath);

                                View.Result = $"Создана новая папка для файла с результатом {targetDirectoryPath}.";
                            }
                        }
                        catch (Exception e)
                        {
                            View.Result = $"\r\nОшибка при создании новой папки для файла с результатом. Возможно, не хватает прав доступа." +
                                $"Попробуйте создать папку \"Результат\" вручную.";
                            Logger.SimpleWrite($"Processing error: {e}");
                        }

                        if (Directory.Exists(targetDirectoryPath))
                        {
                            Array.Sort(files);

                            for (int i = 0; i < files.Length; i++)
                            {
                                View.Result = $"\r\n{i + 1}. Обработка файла {files[i]}.";

                                DateTime month = DateTime.Parse(Path.GetFileNameWithoutExtension(files[i]));

                                Excel2007Worker worker = new Excel2007Worker();

                                try
                                {
                                    worker.OpenFile(files[i]);
                                    Excel2007Worker.ExcelSheet sheet = worker.GetSheet(1);
                                    int rowsCount = sheet.RowsCount;

                                    for (int j = 8; j < rowsCount; j++)
                                    {
                                        string buildingAddress =
                                            sheet
                                                .GetCellValue($"C{j}")
                                                .Replace(".", "")
                                                .Replace(",", "")
                                                .Replace("-", "")
                                                .Replace(" (бывший 5А)", "")
                                                .ToUpper();

                                        string n = string.Empty;
                                        string t = string.Empty;
                                        string an = string.Empty;
                                        string bp = string.Empty;
                                        string bu = string.Empty;

                                        if (buildingAddress.StartsWith("Г ВЛАДИВОСТОК "))
                                        {
                                            buildingAddress = buildingAddress.Replace("Г ВЛАДИВОСТОК ", "");

                                            n = sheet.GetCellValue($"N{j}");
                                            t = sheet.GetCellValue($"T{j}");
                                            an = sheet.GetCellValue($"AN{j}");
                                            bp = sheet.GetCellValue($"BP{j}");
                                            bu = sheet.GetCellValue($"BU{j}");

                                            Building existedBuilding = buildings.Find(b => b.address == buildingAddress);

                                            if (existedBuilding == null)
                                            {
                                                existedBuilding = new Building();
                                                existedBuilding.address = buildingAddress;
                                                buildings.Add(existedBuilding);
                                            }

                                            existedBuilding.months.Add(month, new Month() { N = n, T = t, AN = an, BP = bp, BU = bu });

                                            View.Result = $"Найден дом и данные: C{j} = \"{buildingAddress}\", N{j} = \"{n}\", T{j} = \"{t}\", " +
                                                $"AN{j} = \"{an}\", BP{j} = \"{bp}\", BU{j} = \"{bu}\".";
                                        }
                                    }

                                    worker.Close();
                                }
                                catch (Exception e)
                                {
                                    View.Result = $"\r\nОшибка при открытии файла Excel. Убедитесь, что на компьютере установлен Excel, и файл " +
                                        $"не открыт в другом окне или другой программе.";
                                    Logger.SimpleWrite($"Processing error: {e}");
                                    worker.Close();
                                    continue;
                                }
                            }

                            Excel2007Worker worker1 = new Excel2007Worker();
                            string currentBuilding = string.Empty;

                            try
                            {
                                buildings.Sort((x, y) => x.address.CompareTo(y.address));

                                Excel2007Worker.ExcelSheet sheet1 = worker1.CreateFile(Path.Combine(targetDirectoryPath, "total.xls"), "Лист1");

                                View.Result = $"\r\nCохранение данных.";

                                sheet1.SetCellValue($"A1", "Адрес");
                                sheet1.SetCellValue($"B1", "ЭЭ наличие ОДПУ / норматив");
                                sheet1.SetCellValue($"C1", "Параметры");

                                sheet1.SetCellValue($"D1", "01.01.2017");
                                sheet1.SetCellValue($"E1", "01.02.2017");
                                sheet1.SetCellValue($"F1", "01.03.2017");
                                sheet1.SetCellValue($"G1", "01.04.2017");
                                sheet1.SetCellValue($"H1", "01.05.2017");
                                sheet1.SetCellValue($"I1", "01.06.2017");

                                sheet1.SetCellValue($"J1", "01.07.2017");
                                sheet1.SetCellValue($"K1", "01.08.2017");
                                sheet1.SetCellValue($"L1", "01.09.2017");
                                sheet1.SetCellValue($"M1", "01.10.2017");
                                sheet1.SetCellValue($"N1", "01.11.2017");
                                sheet1.SetCellValue($"O1", "01.12.2017");

                                sheet1.SetCellValue($"P1", "01.01.2018");
                                sheet1.SetCellValue($"Q1", "01.02.2018");
                                sheet1.SetCellValue($"R1", "01.03.2018");
                                sheet1.SetCellValue($"S1", "01.04.2018");
                                sheet1.SetCellValue($"T1", "01.05.2018");
                                sheet1.SetCellValue($"U1", "01.06.2018");

                                sheet1.SetCellValue($"V1", "01.07.2018");
                                sheet1.SetCellValue($"W1", "01.08.2018");
                                sheet1.SetCellValue($"X1", "01.09.2018");
                                sheet1.SetCellValue($"Y1", "01.10.2018");
                                sheet1.SetCellValue($"Z1", "01.11.2018");
                                sheet1.SetCellValue($"AA1", "01.12.2018");

                                sheet1.SetColumnWidth("A", "A", 40);
                                sheet1.SetColumnWidth("B", "AA", 10);
                                sheet1.SetColumnWidth("C", "C", 110);

                                for (int k = 0; k < buildings.Count; k++)
                                {
                                    currentBuilding = buildings[k].address;

                                    /* 1 */
                                    sheet1.SetCellValue($"A{k * 10 + 2}", buildings[k].address);
                                    sheet1.SetCellValue($"C{k * 10 + 2}", "ОДПУ УК ФР (до 08.2019 данные файла \"Расходы по ОДПУ\")");

                                    /* 2 */
                                    sheet1.SetCellValue($"A{k * 10 + 3}", buildings[k].address);
                                    sheet1.SetCellValue($"C{k * 10 + 3}", "ОДПУ ДЭК (данные файлов ДЭК \"Расчет ОДН месяц\", столбец N)");

                                    foreach (var month in months)
                                    {
                                        if (buildings[k].months.ContainsKey(month.Key)
                                            && !string.IsNullOrEmpty(buildings[k].months[month.Key].N))
                                        {
                                            sheet1.SetCellValue($"{month.Value}{k * 10 + 3}", decimal.Parse(buildings[k].months[month.Key].N));
                                        }
                                    }

                                    /* 3 */
                                    sheet1.SetCellValue($"A{k * 10 + 4}", buildings[k].address);
                                    sheet1.SetCellValue($"C{k * 10 + 4}", "кВт для распределения ДЭК (столбец T, файл \"Расчет ОДН месяц\"), кВт.ч");

                                    foreach (var month in months)
                                    {
                                        if (buildings[k].months.ContainsKey(month.Key)
                                            && !string.IsNullOrEmpty(buildings[k].months[month.Key].T))
                                        {
                                            sheet1.SetCellValue($"{month.Value}{k * 10 + 4}", decimal.Parse(buildings[k].months[month.Key].T));
                                        }
                                    }

                                    /* 4 */
                                    sheet1.SetCellValue($"A{k * 10 + 5}", buildings[k].address);
                                    sheet1.SetCellValue($"C{k * 10 + 5}", "ИПУ УК ФР");

                                    /* 5 */
                                    sheet1.SetCellValue($"A{k * 10 + 6}", buildings[k].address);
                                    sheet1.SetCellValue(
                                        $"C{k * 10 + 6}",
                                        "ИПУ ДЭК (данные файлов ДЭК \"Расчет ОДН месяц\", столбец AM = cумма AK+AL), с 01.10.2017 сумма в столбце AN");

                                    foreach (var month in months)
                                    {
                                        if (buildings[k].months.ContainsKey(month.Key)
                                            && !string.IsNullOrEmpty(buildings[k].months[month.Key].AN))
                                        {
                                            sheet1.SetCellValue($"{month.Value}{k * 10 + 6}", decimal.Parse(buildings[k].months[month.Key].AN));
                                        }
                                    }

                                    /* 6 */
                                    sheet1.SetCellValue($"A{k * 10 + 7}", buildings[k].address);
                                    sheet1.SetCellValue($"C{k * 10 + 7}", "ОДН УК ФР (таблица 6784 для РКЦ месяц)");

                                    /* 7 */
                                    sheet1.SetCellValue($"A{k * 10 + 8}", buildings[k].address);
                                    sheet1.SetCellValue($"C{k * 10 + 8}", "ОДН ДЭК (данные файлов ДЭК, столбец BO \"Расчет ОДН месяц\", столбец BP)");

                                    foreach (var month in months)
                                    {
                                        if (buildings[k].months.ContainsKey(month.Key)
                                            && !string.IsNullOrEmpty(buildings[k].months[month.Key].BP))
                                        {
                                            sheet1.SetCellValue($"{month.Value}{k * 10 + 8}", decimal.Parse(buildings[k].months[month.Key].BP));
                                        }
                                    }

                                    /* 8 */
                                    sheet1.SetCellValue($"A{k * 10 + 9}", buildings[k].address);
                                    sheet1.SetCellValue($"C{k * 10 + 9}", "Баланс по объекту ДЭК (данные файлов ДЭК, столбец BS \"Расчет ОДН месяц\")");

                                    foreach (var month in months)
                                    {
                                        if (buildings[k].months.ContainsKey(month.Key)
                                            && !string.IsNullOrEmpty(buildings[k].months[month.Key].BU))
                                        {
                                            sheet1.SetCellValue($"{month.Value}{k * 10 + 9}", decimal.Parse(buildings[k].months[month.Key].BU));
                                        }
                                    }

                                    /* 9 */
                                    sheet1.SetCellValue($"A{k * 10 + 10}", buildings[k].address);
                                    sheet1.SetCellValue($"C{k * 10 + 10}", "ПРОВЕРКА ОДН ДЭК (разность кВт для распределения/ИПУ ДЭК)");

                                    /* 10 */
                                    /* Пустая */

                                    View.Result = $"{k + 1}. Записан дом: \"{buildings[k].address}\".";
                                }

                                worker1.Save();
                                worker1.Close();
                                View.Result = $"\r\nДанные сохранены в файл с результатом \"{Path.Combine(targetDirectoryPath, "total.xls")}\".";
                            }
                            catch (Exception e)
                            {
                                View.Result = $"\r\nОшибка во время записи данных по дому {currentBuilding}.";
                                Logger.SimpleWrite($"Processing error: {e}");
                                worker1.Close();
                            }
                        }
                    }
                }

                View.Result = $"\r\nОбработка завершена.\r\n";
            }
            catch (Exception e)
            {
                Logger.SimpleWrite($"Processing error: {e}");
                View.Result = $"\r\nПроизошла ошибка. Обработка не выполнена. Подробности: {e}\r\n";
            }
        }
    }
}