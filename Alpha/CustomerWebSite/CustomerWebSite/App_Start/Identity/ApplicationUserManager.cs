using CustomerWebSite.Models;
using CustomerWebSite.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;

namespace CustomerWebSite.App_Start.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser, int>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, int> store, IEmailService emailService)
            : base(store)
        {
            UserValidator =
                new UserValidator<ApplicationUser, int>(this)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };

            // Configure validation logic for passwords
            PasswordValidator =
                new PasswordValidator
                {
                    RequiredLength = 6,
                    RequireNonLetterOrDigit = true,
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireUppercase = true,
                };

            // Configure user lockout defaults
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            var _dataProtectionProvider = Startup.DataProtectionProvider;
            if (_dataProtectionProvider != null)
            {
                UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser, int>(_dataProtectionProvider.Create("ASP.NET Identity"));
            }

            EmailService = emailService;
        }
    }
}