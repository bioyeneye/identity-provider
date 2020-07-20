using System;
using System.Threading.Tasks;
using IdentityProvider.Services.Authorization.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProvider.Controllers.Account
{
    [Route("api/resources")]
    public class ApiResourceController : Controller
    {
        private IIdentityApiResourceService _identityApiResourceService;

        public ApiResourceController(IIdentityApiResourceService authorizationService)
        {
            _identityApiResourceService = authorizationService;
        }
        
        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            try
            {
                var apiResource = await _identityApiResourceService.GetApiResourceByName(name);
                return Ok(apiResource);
            }
            catch (Exception e)
            {
                return BadRequest("Error...");
            }
        }
    }
}