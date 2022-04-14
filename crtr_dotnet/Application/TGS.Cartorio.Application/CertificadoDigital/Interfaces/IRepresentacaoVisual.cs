using Lacuna.Pki;
using Lacuna.Pki.Pades;
using System.Security.Cryptography.X509Certificates;

namespace TGS.Cartorio.Application.CertificadoDigital.interfaces
{
    public interface IRepresentacaoVisual
    {
        PadesVisualRepresentation2 Set(PKCertificate pkCertificate = null, X509Certificate x509 = null, long idValidadorLong = 0, string validadorGuid = null);
    }
}
