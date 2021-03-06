using IdentityProvider.Services.Authorization.Abstracts;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityProvider.Services.Authorization
{
    public static class CustomAuthorizationExtensions
    {
        public static IServiceCollection AddIdentityAuthorizationService(this IServiceCollection service)
        {
            service.AddTransient<IIdentityClientService, IdentityClientService>();
            return service;
        }
    }
}