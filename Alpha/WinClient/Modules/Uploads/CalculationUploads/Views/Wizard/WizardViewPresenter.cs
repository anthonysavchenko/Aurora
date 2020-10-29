using DevExpress.XtraWizard;
using System.IO;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.CalculationUploads.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.CalculationUploads.Views.Tabbed;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.CalculationUploads.Views.Wizard
{
    /// <summary>
    /// Презентер
    /// </summary>
    public class WizardViewPresenter : BasePresenter<IWizardView>
    {
        /// <summary>
        /// Завершает работу мастера
        /// </summary>
        internal void FinishWizard()
        {
            IBaseListView _view = (IBaseListView)WorkItem.SmartParts.Get(ModuleViewNames.LIST_VIEW);
            _view.RefreshList();

            ITabbedView _tabbed = ((ITabbedView)WorkItem.SmartParts.Get(ModuleViewNames.TABBED_VIEW));
            _tabbed.SelectTab("tabList");
        }

        /// <summary>
        /// Начинает работу мастера
        /// </summary>
        internal void StartWizard()
        {
            View.IsMasterCompleted = false;
            View.IsMasterInProgress = false;

            View.DirectoryPath = string.Empty;
            View.Month = ServerTime.GetDateTimeInfo().Now;
            View.Note = string.Empty;

            View.Result = string.Empty;
            View.FilesWithNoErrors = 0;
            View.FilesWithErrors = 0;
            View.BuildingsWithNoErrors = 0;
            View.BuildingsWithErrors = 0;

            View.SetInitialProgress("Поготовка к началу оработки данных...");

            View.SelectPage(WizardSteps.ChoosePathPage);
        }

        /// <summary>
        /// Обрабатывает изменение шага мастера
        /// </summary>
        /// <param name="prevPage">Предыдущая страница</param>
        /// <param name="page">Открываемая страница</param>
        /// <param name="direction">Назад / Далее</param>
        /// <returns>Следующая страница мастера</returns>
        internal WizardSteps OnSelectedPageChanging(BaseWizardPage prevPage, BaseWizardPage page, Direction direction)
        {
            WizardSteps _next = WizardSteps.Unknown;

            if (direction == Direction.Forward)
            {
                switch (prevPage.Name)
                {
                    case "ChoosePathWizardPage":
                        {
                            if (string.IsNullOrEmpty(View.DirectoryPath))
                            {
                                View.ShowMessage("Укажите папку для загрузки файлов.", "Ошибка выбора папки");
                                _next = WizardSteps.Unknown;
                            }
                            else if (!Directory.Exists(View.DirectoryPath))
                            {
                                View.ShowMessage(
                                    "Некорректно указана папка с файлами. Такой папки не существует.",
                                    "Ошибка выбора папки");
                                _next = WizardSteps.Unknown;
                            }
                            else if (Directory.GetFiles(
                                View.DirectoryPath,
                                "*.xls",
                                SearchOption.TopDirectoryOnly).Length < 1)
                            {
                                View.ShowMessage(
                                    "В указанной папке не найдено ни одного файла в формате Excel 97-2003 с " +
                                        "расширением .xls.",
                                    "Ошибка выбора папки");
                                _next = WizardSteps.Unknown;
                            }
                            else
                            {
                                _next = WizardSteps.ProcessingPage;
                            }
                        }
                        break;

                    case "ProcessingWizardPage":
                        {
                            _next = WizardSteps.FinishPage;
                        }
                        break;
                }
            }
            else
            {
                switch (prevPage.Name)
                {
                    case "ProcessingWizardPage":
                        _next = WizardSteps.ChoosePathPage;
                        break;
                    case "FinishWizardPage":
                        _next = WizardSteps.ChoosePathPage;
                        break;
                }
            }

            return _next;
        }

        /// <summary>
        /// Обрабатывает событие перехода на новую страницу
        /// </summary>
        /// <param name="page">Страница, на которую был осуществлен переход</param>
        /// <param name="prevPage">Страница предыдущего состояния</param>
        /// <param name="direction">Назад / Далее</param>
        internal void OnSelectedPageChanged(BaseWizardPage page, BaseWizardPage prevPage, Direction direction)
        {
            if (direction == Direction.Forward)
            {
                switch (page.Name)
                {
                    case "ProcessingWizardPage":
                        {
                            switch (prevPage.Name)
                            {
                                case "ChoosePathWizardPage":
                                    {
                                        UploadFile();
                                    }
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        private void UploadFile()
        {
            View.IsMasterInProgress = true;

            Uploader.UploadAsync(
                View.DirectoryPath,
                int.Parse(UserHolder.User.ID),
                View.Month,
                View.Note,
                OnProgress: (int percents, string jobName) => View.SetProgress(jobName, percents),
                OnCompleted: (int? uploadID) =>
                {
                    if (!uploadID.HasValue)
                    {
                        View.Result =
                            "Программная ошибка во время загрузки расшифровок при подготовке к " +
                                "началу обработки данных. Проверьте подключение к сети и серверу БД.";
                        View.FilesWithNoErrors = 0;
                        View.FilesWithErrors = 0;
                        View.BuildingsWithNoErrors = 0;
                        View.BuildingsWithErrors = 0;
                    }
                    else
                    {
                        using (var db = new Entities())
                        {
                            var upload =
                                db.CalculationUploads
                                    .First(u => u.ID == uploadID);

                            if (upload.ProcessingResult != (byte)UploadProcessingResult.OK
                                && string.IsNullOrEmpty(upload.ErrorDescription))
                            {
                                View.Result =
                                    "Программная ошибка во время загрузки расшифровок при обработке данных. " +
                                        "Проверьте подключение к сети и серверу БД.";
                            }
                            else if (upload.ProcessingResult != (byte)UploadProcessingResult.OK
                                && !string.IsNullOrEmpty(upload.ErrorDescription))
                            {
                                View.Result = upload.ErrorDescription;
                            }
                            else
                            {
                                View.Result = "OK";
                            }

                            View.FilesWithNoErrors =
                                db.CalculationFiles
                                    .Count(f =>
                                        f.CalculationUploads.ID == uploadID
                                            && f.ProcessingResult == (byte)FileProcessingResult.OK);
                            View.FilesWithErrors =
                                db.CalculationFiles
                                    .Count(f =>
                                        f.CalculationUploads.ID == uploadID
                                            && f.ProcessingResult != (byte)FileProcessingResult.OK);
                            View.BuildingsWithNoErrors =
                                db.CalculationRows
                                    .Count(r =>
                                        r.CalculationForms.CalculationFiles.CalculationUploads.ID == uploadID
                                            && r.CalculationForms.CalculationFiles.ProcessingResult ==
                                                (byte)FileProcessingResult.OK
                                            && r.ProcessingResult == (byte)RowProcessingResult.OK
                                            && r.RowType == (byte)CalculationRowType.BuildingInfo
                                            && r.BuildingInfo.RowType == (byte)BuildingInfoRowType.Address);
                            View.BuildingsWithErrors =
                                db.CalculationRows
                                    .Count(r =>
                                        r.CalculationForms.CalculationFiles.CalculationUploads.ID == uploadID
                                            && r.CalculationForms.CalculationFiles.ProcessingResult ==
                                                (byte)FileProcessingResult.OK
                                            && r.ProcessingResult != (byte)RowProcessingResult.OK
                                            && r.RowType == (byte)CalculationRowType.BuildingInfo
                                            && r.BuildingInfo.RowType == (byte)BuildingInfoRowType.Address);
                        }
                    }

                    View.IsMasterInProgress = false;
                    View.IsMasterCompleted = true;

                    View.SelectPage(WizardSteps.FinishPage);
                });
        }
    }
}
