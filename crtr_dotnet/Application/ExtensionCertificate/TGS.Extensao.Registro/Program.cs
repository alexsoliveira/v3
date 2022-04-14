using NativeMessaging;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Principal;

namespace TGS.Extensao.Registro
{
    class Program
    {
        static Host Host;

        readonly static string[] AllowedOrigins = new string[] { ConfigurationManager.AppSettings["AllowedOrigins"].ToString() };
        readonly static string Description = ConfigurationManager.AppSettings["Description"].ToString();

        static void Main(string[] args)
        {
            //System.Threading.Thread.Sleep(30000);       
            
            try
            {
                Log.Active = Convert.ToBoolean(ConfigurationManager.AppSettings["Log"].ToString());

                Log.LogMessage("Inicio programa");

                Host = new Gerenciador();
                Host.SupportedBrowsers.Add(ChromiumBrowser.GoogleChrome);
                Host.SupportedBrowsers.Add(ChromiumBrowser.MicrosoftEdge);

                if (args.Contains("--register"))
                {
                    //Host.Register();
                    Host.GenerateManifest(Description, AllowedOrigins);
                }
                else if (args.Contains("--unregister"))
                {
                    //Host.Unregister();
                    Host.RemoveManifest();
                }
            }
            catch (Exception ex)
            {
                Log.LogMessage(ex.Message);
            }
        }
    }
}

