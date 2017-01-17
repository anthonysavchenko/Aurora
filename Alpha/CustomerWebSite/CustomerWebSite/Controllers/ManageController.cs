using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using CustomerWebSite.App_Start.Identity;
using CustomerWebSite.Models.ManageViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using CustomerWebSite.Models;
using Taumis.Alpha.Server.Core.Models.Docs;
using Taumis.Alpha.Server.Infrastructure.Data;

namespace CustomerWebSite.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IAlphaDbContext _db;

        public ManageController(
            ApplicationUserManager userManager, 
            IAuthenticationManager authenticationManager,
            IAlphaDbContext db)
        {
            _userManager = userManager;
            _authenticationManager = authenticationManager;
            _db = db;
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            switch (message)
            {
                case ManageMessageId.ChangePasswordSuccess:
                    ViewBag.StatusMessage = "Ваш пароль был изменен.";
                    break;

                case ManageMessageId.BillSendingEnabled:
                    ViewBag.StatusMessage = "Рассылка ежемесячных квитанций включена.";
                    break;

                case ManageMessageId.BillSendingDisabled:
                    ViewBag.StatusMessage = "Рассылка ежемесячных квитанций выключена.";
                    break;

                case ManageMessageId.Error:
                    ViewBag.StatusMessage = "Произошла ошибка.";
                    break;

                default:
                    ViewBag.StatusMessage = "";
                    break;
            }

            var _user = _userManager.FindById(User.Identity.GetUserId<int>());

            List<IndexViewModel.AccountSetting> _accountSettings =
                _db.Customers
                    .Where(c => c.UserID == _user.Id)
                    .Select(c =>
                        new IndexViewModel.AccountSetting
                        {
                            Account = c.Account,
                            SendBill = c.BillSendingSubscription
                        })
                    .ToList();

            IndexViewModel _model =
                new IndexViewModel
                {
                    HasPassword = string.IsNullOrEmpty(_user.User.Password),
                    Accounts = _accountSettings,
                    BrowserRemembered = await _authenticationManager.TwoFactorBrowserRememberedAsync(User.Identity.GetUserId())
                };

            return View(_model);
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _userManager.ChangePasswordAsync(User.Identity.GetUserId<int>(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/EnableSendingBills
        [Route("Manage/EnableSendingBills/{account}")]
        public async Task<ActionResult> EnableSendingBills(string account)
        {
            await ChangeBillSendingSubscription(account, true);

            return RedirectToAction("Index", new { Message = ManageMessageId.BillSendingEnabled });
        }

        //
        // GET: /Manage/DisableSendingBills
        [Route("Manage/DisableSendingBills/{account}")]
        public async Task<ActionResult> DisableSendingBills(string account)
        {
            await ChangeBillSendingSubscription(account, false);

            return RedirectToAction("Index", new { Message = ManageMessageId.BillSendingDisabled });
        }

        #region Helpers

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(_userManager));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            BillSendingEnabled,
            BillSendingDisabled,
            Error
        }

        private async Task ChangeBillSendingSubscription(string account, bool enabled)
        {
            Customer _customer = await _db.Customers.FirstAsync(c => c.Account == account);
            _customer.BillSendingSubscription = enabled;
            await _db.SaveChangesAsync();
        }

        #endregion
    }
}