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
    public class IdentityApiResourceService : IIdentityApiResourceService
    {
        private IServiceProvider ServiceProvider { get; set; }
        private ConfigurationDbContext ConfigurationDbContext { get; set; }

        public IdentityApiResourceService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            ConfigurationDbContext = ServiceProvider.GetRequiredService<ConfigurationDbContext>();
        }
        
        
        public async Task<CreateApiResourceResponse> GetApiResourceByName(string name)
        {
            try
            {
                var apiResource = await ConfigurationDbContext.ApiResources.FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
                return new CreateApiResourceResponse
                {
                    Name = apiResource?.Name,
                    DisplayName = apiResource?.DisplayName,
                    Description = apiResource?.Description,
                    Scopes = apiResource?.Scopes?.Select(c => c.Scope).ToList().Join(", ")
                };
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<List<CreateApiResourceResponse>> GetApiResources()
        {
            var apiResources = await ConfigurationDbContext.ApiResources.ToListAsync();
            return apiResources == null
                ? new List<CreateApiResourceResponse>()
                : apiResources.Select(apiResource => new CreateApiResourceResponse
                {
                    Name = apiResource.Name,
                    DisplayName = apiResource.DisplayName,
                    Description = apiResource.Description,
                    Scopes = apiResource.Scopes.Select(c => c.Scope).ToList().Join(", ")
                }).ToList();
        }

        public async Task<bool> CreateApiResource(ApiResource request)
        {
            try
            {
                var apiResourceEntity = await ConfigurationDbContext.ApiResources.AddAsync(request.ToEntity());
                var saved = await ConfigurationDbContext.SaveChangesAsync();
                return saved > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public ApiResource ProcessApiResourceRequest(CreateApiResourceRequest request)
        {
            var apiName = request.Name
                .ToLower()
                .Replace(' ', '-');

            List<string> scopes = string.IsNullOrWhiteSpace(request.Scopes)
                ? new List<string> {$"{apiName}.full_access", $"{apiName}.read_only"}
                : request.Scopes.Split(",").ToList();

            List<string> claims = string.IsNullOrWhiteSpace(request.Claims)
                ? new List<string>()
                : request.Claims.Split(",").ToList();

            return new ApiResource
            {
                Name = request.Name,
                Description = request.Description,
                DisplayName = request.DisplayName,
                Enabled = request.IsActive,
                Scopes = scopes,
                UserClaims = claims,
            };
        }

        public async Task<bool> ApiResourceNameExistAsync(string name)
        {
            var apiResourceExist = await ConfigurationDbContext
                .ApiResources
                .AnyAsync(c => c.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            return apiResourceExist;
        }
    }
}