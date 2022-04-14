using NativeMessaging;
using Newtonsoft.Json.Linq;
using System.Configuration;

namespace TGS.Extensao.Registro
{
    class Gerenciador : Host
    {       
        private const bool SendConfirmationReceipt = true;
        public override string Hostname
        {
            get { return ConfigurationManager.AppSettings["Hostname"].ToString(); }
        }

        public Gerenciador() : base(SendConfirmationReceipt)
        {

        }

        protected override void ProcessReceivedMessage(JObject data)
        {
            throw new System.NotImplementedException();
        }
    }
}
