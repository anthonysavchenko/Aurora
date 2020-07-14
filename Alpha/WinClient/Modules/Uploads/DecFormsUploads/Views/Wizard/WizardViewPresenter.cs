using DevExpress.XtraWizard;
using System.IO;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.Infrastructure.Library.Services.DecFormsParser;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.DecFormsUploads.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.DecFormsUploads.Views.Tabbed;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.DecFormsUploads.Views.Wizard
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

            View.DirectoryName = string.Empty;
            View.Month = ServerTime.GetDateTimeInfo().Now;
            View.Note = string.Empty;

            View.RouteForms = 0;
            View.FillForms = 0;
            View.UnknownFiles = 0;
            View.Exceptions = 0;

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
                            if (string.IsNullOrEmpty(View.DirectoryName))
                            {
                                View.ShowMessage("������� ����� ��� �������� ������.", "������ ������ �����");
                                _next = WizardSteps.Unknown;
                            }
                            else if (!Directory.Exists(View.DirectoryName))
                            {
                                View.ShowMessage(
                                    "����������� ������� ����� � �������. ����� ����� �� ����������.",
                                    "������ ������ �����");
                                _next = WizardSteps.Unknown;
                            }
                            else if (Directory.GetFiles(
                                View.DirectoryName,
                                "*.xls",
                                SearchOption.TopDirectoryOnly).Length < 1)
                            {
                                View.ShowMessage(
                                    "� ��������� ����� �� ������� �� ������ ����� � ������� Excel 97-2003 � " +
                                        "����������� .xls.",
                                    "������ ������ �����");
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
                                        SaveProcessingData();
                                    }
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// ��������� ��������� ������
        /// </summary>
        private void SaveProcessingData()
        {
            View.IsMasterInProgress = true;
            View.SetInitialProgress("��������� � ������ ������������� ������...");

            DirectoryParser.ParseDirectoryAsync(
                View.DirectoryName,
                CreateUpload(),
                OnProgress: (int percents) => View.SetProgress("������������� ������", percents),
                OnCompleted: (DirectoryParsingResult result) =>
                {
                    View.RouteForms = result.RouteForms;
                    View.FillForms = result.FillForms;
                    View.UnknownFiles = result.UnknownFiles;
                    View.Exceptions = result.Exceptions;

                    View.IsMasterInProgress = false;
                    View.IsMasterCompleted = true;

                    View.SelectPage(WizardSteps.FinishPage);
                });
        }

        private DataBase.DecFormsUploads CreateUpload()
        {
            DataBase.DecFormsUploads upload = new DataBase.DecFormsUploads()
            {
                Created = ServerTime.GetDateTimeInfo().Now,
                Month = View.Month,
                Note = View.Note,
            };

            int userID = int.Parse(UserHolder.User.ID);

            using (Entities db = new Entities())
            {
                upload.Author = db.Users.First(u => u.ID == userID);

                db.AddToDecFormsUploads(upload);
                db.SaveChanges();
            }

            return upload;
        }
    }
}
