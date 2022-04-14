using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGS.Identity.API.Extensions
{
    public class SiteSettings
    {
        public string Sistema { get; set; }        
        public string Host { get; set; }
        public string ActionConfirmarEmail { get; set; }
        public string ActionResetarSenha { get; set; }
    }
}
