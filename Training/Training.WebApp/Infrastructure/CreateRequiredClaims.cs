using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using Training.BAL.Entities;

namespace Training.WebApp.Infrastructure
{
    public static class CreateRequiredClaims
    {
        public static ClaimsIdentity Create(UserEntity user)
        {
            var claims = new List<Claim> { 
            // create required claims
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim("user", user.ToString())};

            ClaimsIdentity identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            return identity;
        }
    }
}