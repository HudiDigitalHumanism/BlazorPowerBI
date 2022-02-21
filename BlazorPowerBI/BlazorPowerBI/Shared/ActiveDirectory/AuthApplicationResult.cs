using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPowerBI.Shared.ActiveDirectory
{
    public class AuthApplicationResult
    {
        public string AccessToken { get; set; }
        public DateTimeOffset ExpiresOn { get; set; }
        public string IdToken { get; set; }
    }
}
