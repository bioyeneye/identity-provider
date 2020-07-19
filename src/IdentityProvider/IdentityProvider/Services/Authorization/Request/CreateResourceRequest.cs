using System.ComponentModel.DataAnnotations;

namespace IdentityProvider.Services.Authorization.Request
{
    public class CreateResourceRequest
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string DisplayName { get; set; }
        public string Description { get; set; }
        
        public string Scopes { get; set; }
        public string Claims { get; set; }
    }
}