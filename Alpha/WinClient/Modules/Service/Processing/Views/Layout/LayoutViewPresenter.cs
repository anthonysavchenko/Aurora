using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Processing.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Processing.Services.Parser.FillForm;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Processing.Services.Parser.Models;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Processing.Services.Parser.PrintForm;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Processing.Views.Layout;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Processing.Layout
{
    public class LayoutViewPresenter : BaseLayoutViewPresenter<ILayoutView>
    {
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
        /// Выполнеяет переименование.
        /// </summary>
        public void Rename()
        {
            try
            {
                string sourceDirectoryPath = View.DirectoryPathForRename;
                string targetDirectoryPath = string.Empty;

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
                        View.Result = $"Найдено файлов в формате Excel 97-2003 с расширением .xls: {files.Length}.";

                        try
                        {
                            targetDirectoryPath = Path.Combine(sourceDirectoryPath, "Переименованные");

                            if (!Directory.Exists(targetDirectoryPath))
                            {
                                Directory.CreateDirectory(targetDirectoryPath);

                                View.Result = $"Создана новая папка для переименнованных файлов {targetDirectoryPath}.";
                            }
                        }
                        catch (Exception e)
                        {
                            View.Result = $"Ошибка при создании новой папки для переименнованных файлов. Возможно, не хватает прав доступа." +
                                $"Попробуйте создать папку \"Переименованные\" вручную.";
                            Logger.SimpleWrite($"Processing error: {e}");
                        }

                        if (Directory.Exists(targetDirectoryPath))
                        {
                            for (int i = 0; i < files.Length; i++)
                            {
                                View.Result = $"\r\n{i + 1}. Обработка файла {files[i]}.";

                                string building = string.Empty;
                                string form1_for_filling_address = string.Empty;
                                string form2_for_printing_address = string.Empty;
                                string address = string.Empty;
                                Excel2007Worker worker = new Excel2007Worker();

                                try
                                {
                                    worker.OpenFile(files[i]);
                                    Excel2007Worker.ExcelSheet sheet = worker.GetSheet(1);
                                    int rowsCount = sheet.RowsCount;
                                    form1_for_filling_address = rowsCount > 0 ? sheet.GetCellText("E1") : string.Empty;
                                    form2_for_printing_address = rowsCount > 13 ? sheet.GetCellText("C14") : string.Empty;
                                    worker.Close();

                                    View.Result = $"Прочитаны ячейки E1 = \"{form1_for_filling_address}\" и С14 = \"{form2_for_printing_address}\".";
                                }
                                catch (Exception e)
                                {
                                    View.Result = $"Ошибка при открытии файла Excel. Убедитесь, что на компьютере установлен Excel, и файл " +
                                        $"не открыт в другом окне или другой программе.";
                                    Logger.SimpleWrite($"Processing error: {e}");
                                    worker.Close();
                                    continue;
                                }

                                if (form1_for_filling_address.StartsWith("г. Владивосток, "))
                                {
                                    address = form1_for_filling_address;
                                    View.Result = $"Найден адрес первой квартиры \"{address}\".";
                                }
                                else if (form2_for_printing_address.StartsWith("г. Владивосток, "))
                                {
                                    address = form2_for_printing_address;
                                    View.Result = $"Найден адрес первой квартиры \"{address}\".";
                                }
                                else
                                {
                                    View.Result = "Ошибка определения формата файла, убедитесь что в ячейках E1 или C14 файла записан адрес первой " +
                                        "квартиры.";
                                    continue;
                                }

                                try
                                {
                                    address = address.Replace("г. Владивосток, ", string.Empty);
                                    int buildingNumberStartIndex = address.IndexOf(", кв.");
                                    building = address.Remove(buildingNumberStartIndex);

                                    View.Result = $"Найден адрес дома \"{building}\".";
                                }
                                catch (Exception e)
                                {
                                    View.Result = $"Ошибка при определении адреса дома, убедитесь что в ячейках E1 или C14 файла записан адрес первой " +
                                        $"квартиры.";
                                    Logger.SimpleWrite($"Processing error: {e}");
                                    continue;
                                }

                                try
                                {
                                    string newFilePath = 
                                        !string.IsNullOrEmpty(form1_for_filling_address)
                                            ? Path.Combine(
                                                targetDirectoryPath,
                                                Path.GetFileNameWithoutExtension(files[i]) + " " + building + Path.GetExtension(files[i]))
                                            : Path.Combine(
                                                targetDirectoryPath,
                                                building + " (" + Path.GetFileNameWithoutExtension(files[i]) + ")" + Path.GetExtension(files[i]));

                                    if (!File.Exists(newFilePath))
                                    {
                                        File.Copy(files[i], newFilePath);

                                        View.Result = $"Файл переименован в {newFilePath}.";
                                    }
                                    else
                                    {
                                        View.Result = $"Файл уже был переименован ранее в {newFilePath}.";
                                    }
                                }
                                catch (Exception e)
                                {
                                    View.Result = $"Ошибка при переименовании файла. Возможно, не хватает прав для копирования файлов, попробуйте " +
                                        $"переместить файлы в другую папку и запустить программу от имени администратора.";
                                    Logger.SimpleWrite($"Processing error: {e}");
                                    continue;
                                }
                            }
                        }
                    }
                }

                View.Result = $"\r\nПереименование завершено.\r\n";
            }
            catch (Exception e)
            {
                Logger.SimpleWrite($"Rename error: {e}");
                View.Result = $"\r\nПроизошла ошибка. Переименование не выполнено. Подробности: {e}\r\n";
            }
        }

        /// <summary>
        /// Выполняет анализ файлов в директории.
        /// </summary>
        public void Analyze()
        {
            try
            {
                string sourceDirectoryPath = View.DirectoryPathForAnalyze;
                string targetDirectoryPath = string.Empty;

                List<Building> printForms = new List<Building>();
                List<Building> fillForms = new List<Building>();

                if (!Directory.Exists(sourceDirectoryPath))
                {
                    View.Result = $"Некорректно указана папка с файлами. Такой папки не существует.";
                }
                else
                {
                    View.Result = $"Поиск и распознавание файлов по адресу {sourceDirectoryPath}.";

                    string[] files = Directory.GetFiles(sourceDirectoryPath, "*.xls", SearchOption.TopDirectoryOnly);

                    if (files.Length < 1)
                    {
                        View.Result = $"В указанной папке не найдено ни одного файла в формате Excel 97-2003 с расширением .xls.";
                    }
                    else
                    {
                        View.Result = $"Найдено файлов в формате Excel 97-2003 с расширением .xls: {files.Length}.";

                        for (int i = 0; i < files.Length; i++)
                        {
                            View.Result = $"\r\n{i + 1}. Распознавание файла {files[i]}.";

                            Excel2007Worker worker = new Excel2007Worker();
                            Excel2007Worker.ExcelSheet sheet;

                            try
                            {
                                worker.OpenFile(files[i]);
                                sheet = worker.GetSheet(1);

                                if (sheet.RowsCount >= PrintForm.FIRST_LINE
                                    && PrintForm.ParseLine(sheet, PrintForm.FIRST_LINE, out Customer printFormFirstLine, out _))
                                {
                                    View.Result = $"Определен формат файла: \"Маршрутный лист\".";

                                    if (PrintForm.ParseFile(sheet, out List<Customer> customers, out string message))
                                    {
                                        printForms.Add(
                                            new Building()
                                            {
                                                Number = printFormFirstLine.Address.Building,
                                                Customers = customers,
                                            });
                                    }

                                    View.Result = message;
                                }
                                else if (sheet.RowsCount >= FillForm.FIRST_LINE
                                    && FillForm.ParseLine(sheet, FillForm.FIRST_LINE, out Customer fillFormFirstLine, out _))
                                {
                                    View.Result = $"Определен формат файла: \"Форма для заполнения показаний ПУ\".";

                                    if (FillForm.ParseFile(sheet, out List<Customer> customers, out string message))
                                    {
                                        fillForms.Add(
                                            new Building()
                                            {
                                                Number = fillFormFirstLine.Address.Building,
                                                Customers = customers,
                                            });
                                    }

                                    View.Result = message;
                                }
                                else
                                {
                                    View.Result = $"Формат файла не определен.";
                                }
                            }
                            catch (Exception e)
                            {
                                View.Result = $"Внутренняя ошибка при распознавании файла. Убедитесь, что на компьютере установлен Excel, и файл не открыт " +
                                    $"в другом окне или другой программе. А также, что файл составлен в правильном формате.";
                                Logger.SimpleWrite($"Analyze error: {e}");
                            }
                            finally
                            {
                                worker.Close();
                            }
                        }

                        View.Result = $"\r\nАнализ файлов.";

                        View.Result = $"\r\nВсего найдено файлов: {files.Length}.";
                        View.Result = $"В формате \"Маршрутный лист\": {printForms.Count}.";
                        View.Result = $"В формате \"Форма для заполнения показаний ПУ\": {fillForms.Count}.";
                        View.Result = $"Не распознано: {files.Length - printForms.Count - fillForms.Count}.";
                        //View.Result = $"Повторяющиеся дома:";

                        View.Result = $"\r\nДанные в формате \"Маршрутный лист\", которые отличаются от данных в формате \"Форма для заполнения показаний ПУ\":";

                        int diffNumber = 0;
                        for (int i = 0; i < printForms.Count; i++)
                        {
                            Building printForm = printForms[i];

                            View.Result = $"\r\n{i + 1}. {printForm.Number}.";

                            Building fillForm = fillForms.FirstOrDefault(f => f.Number.ToUpper() == printForm.Number.ToUpper());

                            if (fillForm != null)
                            {
                                bool dif1 = false;

                                foreach (Customer printFormCustomer in printForm.Customers)
                                {
                                    Customer fillFormCustomer =
                                        fillForm.Customers.FirstOrDefault(c => c.Address.Apartment == printFormCustomer.Address.Apartment);

                                    if (fillFormCustomer != null)
                                    {
                                        if (printFormCustomer.PrevDate != fillFormCustomer.PrevDate)
                                        {
                                            View.Result = $"{++diffNumber}. Кв. {printFormCustomer.Address.Apartment}. Различные значения дат в столбце \"Дата предыдущих показаний\" " +
                                                $"({printFormCustomer.PrevDate.ToString("dd.MM.yyyy")} и {fillFormCustomer.PrevDate.ToString("dd.MM.yyyy")})";
                                            dif1 = true;
                                        }

                                        if (printFormCustomer.Counter is SingleCounter)
                                        {
                                            if (fillFormCustomer.Counter is DoubleCounter)
                                            {
                                                View.Result = $"{++diffNumber}. Кв. {printFormCustomer.Address.Apartment}. Различные форматы значений в столбце \"Предыдущие показания\" (однотарифный счетчик и двухтарифный счетчик)";
                                                dif1 = true;
                                            }
                                            else if ((printFormCustomer.Counter as SingleCounter).prevValue != (fillFormCustomer.Counter as SingleCounter).prevValue)
                                            {
                                                View.Result = $"{++diffNumber}. Кв. {printFormCustomer.Address.Apartment}. Различные значения показаний в столбце \"Предыдущие показания\" " +
                                                    $"({(printFormCustomer.Counter as SingleCounter).prevValue} и {(fillFormCustomer.Counter as SingleCounter).prevValue})";
                                                dif1 = true;
                                            }
                                        }
                                        else
                                        {
                                            if (fillFormCustomer.Counter is SingleCounter)
                                            {
                                                View.Result = $"{++diffNumber}. Кв. {printFormCustomer.Address.Apartment}. Различные форматы значений в столбце \"Предыдущие показания\" (двухтарифный счетчик и однотарифный счетчик)";
                                                dif1 = true;
                                            }
                                            else
                                            {
                                                if ((printFormCustomer.Counter as DoubleCounter).PrevDayValue != (fillFormCustomer.Counter as DoubleCounter).PrevDayValue)
                                                {
                                                    View.Result = $"{++diffNumber}. Кв. {printFormCustomer.Address.Apartment}. Различные значения дневных показаний в столбце \"Предыдущие показания\" " +
                                                        $"({(printFormCustomer.Counter as DoubleCounter).PrevDayValue} и {(fillFormCustomer.Counter as DoubleCounter).PrevDayValue})";
                                                    dif1 = true;
                                                }

                                                if ((printFormCustomer.Counter as DoubleCounter).PrevNightValue != (fillFormCustomer.Counter as DoubleCounter).PrevNightValue)
                                                {
                                                    View.Result = $"{++diffNumber}. Кв. {printFormCustomer.Address.Apartment}. Различные значения ночных показаний в столбце \"Предыдущие показания\" " +
                                                        $"({(printFormCustomer.Counter as DoubleCounter).PrevNightValue} и {(fillFormCustomer.Counter as DoubleCounter).PrevNightValue})";
                                                    dif1 = true;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        View.Result = $"{++diffNumber}. Кв. {printFormCustomer.Address.Apartment}. Нет соответствующей квартиры в файле в формате " +
                                            $"\"Форма для заполнения показаний ПУ\".";
                                        dif1 = true;
                                    }
                                }

                                if (!dif1)
                                {
                                    View.Result = $"Отличий не обнаружено.";
                                }
                            }
                            else
                            {
                                View.Result = $"Нет соответствующего файла в формате \"Форма для заполнения показаний ПУ\".";
                            }
                        }

                        View.Result = $"\r\nФайлы в формате \"Форма для заполнения показаний ПУ\", для которых отсутствуют файлы в формате \"Маршрутный лист\":";

                        bool dif2 = false;

                        for (int j = 0; j < fillForms.Count; j++)
                        {
                            Building fillForm = fillForms[j];

                            Building printForm = printForms.FirstOrDefault(p => p.Number.ToUpper() == fillForm.Number.ToUpper());

                            if (printForm == null)
                            {
                                View.Result = fillForm.Number;
                                dif2 = true;
                            }
                        }

                        if (!dif2)
                        {
                            View.Result = $"Не обнаружено.";
                        }
                    }
                }

                View.Result = $"\r\nАнализ файлов завершен.\r\n";
            }
            catch (Exception e)
            {
                Logger.SimpleWrite($"Analyze error: {e}");
                View.Result = $"\r\nВнутренняя ошибка. Анализ файлов не выполнен. Подробности: {e}\r\n";
            }
        }
    }
}
