using System;
using System.Security.Cryptography.X509Certificates;

namespace ExtensaoCertificado.Util
{
    public static class InfoCertificate
    {
        public static int ModelCertificate(this X509Certificate2 x509Certificate2)
        {
            if (!string.IsNullOrEmpty(x509Certificate2.Subject))
            {
                try
                {
                    if (((System.Security.Cryptography.RSACryptoServiceProvider)(x509Certificate2.PrivateKey)).CspKeyContainerInfo.Removable)
                    {
                        if (((System.Security.Cryptography.RSACryptoServiceProvider)(x509Certificate2.PrivateKey)).CspKeyContainerInfo.HardwareDevice)
                        {
                            return 30;
                        }
                    }
                }
                catch (Exception)
                {
                    return 0;
                }
            }

            return 10;
        }
    }
}