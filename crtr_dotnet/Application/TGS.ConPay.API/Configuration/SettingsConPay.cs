using System;
using System.Collections.Generic;
using System.Text;

namespace TGS.Pagamento.API.Configuration
{
    public class SettingsConPay : Settings
    {
        public string accessKeyId { get; set; }
        public string secretKey { get; set; }
        public bool SSLEnable { get; set; }
    }
}
