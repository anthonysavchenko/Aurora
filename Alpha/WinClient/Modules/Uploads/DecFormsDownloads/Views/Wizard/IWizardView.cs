using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.DecFormsDownloads.Views.Wizard
{
    public interface IWizardView : IBaseView
    {
        #region ChooseDirectoryPage

        /// <summary>
        /// Папка для скачивания.
        /// </summary>
        string DirectoryPath { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        string Note { get; set; }

        #endregion

        #region ProcessingPage

        void SetInitialProgress(string label);

        void SetProgress(string label, int value);

        #endregion

        #region FinishPage

        /// <summary>
        /// Итоговое количество проверенных писем.
        /// </summary>
        int Emails { set; }

        /// <summary>
        /// Итоговое количество скачанных файлов.
        /// </summary>
        int Files { set; }

        /// <summary>
        /// Итоговое количество ошибок в процессе обработки.
        /// </summary>
        int Errors { set; } 

        #endregion

        /// <summary>
        /// Начинает работу мастера
        /// </summary>
        void StartWizard();

        /// <summary>
        /// Отображает страницу мастера
        /// </summary>
        /// <param name="page">Шаг</param>
        void SelectPage(WizardSteps page);

        /// <summary>
        /// Признак завершения работы мастера
        /// </summary>
        bool IsMasterCompleted { get; set; }

        /// <summary>
        /// Признак активного процесса обработки
        /// </summary>
        bool IsMasterInProgress { get; set; }
    }

    /// <summary>
    /// Шаги мастера
    /// </summary>
    public enum WizardSteps
    {
        ChooseDirectoryPage,
        ProcessingPage,
        FinishPage,
        Unknown
    }
}
