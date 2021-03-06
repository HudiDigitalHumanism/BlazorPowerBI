using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPowerBI.Shared.ActiveDirectory
{
    public class AuthApplicationOptions
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string TenantId { get; set; }
        public string[] Scopes { get; set; }
    }
}
