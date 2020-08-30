using DevExpress.XtraWizard;
using System.IO;
using System.Linq;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Views.Tabbed;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Views.Wizard
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

            View.FilePath = string.Empty;
            View.Month = ServerTime.GetDateTimeInfo().Now;
            View.Note = string.Empty;

            View.BuildingValues = 0;
            View.Errors = 0;

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
                            if (string.IsNullOrEmpty(View.FilePath))
                            {
                                View.ShowMessage("Выберите файл для загрузки.", "Ошибка выбора файла");
                                _next = WizardSteps.Unknown;
                            }
                            else if (!File.Exists(View.FilePath))
                            {
                                View.ShowMessage(
                                    "Некорректно указан файл. Такого файла не существует.",
                                    "Ошибка выбора файла");
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
                View.FilePath,
                int.Parse(UserHolder.User.ID),
                View.Month,
                View.Note,
                OnProgress: (int percents, string jobName) => View.SetProgress(jobName, percents),
                OnCompleted: (DataBase.BuildingValuesUploads upload) =>
                {
                    if (upload == null)
                    {
                        View.BuildingValues = 0;
                        View.Errors = 1;
                        View.ShowMessage(
                            "Проверьте подключение к локальной сети УК ФР и серверу БД.",
                            "Ошибка при подготовке к началу обработки данных");
                    }
                    else
                    {
                        View.BuildingValues =
                            upload
                                .BuildingValuesUploadPoses
                                .Count(p => string.IsNullOrEmpty(p.ErrorDescription));

                        View.Errors =
                            (!string.IsNullOrEmpty(upload.ErrorDescription) ? 1 : 0) +
                                upload
                                    .BuildingValuesUploadPoses
                                    .Count(p => !string.IsNullOrEmpty(p.ErrorDescription));
                    }

                    View.IsMasterInProgress = false;
                    View.IsMasterCompleted = true;

                    View.SelectPage(WizardSteps.FinishPage);
                });
        }
    }
}
