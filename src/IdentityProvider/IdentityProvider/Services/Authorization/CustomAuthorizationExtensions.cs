using Microsoft.Extensions.DependencyInjection;

namespace IdentityProvider.Services.Authorization
{
    public static class CustomAuthorizationExtensions
    {
        public static IServiceCollection AddIdentityAuthorizationService(this IServiceCollection service)
        {
            service.AddTransient<IAuthorizationService, AuthorizationService>();
            return service;
        }
    }
}