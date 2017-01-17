using System;
using System.Linq;
using System.Threading.Tasks;
using CustomerWebSite.Models;
using Microsoft.AspNet.Identity;
using Taumis.Alpha.Server.Core.Models.RefBooks;
using Taumis.Alpha.Server.Infrastructure.Data;

namespace CustomerWebSite.Identity
{
    public class UserStore : 
        IUserPasswordStore<ApplicationUser, int>, 
        IUserLockoutStore<ApplicationUser, int>,
        IUserTwoFactorStore<ApplicationUser, int>,
        IUserEmailStore<ApplicationUser, int>
    {
        private readonly IAlphaDbContext _db;

        public UserStore(IAlphaDbContext db)
        {
            _db = db;
        }

        public void Dispose()
        {
        }

        #region IUserStore

        public Task CreateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            await _db.SaveChangesAsync();
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByIdAsync(int userId)
        {
            User _user = _db.Users.Find(userId);
            ApplicationUser _appUser = _user != null ? new ApplicationUser(_user) : null;
            return Task.FromResult(_appUser);
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            User _user = _db.Users.FirstOrDefault(c => c.Login == userName);
            ApplicationUser _appUser = _user != null ? new ApplicationUser(_user) : null;
            return Task.FromResult(_appUser);
        }

        #endregion

        #region IUserPasswordStore

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.User.Password = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.User.Password);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.User.Password));
        }

        #endregion

        #region IUserLockoutStore

        public Task<DateTimeOffset> GetLockoutEndDateAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return
                Task.FromResult(user.User.LockoutEndDateUtc.HasValue
                    ? new DateTimeOffset(DateTime.SpecifyKind(user.User.LockoutEndDateUtc.Value, DateTimeKind.Utc))
                    : new DateTimeOffset());
        }

        public Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset lockoutEnd)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.User.LockoutEndDateUtc = lockoutEnd == DateTimeOffset.MinValue ? (DateTime?)null : lockoutEnd.UtcDateTime;
            return Task.FromResult(0);
        }

        public Task<int> IncrementAccessFailedCountAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.User.AccessFailedCount++;
            return Task.FromResult(user.User.AccessFailedCount);
        }

        public Task ResetAccessFailedCountAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.User.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        public Task<int> GetAccessFailedCountAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.User.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.User.LockoutEnabled);
        }

        public Task SetLockoutEnabledAsync(ApplicationUser user, bool enabled)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.User.LockoutEnabled = enabled;
            return Task.FromResult(0);
        }

        #endregion

        #region IUserTwoFactorStore

        public Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled)
        {
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user)
        {
            return Task.FromResult(false);
        }

        #endregion

        #region IUserEmailStore

        public Task SetEmailAsync(ApplicationUser user, string email)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.User.Login);
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByEmailAsync(string email)
        {
            User _user = _db.Users.FirstOrDefault(c => c.Login.ToUpper() == email.ToUpper());
            ApplicationUser _appUser = _user != null ? new ApplicationUser(_user) : null;
            return Task.FromResult(_appUser);
        }

        #endregion
    }
}