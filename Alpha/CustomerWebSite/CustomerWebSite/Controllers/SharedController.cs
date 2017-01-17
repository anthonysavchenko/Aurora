using System.Collections.Generic;
using System.Linq;
using CustomerWebSite.Models;
using System.Web.Mvc;
using CustomerWebSite.Constants;
using Microsoft.AspNet.Identity;
using Taumis.Alpha.Server.Core.Models.Enums;
using Taumis.Alpha.Server.Infrastructure.Data;

namespace CustomerWebSite.Controllers
{
    public class SharedController : Controller
    {
        private readonly IAlphaDbContext _db;

        public SharedController(IAlphaDbContext db)
        {
            _db = db;
        }

        [ChildActionOnly]
        // GET: Shared
        public ActionResult LoginMenu()
        {
            LoginMenuViewModel _model =
                new LoginMenuViewModel
                {
                    IsAuthenticated = Request.IsAuthenticated,
                    IsPaymentsAndChargesPageSelected = Request.Url.AbsolutePath.Contains("PaymentsAndCharges"),
                    IsSettigsPageSelected = Request.Url.AbsolutePath.Contains("Manage")
                };

            if (_model.IsAuthenticated)
            {
                _model.Login = User.Identity.Name;

                

                _model.Accounts = (List<LoginMenuViewModel.AccountInfo>)Session[SessionState.ACCOUNTS];

                if (_model.Accounts == null)
                {
                    int _userId = User.Identity.GetUserId<int>();

                    _model.Accounts =
                        _db.Customers
                            .Where(c => c.UserID == _userId)
                            .Select(c =>
                                new LoginMenuViewModel.AccountInfo
                                {
                                    Account = c.Account,
                                    Apartment = c.Apartment,
                                    Building = c.Building.Number,
                                    CustomerName =
                                        c.OwnerType == OwnerTypes.PhysicalPerson
                                            ? c.PhysicalPersonShortName
                                            : c.JuridicalPersonFullName,
                                    Square = c.Square,
                                    Street = c.Building.Street.Name
                                })
                            .ToList();

                    Session[SessionState.ACCOUNTS] = _model.Accounts;
                }

                _model.Account = (string)Session[SessionState.SELECTED_ACCOUNT];

                if (string.IsNullOrEmpty(_model.Account))
                {
                    _model.Account = _model.Accounts[0].Account;
                    Session[SessionState.SELECTED_ACCOUNT] = _model.Account;
                }
            }

            return PartialView("_LoginPartial", _model);
        }
    }
}