using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.DecFormsUploads.Views.Wizard
{
    public interface IWizardView : IBaseView
    {
        #region ChooseDirectoryPage

        /// <summary>
        /// Имя файла
        /// </summary>
        string DirectoryName { get; set; }

        /// <summary>
        /// Дата платежа
        /// </summary>
        DateTime Month { get; set; }

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
        /// Итоговое количество Маршрутных листов
        /// </summary>
        int RouteForms { set; }

        /// <summary>
        /// Итоговое количество Форм для заполнения
        /// </summary>
        int FillForms { set; }

        /// <summary>
        /// Файлов в неизвестном формате 
        /// </summary>
        int UnknownFiles { set; }

        /// <summary>
        /// Итоговое количество ошибок в процессе обработки
        /// </summary>
        int Exceptions { set; } 

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
