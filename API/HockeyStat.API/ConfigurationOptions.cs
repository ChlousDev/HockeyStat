using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HockeyStat.API
{
    public class ConfigurationOptions
    {
        public const string AuthenticationScheme = "HockeyStatAuthentication";
        public const string AuthenticationCookieName = "HockeyStatAuth";

        public string AdminUserName { get; set; }
        public string AdminPassword { get; set; }
    }
}
