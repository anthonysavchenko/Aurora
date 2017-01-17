using System.IO;
using System.Web.Mvc;
using CustomerWebSite.Constants;
using CustomerWebSite.Models.PaymentsAndChargesModels;
using CustomerWebSite.PrintForms.MutualSettlement;
using CustomerWebSite.PrintForms.RegularBill;
using CustomerWebSite.Services.Home;
using Microsoft.AspNet.Identity;
using Taumis.Alpha.Server.Core.Services.MutualSettlement;
using Taumis.Alpha.Server.Core.Services.RegularBill;

namespace CustomerWebSite.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region Services

        /// <summary>
        /// Сервис получения данных справки о взаиморасчетах
        /// </summary>
        private readonly IMutualSettlementService _mutualSettlementService;

        /// <summary>
        /// Сервис получения данных о ежемесячных квитанциях
        /// </summary>
        private readonly IRegularBillService _regularBillService;

        /// <summary>
        /// Сервис получения данных о платежах и начислениях
        /// </summary>
        private readonly IPaymentsAndChargesService _paymentsAndChargesService;

        /// <summary>
        /// Сервис авторизации действий пользователя
        /// </summary>
        private readonly IAuthorizationService _authService;

        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="mutualSettlementService">Сервис получения данных справки о взаиморасчетах</param>
        /// <param name="regularBillService">Сервис получения данных о ежемесячных квитанциях</param>
        /// <param name="paymentsAndChargesService">Cервиса предоставления данных</param>
        /// <param name="authService">Сервис авторизации действий пользователя</param>
        public HomeController(
            IMutualSettlementService mutualSettlementService,
            IRegularBillService regularBillService,
            IPaymentsAndChargesService paymentsAndChargesService,
            IAuthorizationService authService)
        {
            _mutualSettlementService = mutualSettlementService;
            _regularBillService = regularBillService;
            _paymentsAndChargesService = paymentsAndChargesService;
            _authService = authService;
        }

        /// <summary>
        /// Главная страница, действие перенаправляется на страницу со списком платежей и начислений
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            string _account = _paymentsAndChargesService.GetFirstAccount(User.Identity.GetUserId<int>());
            return RedirectToAction("PaymentsAndCharges", new { account = _account });
        }

        /// <summary>
        /// Возвращает список платежей и начислений за определенный год
        /// </summary>
        /// <param name="account">Номер лицевого счета</param>
        /// <param name="year">Год</param>
        /// <returns>Результата в виде страницы со списоком платежей и начислений</returns>
        [Route("{account}/PaymentsAndCharges/{year:int?}")]
        public ActionResult PaymentsAndCharges(string account, int? year)
        {
            int _userId = User.Identity.GetUserId<int>();
            int _customerId;

            if (!_authService.AuthAccount(account, _userId, out _customerId))
            {
                return HttpNotFound();
            }

            Session[SessionState.SELECTED_ACCOUNT] = account;

            PaymentsAndChargesViewModel _viewModel =
                _paymentsAndChargesService.GetPaymentsAndChargesViewModel(_userId, _customerId, account, year);

            return View(_viewModel);
        }

        /// <summary>
        /// Возвращает справка о взаиморасчетах в формате pdf
        /// </summary>
        /// <param name="account">Номер лицевого счета</param>
        /// <returns>Результат в виде pdf</returns>
        [Route("{account}/MutualSettlement")]
        public ActionResult MutualSettlement(string account)
        {
            int _customerId;
            if (!_authService.AuthAccount(account, User.Identity.GetUserId<int>(), out _customerId))
            {
                return HttpNotFound();
            }

            MutualSettlementReportObject _report =
                new MutualSettlementReportObject
                {
                    ReportDataSource = _mutualSettlementService.GetDataForReport(_customerId)
                };

            MemoryStream _stream = new MemoryStream();
            _report.ExportToPdf(_stream);

            return File(_stream.GetBuffer(), "application/pdf");
        }

        /// <summary>
        /// Возвращает квитанцию в виде pdf по ID
        /// </summary>
        /// <param name="account">Номер лицевого счета</param>
        /// <param name="year">Год</param>
        /// <param name="month">Месяц</param>
        /// <returns>Результат в виде pdf</returns>
        [Route("{account}/RegularBill/{year}/{month}")]
        public ActionResult RegularBill(string account, int year, int month)
        {
            int _id;
            if (!_authService.AuthRegularBill(account, year, month, User.Identity.GetUserId<int>(), out _id))
            {
                return HttpNotFound();
            }

            RegularBillReportObject _report =
                new RegularBillReportObject
                {
                    ShowLineBetweenBills = false,
                    ReceiptType = ReceiptTypes.Standart,
                    ReportDataSource = _regularBillService.GetDataForReport(_id)
                };

            MemoryStream _stream = new MemoryStream();
            _report.ExportToPdf(_stream);

            return File(_stream.GetBuffer(), "application/pdf");
        }
    }
}