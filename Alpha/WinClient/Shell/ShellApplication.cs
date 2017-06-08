using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.Services;
using Taumis.Alpha.Infrastructure.Library.Services;
using Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook;
using Taumis.Alpha.WinClient.Aurora.Library.Services;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Win;
using Taumis.EnterpriseLibrary.Win.Modules.CommonModule;
using Taumis.EnterpriseLibrary.Win.Services;
using Taumis.Infrastructure.Interface.Services;
using Taumis.Infrastructure.Library;
using Taumis.Infrastructure.Library.Services;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.Infrastructure.Library.Services.Excel.ClosedXML;
using Taumis.Alpha.Infrastructure.Interface.Services.Excel;

namespace Taumis.Infrastructure.Shell
{
    /// <summary>
    /// Класс представляющий собой главную входную точку приложения
    /// </summary>
    class ShellApplication : SmartClientApplication<CommonControlledWorkItem, ShellForm>
    {
        /// <summary>
        /// Application entry point.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if (DEBUG)
            RunInDebugMode();
#else
            RunInReleaseMode();
#endif
        }

        /// <summary>
        /// Запускает приложение в дебаг режиме
        /// </summary>
        private static void RunInDebugMode()
        {
            Application.SetCompatibleTextRenderingDefault(false);
            new ShellApplication().Run();
        }

        /// <summary>
        /// Запускает приложение
        /// </summary>
        private static void RunInReleaseMode()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(AppDomainUnhandledException);

            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                new ShellApplication().Run();
            }
            catch (Exception ex)
            {
                Logger.SimpleWrite(ex.ToString());
                MessageBox.Show("Обратитесь в службу технической поддержки.", "Ошибка");
                Application.Exit();
            }
        }

        #region Overrides WindowsFormsApplication

        protected override void AddServices()
        {
            base.AddServices();

            // Сервис серверного времени
            IServerTimeService sts = RootWorkItem.Services.AddNew<ServerTimeService, IServerTimeService>();
            ServerTimeServiceHolder.ServerTimeService = sts;

            // Паттерн DataMapper (стр. 187) М.Фаулер. "Архитектура корпоративных приложений."
            IDataMapperService _dms = RootWorkItem.Services.AddNew<DataMapperService, IDataMapperService>();
            _dms.RegisterDataMapper(new UserDataMapper());

            // Сохраняем ссылку на датамапперы
            DataMapServHolderForDomain.SaveDataMapService(_dms);

            LocalizerService.LocalizeDevExpress();

            // Сервис работы с доменами, умеющими работать с датамаппером
            RootWorkItem.Services.AddNew<DomainWithDataMapperHelperService, IDomainWithDataMapperHelperService>();

            RootWorkItem.Services.AddNew<CryptoService, ICryptoService>();

            // CAB'овский сервис аутентификации пользователей
            RootWorkItem.Services.AddNew<AuthenticationService, IAuthenticationService>();

            RootWorkItem.Services.AddNew<SettingsService, ISettingsService>();
            RootWorkItem.Services.AddNew<EmailService, IEmailService>();

            RootWorkItem.Services.AddNew<ExcelService, IExcelService>();

            // Регистрируем последним, так как зависит от IAuthenticationService
            RootWorkItem.Services.AddNew<XmlStreamDependentModuleEnumerator, IModuleEnumerator>();
        }

        #endregion

        private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        private static void HandleException(Exception ex)
        {
            if (ex == null)
                return;

            Application.Exit();
        }
    }
}