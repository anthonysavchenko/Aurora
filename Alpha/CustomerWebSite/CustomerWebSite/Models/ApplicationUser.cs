using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace CustomerWebSite.Models
{
    public class ApplicationUser : IUser<int>
    {
        public ApplicationUser(User user)
        {
            User = user;
        }

        public User User { get; private set; }

        public int Id
        {
            get
            {
                return User.ID;
            }
        }

        public string UserName
        {
            get
            {
                return User.Login;
            }
            set { }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}