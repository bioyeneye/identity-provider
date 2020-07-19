namespace IdentityProvider.Services.Authorization
{
    public class ClientResponse
    {
        public ClientResponse(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        public string ClientId { get; }
        public string ClientSecret { get; }
    }
}