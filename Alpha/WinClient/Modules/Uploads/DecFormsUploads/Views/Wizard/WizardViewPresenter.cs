using DevExpress.XtraWizard;
using System.IO;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.Infrastructure.Library.Services.DecFormsUploader;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.DecFormsUploads.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.DecFormsUploads.Views.Tabbed;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.DecFormsUploads.Views.Wizard
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

            View.DirectoryName = string.Empty;
            View.Month = ServerTime.GetDateTimeInfo().Now;
            View.Note = string.Empty;

            View.RouteForms = 0;
            View.FillForms = 0;
            View.UnknownFiles = 0;
            View.Errors = 0;

            View.SetInitialProgress("Поготовка к началу оработки данных...");

            View.SelectPage(WizardSteps.ChooseDirectoryPage);
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
                    case "ChooseDirectoryWizardPage":
                        {
                            if (string.IsNullOrEmpty(View.DirectoryName))
                            {
                                View.ShowMessage("Укажите папку для загрузки файлов.", "Ошибка выбора папки");
                                _next = WizardSteps.Unknown;
                            }
                            else if (!Directory.Exists(View.DirectoryName))
                            {
                                View.ShowMessage(
                                    "Некорректно указана папка с файлами. Такой папки не существует.",
                                    "Ошибка выбора папки");
                                _next = WizardSteps.Unknown;
                            }
                            else if (Directory.GetFiles(
                                View.DirectoryName,
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
                        _next = WizardSteps.ChooseDirectoryPage;
                        break;
                    case "FinishWizardPage":
                        _next = WizardSteps.ChooseDirectoryPage;
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
                                case "ChooseDirectoryWizardPage":
                                    {
                                        UploadFiles();
                                    }
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        private void UploadFiles()
        {
            View.IsMasterInProgress = true;

            Uploader.UploadAsync(
                View.DirectoryName,
                int.Parse(UserHolder.User.ID),
                View.Month,
                View.Note,
                OnProgress: (int percents, string jobName) => View.SetProgress(jobName, percents),
                OnCompleted: (DataBase.DecFormsUploads upload) =>
                {
                    if (upload == null)
                    {
                        View.RouteForms = 0;
                        View.FillForms = 0;
                        View.UnknownFiles = 0;
                        View.Errors = 1;
                        View.ShowMessage(
                            "Проверьте подключение к локальной сети УК ФР и серверу БД.",
                            "Ошибка при подготовке к началу обработки данных");
                    }
                    else
                    {
                        View.RouteForms =
                            upload
                                .DecFormsUploadPoses.Count(p =>
                                    (DecFormsType)p.FormType == DecFormsType.RouteForm
                                    && string.IsNullOrEmpty(p.ErrorDescription));

                        View.FillForms =
                            upload
                                .DecFormsUploadPoses.Count(p =>
                                    (DecFormsType)p.FormType == DecFormsType.FillForm
                                    && string.IsNullOrEmpty(p.ErrorDescription));

                        View.UnknownFiles =
                            upload
                                .DecFormsUploadPoses.Count(p =>
                                    (DecFormsType)p.FormType == DecFormsType.Unknown);

                        View.Errors =
                            (!string.IsNullOrEmpty(upload.ErrorDescription) ? 1 : 0) +
                                upload
                                    .DecFormsUploadPoses.Count(p => 
                                        !string.IsNullOrEmpty(p.ErrorDescription)
                                        && (DecFormsType)p.FormType != DecFormsType.Unknown);
                    }

                    View.IsMasterInProgress = false;
                    View.IsMasterCompleted = true;

                    View.SelectPage(WizardSteps.FinishPage);
                });
        }
    }
}
