using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HockeyStat.API
{
    public class ConfigurationOptions
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string DBUserName { get; set; }
        public string DBPassword { get; set; }
    }
}
