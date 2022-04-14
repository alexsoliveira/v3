using System;
using System.Collections.Generic;
using System.Text;

namespace TGS.Cartorio.Infrastructure.Utility.Settings
{
    public class Settings
    {
        public Uri UrlApi { get; set; }
        public TimeSpan Timeout { get; set; }
        public string NameHeadersKeyAccess { get; set; }
        public string ValueHeadersKeyAccess { get; set; }
    }

}
