using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HockeyStat.API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace HockeyStat.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private ConfigurationOptions configurationOptions;

        public UserController(IOptions<ConfigurationOptions> optionsAccessor)
        {
            this.configurationOptions = optionsAccessor.Value;
        }

        [HttpGet("login")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public User Login([FromQuery] string userName, [FromQuery] string password)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userName));
            claims.Add(new Claim(ClaimTypes.Name, userName));
            User user = new User() { UserName = userName, Password = password };
            if (userName == this.configurationOptions.AdminUserName && password == this.configurationOptions.AdminPassword)
            {
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
                user.IsAdmin = true;
            }
            ClaimsIdentity identity = new ClaimsIdentity(claims);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            this.Request.HttpContext.SignInAsync(ConfigurationOptions.AuthenticationScheme,  principal, new AuthenticationProperties() { IsPersistent=true, ExpiresUtc=DateTime.UtcNow.AddYears(1) } );
            return user;
        }

        [HttpGet("logout")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public ObjectResult Logout()
        {
            this.Request.HttpContext.SignOutAsync(ConfigurationOptions.AuthenticationScheme);
            return this.StatusCode(StatusCodes.Status200OK, "OK");
        }
        
    }
}
