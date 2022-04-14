using System;
using System.Collections.Generic;
using System.Text;

namespace TGS.Pagamento.API.Configuration
{
    public class Settings
    {
        public Uri UrlApi { get; set; }
        public TimeSpan Timeout { get; set; }
        public string NameHeadersKeyAccess { get; set; }
        public string ValueHeadersKeyAccess { get; set; }
    }

}
