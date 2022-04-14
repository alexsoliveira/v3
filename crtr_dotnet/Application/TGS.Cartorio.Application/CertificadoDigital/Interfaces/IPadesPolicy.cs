using Lacuna.Pki.Pades;

namespace TGS.Cartorio.Application.CertificadoDigital.interfaces
{
    public interface IPadesPolicy
    {
        IPadesPolicyMapper GetTrustArbitrator();
    }
}
