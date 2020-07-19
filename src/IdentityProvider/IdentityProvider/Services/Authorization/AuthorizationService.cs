using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityProvider.Services.Authorization.Request;
using IdentityProvider.Services.Authorization.Response;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityProvider.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private IServiceProvider ServiceProvider { get; set; }

        public AuthorizationService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }


        public Task<ClientResponse> CreateClient()
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> ClientNameExistAsync(string name)
        {
            var context = ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            var client = await context.Clients.FirstOrDefaultAsync(c=> c.ClientName.Equals(name, StringComparison.CurrentCultureIgnoreCase));

            return client != null;
        }

        public Task<CreateResourceResponse> GetResourceByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<CreateResourceResponse>> GetResources()
        {
            throw new NotImplementedException();
        }

        public Task<CreateResourceResponse> CreateResource(CreateResourceRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ResourceNameExistAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}