using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.CalculationUploads.Views.Wizard
{
    public interface IWizardView : IBaseView
    {
        #region ChoosePathPage

        /// <summary>
        /// Полное имя папки
        /// </summary>
        string DirectoryPath { get; set; }

        /// <summary>
        /// Дата платежа
        /// </summary>
        DateTime Month { get; set; }

        /// <summary>
        /// Использовать черновики
        /// </summary>
        bool UseDrafts { get; set; }

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

        string Result { set; }

        int FilesWithNoErrors { set; }

        int FilesWithErrors { set; }

        int BuildingsWithNoErrors { set; }

        int BuildingsWithErrors { set; }

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
        ChoosePathPage,
        ProcessingPage,
        FinishPage,
        Unknown
    }
}
