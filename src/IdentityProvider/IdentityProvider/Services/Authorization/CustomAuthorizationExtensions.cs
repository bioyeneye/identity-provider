using Microsoft.Extensions.DependencyInjection;

namespace IdentityProvider.Services.Authorization
{
    public static class CustomAuthorizationExtensions
    {
        public static IServiceCollection AddCustomUserStore(this IServiceCollection service)
        {
            return service;
        }
    }
}