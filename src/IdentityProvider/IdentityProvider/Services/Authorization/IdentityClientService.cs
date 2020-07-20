using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityProvider.Services.Authorization.Abstracts;
using IdentityProvider.Services.Authorization.Request;
using IdentityProvider.Services.Authorization.Response;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityProvider.Services.Authorization
{
    public class IdentityClientService : IIdentityClientService
    {
        private IServiceProvider ServiceProvider { get; set; }
        private ConfigurationDbContext ConfigurationDbContext { get; set; }

        public IdentityClientService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            ConfigurationDbContext = ServiceProvider.GetRequiredService<ConfigurationDbContext>();
        }


        public Task<ClientResponse> CreateClient()
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> ClientNameExistAsync(string name)
        {
            var client = await ConfigurationDbContext.Clients.FirstOrDefaultAsync(c => c.ClientName.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            return client != null;
        }


    }
}