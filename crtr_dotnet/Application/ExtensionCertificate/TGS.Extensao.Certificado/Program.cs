using NativeMessaging;
using System;
using System.Configuration;

namespace TGS.Extensao.Certificado

{
    class Program
    {
        static Host Host;

        static void Main(string[] args)
        {          
            Log.Active = Convert.ToBoolean(ConfigurationManager.AppSettings["Log"].ToString());

            Host = new MyHost();

            Host.Listen();
        }
    }
}
