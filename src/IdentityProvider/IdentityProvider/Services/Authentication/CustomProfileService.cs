using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace IdentityProvider.Services.Authentication
{
    public class CustomProfileService : IProfileService
    {
        private readonly IUserRepository _userRepository;

        public CustomProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                if (!string.IsNullOrEmpty(context.Subject.Identity.Name))
                {
                    var user = await _userRepository.FindByUsername(context.Subject.Identity.Name);
                    if (user != null)
                    {
                        var claims = GetUserClaims(user);
                        context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
                    }
                }
                else
                {
                    var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub");
                    if (!string.IsNullOrEmpty(userId?.Value))
                    {
                        //get user from db (find user by user id)
                        if (userId != null)
                        {
                            var user = await _userRepository.FindBySubjectId(userId.Value);

                            // issue the claims for the user
                            if (user != null)
                            {
                                var claims = GetUserClaims(user);
                                context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //log your error
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                //get subject from context (set in ResourceOwnerPasswordValidator.ValidateAsync),
                var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "user_id");

                if (!string.IsNullOrEmpty(userId?.Value))
                {
                    if (userId != null)
                    {
                        var user = await _userRepository.FindBySubjectId(userId.Value);
                        if (user != null)
                        {
                            // if (user.IsActive)
                            // {
                            //     context.IsActive = user.IsActive;
                            // }
                            context.IsActive = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //handle error logging
            }
        }
        
        public static Claim[] GetUserClaims(IdentityUser user)
        {
            return new Claim[]
            {
                new Claim("user_id", user.Id ?? ""),
                //new Claim(JwtClaimTypes.Name, (!string.IsNullOrEmpty(user.UserName) && !string.IsNullOrEmpty(user.UserName)) ? (user.Firstname + " " + user.Lastname) : ""),
                //new Claim(JwtClaimTypes.GivenName, user.Firstname  ?? ""),
               // new Claim(JwtClaimTypes.FamilyName, user.Lastname  ?? ""),
                new Claim(JwtClaimTypes.Email, user.Email  ?? ""),
                //new Claim("some_claim_you_want_to_see", user.Some_Data_From_User ?? ""),

                //roles
                new Claim(JwtClaimTypes.Role, "User")
            };
        }
    }
}