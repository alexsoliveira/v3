using NativeMessaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;

namespace TGS.Extensao.Certificado
{
    class MyHost : Host
    {       
        private const bool SendConfirmationReceipt = true;

        Certificados cert = new Certificados();

        public override string Hostname
        {
            get { return ConfigurationManager.AppSettings["Hostname"].ToString(); }
        }

        public MyHost() : base(SendConfirmationReceipt)
        {

        }

        protected override void ProcessReceivedMessage(JObject data)
        {
            switch (((JValue)data["seq"]).Value)
            {
                case "isHostInstalado":
                    SendMessage(JsonConvert.SerializeObject(true));
                    break;

                case "lerCertificados":
                    SendMessage(JsonConvert.SerializeObject(cert.BuscarCertificadosUsuario()));
                    break;
            }            
        }
    }
}
