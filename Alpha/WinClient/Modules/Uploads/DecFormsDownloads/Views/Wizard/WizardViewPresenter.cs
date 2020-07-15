using DevExpress.XtraWizard;
using System.IO;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.Infrastructure.Library.Services.DecFormsDownloader;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.DecFormsDownloads.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.DecFormsDownloads.Views.Tabbed;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.DecFormsDownloads.Views.Wizard
{
    /// <summary>
    /// ���������
    /// </summary>
    public class WizardViewPresenter : BasePresenter<IWizardView>
    {
        /// <summary>
        /// ��������� ������ �������
        /// </summary>
        internal void FinishWizard()
        {
            IBaseListView _view = (IBaseListView)WorkItem.SmartParts.Get(ModuleViewNames.LIST_VIEW);
            _view.RefreshList();

            ITabbedView _tabbed = ((ITabbedView)WorkItem.SmartParts.Get(ModuleViewNames.TABBED_VIEW));
            _tabbed.SelectTab("tabList");
        }

        /// <summary>
        /// �������� ������ �������
        /// </summary>
        internal void StartWizard()
        {
            View.IsMasterCompleted = false;
            View.IsMasterInProgress = false;

            View.DirectoryPath = string.Empty;
            View.Note = string.Empty;

            View.Emails = 0;
            View.Files = 0;
            View.Errors = 0;

            View.SetInitialProgress("��������� � ������ �������� ������...");

            View.SelectPage(WizardSteps.ChooseDirectoryPage);
        }

        /// <summary>
        /// ������������ ��������� ���� �������
        /// </summary>
        /// <param name="prevPage">���������� ��������</param>
        /// <param name="page">����������� ��������</param>
        /// <param name="direction">����� / �����</param>
        /// <returns>��������� �������� �������</returns>
        internal WizardSteps OnSelectedPageChanging(BaseWizardPage prevPage, BaseWizardPage page, Direction direction)
        {
            WizardSteps _next = WizardSteps.Unknown;

            if (direction == Direction.Forward)
            {
                switch (prevPage.Name)
                {
                    case "ChooseDirectoryWizardPage":
                        {
                            if (string.IsNullOrEmpty(View.DirectoryPath))
                            {
                                View.ShowMessage(
                                    "������� �����.",
                                    "����� ��� ���������� ������");
                                _next = WizardSteps.Unknown;
                            }
                            else if (View.DirectoryPath.Length > 200)
                            {
                                View.ShowMessage(
                                    "������� ������� ��� � ���� � �����.",
                                    "����� ��� ���������� ������");
                                _next = WizardSteps.Unknown;
                            }
                            else if (!Directory.Exists(View.DirectoryPath))
                            {
                                View.ShowMessage(
                                    "��������� ����� �� ����������.",
                                    "����� ��� ���������� ������");
                                _next = WizardSteps.Unknown;
                            }
                            else if (Directory.GetFileSystemEntries(View.DirectoryPath).Length > 0
                                && !View.IsOk(
                                    "��������� ����� �� �����. �� �������, ��� ������ ������� ����� � ���?",
                                    "����� ��� ���������� ������"))
                            {
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
        /// ������������ ������� �������� �� ����� ��������
        /// </summary>
        /// <param name="page">��������, �� ������� ��� ����������� �������</param>
        /// <param name="prevPage">�������� ����������� ���������</param>
        /// <param name="direction">����� / �����</param>
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
                                        DownloadFiles();
                                    }
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        private void DownloadFiles()
        {
            View.IsMasterInProgress = true;
            View.SetInitialProgress("��������� � ������ ���������� ������...");

            Downloader.DownloadAsync(
                View.DirectoryPath,
                int.Parse(UserHolder.User.ID),
                View.Note,
                OnProgress: (int percents) => View.SetProgress("���������� ������ �� ��������� �����", percents),
                OnCompleted: (DataBase.DecFormsDownloads download) =>
                {
                    if (download == null)
                    {
                        View.Emails = 0;
                        View.Files = 0;
                        View.Errors = 1;
                        View.ShowMessage(
                            "��������� ����������� � ��������� ���� �� �� � ������� ��.",
                            "������ ��� ���������� � ��������� ������");
                    }
                    else
                    {
                        View.Emails = download.Emails.Count(e => e.ErrorDescription == null);
                        View.Files = download.Emails.Sum(e => e.Attachments.Count(a => a.ErrorDescription == null));
                        View.Errors =
                            (download.ErrorDescription != null ? 1 : 0) +
                                download.Emails.Count(e => e.ErrorDescription != null) +
                                download.Emails.Sum(e => e.Attachments.Count(a => a.ErrorDescription != null));
                    }

                    View.IsMasterInProgress = false;
                    View.IsMasterCompleted = true;

                    View.SelectPage(WizardSteps.FinishPage);
                });
        }
    }
}
