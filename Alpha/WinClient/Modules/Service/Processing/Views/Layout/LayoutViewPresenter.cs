using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using System;
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