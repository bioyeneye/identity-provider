using System.Threading.Tasks;
using IdentityServer4.Validation;

namespace IdentityProvider.Services.User
{
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}