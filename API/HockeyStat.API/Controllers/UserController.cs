using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HockeyStat.API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Http.Authentication;

namespace HockeyStat.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ApiController
    {

        private ConfigurationOptions configurationOptions;

        public UserController(IOptions<ConfigurationOptions> optionsAccessor)
        {
            this.configurationOptions = optionsAccessor.Value;
        }

        [HttpGet("login")]
        public User Login([FromQuery] string userName, [FromQuery] string password)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userName));
            claims.Add(new Claim(ClaimTypes.Name, userName));
            User user = new User() { UserName = userName, Password = password };
            if (userName == this.configurationOptions.UserName && password == this.configurationOptions.Password)
            {
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
                user.IsAdmin = true;
            }
            ClaimsIdentity identity = new ClaimsIdentity(claims);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            this.Context.Authentication.SignInAsync("HockeyStatAuthorization", principal, new AuthenticationProperties() { IsPersistent=true, ExpiresUtc=DateTime.UtcNow.AddYears(1) } );
            return user;
        }

        [HttpGet("logout")]
        public HttpResponseMessage Logout()
        {
            this.Context.Authentication.SignOutAsync("HockeyStatAuthorization");
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }
        
    }
}
