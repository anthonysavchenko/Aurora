using System.Security.Claims;
using System.Threading.Tasks;
using CustomerWebSite.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace CustomerWebSite.App_Start.Identity
{
    public class ApplicationSignInManager : SignInManager<ApplicationUser, int>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync(UserManager);
        }
    }
}