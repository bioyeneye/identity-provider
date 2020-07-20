using System.Threading.Tasks;
using IdentityServer4.Events;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace IdentityProvider.Services.Authentication
{
    public interface IUserRepository
    {
        Task<bool> ValidateCredentials(string username, string password);

        Task<IdentityUser> FindBySubjectId(string subjectId);

        Task<IdentityUser> FindByUsername(string username);

        Task<bool> ValidatePassword(IdentityUser user, string password);
    }
    
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEventService _events;
        public UserRepository(UserManager<IdentityUser> userManager, IEventService events)
        {
            _userManager = userManager;
            _events = events;
        }

        public async Task<bool> ValidateCredentials(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName));
                return true;
            }

            return false;
        }

        public async Task<IdentityUser> FindBySubjectId(string subjectId)
        {
            return await _userManager.FindByIdAsync(subjectId);
        }

        public async Task<IdentityUser> FindByUsername(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<bool> ValidatePassword(IdentityUser user, string password)
        {
            return user != null && await _userManager.CheckPasswordAsync(user, password);
        }
    }
}