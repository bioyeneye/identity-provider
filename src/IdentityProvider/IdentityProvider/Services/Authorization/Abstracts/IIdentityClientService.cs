using System.Threading.Tasks;

namespace IdentityProvider.Services.Authorization.Abstracts
{
    public interface IIdentityClientService
    {
        public Task<ClientResponse> CreateClient();
        public Task<bool> ClientNameExistAsync(string name);
    }
}