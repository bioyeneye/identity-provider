using Microsoft.AspNetCore.Identity;

namespace IdentityProvider.Services.User
{
    public interface IUserRepository
    {
        bool ValidateCredentials(string username, string password);

        IdentityUser FindBySubjectId(string subjectId);

        IdentityUser FindByUsername(string username);
    }
    
    public class UserRepository : IUserRepository
    {
        
        public bool ValidateCredentials(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public IdentityUser FindBySubjectId(string subjectId)
        {
            throw new System.NotImplementedException();
        }

        public IdentityUser FindByUsername(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}