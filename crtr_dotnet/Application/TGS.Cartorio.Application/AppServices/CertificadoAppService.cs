using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Infrastructure.Utility.ApiClient;

namespace TGS.Cartorio.Application.AppServices
{
    public class CertificadoAppService : ICertificadoAppService
    {
        private readonly ApiAssinaturaDigital _apiAssinaturaDigital;

        public CertificadoAppService(ApiAssinaturaDigital apiAssinaturaDigital)
        {
            _apiAssinaturaDigital = apiAssinaturaDigital;
        }

        //public async Task<List<eCertificadoDTO>> Buscar()
        //{
        //    var certificados = await _apiAssinaturaDigital.Get<List<eCertificadoDTO>>("api/Certificado/BuscarCertificadosUsuario");
        //    return certificados;
        //}
    }
}
