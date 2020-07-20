using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityProvider.Services.Authorization.Request;
using IdentityProvider.Services.Authorization.Response;
using IdentityServer4.Models;

namespace IdentityProvider.Services.Authorization.Abstracts
{
    public interface IIdentityApiResourceService
    {
        public Task<CreateApiResourceResponse> GetApiResourceByName(string name);
        public Task<List<CreateApiResourceResponse>> GetApiResources();
        public Task<bool> CreateApiResource(ApiResource request);
        public ApiResource ProcessApiResourceRequest(CreateApiResourceRequest request);
        public Task<bool> ApiResourceNameExistAsync(string name);
    }
}