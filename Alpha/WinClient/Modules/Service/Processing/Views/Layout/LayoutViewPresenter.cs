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
                    View.Result += $"Некорректно указана папка с файлами. Такой папки не существует.\r\n";
                }
                else
                {
                    View.Result += $"Поиск файлов по адресу {sourceDirectoryPath}.\r\n";

                    string[] files = Directory.GetFiles(sourceDirectoryPath, "*.xls", SearchOption.TopDirectoryOnly);

                    if (files.Length < 1)
                    {
                        View.Result += $"В указанной папке не найдено ни одного файла в формате Excel 97-2003 с расширением .xls.\r\n";
                    }
                    else
                    {
                        View.Result += $"Найдено {files.Length} файлов.\r\n";

                        try
                        {
                            targetDirectoryPath = Path.Combine(sourceDirectoryPath, "Переименованные");

                            if (!Directory.Exists(targetDirectoryPath))
                            {
                                Directory.CreateDirectory(targetDirectoryPath);

                                View.Result += $"Создана новая папка для переименнованных файлов.\r\n";
                            }
                        }
                        catch (Exception e)
                        {
                            View.Result += $"Ошибка при создании новой папки для переименнованных файлов. Возможно, не хватает прав доступа." +
                                $"Попробуйте создать папку \"Переименованные\" вручную.\r\n";
                            Logger.SimpleWrite($"Processing error: {e}");
                        }

                        if (Directory.Exists(targetDirectoryPath))
                        {
                            foreach (string filePath in files)
                            {
                                View.Result += $"Обработка файла {filePath}.\r\n";

                                string building = string.Empty;
                                string address = string.Empty;
                                Excel2007Worker worker = new Excel2007Worker();

                                try
                                {
                                    worker.OpenFile(filePath);
                                    Excel2007Worker.ExcelSheet sheet = worker.GetSheet("Лист1");
                                    address = sheet.GetCellText("E1");
                                    worker.Close();

                                    View.Result += $"Найден адрес первой квартиры \"{address}\".\r\n";
                                }
                                catch (Exception e)
                                {
                                    View.Result += $"Ошибка при открытии файла Excel. Убедитесь:\r\n - что на компьютере установлен Excel,\r\n - что файл не " +
                                        $"открыт в другой программе,\r\n - что в файле присутствует Лист1,\r\n - что в ячейке E1 файла записан адрес первой " +
                                        $"квартиры.\r\n";
                                    Logger.SimpleWrite($"Processing error: {e}");
                                    worker.Close();
                                    continue;
                                }

                                try
                                {
                                    address = address.Replace("г. Владивосток, ", string.Empty);
                                    int buildingNumberStartIndex = address.IndexOf(", кв.");
                                    building = address.Remove(buildingNumberStartIndex);

                                    View.Result += $"Найден адрес дома \"{building}\".\r\n";
                                }
                                catch (Exception e)
                                {
                                    View.Result += $"Ошибка при поиске адреса дома.\r\n";
                                    Logger.SimpleWrite($"Processing error: {e}");
                                    continue;
                                }

                                try
                                {
                                    string newFilePath = Path.Combine(
                                        targetDirectoryPath,
                                        building + " " + Path.GetFileNameWithoutExtension(filePath) + Path.GetExtension(filePath));

                                    if (!File.Exists(newFilePath))
                                    {
                                        File.Copy(filePath, newFilePath);

                                        View.Result += $"Файл переименован в {newFilePath}.\r\n";
                                    }
                                    else
                                    {
                                        View.Result += $"Файл уже был переименован ранее в {newFilePath}.\r\n";
                                    }
                                }
                                catch (Exception e)
                                {
                                    View.Result += $"Ошибка при переименовании файла.\r\n";
                                    Logger.SimpleWrite($"Processing error: {e}");
                                    continue;
                                }
                            }
                        }
                    }
                }

                View.Result += $"Обработка завершена.\r\n\r\n";
            }
            catch (Exception e)
            {
                Logger.SimpleWrite($"Processing error: {e}");
                View.Result += $"Произошла ошибка. Обработка не выполнена. Подробности: {e}\r\n\r\n";
            }
        }
    }
}