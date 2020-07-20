using System;
using System.Threading.Tasks;
using IdentityProvider.Services.Authorization;
using IdentityProvider.Services.Authorization.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProvider.Controllers.Account
{
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private IIdentityClientService _authorizationService;

        public AuthorizationController(IIdentityClientService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        
    }
}